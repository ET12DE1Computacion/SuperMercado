using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace SuperMercado.ADO.MySQL
{
    public class AdoMySQL : IADO
    {
        private MySqlConnection Conexion { get; set; }
        public void actualizarProducto(Producto producto)
        {
            throw new NotImplementedException();
        }

        public void actualizarTicket(Ticket ticket)
        {
            throw new NotImplementedException();
        }

        public void agregarCategoria(Categoria categoria)
        {
            throw new NotImplementedException();
        }

        public void agregarProducto(Producto producto)
        {
            throw new NotImplementedException();
        }

        public void agregarTicket(Ticket ticket)
        {
            throw new NotImplementedException();
        }

        public void altaCajero(Cajero cajero)
        {
            throw new NotImplementedException();
        }

        public Cajero cajeroPorDniPass(int dni, string passEncriptada)
        {
            throw new NotImplementedException();
        }

        public List<HistorialPrecio> historialDe(Producto producto)
        {
            throw new NotImplementedException();
        }

        public List<Item> itemsDe(Ticket ticket)
        {
            throw new NotImplementedException();
        }

        public List<Cajero> obtenerCajeros()
        {
            throw new NotImplementedException();
        }

        public List<Categoria> obtenerCategorias()
        {
            throw new NotImplementedException();
        }

        public List<Producto> obtenerProductos()
        {
            throw new NotImplementedException();
        }

        public List<Ticket> obtenerTickets()
        {
            throw new NotImplementedException();
        }
    }
}
