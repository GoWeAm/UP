using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace MasterPolApp
{
    public partial class PartnerDiscountForm : Form
    {
        private string connectionString = @"Data Source=ADCLG1;Initial Catalog=MasterPol_2092_29;Integrated Security=True";
        private int partnerId;
        private string partnerName;

        public PartnerDiscountForm(int partnerId, string partnerName)
        {
            InitializeComponent();
            this.partnerId = partnerId;
            this.partnerName = partnerName;
            lblPartnerInfo.Text = $"Партнер: {partnerName}";
            CalculateDiscount();
        }

        private void CalculateDiscount()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                        SELECT ISNULL(SUM(Amount), 0) as TotalSales
                        FROM PartnerSalesHistory
                        WHERE PartnerID = @PartnerId";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PartnerId", partnerId);
                        decimal totalSales = Convert.ToDecimal(command.ExecuteScalar());

                        // Расчет скидки по правилам
                        decimal discount = 0;
                        string discountLevel = "";

                        if (totalSales > 300000)
                        {
                            discount = 15;
                            discountLevel = "Высший уровень (15%)";
                        }
                        else if (totalSales > 50000)
                        {
                            discount = 10;
                            discountLevel = "Средний уровень (10%)";
                        }
                        else if (totalSales > 10000)
                        {
                            discount = 5;
                            discountLevel = "Базовый уровень (5%)";
                        }
                        else
                        {
                            discountLevel = "Начальный уровень (0%)";
                        }

                        lblTotalSales.Text = $"Общий объем продаж: {totalSales:N2} руб.";
                        lblDiscountPercent.Text = $"Текущая скидка: {discount}%";
                        lblDiscountLevel.Text = $"Уровень скидки: {discountLevel}";

                        // Расчет до следующего уровня
                        decimal nextLevel = 0;
                        string nextLevelInfo = "";

                        if (totalSales < 10000)
                        {
                            nextLevel = 10000 - totalSales;
                            nextLevelInfo = $"До скидки 5% осталось: {nextLevel:N2} руб.";
                        }
                        else if (totalSales < 50000)
                        {
                            nextLevel = 50000 - totalSales;
                            nextLevelInfo = $"До скидки 10% осталось: {nextLevel:N2} руб.";
                        }
                        else if (totalSales < 300000)
                        {
                            nextLevel = 300000 - totalSales;
                            nextLevelInfo = $"До скидки 15% осталось: {nextLevel:N2} руб.";
                        }
                        else
                        {
                            nextLevelInfo = "Достигнут максимальный уровень скидки!";
                        }

                        lblNextLevel.Text = nextLevelInfo;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка расчета скидки: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
