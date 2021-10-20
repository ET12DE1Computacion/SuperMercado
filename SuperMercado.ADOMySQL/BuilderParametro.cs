using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace SuperMercado.ADO.MySQL
{
    public class BuilderParametro
    {
        public MySqlParameter Parametro { get; private set; }

        public BuilderParametro CrearParametro()
        {
            Parametro = new MySqlParameter()
            {
                Direction = ParameterDirection.Input,
                Value = DBNull.Value
            };
            return this;
        }
        public BuilderParametro CrearParametro(string nombre)
        {
            CrearParametro();
            return SetNombre(nombre);
        }
        public BuilderParametro SetNombre(string nombre)
        {
            Parametro.ParameterName = nombre;
            return this;
        }
        public BuilderParametro SetDireccion(ParameterDirection direccion)
        {
            Parametro.Direction = direccion;
            return this;
        }
        public BuilderParametro SetValor(object valor)
        {
            Parametro.Value = valor ?? DBNull.Value;
            return this;
        }
        public BuilderParametro SetTipo(MySqlDbType tipo)
        {
            Parametro.MySqlDbType = tipo;
            return this;
        }
        public BuilderParametro SetTipoDecimal(byte precision, byte escala)
        {
            Parametro.Precision = precision;
            Parametro.Scale = escala;
            return SetTipo(MySqlDbType.Decimal);
        }
        public BuilderParametro SetTipoVarchar(int longitud)
        {
            Parametro.Size = longitud;
            return SetTipo(MySqlDbType.VarChar);
        }
        public BuilderParametro SetTipoChar(int longitud)
        {
            Parametro.Size = longitud;
            return SetTipo(MySqlDbType.String);
        }
        public BuilderParametro SetLongitud(int longitud)
        {
            Parametro.Size = longitud;
            return this;
        }
    }
}
