using System;
using System.Collections.Generic;
using System.Text;

namespace SuperMercado.ADO
{
    public interface IADO
    {
        #region Categoria
        void agregarCategoria(Categoria categoria);
        List<Categoria> obtenerCategorias();
        #endregion

        #region Producto
        void agregarProducto(Producto producto);
        void actualizarProducto(Producto producto);
        List<Producto> obtenerProductos();
        List<HistorialPrecio> historialDe(Producto producto);
        #endregion

        #region Ticket
        List<Item> itemsDe(Ticket ticket);
        void agregarTicket(Ticket ticket);
        void actualizarTicket(Ticket ticket);
        List<Ticket> obtenerTickets();
        #endregion

        #region Cajero
        void altaCajero(Cajero cajero);
        Cajero cajeroPorDniPass(int dni, string passEncriptada);
        List<Cajero> obtenerCajeros();
        #endregion
    }
}
