using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace MasterPolApp
{
    public partial class MainForm : Form
    {
        private string connectionString = @"Data Source=ADCLG1;Initial Catalog=MasterPol_2092_29;Integrated Security=True";
        private string currentUserRole;
        private int currentUserId;

        public MainForm(string role, int userId)
        {
            InitializeComponent();
            currentUserRole = role;
            currentUserId = userId;
            ConfigureAccessByRole();
            LoadPartnersData();
        }

        private void ConfigureAccessByRole()
        {
            switch (currentUserRole)
            {
                case "Администратор":
                    // Полный доступ ко всем функциям
                    break;
                case "Финансовый аналитик":
                    btnAddPartner.Enabled = false;
                    btnEditPartner.Enabled = false;
                    btnDeletePartner.Enabled = false;
                    break;
                case "Партнер":
                    btnAddPartner.Enabled = false;
                    btnEditPartner.Enabled = false;
                    btnDeletePartner.Enabled = false;
                    btnSalesReport.Enabled = false;
                    break;
                case "Сотрудник":
                    btnAddPartner.Enabled = false;
                    btnEditPartner.Enabled = false;
                    btnDeletePartner.Enabled = false;
                    btnSalesReport.Enabled = false;
                    btnMaterialCalculation.Enabled = false;
                    break;
            }

            lblUserRole.Text = $"Роль: {currentUserRole}";
        }

        private void LoadPartnersData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"
                        SELECT 
                            PartnerID,
                            Name AS PartnerName,
                            LegalAddress,
                            INN,
                            DirectorFullName,
                            Phone,
                            Email,
                            Rating
                        FROM Partners";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    dataGridViewPartners.DataSource = table;
                    FormatDataGridView();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatDataGridView()
        {
            dataGridViewPartners.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewPartners.BackgroundColor = Color.White;
            dataGridViewPartners.GridColor = Color.FromArgb(244, 232, 211);

            foreach (DataGridViewColumn column in dataGridViewPartners.Columns)
            {
                column.HeaderCell.Style.BackColor = Color.FromArgb(103, 186, 128);
                column.HeaderCell.Style.ForeColor = Color.White;
                column.HeaderCell.Style.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            }
        }

        private void btnAddPartner_Click(object sender, EventArgs e)
        {
            PartnerForm partnerForm = new PartnerForm();
            if (partnerForm.ShowDialog() == DialogResult.OK)
            {
                LoadPartnersData();
            }
        }

        private void btnEditPartner_Click(object sender, EventArgs e)
        {
            if (dataGridViewPartners.CurrentRow != null)
            {
                int partnerId = Convert.ToInt32(dataGridViewPartners.CurrentRow.Cells["PartnerID"].Value);
                PartnerForm partnerForm = new PartnerForm(partnerId);
                if (partnerForm.ShowDialog() == DialogResult.OK)
                {
                    LoadPartnersData();
                }
            }
            else
            {
                MessageBox.Show("Выберите партнера для редактирования", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDeletePartner_Click(object sender, EventArgs e)
        {
            if (dataGridViewPartners.CurrentRow != null)
            {
                int partnerId = Convert.ToInt32(dataGridViewPartners.CurrentRow.Cells["PartnerID"].Value);
                string partnerName = dataGridViewPartners.CurrentRow.Cells["PartnerName"].Value.ToString();

                DialogResult result = MessageBox.Show(
                    $"Вы уверены, что хотите удалить партнера '{partnerName}'?",
                    "Подтверждение удаления",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    DeletePartner(partnerId);
                }
            }
        }

        private void DeletePartner(int partnerId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM Partners WHERE PartnerID = @PartnerId";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PartnerId", partnerId);
                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Партнер успешно удален", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadPartnersData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении партнера: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSalesReport_Click(object sender, EventArgs e)
        {
            SalesReportForm reportForm = new SalesReportForm();
            reportForm.ShowDialog();
        }

        private void btnMaterialCalculation_Click(object sender, EventArgs e)
        {
            MaterialCalculationForm calcForm = new MaterialCalculationForm();
            calcForm.ShowDialog();
        }

        private void btnViewSalesHistory_Click(object sender, EventArgs e)
        {
            if (dataGridViewPartners.CurrentRow != null)
            {
                int partnerId = Convert.ToInt32(dataGridViewPartners.CurrentRow.Cells["PartnerID"].Value);
                string partnerName = dataGridViewPartners.CurrentRow.Cells["PartnerName"].Value.ToString();

                SalesHistoryForm historyForm = new SalesHistoryForm(partnerId, partnerName);
                historyForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Выберите партнера для просмотра истории продаж", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void CalculateDiscountForSelectedPartner()
        {
            if (dataGridViewPartners.CurrentRow != null)
            {
                int partnerId = Convert.ToInt32(dataGridViewPartners.CurrentRow.Cells["PartnerID"].Value);
                decimal discount = CalculatePartnerDiscount(partnerId);

                MessageBox.Show($"Рассчитанная скидка для партнера: {discount}%", "Расчет скидки",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private decimal CalculatePartnerDiscount(int partnerId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                        SELECT SUM(Amount) AS TotalSales
                        FROM PartnerSalesHistory
                        WHERE PartnerID = @PartnerId";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PartnerId", partnerId);
                        object result = command.ExecuteScalar();

                        decimal totalSales = result != DBNull.Value ? Convert.ToDecimal(result) : 0;

                        // Расчет скидки по правилам из ТЗ
                        if (totalSales > 300000) return 15;
                        if (totalSales > 50000) return 10;
                        if (totalSales > 10000) return 5;
                        return 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка расчета скидки: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }

        private void btnManageOrders_Click(object sender, EventArgs e)
        {
            if (currentUserRole == "Директор")
            {
                OrderManagementForm orderForm = new OrderManagementForm();
                orderForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Доступно только для директора", "Ограничение доступа",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
