using SuperMercado;
using System;

namespace ProgramaGerente.Menu
{
    public class MenuModificarProducto: MenuListadorProducto
    {
        public Producto Producto { get; set; }
        public override void mostrar()
        {
            Console.Clear();
            Console.WriteLine(Nombre);

            Producto = seleccionarElemento();
            Console.WriteLine();
            menuModificiarProducto();
        }

        private void menuModificiarProducto()
        {
            bool cambio = false;
            if (preguntaCerrada("¿Cambiar nombre?"))
            {
                Producto.Nombre = prompt("Ingrese nombre");
                cambio = true;
            }

            if (preguntaCerrada("¿Cambiar precio unitario?"))
            {
                var precio = decimal.Parse(prompt("Precio Unitario"));
                Producto.PrecioUnitario = precio;
                cambio = true;
            }

            if (preguntaCerrada("¿Incrementar stock?"))
            {
                var cantidad = Convert.ToInt16(prompt("Ingrese stock a incrementar"));
                Producto.Cantidad += cantidad;
                cambio = true;
            }

            if (cambio)
            {
                try
                {
                    AdoGerente.ADO.ActualizarProducto(Producto);
                    Console.WriteLine("Producto actualizado con exito");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"No se pudo modificar por: {e.InnerException.Message}");
                }
                Console.ReadKey();
            }
        }
    }
}