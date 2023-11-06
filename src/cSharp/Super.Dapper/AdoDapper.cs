using System.Data;
using Dapper;
using MySqlConnector;
using Super.Core;
using Super.Core.Product;

namespace Super.Dapper;
public class AdoDapper : IAdo
{
    private readonly IDbConnection _conexion;

    public AdoDapper(IDbConnection conexion) => this._conexion = conexion;

    //Este constructor usa por defecto la cadena para un conector MySQL
    public AdoDapper(string cadena) => _conexion = new MySqlConnection(cadena);

    #region Cajero

    private static readonly string _queryCajeroPass
        = @"SELECT  *
            FROM    Cajero
            WHERE   dni = @unDni
            AND     pass = SHA2(@unaPass, 256)
            LIMIT   1";
    private static readonly string _queryAltaCajero
        = @"INSERT INTO Cajero VALUES (@dni, @nombre, @apellido, @pass)";
    public void AltaCajero(Cajero cajero, string pass)
        => _conexion.Execute(
                _queryAltaCajero,
                new
                {
                    dni = cajero.Dni,
                    nombre = cajero.Nombre,
                    apellido = cajero.Apellido,
                    pass = pass
                }
            );
    public Cajero? CajeroPorPass(uint dni, string pass)
    //En caso de que exista un cajero, lo devuelve instanciado, caso contrario devuelve NULL.
        => _conexion.QueryFirstOrDefault<Cajero>(_queryCajeroPass, new { unDni = dni, unaPass = pass });

    #endregion
    #region Categoria

    private static readonly string _queryCategorias
        = "SELECT idRubro AS idCategoria, rubro AS nombre FROM Rubro";
    public List<Categoria> ObtenerCategorias()
        => _conexion.Query<Categoria>(_queryCategorias).ToList();
    public void AltaCategoria(Categoria categoria)
    {
        //Preparo los parametros del Stored Procedure
        var parametros = new DynamicParameters();
        parametros.Add("@unIdRubro", direction: ParameterDirection.Output);
        parametros.Add("@unRubro", categoria.Nombre);

        try
        {
            _conexion.Execute("altaRubro", parametros);

            //Obtengo el valor de parametro de tipo salida
            categoria.IdCategoria = parametros.Get<byte>("@unIdRubro");
        }
        catch (MySqlException e)
        {
            if (e.ErrorCode == MySqlErrorCode.DuplicateKeyEntry)
            {
                throw new ConstraintException(categoria.Nombre + " ya se encuentra en uso.");
            }
            throw;
        }
    }

    #endregion
    #region Producto

    private static readonly string _queryProductos
        = @"SELECT  idProducto, nombre, precioUnitario, cantidad, Producto.idRubro AS idCategoria, rubro AS nombre
            FROM    Producto
            JOIN    Rubro USING (idRubro)";
    private static readonly string _queryProducto
        //TODO fijarse que mapee bien todos los atributos
        = @"SELECT  *
            FROM    Producto
            WHERE   idProducto = @id;

            SELECT  Producto.idRubro AS idCategoria, rubro AS nombre
            FROM    Producto
            JOIN    Rubro USING (idRubro)
            WHERE   idProducto = @id;

            SELECT  *
            FROM    HistorialPrecio
            WHERE   idProducto = @id;
            
            SELECT  *
            FROM    IngresoStock
            WHERE   idProducto = @id;";
    public List<Producto> ObtenerProductos()
    {
        /*Este codigo, nos va a devolver una lista de productos agregados por Categorias,
        el unico "problema" es que si bien existe una sola Categoria "Gaseosa", Dapper
        va a realizar multiples instancias "iguales" de gaseosa.*/

        var productos = _conexion.Query<Producto, Categoria, Producto>
            (_queryProductos,
            (producto, categoria) =>
            {
                producto.Categoria = categoria;
                return producto;
            },
            splitOn: "idCategoria")
            .ToList();

        //Vamos a quedarnos con las distintas categorias
        var categorias = productos.
                            Select(p => p.Categoria).
                            Distinct().
                            ToList();

        //Y ahora vamos a recorrer los productos para dejar en todos las mismas instancias hacia sus categorias
        productos.ForEach
            (p => p.Categoria = categorias.First(c => c.IdCategoria == p.Categoria.IdCategoria));

        return productos;


    }
    public void AltaProducto(Producto producto)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@unIdProducto", direction: ParameterDirection.Output);
        parametros.Add("@unIdRubro", producto.Categoria.IdCategoria);
        parametros.Add("@unNombre", producto.Nombre);
        parametros.Add("@unPrecioUnitario", producto.PrecioUnitario);
        parametros.Add("@unaCantidad", producto.Cantidad);

        _conexion.Execute("altaProducto", parametros);

        //Obtengo el valor de parametro de tipo salida
        producto.IdProducto = parametros.Get<short>("@unIdProducto");
    }
    public Producto? ObtenerProducto(short idProducto)
    {
        using (var multi = _conexion.QueryMultiple(_queryProducto, new { id = idProducto }))
        {
            var producto = multi.ReadSingleOrDefault<Producto>();
            if (producto is not null)
            {
                producto.Categoria = multi.ReadSingle<Categoria>();
                producto.Precios = multi.Read<HistorialPrecio>().ToList();
                producto.Ingresos = multi.Read<IngresoStock>().ToList();
            }
            return producto;
        }
    }

    #endregion
    #region Ticket
    private static readonly string _queryTicket
        = @"SELECT  idTicket, fechaHora, C.dni, nombre, apellido
            FROM    Ticket
            JOIN    Cajero C USING (dni)
            WHERE   idTicket = @id";
    public void AltaTicket(Ticket ticket)
    {
        //Parametros para el ticket
        var parametros = new DynamicParameters();
        parametros.Add("@unIdTicket", direction: ParameterDirection.Output);
        parametros.Add("@unDni", ticket.Cajero.Dni);
        parametros.Add("@unaFechaHora", ticket.FechaHora);

        //Abro la conexion
        _conexion.Open();
        using (var transaccion = _conexion.BeginTransaction())
        {
            try
            {
                _conexion.Execute("altaTicket", parametros, commandType: CommandType.StoredProcedure, transaction: transaccion);
                ticket.Id = parametros.Get<int>("@unIdTicket");

                //creo una lista con los valores que le vamos a pasar al SP
                var paraItems = ticket.Items.
                    Select(i => new { unIdProducto = i.Producto.IdProducto, unIdTicket = ticket.Id, unaCantidad = i.Cantidad }).
                    ToList();

                _conexion.Execute("ingresoItem", paraItems, commandType: CommandType.StoredProcedure, transaction: transaccion);

                //Como todo se ejecuto ok, confirmo los cambios
                transaccion.Commit();

                ticket.Items.ForEach(i => i.IdTicket = ticket.Id);
            }
            catch (MySqlException e)
            {
                //Si hubo algun problema, doy marcha atras con los posibles cambios
                transaccion.Rollback();
                throw new InvalidOperationException(e.Message, e);
            }
        }
    }
    public Ticket? ObtenerTicket(int idTicket)
    {
        var ticket = _conexion.Query<Ticket, Cajero, Ticket>
            (_queryTicket,
            (ticket, cajero) =>
                {
                    ticket.Cajero = cajero;
                    return ticket;
                },
            new { id = idTicket },
            splitOn: "dni").
            FirstOrDefault();

        if (ticket is null)
            return null;
        ticket.Id = idTicket;
        ticket.Items = _conexion.Query<Item, Producto, Categoria, Item>
            ("DetalleTicket",
            (item, producto, categoria) =>
                {
                    producto.Categoria = categoria;
                    item.Producto = producto;
                    item.IdTicket = idTicket;
                    return item;
                },
            new { unIdTicket = idTicket },
            splitOn: "idProducto, idCategoria",
            commandType: CommandType.StoredProcedure).
            ToList();
        
        return ticket;
    }
    #endregion
}
