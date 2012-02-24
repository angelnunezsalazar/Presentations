namespace OnlineStore.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using OnlineStore.Domain;

    [TestClass]
    public class ProductTests
    {
        [TestMethod]
        public void ProductImageReturnTheType()
        {
            ImageInfo imageInfo = new ImageInfo("Bike01.jpg");

            string type = imageInfo.ImageType;

            Assert.AreEqual("jpg", type);
        }

        [TestMethod]
        public void SerializeToXml()
        {
            Product product = this.CreateProduct();

            string xml = product.ToXml();

            Assert.AreEqual("<product><name>Black Bike</name><category>Bikes</category></product>", xml);
        }

        private Product CreateProduct()
        {
            return new Product("Black Bike", 250, ProductCategory.Bikes, new ImageInfo("Bike01.jpg"));
        }
    }
}