using SuperMercado.ADO;

namespace ProgramaGerente
{
    public static class AdoGerente
    {
        public static IADO ADO { get; set; } =
            FactoryAdoMySQL.GetAdoDesdeJson("appsettings.json", "gerente");
    }
}