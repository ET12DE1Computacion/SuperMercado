using System;

namespace SuperMercado.Product
{
    public record HistorialPrecio(Producto Producto, DateTime FechaHora, decimal PrecioUnitario)
    {
        public bool Entre(DateTime inicio, DateTime fin)
            => FechaHora >= inicio && FechaHora <= fin;
    }
}