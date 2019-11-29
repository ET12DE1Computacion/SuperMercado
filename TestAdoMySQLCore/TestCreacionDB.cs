using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuperMercado.ADO;

namespace TestAdoMySQLCore
{
    [TestClass]
    public class TestCreacionDB
    {
        public static AdoMySQLEntityCore AdoMySQL { get; set; }
        
        [ClassInitialize]
        public static void SetUpClase(TestContext context)
        {
            //AdoMySQL = new AdoMySQLEntityCore();
            AdoMySQL = FactoryAdoMySQL.GetAdoDesdeJson("appsettings.json", "root");
            AdoMySQL.Database.EnsureDeleted();
        }

        [TestMethod]
        public void SeCreaDB()
        {
            AdoMySQL.Database.EnsureCreated();           
        }
    }
}