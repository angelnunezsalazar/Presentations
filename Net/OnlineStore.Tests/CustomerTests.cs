namespace OnlineStore.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using OnlineStore.Domain;

    [TestClass]
    public class CustomerTests
    {
        [TestMethod]
        public void FormatThePhoneNumber()
        {
            Customer customer = new Customer("Alberto", "Paez", "54115678654");

            string formattedPhone = customer.FormatPhone();

            Assert.AreEqual("CountryCode:54 - Citycode:11 - LocalNumber:5678654", formattedPhone);
        }
    }
}