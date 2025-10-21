using System.Data;
using Dapper;
using Super.Core;
using Super.Core.Repos;

namespace Super.Dapper;
public class RepoTicket : Repo, IRepoTicket
{
    private static readonly string _queryTicket
        = @"SELECT  idTicket, fechaHora, C.dni, nombre, apellido
            FROM    Ticket
            JOIN    Cajero C USING (dni)
            WHERE   idTicket = @id";

    public RepoTicket(IDbConnection conexion)
        : base(conexion) { }

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

                //Asigno el IdTicket a cada item del ticket
                ticket.Items.ForEach(i => i.IdTicket = ticket.Id);
            }
            catch (Exception e)
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
}
