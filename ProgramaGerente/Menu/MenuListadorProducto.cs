using MenuesConsola;
using SuperMercado;
using System;
using System.Collections.Generic;

namespace ProgramaGerente.Menu
{
    public class MenuListadorProducto : MenuListador<Producto>
    {
        public override void imprimirElemento(Producto p) => Console.WriteLine(p.ToString());

        public override List<Producto> obtenerLista() => AdoGerente.ADO.obtenerProductos();
    }
}