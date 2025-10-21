namespace Super.Core.Repos;
public interface IRepoTicket
{
    void AltaTicket (Ticket ticket);
    Ticket? ObtenerTicket(int id);
}
