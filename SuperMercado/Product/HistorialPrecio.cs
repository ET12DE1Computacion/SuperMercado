using System;

namespace SuperMercado.Product
{
    public record HistorialPrecio(Producto Producto, DateTime FechaHora, float PrecioUnitario)
    {
        public HistorialPrecio(Producto producto)
        {
            Producto = producto;
        }

        public bool Entre(DateTime inicio, DateTime fin)
            => FechaHora >= inicio && FechaHora <= fin;
    }
}