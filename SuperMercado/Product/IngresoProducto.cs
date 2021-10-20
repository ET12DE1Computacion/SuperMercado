using System;

namespace SuperMercado.Product
{
    public record IngresoProducto (Producto Producto, DateTime FechaHora, ushort Unidades);
}
