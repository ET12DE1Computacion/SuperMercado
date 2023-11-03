using Super.Core;

namespace Super.Test;

public class TestAdoTicket : TestAdo
{
    private static string _cadena =
        @"Server=localhost;Database=Supermercado;Uid=cajero;pwd=passCajero;Allow User Variables=True";
    public TestAdoTicket() : base(_cadena) { }
    [Fact]
    public void AltaTicketOK()
    {
        var pepe = Ado.CajeroPorPass(100, "zapatos");
        Assert.NotNull(pepe);

        var produtos = Ado.ObtenerProductos();
        Assert.NotNull(produtos);
        Assert.NotEmpty(produtos);

        var ticket = new Ticket()
        {
            Cajero = pepe
        };

        //Cargo el ticket con items
        produtos.ForEach(p => ticket.AgregarItem(p, 1));

        Ado.AltaTicket(ticket);
        Assert.NotEqual(0, ticket.Id);
    }

    [Fact]
    public void AltaTicketRompe()
    {
        var pepe = Ado.CajeroPorPass(100, "zapatos");
        Assert.NotNull(pepe);

        var produtos = Ado.ObtenerProductos();
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
        var excep = Assert.Throws<InvalidOperationException>(() => Ado.AltaTicket(ticket));
        Assert.StartsWith("No alcanza stock", excep.Message);
    }
    [Fact]
    public void DetalleTicketOK()
    {
        var ticket = Ado.ObtenerTicket(1);

        Assert.NotNull(ticket);
        Assert.NotEmpty(ticket.Items);
        Assert.Contains(ticket.Items, i => i.Cantidad == 2 && i.PrecioUnitario == 65.15M);
    }
    [Fact]
    public void DetalleTicketFalla()
    {
        var ticket = Ado.ObtenerTicket(0);

        Assert.Null(ticket);
    }
}
