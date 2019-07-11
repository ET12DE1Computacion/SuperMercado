using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SuperMercado
{
    [Table("Ticket")]
    public class Ticket
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column("idTicket")]
        public int Id { get; set; }

        [Column("fechaHora"), Required]
        public DateTime FechaHora { get; set; }

        //Ya mapeado en Item
        public List<Item> Items { get; set; }

        [Column("confirmado"), Required]
        public bool Confirmado { get; set; } = false;
        public Ticket()
        {
            FechaHora = DateTime.Now;
            Items = new List<Item>();
        }

        //Indico que la propiedad es un calculo
        //y no se debe mapear
        [NotMapped]
        public float TotalTicket => Items.Sum(item =>item.TotalItem);
        public void agregartItem(Item item)
        {
            Items.Add(item);
        }

        public void agregarProducto(Producto producto, short cantidad)
        {
            //Busco si existe actualmente algun item para el producto
            //del parametro y este ticket
            Item item = Items.FirstOrDefault(i => i.Producto == producto);

            //si no lo encuentro, creo uno.
            if (item == null)
            {
                item = new Item()
                {
                    Producto = producto,
                    Ticket = this,
                    Cantidad = 0                    
                };
            }
            
            //incremento la cantida del item, en base a lo recibido por parametro
            item.Cantidad += cantidad;

        }
        public void confirmar()
        {
            if (!Confirmado)
            {
                Items.ForEach(item => item.decrementarProducto());
                Confirmado = true;
            }
        }
    }
}