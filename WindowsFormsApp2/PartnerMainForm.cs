using System;
using System.Drawing;
using System.Windows.Forms;

namespace MasterPolApp
{
    public partial class PartnerMainForm : Form
    {
        private int partnerId;
        private string partnerName;

        public PartnerMainForm(int partnerId, string partnerName)
        {
            InitializeComponent();
            this.partnerId = partnerId;
            this.partnerName = partnerName;
            lblPartnerInfo.Text = $"Партнер: {partnerName}";
        }

        private void btnCalculateDiscount_Click(object sender, EventArgs e)
        {
            try
            {
                // Открытие формы для расчета скидки
                PartnerDiscountForm discountForm = new PartnerDiscountForm(partnerId, partnerName);
                discountForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при открытии формы расчета скидки: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCreateOrder_Click(object sender, EventArgs e)
        {
            try
            {
                // Открытие формы для создания заказа
                PartnerOrderForm orderForm = new PartnerOrderForm(partnerId, partnerName);
                orderForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при открытии формы для создания заказа: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnViewOrders_Click(object sender, EventArgs e)
        {
            try
            {
                // Открытие формы для просмотра заказов
                PartnerOrdersViewForm ordersForm = new PartnerOrdersViewForm(partnerId, partnerName);
                ordersForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при открытии формы для просмотра заказов: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            // Закрытие приложения
            Application.Exit();
        }
    }
}
