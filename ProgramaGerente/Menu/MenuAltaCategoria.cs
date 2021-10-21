using MenuesConsola;
using SuperMercado;
using System;

namespace ProgramaGerente.Menu
{
    public class MenuAltaCategoria: MenuComponente
    {
        public Rubro Categoria { get; set; }
        public override void mostrar()
        {
            base.mostrar();
            Console.WriteLine();
            var nombre = prompt("Ingrese nombre categoria: ");
            Categoria = new Rubro(nombre);
            try
            {
                AdoGerente.ADO.AgregarRubro(Categoria);
                Console.WriteLine("Categoria agregada con exito");
            }
            catch (Exception e)
            {
                Console.WriteLine($"No se pudo cargar la categoria por: {e.Message}");
            }
            Console.ReadKey();
        }
    }
}