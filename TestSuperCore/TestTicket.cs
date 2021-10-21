using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuperMercado;
using System;

namespace TestSuperCore
{
    [TestClass]
    public class TestTicket
    {
        static Rubro Gaseosa { get; set; }
        static Producto CocaCola { get; set; }
        static Producto CunningtonPome { get; set; }
        static Item ItemCoca { get; set; }
        static Item ItemPomelo { get; set; }
        Ticket Ticket { get; set; }

        [ClassInitialize]
        public static void fixture(TestContext context)
        {
            Gaseosa = new Rubro() { Nombre = "Gaseosa " };
            CocaCola = new Producto()
            {
                Rubro = Gaseosa,
                Nombre = "Coca Cola 2.25 L.",
                PrecioUnitario = 100

            };
            CunningtonPome = new Producto()
            {
                Rubro = Gaseosa,
                Nombre = "Cunnignton Pomelo 2.25 L.",
                PrecioUnitario = 40
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
            Ticket.AgregartItem(ItemCoca);
            Ticket.AgregartItem(ItemPomelo);
        }

        [TestMethod]
        public void TestCantidadItems()
        {
            Assert.AreEqual(2, Ticket.Items.Count);
        }

        [TestMethod]
        public void TestTotalTicket()
        {
            Assert.AreEqual(580, Ticket.TotalTicket);
        }
    }
}