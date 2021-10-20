using MenuesConsola;
using SuperMercado;
using System;
using System.Collections.Generic;

namespace ProgramaCajero.Menu
{
    public class SeleccionProducto : MenuListador<Producto>
    {
        public override void imprimirElemento(Producto producto)
            => Console.WriteLine(cadenaProducto(producto));
        private string cadenaProducto(Producto p)
            => $"{p.Nombre} - {p.Rubro.Nombre} - {p.PrecioUnitario}";
        public override List<Producto> obtenerLista()
        {
            return AdoCajero.ADO.ObtenerProductos();
        }
    }
}