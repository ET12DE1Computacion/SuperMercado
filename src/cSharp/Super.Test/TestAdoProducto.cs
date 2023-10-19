using Super.Core;
using Super.Dapper;

namespace Super.Test;
public class TestAdoProducto
{
    private readonly IAdo _ado;
    public TestAdoProducto()
    {
        var cadena = "Server=localhost;Database=Supermercado;Uid=gerenteSuper;pwd=passGerente;Allow User Variables=True";
        _ado = new AdoDapper(cadena);
    }
    [Fact]
    public void TraerProductos()
    {
        var productos = _ado.ObtenerProductos();

        Assert.NotEmpty(productos);
        Assert.Contains(productos, p => p.Nombre == "Manaos Cola 2.25L." && p.Categoria.Nombre == "Gaseosa");
    }
    [Fact]
    public void ProductoPorId()
    {
        var producto = _ado.ObtenerProducto(1);

        Assert.NotNull(producto);
        Assert.Equal("Manaos Cola 2.25L.", producto.Nombre);
        Assert.NotEmpty(producto.Ingresos);
        Assert.NotEmpty(producto.Precios);
    }
}
