using MenuesConsola;
using ProgramaGerente.Menu;

namespace ProgramaGerente
{
    class Program
    {
        static void Main(string[] args)
        {
            var menuAltaCategoria = new MenuAltaCategoria() { Nombre = "Alta Categoria" };
            var menuListaCategoria = new MenuListaCategorias() { Nombre = "Listado Categorias" };
            var menuAltaCajero = new MenuAltaCajero() { Nombre = "Alta Cajero" };

            var menuCategoria = new MenuCompuesto() { Nombre = "Categorias" };
            menuCategoria.agregarMenu(menuAltaCategoria);
            menuCategoria.agregarMenu(menuListaCategoria);

            var menuCajero = new MenuCompuesto() { Nombre = "Cajeros" };
            menuCajero.agregarMenu(menuAltaCajero);

            var menuPrincipal = new MenuCompuesto() { Nombre = "Menu Gerente" };
            menuPrincipal.agregarMenu(menuCategoria);
            menuPrincipal.agregarMenu(menuCajero);

            menuPrincipal.mostrar();
        }
    }
}