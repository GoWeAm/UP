using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace MasterPolApp
{
    public partial class SalesHistoryForm : Form
    {
        private string connectionString = @"Data Source=ADCLG1;Initial Catalog=MasterPol_2092_29;Integrated Security=True";
        private int partnerId;
        private string partnerName;

        public SalesHistoryForm(int id, string name)
        {
            InitializeComponent();
            partnerId = id;
            partnerName = name;
            lblPartnerName.Text = $"История продаж: {partnerName}";
            LoadSalesHistory();
        }

        private void LoadSalesHistory()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"
                        SELECT 
                            ph.SaleID,
                            ph.SaleDate,
                            o.OrderID AS [Номер заявки],
                            ph.Amount AS [Сумма]
                        FROM PartnerSalesHistory ph
                        LEFT JOIN Orders o ON ph.PartnerID = o.PartnerID
                        WHERE ph.PartnerID = @PartnerId
                        ORDER BY ph.SaleDate DESC";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@PartnerId", partnerId);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    dataGridViewSalesHistory.DataSource = table;
                    FormatDataGridView();

                    CalculateTotals(table);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки истории продаж: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatDataGridView()
        {
            dataGridViewSalesHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewSalesHistory.BackgroundColor = Color.White;
            dataGridViewSalesHistory.GridColor = Color.FromArgb(244, 232, 211);

            foreach (DataGridViewColumn column in dataGridViewSalesHistory.Columns)
            {
                column.HeaderCell.Style.BackColor = Color.FromArgb(103, 186, 128);
                column.HeaderCell.Style.ForeColor = Color.White;
                column.HeaderCell.Style.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            }
        }

        private void CalculateTotals(DataTable table)
        {
            decimal totalSales = 0;

            foreach (DataRow row in table.Rows)
            {
                totalSales += Convert.ToDecimal(row["Сумма"]);
            }

            lblTotalSales.Text = $"Общая сумма продаж: {totalSales:C}";
        }

        private void btnDeleteSale_Click(object sender, EventArgs e)
        {
            if (dataGridViewSalesHistory.CurrentRow != null)
            {
                int saleId = Convert.ToInt32(dataGridViewSalesHistory.CurrentRow.Cells["SaleID"].Value);
                string saleDate = dataGridViewSalesHistory.CurrentRow.Cells["SaleDate"].Value.ToString();
                string orderNumber = dataGridViewSalesHistory.CurrentRow.Cells["Номер заявки"].Value.ToString();

                DialogResult result = MessageBox.Show(
                    $"Вы уверены, что хотите удалить запись о продаже?\n\n" +
                    $"Дата: {saleDate}\n" +
                    $"Номер заявки: {orderNumber}",
                    "Подтверждение удаления",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    DeleteSale(saleId);
                }
            }
            else
            {
                MessageBox.Show("Выберите запись о продаже для удаления", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void DeleteSale(int saleId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM PartnerSalesHistory WHERE SaleID = @SaleId";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@SaleId", saleId);
                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Запись о продаже успешно удалена", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadSalesHistory();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении записи о продаже: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadSalesHistory();
            MessageBox.Show("Данные истории продаж обновлены", "Информация",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
