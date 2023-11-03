using System.Diagnostics.CodeAnalysis;

namespace Super.Core;
public class Item
{
    public required Producto Producto { get; set; }
    public int IdTicket { get; set; }
    public decimal PrecioUnitario { get; set; }
    public byte Cantidad { get; set; }

    //Este DA le indica al compilador que este constructor inicia las propiedades marcadas como requeried
    [SetsRequiredMembers]
    public Item(Producto producto, byte cantidad)
    {
        Producto = producto;
        PrecioUnitario = producto.PrecioUnitario;
        Cantidad = cantidad;
    }
    //Este constructor es necesario para Dapper
    [SetsRequiredMembers]
    public Item(byte cantidad, decimal precioUnitario)
    {
        Cantidad = cantidad;
        PrecioUnitario = precioUnitario;
    }
    public void IncrementarCantidad(byte cantidad)
        => Cantidad += cantidad;
}
