using MenuesConsola;
using SuperMercado;

namespace ProgramaCajero.Menu
{
    internal class MenuAltaTicket: MenuComponente
    {
        private Cajero cajero;

        public MenuAltaTicket()
        {
        }

        public MenuAltaTicket(Cajero cajero): this("Alta Ticket")
        {
            this.cajero = cajero;
        }

        public MenuAltaTicket(string nombre) : base(nombre)
        {
        }

        public override void mostrar()
        {
            base.mostrar();
            System.Console.WriteLine("Acciones de alta ticket");
        }
    }
}