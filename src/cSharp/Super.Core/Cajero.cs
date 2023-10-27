namespace Super.Core;
public class Cajero
{
    public required uint Dni { get; set; }
    public required string Nombre { get; set; }
    public required string Apellido { get; set; }
    public string NombreCompleto
        => string.Format("{0}, {1}", Apellido, Nombre);
}
