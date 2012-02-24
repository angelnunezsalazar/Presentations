namespace OnlineStore.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using OnlineStore.Domain;

    [TestClass]
    public class SalesmanTests
    {
        [TestMethod]
        public void CalculateTheNetSalaryWhenFixedSalaryIsUnderTheMinimumTax()
        {
            int fixedSalary = 3000;
            Salesman salesman = this.CreateSalesman(fixedSalary);

            Assert.AreEqual(2700, salesman.NetSalary());
        }

        [TestMethod]
        public void CalculateTheNetSalaryWhenFixedSalaryIsOverTheMinimumTax()
        {
            int fixedSalary = 5000;
            Salesman salesman = this.CreateSalesman(fixedSalary);

            Assert.AreEqual(4250, salesman.NetSalary());
        }

        [TestMethod]
        public void CalculateTheNetSalaryWhenHaveMonthQuota()
        {
            int fixedSalary = 3000;
            Salesman salesman = this.CreateSalesman(fixedSalary);
            salesman.UpdateMonthQuota(1000);

            Assert.AreEqual(2800, salesman.NetSalary());
        }

        private Salesman CreateSalesman(int fixedSalary)
        {
            return new Salesman("Carlos", "Rodriguez", fixedSalary, 10);
        }
    }
}