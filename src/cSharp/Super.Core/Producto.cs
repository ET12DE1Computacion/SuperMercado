using System.Diagnostics.CodeAnalysis;
using Super.Core.Product;

namespace Super.Core;
public class Producto
{
    public short IdProducto { get; set; }
    public required string Nombre { get; set; }
    public decimal PrecioUnitario { get; private set; }
    // El set de Stock deberia ser privado, pero por dapper lo necesito colocar el publico
    public ushort Cantidad { get; set; }
    public List<IngresoStock> Ingresos { get; set; }
    public List<HistorialPrecio> Precios { get; set; }
    public required Categoria Categoria { get; set; }
    
    // Constructor agregado para dapper
    public Producto()
    {
        Ingresos = new();
        Precios = new();
    }
    [SetsRequiredMembers]
    public Producto(Categoria categoria, string nombre, decimal precio, ushort cantidad)
    {
        Nombre = nombre;
        PrecioUnitario = precio;
        Ingresos = new List<IngresoStock>();
        Precios = new List<HistorialPrecio>();
        Categoria = categoria;
        Cantidad = cantidad;
    }
    public void CambiarPrecio(decimal precio)
    {
        PrecioUnitario = precio;
        var historial = new HistorialPrecio(this);
    }

}
