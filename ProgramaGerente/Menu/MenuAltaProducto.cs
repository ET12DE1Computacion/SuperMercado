using MenuesConsola;
using SuperMercado;
using System;

namespace ProgramaGerente.Menu
{
    public class MenuAltaProducto: MenuComponente
    {
        private MenuListaCategorias MenuListaCategorias { get; set; }
        private Producto Producto { get; set; }
        
        public MenuAltaProducto(MenuListaCategorias menuListaCategorias)
        {
            MenuListaCategorias = menuListaCategorias;
            Nombre = "Alta Producto";
        }

        public override void mostrar()
        {
            base.mostrar();

            var nombre = prompt("Ingrese nombre del producto");
            var precio = decimal.Parse(prompt("Ingrese precio unitario"));
            var cantidad = Convert.ToInt16(prompt("Ingrese stock"));
            Console.WriteLine("Seleccione una categoria x)");
            var categoria = MenuListaCategorias.seleccionarElemento();

            Producto = new Producto()
            {
                Nombre = nombre,
                PrecioUnitario = precio,
                Rubro = categoria,
                Cantidad = cantidad
            };

            try
            {
                AdoGerente.ADO.AgregarProducto(Producto);
                Console.WriteLine("Producto dado de alta con exito");
            }
            catch (Exception e)
            {
                Console.WriteLine($"No se pudo dar de alta el producto: {e.Message}");
            }
            Console.ReadKey();
        }
    }
}
