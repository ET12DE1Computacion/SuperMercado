namespace Super.Core.Product;
public abstract class PosesionProducto
{
    public short IdProducto { get; set; }
    public DateTime FechaHora { get; set; }
    public PosesionProducto() => FechaHora = DateTime.Now;
    
    
    public PosesionProducto(short idProducto) : this()
        => IdProducto = idProducto;
}
