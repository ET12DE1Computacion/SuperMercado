using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperMercado
{
    [Table("Item")]
    public class Item
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column("idItem")]
        public int Id { get; set; }
        [ForeignKey("idProducto"), Required]
        public Producto Producto { get; set; }

        [ForeignKey("idTicket"), Required]
        public Ticket Ticket { get; set; }

        [Column("cantidad"), Required]
        public short Cantidad { get; set; }

        [Column("precioUnitario"), Required]
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

        public Item(Producto producto, Ticket ticket, short cantidad)
            :this(producto, cantidad)
        {
            Ticket = ticket;
        }
        public void decrementarProducto()
        {
            Producto.decrementarCantidad(Cantidad);
        }

        //Indico que esta propiedad no se debe almacenar
        //Ya que es un calculo
        [NotMapped]
        public float TotalItem => Cantidad * PrecioUnitario;
    }
}