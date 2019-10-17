using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using SuperMercado.ADO;
using SuperMercado;
using NETCore.Encrypt;

namespace TestAdoMySQLCore
{
    [TestClass]
    public class TestCajero
    {
        private IADO Ado { get; set; }

        [ClassInitialize]
        public static void fixture(TestContext context)
        {
            var ado = new AdoMySQLEntityCore();
            ado.Database.EnsureDeleted();
            ado.Database.EnsureCreated();
        }

        [TestInitialize]
        public void setAdo()
        {
            Ado = new AdoMySQLEntityCore();
        }

        [TestMethod]
        public void altaCajero()
        {
            Cajero cajero = new Cajero()
            {
                Dni = 1000000,
                Nombre = "Pepe",
                Apellido = "Lotas de Humo",
                Password = EncryptProvider.Sha256("Argento")
            };
            Ado.altaCajero(cajero);
        }
    }
}
