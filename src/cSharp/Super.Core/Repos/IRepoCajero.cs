namespace Super.Core.Repos;

public interface IRepoCajero
{
    void AltaCajero(Cajero cajero, string pass);
    Cajero? CajeroPorPass(uint dni, string pass);
    // Tu repositorio Dapper
    Task<Cajero?> GetByUserIdAsync(uint dni);
}
