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

        [TestMethod]
        public void persistenciaCajero()
        {
            Ado = new AdoMySQLEntityCore();
            string passEncriptada = EncryptProvider.Sha256("123456");
            string otraPass = EncryptProvider.Sha256("2354567");
            int dni = 1000000;
            int otroDni = 999;

            Cajero cajero = new Cajero()
            {
                Dni = dni,
                Nombre = "Juan",
                Apellido = "Gomez",
                Password = passEncriptada
            };
            Ado.altaCajero(cajero);

            Ado = new AdoMySQLEntityCore();

            Cajero cajero2 = Ado.cajeroPorDniPass(dni, otraPass);
            Assert.IsNull(cajero2);

            Cajero cajero3 = Ado.cajeroPorDniPass(otroDni, passEncriptada);
            Assert.IsNull(cajero3);

            Cajero cajero4 = Ado.cajeroPorDniPass(otroDni, otraPass);
            Assert.IsNull(cajero4);

            Cajero cajero5 = Ado.cajeroPorDniPass(dni, passEncriptada);
            Assert.IsNotNull(cajero5);
            Assert.AreEqual("Gomez, Juan", cajero5.NombreCompleto);
        }
    }
}