using SuperMercado.ADO;
using et12.edu.ar.AGBD.Ado;

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
            FactoryAdoAGBD.GetAdoMySQL("appsettings.json", "gerente");
    }
}