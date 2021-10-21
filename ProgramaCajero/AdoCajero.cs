using SuperMercado.ADO;
using SuperMercado.ADO.MySQL;

namespace ProgramaCajero
{
    public static class AdoCajero
    {
        public static IADO ADO { get; set; } =
            FactoryAdoMySQL.GetAdoDesdeJson("appsettings.json", "cajero");
    }
}
