using MenuesConsola;
using SuperMercado;
using System;
using static System.ReadLine;

namespace ProgramaCajero.Menu
{
    public class Login: MenuComponente
    {
        private Cajero Cajero { get; set; }
        private MenuCompuesto PrincipalUsuario { get; set; }


        public override void mostrar()
        {
            base.mostrar();

            var dni = Convert.ToInt32(prompt("Ingrese dni"));
            var pass = ReadPassword("Ingrese contraseña: ");

            try
            {
                Cajero = AdoCajero.ADO.CajeroPorDniPass(dni, pass);
                if (Cajero is null)
                {
                    Console.WriteLine("DNI o contraseña incorrecta");
                    Console.ReadKey();
                }
                else
                {
                    instanciarMenuesPara(Cajero);
                    PrincipalUsuario.mostrar();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"No se pudo iniciar sesion debido a un error: {e.Message}");
                Console.ReadKey();
            }
        }

        private void instanciarMenuesPara(Cajero cajero)
        {
            var menuAltaTicket = new MenuAltaTicket(cajero);
            //De haber mas menues para el cajero, se siguen instanciado aca

            PrincipalUsuario = new MenuCompuesto(menuAltaTicket) { Nombre = "Menu Cajero" };
        }
    }
}