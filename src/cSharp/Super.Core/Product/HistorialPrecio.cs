namespace Super.Core.Product;
public class HistorialPrecio : PosesionProducto
{
    // Como el precio no se cambia, lo colocamos como init.
    public decimal Precio { get; init; }
    public HistorialPrecio(Producto producto)
        : base(producto.Id)
        => Precio = producto.PrecioUnitario;
}
