using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace SuperMercado.ADO
{
    public static class FactoryAdoMySQL
    {
        public static AdoMySQLEntityCore GetAdo(string cadena)
        {
            var dbContextOptions = new DbContextOptionsBuilder();
            dbContextOptions.UseMySQL(cadena);            
            return new AdoMySQLEntityCore(dbContextOptions.Options);
        }

        public static AdoMySQLEntityCore GetAdoDesdeJson(string archivo, string atributoJson)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile(archivo, optional: true, reloadOnChange: true)
                .Build();
            string cadena = config.GetConnectionString(atributoJson);
            return GetAdo(cadena);            
        }
    }
}