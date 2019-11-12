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
            var menuListaCajeros = new MenuListaCajeros() { Nombre = "Listado Cajeros" };
            var menuAltaProducto = new MenuAltaProducto(menuListaCategoria);
            var menuListadorProductos = new MenuListadorProducto() { Nombre = "Listado Productos" };
            var menuModificarProducto = new MenuModificarProducto() { Nombre = "Modificar producto" };

            var menuCategoria = new MenuCompuesto() { Nombre = "Categorias" };
            menuCategoria.agregarMenu(menuAltaCategoria);
            menuCategoria.agregarMenu(menuListaCategoria);

            var menuCajero = new MenuCompuesto() { Nombre = "Cajeros" };
            menuCajero.agregarMenu(menuListaCajeros);
            menuCajero.agregarMenu(menuAltaCajero);

            var menuProducto = new MenuCompuesto(menuAltaProducto) { Nombre = "Productos" };
            menuProducto.agregarMenu(menuListadorProductos);
            menuProducto.agregarMenu(menuModificarProducto);
            
            var menuPrincipal = new MenuCompuesto() { Nombre = "Menu Gerente" };
            menuPrincipal.agregarMenu(menuCategoria);
            menuPrincipal.agregarMenu(menuCajero);
            menuPrincipal.agregarMenu(menuProducto);

            menuPrincipal.mostrar();
        }
    }
}