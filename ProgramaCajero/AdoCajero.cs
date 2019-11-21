using SuperMercado.ADO;

namespace ProgramaCajero
{
    public static class AdoCajero
    {
        public static IADO ADO { get; set; } =
            FactoryAdoMySQL.GetAdoDesdeJson("appsettings.json", "cajero");
    }
}
