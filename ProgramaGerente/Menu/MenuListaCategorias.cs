using MenuesConsola;
using SuperMercado;
using System;
using System.Collections.Generic;

namespace ProgramaGerente.Menu
{
    public class MenuListaCategorias : MenuListador<Categoria>
    {
        public override void imprimirElemento(Categoria elemento) 
            => Console.WriteLine($"{elemento.Id} - {elemento.Nombre}");

        public override List<Categoria> obtenerLista()
            => AdoGerente.ADO.obtenerCategorias();
    }
}