using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TiendaVirtual.UnitTests.Domain
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using TiendaVirtual.Domain;
    [TestClass]
    public class CarroComprasTests
    {
        [TestMethod]
        public void AgregarLinea_ProductoNoExiste_LineaAgregada()
        {
            //Arrange
            var carroCompras = new CarroCompras();

            //Act
            carroCompras.AgregarLinea(new Producto());

            //Assert
            Assert.AreEqual(1, carroCompras.CantidadProductos);
        }

        [TestMethod]
        public void AgregarLinea_ProductoYaExiste_IncrementaLaCantidadDeLaLinea()
        {
            var carroCompras = new CarroCompras();
            carroCompras.AgregarLinea(new Producto { Id = 1 });

            carroCompras.AgregarLinea(new Producto { Id = 1 });

            Assert.AreEqual(2, carroCompras.CantidadProductos);
        }

        [TestMethod]
        public void ActualizarLinea_ProductoExiste_CantidadIncrementada()
        {
            var carroCompras = new CarroCompras();
            carroCompras.AgregarLinea(new Producto { Id = 1 });

            carroCompras.ActualizarLinea(1, 3);

            Assert.AreEqual(3, carroCompras.CantidadProductos);
        }

        [TestMethod]
        public void ActualizarLinea_CantidadCero_RemueveLaLinea()
        {
            var carroCompras = new CarroCompras();
            carroCompras.AgregarLinea(new Producto { Id = 1 });

            carroCompras.ActualizarLinea(1, 0);

            Assert.AreEqual(0, carroCompras.CantidadProductos);
        }

        [TestMethod]
        public void RemoverLinea_ProductoExiste_RemueveLaLinea()
        {
            //Arrange
            var carroCompras = new CarroCompras();
            carroCompras.AgregarLinea(new Producto { Id = 1 });

            //Act
            carroCompras.RemoverLinea(1);

            //Assert
            Assert.AreEqual(0, carroCompras.CantidadProductos);
        }

        [TestMethod]
        public void RemoverLinea_ProductoNoExiste_LanzarException()
        {
            
        }


    }
}
