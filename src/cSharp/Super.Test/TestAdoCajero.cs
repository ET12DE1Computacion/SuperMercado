using Super.Core;

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
    [Fact]
    public void AltaCajero()
    {
        uint dni = 1000;
        string pass = "123456";
        string nombre = "Nuevo";
        string apellido = "Gonzales";

        var cajero = Ado.CajeroPorPass(dni, pass);

        Assert.Null(cajero);

        var nuevoGonzales = new Cajero()
        {
            Dni = dni,
            Nombre = nombre,
            Apellido = apellido
        };

        Ado.AltaCajero(nuevoGonzales, pass);

        var mismoCajero = Ado.CajeroPorPass(dni, pass);
        
        Assert.NotNull(mismoCajero);
        Assert.Equal(nombre, mismoCajero.Nombre);
        Assert.Equal(apellido, mismoCajero.Apellido);
    }
}
