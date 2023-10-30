namespace Super.Core;
public class Ticket
{
    public int Id { get; set; }
    public DateTime FechaHora { get; set; }
    public List<Item> Items { get; set; }
    public required Cajero Cajero { get; set; }
    public Ticket()
    {
        FechaHora = DateTime.Now;
        Items = new List<Item>();
    }
    public void AgregarItem(Producto producto, byte cantidad)
    {
        //Me fijo si el producto ya existe en la lista de items
        var item = Items.Find(i => i.Producto.IdProducto == producto.IdProducto);
        
        if (item is not null)
            item.IncrementarCantidad(cantidad);
        else
        {
            item = new Item(producto, cantidad);
            Items.Add(item);
        }
    }
}
