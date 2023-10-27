namespace Super.Test;
public class TestAdoCategoria : TestAdo
{
    [Fact]
    public void TraerCategorias()
    {
        var categorias = Ado.ObtenerCategorias();

        Assert.NotEmpty(categorias);
        //Pregunto por rubros que se dan de alta en "scripts/bd/MySQL/03 Inserts.sql"
        Assert.Contains(categorias, c => c.Nombre == "Gaseosa");
        Assert.Contains(categorias, c => c.Nombre == "Lacteo");
    }
}