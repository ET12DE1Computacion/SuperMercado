using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuperMercado;
using SuperMercado.ADO;
using System.Collections.Generic;

namespace TestAdoMySQLCore
{
    [TestClass]
    public class TestPersistencia
    {
        static Categoria Gaseosa { get; set; }
        static Categoria Cereal{ get; set; }
        static Producto Cola { get; set; }
        static Producto Sprite { get; set; }
        static AdoMySQLEntityCore Ado { get; set; }

        [ClassInitialize]
        public static void iniciarClase(TestContext context)
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
            Ado = new AdoMySQLEntityCore();
            Ado.Database.EnsureCreated();
            agregarObjetosAPersistir();
        }

        private static void agregarObjetosAPersistir()
        {
            Ado.agregarCategoria(Gaseosa);
            Ado.agregarCategoria(Cereal);
            Ado.agregarProducto(Cola);
            Ado.agregarProducto(Sprite);
        }

        [TestMethod]
        public void persistenciaCategorias()
        {
            List<Categoria> categorias = Ado.obtenerCategorias();
            Assert.IsTrue(categorias.Contains(Gaseosa));
            Assert.IsTrue(categorias.Contains(Cereal));
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
            Assert.AreSame(manaos2, manaos);
            Assert.AreEqual(80, manaos.PrecioUnitario, 0.01);
            Assert.AreEqual(2, manaos.HistorialPrecios.Count);
            Assert.AreEqual(45, manaos.HistorialPrecios[0].PrecioUnitario, 0.01);
            Assert.AreEqual(80, manaos.HistorialPrecios[1].PrecioUnitario, 0.01);
        }
    }
}
