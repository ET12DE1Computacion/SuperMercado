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
        //Propiedad automatica agregada para la persistencia
        [Column("idTicket")]
        [Key]
        public int Id { get; set; }
        [Column("fechaHora")]
        [Required]
        public DateTime FechaHora { get; set; }
        public List<Item> Items { get; set; }

        [Column("confirmado")]
        [Required]
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
            Item item = Items.FirstOrDefault(i => i.Producto == producto);
            if (item == null)
            {
                item = new Item()
                {
                    Producto = producto,
                    Ticket = this,
                    Cantidad = 0                    
                };
            }

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