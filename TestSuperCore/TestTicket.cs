using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuperMercado;
using System;

namespace TestSuperCore
{
    [TestClass]
    public class TestTicket
    {
        static Categoria Gaseosa { get; set; }
        static Producto CocaCola { get; set; }
        static Producto CunningtonPome { get; set; }
        static Item ItemCoca { get; set; }
        static Item ItemPomelo { get; set; }
        Ticket Ticket { get; set; }

        [ClassInitialize]
        public static void fixture(TestContext context)
        {
            Gaseosa = new Categoria() { Nombre = "Gaseosa " };
            CocaCola = new Producto(100)
            {
                Categoria = Gaseosa,
                Nombre = "Coca Cola 2.25 L."
            };
            CunningtonPome = new Producto(40)
            {
                Categoria = Gaseosa,
                Nombre = "Cunnignton Pomelo 2.25 L."
            };
            ItemCoca = new Item(CocaCola, 5);
            ItemPomelo = new Item(CunningtonPome, 2);
        }

        [TestInitialize]
        public void setupTicket()
        {
            CocaCola.Cantidad = 20;
            CunningtonPome.Cantidad = 10;
            Ticket = new Ticket();
            Ticket.FechaHora = new DateTime(2019, 06, 15);
            Ticket.agregartItem(ItemCoca);
            Ticket.agregartItem(ItemPomelo);
        }

        [TestMethod]
        public void TestCantidadItems()
        {
            Assert.AreEqual(2, Ticket.Items.Count);
        }

        [TestMethod]
        public void TestDecrementoPostConfirmar()
        {
            Assert.IsFalse(Ticket.Confirmado);
            Assert.AreEqual(20, CocaCola.Cantidad);
            Assert.AreEqual(10, CunningtonPome.Cantidad);
            Ticket.confirmar();
            Assert.IsTrue(Ticket.Confirmado);
            Assert.AreEqual(15, CocaCola.Cantidad);
            Assert.AreEqual(8, CunningtonPome.Cantidad);
            Ticket.confirmar();
            Assert.AreEqual(15, CocaCola.Cantidad);
            Assert.AreEqual(8, CunningtonPome.Cantidad);
        }

        [TestMethod]
        public void TestTotalTicket()
        {
            Assert.AreEqual(580, Ticket.TotalTicket, 0.01);
        }
    }
}