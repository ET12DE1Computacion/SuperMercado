namespace SuperMercado
{
    public class Item
    {
        //Propiedad automatica para el producto del Item
        public Producto Producto { get; set; }
        public short Cantidad { get; set; }
        public float PrecioUnitario { get; set; }
        //Constructor por defecto
        public Item() { }
        //Constructor que recibe un producto y una cantidad
        public Item(Producto producto, short cantidad)
        {
            Producto = producto;
            Cantidad = cantidad;
            PrecioUnitario = producto.PrecioUnitario;
        }
        public void decrementarProducto()
        {
            Producto.decrementarCantidad(Cantidad);
        }
        public float TotalItem => Cantidad * PrecioUnitario;
    }
}