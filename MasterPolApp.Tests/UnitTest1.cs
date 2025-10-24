using Microsoft.VisualStudio.TestTools.UnitTesting;
using MasterPolApp;
using System;

namespace MasterPolApp.Tests
{
    [TestClass]
    public class PartnerTests
    {
        // Позитивные тесты
        [TestMethod]
        public void CreatePartner_ValidData_ShouldPass()
        {
            // Arrange
            PartnerForm partnerForm = new PartnerForm();
            string name = "ООО Ромашка";
            string inn = "1234567890";

            // Act
            bool result = partnerForm.ValidateInputForTest(name, inn, "test@mail.com", "1234567890");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CalculateDiscount_TotalSalesAbove300000_ShouldReturn15()
        {
            // Arrange
            decimal totalSales = 350000;

            // Act
            decimal discount = DiscountCalculator.Calculate(totalSales);

            // Assert
            Assert.AreEqual(15, discount);
        }

        [TestMethod]
        public void CalculateDiscount_TotalSalesBetween50000And300000_ShouldReturn10()
        {
            decimal totalSales = 100000;
            decimal discount = DiscountCalculator.Calculate(totalSales);
            Assert.AreEqual(10, discount);
        }

        // Негативные тесты
        [TestMethod]
        public void CreatePartner_InvalidINN_ShouldFail()
        {
            PartnerForm partnerForm = new PartnerForm();
            string name = "ООО Лилия";
            string inn = "123";

            bool result = partnerForm.ValidateInputForTest(name, inn, "test@mail.com", "1234567890");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CalculateDiscount_TotalSalesBelow10000_ShouldReturn0()
        {
            decimal totalSales = 5000;
            decimal discount = DiscountCalculator.Calculate(totalSales);
            Assert.AreEqual(0, discount);
        }

        [TestMethod]
        public void CreatePartner_EmptyName_ShouldFail()
        {
            PartnerForm partnerForm = new PartnerForm();
            bool result = partnerForm.ValidateInputForTest("", "1234567890", "test@mail.com", "1234567890");
            Assert.IsFalse(result);
        }
    }

    // Вспомогательный статический класс для расчета скидки
    public static class DiscountCalculator
    {
        public static decimal Calculate(decimal totalSales)
        {
            if (totalSales > 300000) return 15;
            if (totalSales > 50000) return 10;
            if (totalSales > 10000) return 5;
            return 0;
        }
    }
}
