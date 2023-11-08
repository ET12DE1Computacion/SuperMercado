using System.Data;
using Super.Core;

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
    [Fact]
    /// <summary>
    /// Este test fallará porque la categoria gaseosa ya existe y viola la constraint de unique en nombre de categoria.
    /// </summary>
    public void AltaCategoriaFalla()
    {
        var gaseosa = new Categoria()
        {
            Nombre = "Gaseosa"
        };

        var excep = Assert.Throws<ConstraintException>(() => Ado.AltaCategoria(gaseosa));
        Assert.Contains("ya se encuentra en uso", excep.Message);
    }
    [Fact]
    public void AltaCategoria()
    {
        var almacen = new Categoria()
        {
            Nombre = "Almacen"
        };

        Assert.Equal(0, almacen.IdCategoria);
        
        Ado.AltaCategoria(almacen);
        
        Assert.NotEqual(0, almacen.IdCategoria);
    }
}