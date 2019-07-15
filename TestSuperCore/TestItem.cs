using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuperMercado;

namespace TestSuperCore
{
    [TestClass]
    public class TestItem
    {
        //Producto que se va a usar a traves de todo el test
        static Producto CocaCola { get; set; }
        Item Item { get; set; }

        //Metodo que se invocara siempre automaticamente
        //al comienzo de la bateria de pruebas
        [ClassInitialize]
        public static void iniciarCoca(TestContext context)
        {
            CocaCola = new Producto(100);
            CocaCola.Nombre = "Coca Cola";            
        }


        //Metodo que se invoca solo, antes de cada prueba
        [TestInitialize]
        public void iniciarItem()
        {
            CocaCola.Cantidad = 50;
            Item = new Item(CocaCola, 2);
        }


        [TestMethod]
        public void testTotalItem()
        {
            Assert.AreEqual(200f, Item.TotalItem, 0.01);
        }

        [TestMethod]
        public void testDecrementoProducto()
        {
            Item.decrementarProducto();
            Assert.AreEqual(48, CocaCola.Cantidad);
        }
    }
}