using MenuesConsola;
using SuperMercado;
using System;
using System.Collections.Generic;

namespace ProgramaGerente.Menu
{
    public class MenuListaCajeros : MenuListador<Cajero>
    {
        public override void imprimirElemento(Cajero elemento)
        {
            Console.WriteLine($"{elemento.NombreCompleto}\t\t{elemento.Dni}");
        }

        public override List<Cajero> obtenerLista() => AdoGerente.ADO.obtenerCajeros();
    }
}
