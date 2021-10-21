using MenuesConsola;
using SuperMercado;
using System;
using System.Collections.Generic;

namespace ProgramaGerente.Menu
{
    public class MenuListaCategorias : MenuListador<Rubro>
    {
        public override void imprimirElemento(Rubro elemento) 
            => Console.WriteLine($"{elemento.Id} - {elemento.Nombre}");

        public override List<Rubro> obtenerLista()
            => AdoGerente.ADO.ObtenerRubros();
    }
}