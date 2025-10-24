using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace MasterPolApp
{
    public partial class PartnerOrdersViewForm : Form
    {
        private string connectionString = @"Data Source=ADCLG1;Initial Catalog=MasterPol_2092_29;Integrated Security=True";
        private int partnerId;
        private string partnerName;

        public PartnerOrdersViewForm(int partnerId, string partnerName)
        {
            InitializeComponent();
            this.partnerId = partnerId;
            this.partnerName = partnerName;
            lblPartnerInfo.Text = $"Партнер: {partnerName}";
            LoadMyOrders();
        }

        private void LoadMyOrders()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"
                        SELECT 
                            o.OrderID AS [Номер заявки],
                            o.OrderDate AS [Дата создания],
                            o.Status AS [Статус],
                            o.TotalAmount AS [Общая стоимость],
                            o.PrepaymentAmount AS [Предоплата],
                            m.FullName AS [Менеджер]
                        FROM Orders o
                        LEFT JOIN Managers m ON o.ManagerID = m.ManagerID
                        WHERE o.PartnerID = @PartnerId
                        ORDER BY o.OrderDate DESC";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@PartnerId", partnerId);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    dataGridViewOrders.DataSource = table;
                    FormatOrdersGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки заявок: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatOrdersGrid()
        {
            dataGridViewOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewOrders.BackgroundColor = Color.White;
            dataGridViewOrders.GridColor = Color.FromArgb(244, 232, 211);
            dataGridViewOrders.RowHeadersVisible = false;

            foreach (DataGridViewColumn column in dataGridViewOrders.Columns)
            {
                column.HeaderCell.Style.BackColor = Color.FromArgb(103, 186, 128);
                column.HeaderCell.Style.ForeColor = Color.White;
            }

            // Подсветка статусов
            foreach (DataGridViewRow row in dataGridViewOrders.Rows)
            {
                if (!row.IsNewRow)
                {
                    string status = row.Cells["Статус"].Value?.ToString();
                    switch (status)
                    {
                        case "Created":
                            row.DefaultCellStyle.BackColor = Color.LightBlue;
                            break;
                        case "Approved":
                            row.DefaultCellStyle.BackColor = Color.LightGreen;
                            break;
                        case "InProgress":
                            row.DefaultCellStyle.BackColor = Color.LightYellow;
                            break;
                        case "Rejected":
                            row.DefaultCellStyle.BackColor = Color.LightCoral;
                            break;
                        case "Closed":
                            row.DefaultCellStyle.BackColor = Color.LightGray;
                            break;
                        default:
                            row.DefaultCellStyle.BackColor = Color.White;
                            break;
                    }
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadMyOrders();
            MessageBox.Show("Список заявок обновлен", "Информация",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
