using System.Collections.Generic;
using SuperMercado.Product;

namespace SuperMercado.ADO
{
    public interface IADO
    {
        #region Categoria
        void AgregarCategoria(Rubro categoria);
        List<Rubro> ObtenerCategorias();
        #endregion

        #region Producto
        void AgregarProducto(Producto producto);
        void ActualizarProducto(Producto producto);
        List<Producto> ObtenerProductos();
        List<HistorialPrecio> HistorialDe(Producto producto);
        ICollection<IngresoProducto> IngresosDe(Producto producto);
        #endregion

        #region Ticket
        List<Item> ItemsDe(Ticket ticket);
        void AgregarTicket(Ticket ticket);
        void ActualizarTicket(Ticket ticket);
        List<Ticket> ObtenerTickets();
        #endregion

        #region Cajero
        void AltaCajero(Cajero cajero);
        Cajero CajeroPorDniPass(int dni, string passEncriptada);
        List<Cajero> ObtenerCajeros();
        #endregion
    }
}
