using System;
using MenuesConsola;
using SuperMercado;

namespace ProgramaCajero.Menu
{
    internal class MenuAltaTicket: MenuComponente
    {
        private Cajero cajero;
        private SeleccionProducto seleccionador;
        public Ticket Ticket { get; set; }

        public MenuAltaTicket()
        {
        }

        public MenuAltaTicket(Cajero cajero): this("Alta Ticket")
        {
            this.cajero = cajero;
            seleccionador = new SeleccionProducto();
        }

        public MenuAltaTicket(string nombre) : base(nombre)
        {
        }

        public override void mostrar()
        {
            base.mostrar();
            Ticket = new Ticket(cajero);
            do
            {
                agregarItem();
            }
            while (preguntaCerrada("¿Desea agregar otro Item?"));

            procesarTicket();

            Console.ReadKey();
        }

        private void procesarTicket()
        {
            try
            {
                AdoCajero.ADO.agregarTicket(Ticket);
                Console.Write("Ticket agregado con exito");
                if (preguntaCerrada("¿Confirmar Ticket?"))
                {
                    Ticket.confirmar();
                    AdoCajero.ADO.actualizarTicket(Ticket);
                    Console.WriteLine("Actualizado con exito");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Hubo un error: {e.Message} - {e.InnerException.Message}");
            }
        }

        private void agregarItem()
        {
            Console.WriteLine("Seleccione producto");
            var producto = seleccionador.seleccionarElemento();
            var cantidad = Convert.ToInt16(prompt("Ingrese cantidad a agregar"));
            Ticket.agregarProducto(producto, cantidad);
            Console.WriteLine($"Subtotal: ${Ticket.TotalTicket:0.00}");
        }
    }
}