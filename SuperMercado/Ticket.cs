using System;
using System.Collections.Generic;
using System.Linq;

namespace SuperMercado
{
    public class Ticket
    {
        public int Id { get; set; }
        public DateTime FechaHora { get; set; }
        public Cajero Cajero { get; set; }
        public List <Item> Items { get; set; }
        public Ticket()
        {
            FechaHora = DateTime.Now;
            Items = new List<Item>();
        }
        public Ticket(Cajero cajero): this()
        {
            Cajero = cajero;
        }
        public decimal TotalTicket => Items.Sum(item =>item.TotalItem);
        public void AgregartItem(Item item) => Items.Add(item);
        public void AgregarProducto(Producto producto, short cantidad)
        {
            //Busco si existe actualmente algun item para el producto
            //del parametro y este ticket
            Item item = Items.FirstOrDefault(i => i.Producto == producto);

            //si no lo encuentro, creo uno.
            if (item is null)
            {
                item = new Item(producto, this, cantidad);
                AgregartItem(item);
            }
            else
            {
                //incremento la cantida del item, en base a lo recibido por parametro
                item.Cantidad += cantidad;
            }
        }
    }
}