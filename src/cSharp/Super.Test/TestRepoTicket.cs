using Super.Core;
using Super.Core.Repos;
using Super.Dapper;

namespace Super.Test;
public class TestRepoTicket : TestRepo
{
    private static string _cadena =
        @"Server=localhost;Database=5to_Supermercado;Uid=cajero;pwd=passCajero;Allow User Variables=True";
    readonly IRepoCajero repoCajero;
    readonly IRepoProducto repoProducto;

    //Este test, va a usar la cadena de conexión que definí en la linea superior.
    readonly IRepoTicket repoTicket;
    public TestRepoTicket() : base(_cadena)
    {
        //var conexionTicket = new MySqlConnection(_cadena);
        repoTicket = new RepoTicket(_conexion);
        repoCajero = new RepoCajero(_conexion);
        repoProducto = new RepoProducto(_conexion);
    }

    [Fact]
    public void AltaTicketOK()
    {
        var pepe = repoCajero.CajeroPorPass(100, "zapatos");
        Assert.NotNull(pepe);

        var produtos = repoProducto.ObtenerProductos();
        Assert.NotNull(produtos);
        Assert.NotEmpty(produtos);

        var ticket = new Ticket()
        {
            Cajero = pepe
        };

        //Cargo el ticket con items
        produtos.ForEach(p => ticket.AgregarItem(p, 1));

        repoTicket.AltaTicket(ticket);
        Assert.NotEqual(0, ticket.Id);
    }

    [Fact]
    public void AltaTicketRompe()
    {
        var pepe = repoCajero.CajeroPorPass(100, "zapatos");
        Assert.NotNull(pepe);

        var produtos = repoProducto.ObtenerProductos();
        Assert.NotNull(produtos);
        Assert.NotEmpty(produtos);

        var ticket = new Ticket()
        {
            Cajero = pepe
        };

        //Cargo el ticket con items cuyo stock no alcanza
        produtos.ForEach(p => ticket.AgregarItem(p, 200));

        /*Aseguro que la operacion Ado.AltaTicket(ticket), va a devolver una
        excepción del tipo InvalidOperationException*/
        var excep = Assert.Throws<InvalidOperationException>(() => repoTicket.AltaTicket(ticket));
        Assert.StartsWith("No alcanza stock", excep.Message);
    }
    [Fact]
    public void DetalleTicketOK()
    {
        var ticket = repoTicket.ObtenerTicket(1);

        Assert.NotNull(ticket);
        Assert.NotEmpty(ticket.Items);
        Assert.Contains(ticket.Items, i => i.Cantidad == 2 && i.PrecioUnitario == 65.15M);
    }
    [Fact]
    public void DetalleTicketFalla()
    {
        var ticket = repoTicket.ObtenerTicket(0);

        Assert.Null(ticket);
    }
}
