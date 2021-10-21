using SuperMercado.ADO;
using SuperMercado.ADO.MySQL;

namespace ProgramaGerente
{
    /// <summary>
    /// Clase estatica que proporcionado el ADO seteado para un gerente
    /// </summary>
    public static class AdoGerente
    {
        /// <summary>
        /// Propiedad para acceso al ADO
        /// </summary>
        public static IADO ADO { get; set; } =
            FactoryAdoMySQL.GetAdoDesdeJson("appsettings.json", "gerente");
    }
}