using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using SuperMercado.Product;

namespace SuperMercado.ADO.MySQL
{
    public class AdoMySQL : IADO
    {
        private MySqlConnection Conexion { get; set; }
        private MySqlCommand Comando { get; set; }
        private MySqlDataAdapter Adaptador { get; set; }
        private BuilderParametro BP { get; set; }
        private const string BD = "Supermercado";
        public AdoMySQL(string cadConexion)
        {
            Conexion = new MySqlConnection(cadConexion);
            Comando = new MySqlCommand();
            Comando.Connection = Conexion;
        }
        public void PrepararComandoSP()
        {
            Comando.CommandType = System.Data.CommandType.StoredProcedure;
            Comando.Parameters.Clear();
            BP = new BuilderParametro();
        }
        public void PrepararComandoSP(string nombre)
        {
            PrepararComandoSP();
            Comando.CommandText = nombre;
        }
        public void EjecutarComando()
        {
            try
            {
                Conexion.Open();
                Comando.ExecuteNonQuery();
                Conexion.Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Conexion.Close();
            }
        }
        public void ActualizarProducto(Producto producto)
        {
            throw new NotImplementedException();
        }

        public void ActualizarTicket(Ticket ticket)
        {
            throw new NotImplementedException();
        }

        public void AgregarCategoria(Rubro categoria)
        {
            throw new NotImplementedException();
        }

        public void AgregarProducto(Producto producto)
        {
            PrepararComandoSP("altaProducto");
            
            var unNombre = BP.CrearParametro("unNombre")
                             .SetTipoVarchar(45)
                             .SetValor(producto.Nombre)
                             .Parametro;
            Comando.Parameters.Add(unNombre);

            var unIdRubro = BP.CrearParametro("unIdRubro")
                              .SetTipo(MySqlDbType.Int16)
                              .SetValor(producto.Rubro.Id)
                              .Parametro;
            Comando.Parameters.Add(unIdRubro);

        }

        public void AgregarTicket(Ticket ticket)
        {
            throw new NotImplementedException();
        }

        public void AltaCajero(Cajero cajero)
        {
            throw new NotImplementedException();
        }

        public Cajero CajeroPorDniPass(int dni, string passEncriptada)
        {
            throw new NotImplementedException();
        }

        public List<HistorialPrecio> HistorialDe(Producto producto)
        {
            throw new NotImplementedException();
        }

        public ICollection<IngresoProducto> IngresosDe(Producto producto)
        {
            throw new NotImplementedException();
        }

        public List<Item> ItemsDe(Ticket ticket)
        {
            throw new NotImplementedException();
        }

        public List<Cajero> ObtenerCajeros()
        {
            throw new NotImplementedException();
        }

        public List<Rubro> ObtenerCategorias()
        {
            throw new NotImplementedException();
        }

        public List<Producto> ObtenerProductos()
        {
            throw new NotImplementedException();
        }

        public List<Ticket> ObtenerTickets()
        {
            throw new NotImplementedException();
        }
    }
}
