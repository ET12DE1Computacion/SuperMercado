using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuperMercado;
using System;
using System.Collections.Generic;
using SuperMercado.Product;

namespace TestSuperCore
{
    [TestClass]
    public class TestProducto
    {
        Rubro Gaseosa { get; set; }
        Producto CocaCola { get; set; }
        
        [TestInitialize]
        public void setup()
        {
            Gaseosa = new Rubro() { Nombre = "Gaseosa" };
            
            CocaCola = new Producto();
            CocaCola.Nombre = "Coca Cola 2.25L";
            CocaCola.Rubro = Gaseosa;
            CocaCola.Cantidad = 200;
            //CocaCola.PrecioUnitario = h2.PrecioUnitario;

            var h1 = new HistorialPrecio
                (Producto: CocaCola, new DateTime(2019, 06, 26), PrecioUnitario: 100);

            var h2 = new HistorialPrecio
                (CocaCola, new DateTime(2019, 06, 27), 150);
            
            CocaCola.HistorialPrecios = new List<HistorialPrecio>() { h1, h2 };
        }
        [TestMethod]
        public void DecrementarCantidadProducto()
        {
            CocaCola.DecrementarCantidad(5);
            Assert.AreEqual(195, CocaCola.Cantidad);
        }

        [TestMethod]
        public void ProductoPrecioPromedioEntre()
        {
            DateTime inicio = new DateTime(2019, 06, 25);
            DateTime fin = new DateTime(2019, 06, 28);
            Assert.AreEqual(125f, CocaCola.PrecioPromedioEntre(inicio, fin), 0.01);
        }
    }
}