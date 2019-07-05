using System;
using System.Collections.Generic;
using System.Linq;

namespace SuperMercado
{
    public class Ticket
    {
        public DateTime FechaHora { get; set; }
        public List<Item> Items { get; set; }
        public bool Confirmado { get; set; } = false;
        public Ticket()
        {
            FechaHora = DateTime.Now;
            Items = new List<Item>();
        }
        public float TotalTicket => Items.Sum(item =>item.TotalItem);
        public void agregartItem(Item item)
        {
            Items.Add(item);
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