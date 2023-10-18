using System.Data;
using Dapper;
using MySqlConnector;
using Super.Core;

namespace Super.Dapper;
public class AdoDapper : IAdo
{
    private readonly IDbConnection _conexion;
    private readonly string _queryCategorias
        = "SELECT idRubro AS id, rubro AS nombre FROM Rubro";
    private readonly string _queryProductos
        = @"SELECT  idProducto AS 'Producto.id', nombre, precioUnitario, cantidad, Producto.idRubro AS 'Categoria.id', rubro AS 'Categoria.nombre'
            FROM    Producto
            JOIN    Rubro USING (idRubro)";

    public AdoDapper(IDbConnection conexion) => this._conexion = conexion;
    
    //Este constructor usa por defecto la cadena para un conector MySQL
    public AdoDapper(string cadena) => _conexion = new MySqlConnection(cadena);

    public void AltaCategoria(Categoria categoria)
    {
        //Preparo los parametros
        var parametros = new DynamicParameters();
        parametros.Add("@unIdRubro", direction: ParameterDirection.Output);
        parametros.Add("@unRubro", categoria.Nombre);

        _conexion.Execute("altaRubro", parametros);

        //Obtengo el valor de parametro de tipo salida
        categoria.Id = parametros.Get<byte>("@unIdRubro");
    }

    public void AltaProducto(Producto producto)
    {
        throw new NotImplementedException();
    }

    public List<Categoria> ObtenerCategorias()
        => _conexion.Query<Categoria>(_queryCategorias).ToList();

    public Producto? ObtenerProducto(short id)
    {
        throw new NotImplementedException();
    }

    public List<Producto> ObtenerProductos()
    {
        return _conexion.Query<Producto>(_queryProductos).ToList();
    }
}
