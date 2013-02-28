using System;
using ClassLibrary;
using Moq;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class OrderServicesTests
    {
        [Test]
        public void NoTieneDescuentoCuandoElMontoEsMenorAMil()
        {
            OrderServices orderServices = new OrderServices(new MyDummyBd());

            var discount = orderServices.CalculateDiscount(900, CustomerCategory.Good);

            Assert.AreEqual(0, discount);
        }

        [Test]
        public void TieneDescuentoSiElMontoEsMayorAMil()
        {
            var stub = new Mock<IDiscountDB>();
            stub.Setup(x => x.GetDiscount(CustomerCategory.Good)).Returns(10);

            OrderServices orderServices = new OrderServices(stub.Object);

            var discount = orderServices.CalculateDiscount(1900, CustomerCategory.Good);

            Assert.AreEqual(190, discount);
        }

        [Test]
        public void AlmacenaLaOrdenSiEsElTotalEsMayorACero()
        {
            var mock = new Mock<IDiscountDB>();

            OrderServices orderServices = new OrderServices(mock.Object);
            Order order = new Order { Id = 1234, Total = 100 };

            orderServices.Save(order);

            mock.Verify(x=>x.Save(order));

        }


    }

    public class MyDummyBd : IDiscountDB
    {
        public int GetDiscount(CustomerCategory customerCategory)
        {
            return 10;
        }

        public void Save(Order order)
        {
            throw new NotImplementedException();
        }

        public Order GetOrderById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
