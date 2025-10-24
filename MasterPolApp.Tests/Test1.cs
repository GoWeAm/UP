using Microsoft.VisualStudio.TestTools.UnitTesting;
using MasterPolApp;

namespace MasterPolApp.Tests
{
    [TestClass]
    public class PartnerTests
    {
        // ---------------- PartnerForm Tests ----------------
        [TestMethod]
        public void AddNewPartner_ValidData_ShouldReturnTrue()
        {
            var form = new PartnerForm();
            form.SetPartnerData(
                "ООО Вест",
                "111111111111",
                "+79999999999",
                "ул. Вест, д. 21",
                "Вестиков А.А.",
                "info10@vest.ru",
                5
            );

            bool result = form.ValidateInput();
            Assert.IsTrue(result, "Должен успешно валидироваться корректный партнер");
        }

        [TestMethod]
        public void AddPartner_EmptyName_ShouldReturnFalse()
        {
            var form = new PartnerForm();
            form.SetPartnerData(
                "",
                "1234567890",
                "+79999999999",
                "ул. Тест, д.1",
                "Тестов Т.Т.",
                "test@mail.com",
                5
            );

            bool result = form.ValidateInput();
            Assert.IsFalse(result, "Проверка на пустое имя должна вернуть false");
        }

        [TestMethod]
        public void AddPartner_InvalidEmail_ShouldReturnFalse()
        {
            var form = new PartnerForm();
            form.SetPartnerData(
                "ООО Тест",
                "1234567890",
                "+79999999999",
                "ул. Тест, д.1",
                "Тестов Т.Т.",
                "test@@mail",
                5
            );

            bool result = form.ValidateInput();
            Assert.IsFalse(result, "Некорректный email должен не проходить валидацию");
        }

        // ---------------- PartnerOrderForm Tests ----------------
        [TestMethod]
        public void CreateOrder_ValidQuantity_ShouldReturnTrue()
        {
            var orderForm = new PartnerOrderForm(1, "ООО Вест");
            int validQuantity = 10;

            bool result = orderForm.ValidateQuantity(validQuantity);
            Assert.IsTrue(result, "Количество > 0 должно быть валидным");
        }

        [TestMethod]
        public void CreateOrder_NegativeQuantity_ShouldReturnFalse()
        {
            var orderForm = new PartnerOrderForm(1, "ООО Вест");
            int invalidQuantity = -5;

            bool result = orderForm.ValidateQuantity(invalidQuantity);
            Assert.IsFalse(result, "Отрицательное количество не должно проходить валидацию");
        }

        [TestMethod]
        public void CalculateDiscount_PartnerSales2000_ShouldReturn0()
        {
            decimal totalSales = 2000;
            decimal discount = PartnerOrderForm.CalculateDiscountBySales(totalSales);

            Assert.AreEqual(0, discount, "Продажи 2000 → скидка 0%");
        }

        [TestMethod]
        public void CalculateDiscount_PartnerSales60000_ShouldReturn10()
        {
            decimal totalSales = 60000;
            decimal discount = PartnerOrderForm.CalculateDiscountBySales(totalSales);

            Assert.AreEqual(10, discount, "Продажи 60000 → скидка 10%");
        }

        [TestMethod]
        public void CalculateDiscount_PartnerSales400000_ShouldReturn15()
        {
            decimal totalSales = 400000;
            decimal discount = PartnerOrderForm.CalculateDiscountBySales(totalSales);

            Assert.AreEqual(15, discount, "Продажи 400000 → скидка 15%");
        }
    }
}
