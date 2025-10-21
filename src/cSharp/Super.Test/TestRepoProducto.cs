using Super.Core;
using Super.Core.Repos;
using Super.Dapper;

namespace Super.Test;
public class TestAdoProducto : TestRepo
{
    private readonly IRepoProducto repoProducto;
    public TestAdoProducto() : base() => repoProducto = new RepoProducto(_conexion);
    [Fact]
    public void TraerProductos()
    {
        var productos = repoProducto.ObtenerProductos();

        Assert.NotEmpty(productos);
        Assert.Contains(productos, p => p.Nombre == "Manaos Cola 2.25L." && p.Categoria.Nombre == "Gaseosa");
    }
    [Fact]
    public void ProductoPorId()
    {
        var producto = repoProducto.ObtenerProducto(1);

        Assert.NotNull(producto);
        Assert.Equal("Manaos Cola 2.25L.", producto.Nombre);
        Assert.NotEmpty(producto.Ingresos);
        Assert.NotEmpty(producto.Precios);
    }
    [Fact]
    public void AltaProducto()
    {
        var gaseosa = repoProducto.ObtenerCategorias().
            First(g => g.Nombre == "Gaseosa");
        Assert.NotNull(gaseosa);
        
        var pretty = new Producto(gaseosa, "Pretty Limon 2.25L.", precio: 200, cantidad: 100);
        Assert.Equal(0, pretty.IdProducto);

        repoProducto.AltaProducto(pretty);
        Assert.NotEqual(0, pretty.IdProducto);

        var mismaPretty = repoProducto.ObtenerProducto(pretty.IdProducto);
        Assert.NotNull(mismaPretty);
        Assert.Equal(pretty.PrecioUnitario, mismaPretty.PrecioUnitario, 1);
        Assert.Equal<ushort>(pretty.Cantidad, mismaPretty.Cantidad);
        Assert.NotEmpty(mismaPretty.Ingresos);
        Assert.NotEmpty(mismaPretty.Precios);
    }
}
