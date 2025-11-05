using System.Data;
using Dapper;
using Super.Core;
using Super.Core.Repos;

namespace Super.Dapper;

public class RepoCajero : Repo, IRepoCajero
{
    //Creo el constructor que recibe la conexion y la asigna a traves del constructor base.
    public RepoCajero(IDbConnection conexion) : base(conexion) { }

    private static readonly string _queryCajeroPass
        = @"SELECT  *
            FROM    Cajero
            WHERE   dni = @unDni
            AND     pass = SHA2(@unaPass, 256)
            LIMIT   1";
    private static readonly string _queryAltaCajero
        = @"INSERT INTO Cajero VALUES (@dni, @nombre, @apellido, @IdentityUserId)";
    public void AltaCajero(Cajero cajero, string userId)
    //Aca podría verificar que el cajero se haya insertado correctamente
        => _conexion.Execute(
                _queryAltaCajero,
                new
                {
                    dni = cajero.Dni,
                    nombre = cajero.Nombre,
                    apellido = cajero.Apellido,
                    IdentityUserId = userId
                }
            );
    public Cajero? CajeroPorPass(uint dni, string pass)
    //En caso de que exista un cajero, lo devuelve instanciado, caso contrario devuelve NULL.
        => _conexion.QueryFirstOrDefault<Cajero>(
            _queryCajeroPass,
            new { unDni = dni, unaPass = pass }
            );
    private static readonly string _queryCajeroPorUserId
        = @"SELECT  dni, nombre, apellido
            FROM    Cajero
            WHERE   IdentityUserId = @userId
            LIMIT   1";
    public async Task<Cajero?> GetByUserIdAsync(string userId)
    {
        var cajero = await _conexion.QuerySingleOrDefaultAsync<Cajero>(_queryCajeroPorUserId, new { UserId = userId });
        return cajero;
    }

    public async Task<Cajero?> GetByUserIdAsync(uint dni)
    {
        throw new NotImplementedException();
    }
}