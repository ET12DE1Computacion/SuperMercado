namespace Super.Core.Product;
public class HistorialPrecio : PosesionProducto
{
    // Como el precio no se cambia, lo colocamos como init.
    public decimal PrecioUnitario { get; init; }
    public HistorialPrecio(Producto producto)
        : base(producto.IdProducto)
        => PrecioUnitario = producto.PrecioUnitario;
    public HistorialPrecio()
    {
        
    }
}
