using Super.Core;
using Super.Core.Repos;
using Super.Dapper;

namespace Super.Test;
public class TestAdoCajero : TestRepo
{
    //Repo que vamos a usar en este test
    readonly IRepoCajero repoCajero;
    //Este test, va a usar la cadena de conexión que definí en la clase base TestRepo para instanciar RepoCajero.
    public TestAdoCajero() : base()
        => repoCajero = new RepoCajero(_conexion);
    [Theory]
    [InlineData(100,"Pepe", "zapatos")]
    [InlineData(90,"Moni", "cafecito")]
    public void TraerCajero(uint dni, string nombre, string pass)
    {
        var cajero = repoCajero.CajeroPorPass(dni, pass);

        Assert.NotNull(cajero);
        Assert.Equal(nombre, cajero.Nombre);
        Assert.Equal<uint>(dni, cajero.Dni);
    }

    [Theory]
    [InlineData(10, "NoExisto")]
    [InlineData(11, "yoTampoco")]
    public void CajerosNoExisten(uint dni, string pass)
    {
        var cajero = repoCajero.CajeroPorPass(dni, pass);

        Assert.Null(cajero);
    }
    [Fact]
    public void AltaCajero()
    {
        uint dni = 1000;
        string pass = "123456";
        string nombre = "Nuevo";
        string apellido = "Gonzales";

        var cajero = repoCajero.CajeroPorPass(dni, pass);

        Assert.Null(cajero);

        var nuevoGonzales = new Cajero()
        {
            Dni = dni,
            Nombre = nombre,
            Apellido = apellido
        };

        repoCajero.AltaCajero(nuevoGonzales, pass);

        var mismoCajero = repoCajero.CajeroPorPass(dni, pass);
        
        Assert.NotNull(mismoCajero);
        Assert.Equal(nombre, mismoCajero.Nombre);
        Assert.Equal(apellido, mismoCajero.Apellido);
    }
}
