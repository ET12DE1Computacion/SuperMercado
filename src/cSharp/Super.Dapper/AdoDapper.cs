using System.Data;
using Dapper;
using MySqlConnector;
using Super.Core;
using Super.Core.Product;

namespace Super.Dapper;
public class AdoDapper : IAdo
{
    private readonly IDbConnection _conexion;
    private readonly string _queryCategorias
        = "SELECT idRubro AS idCategoria, rubro AS nombre FROM Rubro";
    private readonly string _queryProductos
        = @"SELECT  idProducto, nombre, precioUnitario, cantidad, Producto.idRubro AS idCategoria, rubro AS nombre
            FROM    Producto
            JOIN    Rubro USING (idRubro)";
    private readonly string _queryProducto
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

    public AdoDapper(IDbConnection conexion) => this._conexion = conexion;

    //Este constructor usa por defecto la cadena para un conector MySQL
    public AdoDapper(string cadena) => _conexion = new MySqlConnection(cadena);

    public void AltaCategoria(Categoria categoria)
    {
        //Preparo los parametros del Stored Procedure
        var parametros = new DynamicParameters();
        parametros.Add("@unIdRubro", direction: ParameterDirection.Output);
        parametros.Add("@unRubro", categoria.Nombre);

        _conexion.Execute("altaRubro", parametros);

        //Obtengo el valor de parametro de tipo salida
        categoria.IdCategoria = parametros.Get<byte>("@unIdRubro");
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

    public List<Categoria> ObtenerCategorias()
        => _conexion.Query<Categoria>(_queryCategorias).ToList();

    public Producto? ObtenerProducto(short idProducto)
    {
        using (var multi = _conexion.QueryMultiple(_queryProducto, new {id = idProducto}))
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

    public List<Producto> ObtenerProductos()
    {
        //Este codigo, nos va a devolver una lista de productos agregados por Categorias, el unico "problema" es que si bien existe una sola Categoria "Gaseosa", Dapper va a realizar multiples instancias "iguales" de gaseosa.

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
}
