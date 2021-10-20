namespace SuperMercado
{
    public class Item
    {
        public Producto Producto { get; set; }
        public Ticket Ticket { get; set; }
        public short Cantidad { get; set; }
        public float PrecioUnitario { get; set; }
        public Item() { }
        //Constructor que recibe un producto y una cantidad
        public Item(Producto producto, short cantidad)
        {   
            Producto = producto;
            Cantidad = cantidad;
            PrecioUnitario = producto.PrecioUnitario;
        }

        public Item(Producto producto, Ticket ticket, short cantidad)
            :this(producto, cantidad)
        {
            Ticket = ticket;
        }
        public float TotalItem => Cantidad * PrecioUnitario;
    }
}