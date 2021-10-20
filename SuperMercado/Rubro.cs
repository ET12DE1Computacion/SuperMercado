namespace SuperMercado
{
    public class Rubro
    {
        public byte Id { get; set; }
        public string Nombre { get; set; }
        public Rubro() { }
        public Rubro(string nombre) => Nombre = nombre;
    }
}