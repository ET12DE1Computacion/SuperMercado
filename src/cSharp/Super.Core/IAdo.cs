namespace Super.Core;

public interface IAdo
{
    void AltaCategoria(Categoria categoria);
    List<Categoria> ObtenerCategorias();
    void AltaProducto(Producto producto);
    List<Producto> ObtenerProductos();
    Producto? ObtenerProducto(short id);
    void AltaCajero(Cajero cajero, string pass);
    Cajero? CajeroPorPass(uint dni, string pass);
    void AltaTicket (Ticket ticket);
    Ticket? ObtenerTicket(int id);
}
