using Super.Core;
using Super.Dapper;

namespace Super.Test;

public class TestAdoDapper
{
    private readonly IAdo _ado;
    public TestAdoDapper()
    {
        var cadena = "Server=localhost;Database=Supermercado;Uid=gerenteSuper;pwd=passGerente";
        _ado = new AdoDapper(cadena);
    }
    [Fact]
    public void TraerCategorias()
    {
        var categorias = _ado.ObtenerCategorias();

        Assert.NotEmpty(categorias);
        //Pregunto por rubros que se dan de alta en "scripts/bd/MySQL/03 Inserts.sql"
        Assert.Contains(categorias, c => c.Nombre == "Gaseosa");
        Assert.Contains(categorias, c => c.Nombre == "Lacteo");
    }
}