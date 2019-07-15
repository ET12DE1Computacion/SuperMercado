using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuperMercado;
using SuperMercado.ADO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestAdoMySQLCore
{
    [TestClass]
    public class TestPersistencia
    {
        static Categoria Gaseosa { get; set; }
        static Categoria Cereal{ get; set; }
        static Producto Cola { get; set; }
        static Producto Sprite { get; set; }
        static Producto CerealTrix { get; set; }
        static AdoMySQLEntityCore Ado { get; set; }
        static Ticket TicketGaseosas { get; set; }
        static Ticket TicketMix { get; set; }

        [ClassInitialize]
        public static void iniciarClase(TestContext context)
        {
            intanciarPropiedadesEstaticas();
            Ado = new AdoMySQLEntityCore();
            Ado.Database.EnsureDeleted();
            Ado.Database.EnsureCreated();
            agregarObjetosAPersistir();
        }

        private static void intanciarPropiedadesEstaticas()
        {
            Gaseosa = new Categoria("Gaseosa");
            Cereal = new Categoria("Cereal");
            Cola = new Producto(45)
            {
                Nombre = "Manaos Cola 2.25L",
                Cantidad = 100,
                Categoria = Gaseosa
            };
            Sprite = new Producto(120)
            {
                Nombre = "Sprite 2.25L",
                Cantidad = 20,
                Categoria = Gaseosa
            };
            CerealTrix = new Producto(75)
            {
                Nombre = "Cereal Trix 500gr.",
                Cantidad = 100,
                Categoria = Cereal
            };

            TicketGaseosas = new Ticket();
            TicketGaseosas.FechaHora = new DateTime(2019, 1, 1, 12, 0, 0);
            TicketGaseosas.agregarProducto(Cola, 2);
            TicketGaseosas.agregarProducto(Cola, 1);
            TicketGaseosas.agregarProducto(Sprite, 3);

            TicketMix = new Ticket();
            TicketMix.FechaHora = new DateTime(2019, 6, 15, 18, 0, 0);
            TicketMix.agregarProducto(CerealTrix, 5);
            TicketMix.agregarProducto(Sprite, 2);
        }

        private static void agregarObjetosAPersistir()
        {
            Ado.agregarCategoria(Gaseosa);
            Ado.agregarCategoria(Cereal);
            Ado.agregarProducto(Cola);
            Ado.agregarProducto(Sprite);
            Ado.agregarTicket(TicketGaseosas);
            Ado.agregarTicket(TicketMix);
        }

        [TestInitialize]
        public void instanciarAdo()
        {
            Ado = new AdoMySQLEntityCore();
        }

        [TestMethod]
        public void persistenciaCategorias()
        {
            List<Categoria> categorias = Ado.obtenerCategorias();
            Assert.IsTrue(categorias.Any(c=>c.Nombre=="Gaseosa"));
            Assert.IsTrue(categorias.Any(c=>c.Nombre=="Cereal"));
        }

        [TestMethod]
        public void actualizarProducto()
        {
            Producto manaos = Ado.obtenerProductos()
                                 .Find(p => p.Nombre == "Manaos Cola 2.25L");
            manaos.cambiarPrecioUnitario(80);
            Ado.actualizarProducto(manaos);
            Producto manaos2 = Ado.obtenerProductos()
                                 .Find(p => p.Nombre == "Manaos Cola 2.25L");
            var historialesManaos = Ado.historialDe(manaos);
            Assert.AreSame(manaos2, manaos);
            Assert.AreEqual(80, manaos.PrecioUnitario, 0.01);
            Assert.AreEqual(2, historialesManaos.Count);
            Assert.AreEqual(45, historialesManaos[0].PrecioUnitario, 0.01);
            Assert.AreEqual(80, historialesManaos[1].PrecioUnitario, 0.01);
        }

        [TestMethod]
        public void persistirTicket()
        {
            DateTime primerEnero19 = new DateTime(2019, 1, 1, 12, 0, 0);
            Ticket ticketGaseosa = Ado.obtenerTickets()
                                      .Find(t => t.FechaHora.Equals(primerEnero19));
            Assert.IsNotNull(ticketGaseosa);
            List<Item> itemsTicketGaseosa = Ado.itemsDe(ticketGaseosa);
            Producto manaos = Ado.obtenerProductos()
                                 .Find(p => p.Nombre == "Manaos Cola 2.25L");
            Assert.AreEqual(2, itemsTicketGaseosa.Count);
            Assert.IsTrue(itemsTicketGaseosa.Any(i => i.Producto == manaos));
        }

        [TestMethod]
        public void confirmarTicketPersistido()
        {
            DateTime quinceJunio19 = new DateTime(2019, 6, 15, 18, 0, 0);
            Ticket ticketMix = Ado.obtenerTickets()
                                  .Find(t => t.FechaHora.Equals(quinceJunio19));
            Assert.IsNotNull(ticketMix);
            ticketMix.Items = Ado.itemsDe(ticketMix);
            Assert.IsFalse(ticketMix.Confirmado);
            ticketMix.confirmar();
            Ado.actualizarTicket(ticketMix);
        }
    }
}