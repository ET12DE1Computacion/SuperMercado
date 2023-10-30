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

        Ado.AltaTicket(ticket);
        Assert.NotEqual(0, ticket.Id);
    }
}
