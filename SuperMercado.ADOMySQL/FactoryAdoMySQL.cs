using Microsoft.Extensions.Configuration;

namespace SuperMercado.ADO.MySQL
{
    public static class FactoryAdoMySQL
    {
        public static AdoMySQL GetAdoDesdeJson(string archivo, string atributoJson)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile(archivo, optional: true, reloadOnChange: true)
                .Build();
            string cadena = config.GetConnectionString(atributoJson);
            return new AdoMySQL(cadena);            
        }
    }
}