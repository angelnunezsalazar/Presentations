namespace OnlineStore.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using OnlineStore.Domain;

    [TestClass]
    public class ManagerTests
    {
        [TestMethod]
        public void CalculateTheNetSalaryWhenFixedSalaryIsUnderTheMinimumTax()
        {
            int fixedSalary = 3000;
            Manager manager = this.CreateManager(fixedSalary);

            Assert.AreEqual(2700, manager.SalaryAfterAdditionsAndDeductions());
        }

        [TestMethod]
        public void CalculateTheNetSalaryWhenFixedSalaryIsOverTheMinimumTax()
        {
            int fixedSalary = 5000;
            Manager manager = this.CreateManager(fixedSalary);

            Assert.AreEqual(4250, manager.SalaryAfterAdditionsAndDeductions());
        }

        [TestMethod]
        public void CalculateTheNetSalaryWhenBenefits()
        {
            int fixedSalary = 3000;
            Manager manager = this.CreateManager(fixedSalary);
            manager.AddSubordinate(this.CreateSubordinate());

            Assert.AreEqual(2720, manager.SalaryAfterAdditionsAndDeductions());
        }

        private Manager CreateManager(int fixedSalary)
        {
            return new Manager("Carlos", "Rodriguez", fixedSalary);
        }

        private Salesman CreateSubordinate()
        {
            return new Salesman("Miguel", "Gonzales", 2000, 2);
        }
    }
}