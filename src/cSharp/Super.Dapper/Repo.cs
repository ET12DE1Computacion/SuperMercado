using System.Data;
namespace Super.Dapper;

public abstract class Repo
{
    protected readonly IDbConnection _conexion;

    public Repo(IDbConnection conexion) => _conexion = conexion;
}
