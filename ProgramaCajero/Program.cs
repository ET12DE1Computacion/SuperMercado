using System;
using ProgramaCajero.Menu;

namespace ProgramaCajero
{
    public class Program
    {
        static void Main(string[] args)
        {
            var login = new Login(){ Nombre = "Inicio Usuario"};
            login.mostrar();
        }
    }
}