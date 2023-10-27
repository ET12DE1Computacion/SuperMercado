namespace Super.Test;
public class TestAdoCajero : TestAdo
{
    [Theory]
    [InlineData(100,"Pepe", "zapatos")]
    [InlineData(90,"Moni", "cafecito")]
    public void TraerCajero(uint dni, string nombre, string pass)
    {
        var cajero = Ado.CajeroPorPass(dni, pass);

        Assert.NotNull(cajero);
        Assert.Equal(nombre, cajero.Nombre);
        Assert.Equal<uint>(dni, cajero.Dni);
    }

    [Theory]
    [InlineData(10, "NoExisto")]
    [InlineData(11, "yoTampoco")]
    public void CajerosNoExisten(uint dni, string pass)
    {
        var cajero = Ado.CajeroPorPass(dni, pass);

        Assert.Null(cajero);
    }
}
