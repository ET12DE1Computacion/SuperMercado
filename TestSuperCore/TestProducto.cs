using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuperMercado;
using System;
using System.Collections.Generic;

namespace TestSuperCore
{
    [TestClass]
    public class TestProducto
    {
        Categoria Gaseosa { get; set; }
        Producto CocaCola { get; set; }
        
        [TestInitialize]
        public void setup()
        {
            Gaseosa = new Categoria() { Nombre = "Gaseosa" };
            HistorialPrecio h1 = new HistorialPrecio()
            {
                PrecioUnitario = 100,
                FechaHora = new DateTime(2019, 06, 26)
            };
            HistorialPrecio h2 = new HistorialPrecio()
            {
                PrecioUnitario = 150,
                FechaHora = new DateTime(2019, 06, 27)
            };
            CocaCola = new Producto();
            CocaCola.Nombre = "Coca Cola 2.25L";
            CocaCola.Categoria = Gaseosa;
            CocaCola.Cantidad = 200;
            CocaCola.PrecioUnitario = h2.PrecioUnitario;
            CocaCola.HistorialPrecios = new List<HistorialPrecio>() { h1, h2 };
        }
        [TestMethod]
        public void DecrementarCantidadProducto()
        {
            CocaCola.decrementarCantidad(5);
            Assert.AreEqual(195, CocaCola.Cantidad);
        }

        [TestMethod]
        public void ProductoCambiarPrecio()
        {
            Assert.AreEqual(2, CocaCola.HistorialPrecios.Count);    
            CocaCola.cambiarPrecioUnitario(175F);
            Assert.AreEqual(3, CocaCola.HistorialPrecios.Count);
            Assert.AreEqual(175F, CocaCola.PrecioUnitario, 0.01);            
        }

        [TestMethod]
        public void ProductoPrecioPromedioEntre()
        {
            DateTime inicio = new DateTime(2019, 06, 25);
            DateTime fin = new DateTime(2019, 06, 28);
            Assert.AreEqual(125f, CocaCola.precioPromedioEntre(inicio, fin), 0.01);
        }
    }
}