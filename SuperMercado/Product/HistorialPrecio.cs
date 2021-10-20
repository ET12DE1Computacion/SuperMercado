using System;

namespace SuperMercado.Product
{
    public record HistorialPrecio(Producto Producto, DateTime FechaHora, float PrecioUnitario)
    {
        public bool Entre(DateTime inicio, DateTime fin)
            => FechaHora >= inicio && FechaHora <= fin;
    }
}