using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using SuperMercado.Product;
using System.Data;
using System.Text;
using System.Globalization;

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
        private void AgregarParametro(MySqlParameter parametro) => Comando.Parameters.Add(parametro);
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
        public DataTable EjecutarComandoConFill()
        {
            var tabla = new DataTable();
            Adaptador = new MySqlDataAdapter(Comando);
            Adaptador.Fill(tabla);
            return tabla;
        }
        
        public void ActualizarProducto(Producto producto)
        {
            throw new NotImplementedException();
        }
        public void ActualizarTicket(Ticket ticket)
        {
            throw new NotImplementedException();
        }

        public void AgregarRubro(Rubro rubro)
        {
            PrepararComandoSP("altaRubro");

            var undIdRubro = BP.CrearParametroSalida("unIdRubro")
                               .SetTipo(MySqlDbType.UByte)
                               .Parametro;
            AgregarParametro(undIdRubro);

            var unRubro = BP.CrearParametro("unRubro")
                            .SetTipoVarchar(45)
                            .SetValor(rubro.Nombre)
                            .Parametro;
            AgregarParametro(unRubro);
            EjecutarComando();
            rubro.Id = Convert.ToByte(undIdRubro.Value);
        }
        public void AgregarProducto(Producto producto)
        {
            PrepararComandoSP("altaProducto");

            var unIdProducto = BP.CrearParametroSalida("unIdProducto")
                                 .SetTipo(MySqlDbType.Int16)
                                 .Parametro;
            AgregarParametro(unIdProducto);

            var unIdRubro = BP.CrearParametro("unIdRubro")
                              .SetTipo(MySqlDbType.UByte)
                              .SetValor(producto.Rubro.Id)
                              .Parametro;
            AgregarParametro(unIdRubro);

            var unNombre = BP.CrearParametro("unNombre")
                             .SetTipoVarchar(45)
                             .SetValor(producto.Nombre)
                             .Parametro;
            AgregarParametro(unNombre);

            var unPrecioUnitario = BP.CrearParametro("unPrecioUnitario")
                                     .SetTipoDecimal(7, 2)
                                     .SetValor(producto.PrecioUnitario)
                                     .Parametro;
            AgregarParametro(unPrecioUnitario);

            var unaCantidad = BP.CrearParametro("unaCantidad")
                                .SetTipo(MySqlDbType.UInt16)
                                .SetValor(producto.Cantidad)
                                .Parametro;
            AgregarParametro(unaCantidad);

            try
            {
                EjecutarComando();
                producto.Id = Convert.ToInt16(unIdProducto.Value);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void AgregarTicket(Ticket ticket)
        {
            PrepararComandoSP("altaTicket");

            var unIdTicket = BP.CrearParametroSalida("unIdTicket")
                               .SetTipo(MySqlDbType.Int32)
                               .Parametro;
            AgregarParametro(unIdTicket);
            
            var unDni = BP.CrearParametro("unDni")
                          .SetTipo(MySqlDbType.UInt32)
                          .SetValor(ticket.Cajero.Dni)
                          .Parametro;
            AgregarParametro(unDni);

            var unaFechaHora = BP.CrearParametro("unaFechaHora")
                                 .SetTipo(MySqlDbType.DateTime)
                                 .SetValor(ticket.FechaHora)
                                 .Parametro;
            AgregarParametro(unaFechaHora);

            EjecutarComando();
            ticket.Id = Convert.ToInt32(unIdTicket.Value);
            AgregarItemsDe(ticket);
        }

        private void AgregarItemsDe(Ticket ticket)
        {
            Comando = new MySqlCommand();
            Comando.Connection = Conexion;
            Comando.CommandType = CommandType.Text;
            var sb = new StringBuilder("INSERT INTO Item (idProducto, idTicket, cantidad, precioUnitario) VALUES ");
            sb.Append('(')
              .Append(TuplaValorItem(ticket.Items[0]))
              .Append(')');
            for (int i = 1; i < ticket.Items.Count; i++)
            {
                sb.AppendLine(",")
                  .Append('(')
                  .Append(TuplaValorItem(ticket.Items[i]))
                  .Append(')');
            }
            Comando.CommandText = sb.ToString();
            EjecutarComando();
        }
        private string TuplaValorItem(Item item)
            => $"{item.Producto.Id}, {item.Ticket.Id}, {item.Cantidad}, {item.PrecioUnitario.ToString(new CultureInfo("en-US"))}";
        public void AltaCajero(Cajero cajero)
        {
            throw new NotImplementedException();
        }
        public Cajero CajeroPorDniPass(int dni, string passEncriptada)
        {
            PrepararComandoSP("cajeroPorDniPass");
            
            var pDni = BP.CrearParametro("unDni")
                         .SetTipo(MySqlDbType.UInt32)
                         .SetValor(dni)
                         .Parametro;
            AgregarParametro(pDni);

            var pPass = BP.CrearParametro("unaPass")
                          .SetTipoVarchar(45)
                          .SetValor(passEncriptada)
                          .Parametro;
            AgregarParametro(pPass);

            return FilaACajero(EjecutarComandoConFill().Rows[0]);
        }
        public List<HistorialPrecio> HistorialDe(Producto producto)
        {
            PrepararComandoSP("historialPrecioDe");

            var unIdProducto = BP.CrearParametro("unIdProducto")
                                 .SetTipo(MySqlDbType.Int16)
                                 .SetValor(producto.Id)
                                 .Parametro;
            AgregarParametro(unIdProducto);

            var tabla = EjecutarComandoConFill();
            var lista = new List<HistorialPrecio>();
            for (int i = 0; i < tabla.Rows.Count; i++)
            {
                lista.Add(FilaAHistorialPrecio(tabla.Rows[i], producto));
            }
            return lista;
        }

        private HistorialPrecio FilaAHistorialPrecio(DataRow fila, Producto producto)
            => new HistorialPrecio(producto, Convert.ToDateTime(fila["fechaHora"]), Convert.ToDecimal(fila["precioUnitario"]));

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
        public List<Rubro> ObtenerRubros()
        {
            PrepararComandoSP("Rubro");
            Comando.CommandType = CommandType.TableDirect;
            var tabla = EjecutarComandoConFill();
            var lista = new List<Rubro>();
            for (int i = 0; i < tabla.Rows.Count; i++)
            {
                lista.Add(FilaARubro(tabla.Rows[i]));
            }
            return lista;
        }
        private Rubro FilaARubro(DataRow fila) => new Rubro()
        {
            Id = Convert.ToByte(fila["idRubro"]),
            Nombre = fila["rubro"].ToString()
        };
        private Cajero FilaACajero(DataRow fila)
        {
            Cajero cajero = null;
            if (fila is not null)
            {
                cajero = new Cajero()
                {
                    Dni = Convert.ToInt32(fila["dni"]),
                    Nombre = fila["nombre"].ToString(),
                    Apellido = fila["apellido"].ToString()
                };
            }
            return cajero;
        }

        public List<Producto> ObtenerProductos()
        {
            PrepararComandoSP("Producto");
            Comando.CommandType = CommandType.TableDirect;
            var tabla = EjecutarComandoConFill();
            var lista = new List<Producto>();
            for (int i = 0; i < tabla.Rows.Count; i++)
            {
                lista.Add(FilaAProducto(tabla.Rows[i]));
            }
            return lista;
        }
        private Producto FilaAProducto(DataRow fila) => new Producto()
        {
            Id = Convert.ToInt16(fila["idProducto"]),
            Nombre = fila["nombre"].ToString(),
            Cantidad = Convert.ToInt16(fila["cantidad"]),
            PrecioUnitario = Convert.ToDecimal(fila["precioUnitario"]),
            Rubro = ObtenerRubros()
                   .Find(r=>r.Id==Convert.ToByte(fila["idRubro"]))
        };
        public List<Ticket> ObtenerTickets()
        {
            throw new NotImplementedException();
        }
    }
}
