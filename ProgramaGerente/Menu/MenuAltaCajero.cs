using MenuesConsola;
using SuperMercado;
using System;
using static System.ReadLine;

namespace ProgramaGerente.Menu
{
    public class MenuAltaCajero: MenuComponente
    {
        public Cajero Cajero { get; set; }
        public override void mostrar()
        {
            base.mostrar();

            var dni = Convert.ToInt32(prompt("Ingrese DNI"));
            var nombre = prompt("Ingrese nombre cajero");
            var apellido = prompt("Ingrese apellido");
            //Uso la libreria System.ReadLine para leer una contraseña
            var pass = ReadPassword("Ingrese contraseña: ");

            Cajero = new Cajero()
            {
                Apellido = apellido,
                Nombre = nombre,
                Dni = dni,
                Password = pass
            };

            try
            {
                AdoGerente.ADO.AltaCajero(Cajero);
                Console.WriteLine("Cajero dada de alta con exito");
            }
            catch (Exception e)
            {
                Console.WriteLine($"No se pudo dar de alta: {e.Message}");
            }
            Console.ReadKey();
        }
    }
}
