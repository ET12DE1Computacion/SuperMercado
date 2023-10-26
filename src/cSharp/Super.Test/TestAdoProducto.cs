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
    [Fact]
    public void AltaProducto()
    {
        var gaseosa = _ado.ObtenerCategorias().
            First(g => g.Nombre == "Gaseosa");
        Assert.NotNull(gaseosa);
        
        var pretty = new Producto(gaseosa, "Pretty Limon 2.25L.", precio: 200, cantidad: 100);
        Assert.Equal(0, pretty.IdProducto);

        _ado.AltaProducto(pretty);
        Assert.NotEqual(0, pretty.IdProducto);

        var mismaPretty = _ado.ObtenerProducto(pretty.IdProducto);
        Assert.NotNull(mismaPretty);
        Assert.Equal(pretty.PrecioUnitario, mismaPretty.PrecioUnitario, 1);
        Assert.Equal<ushort>(pretty.Cantidad, mismaPretty.Cantidad);
        Assert.NotEmpty(mismaPretty.Ingresos);
        Assert.NotEmpty(mismaPretty.Precios);
    }
}
