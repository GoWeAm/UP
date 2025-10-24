using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace MasterPolApp
{
    public partial class OrderManagementForm : Form
    {
        private string connectionString = @"Data Source=ADCLG1;Initial Catalog=MasterPol_2092_29;Integrated Security=True";

        public OrderManagementForm()
        {
            InitializeComponent();
            LoadOrders();
        }

        private void LoadOrders()
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
                            p.Name AS [Партнер],
                            m.FullName AS [Менеджер]
                        FROM Orders o
                        INNER JOIN Partners p ON o.PartnerID = p.PartnerID
                        LEFT JOIN Managers m ON o.ManagerID = m.ManagerID
                        ORDER BY o.OrderDate DESC";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    dataGridViewOrders.DataSource = table;
                    FormatOrdersGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки заказов: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatOrdersGrid()
        {
            dataGridViewOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewOrders.BackgroundColor = Color.White;
            dataGridViewOrders.GridColor = Color.FromArgb(244, 232, 211);
            dataGridViewOrders.RowHeadersVisible = false;
            dataGridViewOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

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
                        case "Rejected":
                            row.DefaultCellStyle.BackColor = Color.LightCoral;
                            break;
                        case "InProgress":
                            row.DefaultCellStyle.BackColor = Color.LightYellow;
                            break;
                        case "Completed":
                            row.DefaultCellStyle.BackColor = Color.LightGray;
                            break;
                        default:
                            row.DefaultCellStyle.BackColor = Color.White;
                            break;
                    }
                }
            }
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            if (dataGridViewOrders.CurrentRow == null) return;

            int orderId = Convert.ToInt32(dataGridViewOrders.CurrentRow.Cells["Номер заявки"].Value);
            string status = dataGridViewOrders.CurrentRow.Cells["Статус"].Value?.ToString();

            if (status != "Created")
            {
                MessageBox.Show("Можно одобрять только заявки со статусом 'Created'", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult result = MessageBox.Show($"Одобрить заявку #{orderId}?", "Подтверждение",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                UpdateOrderStatus(orderId, "Approved");
            }
        }

        private void btnReject_Click(object sender, EventArgs e)
        {
            if (dataGridViewOrders.CurrentRow == null) return;

            int orderId = Convert.ToInt32(dataGridViewOrders.CurrentRow.Cells["Номер заявки"].Value);
            string status = dataGridViewOrders.CurrentRow.Cells["Статус"].Value?.ToString();

            if (status != "Created")
            {
                MessageBox.Show("Можно отклонять только заявки со статусом 'Created'", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string reason = Microsoft.VisualBasic.Interaction.InputBox("Введите причину отклонения:", "Отклонение заказа", "");

            if (!string.IsNullOrWhiteSpace(reason))
            {
                UpdateOrderStatus(orderId, "Rejected", reason);
            }
        }

        private void UpdateOrderStatus(int orderId, string newStatus, string reason = null)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "UPDATE Orders SET Status = @Status WHERE OrderID = @OrderId";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Status", newStatus);
                        cmd.Parameters.AddWithValue("@OrderId", orderId);
                        cmd.ExecuteNonQuery();
                    }

                    if (!string.IsNullOrEmpty(reason))
                    {
                        // Можно добавить таблицу для хранения причин отклонения
                    }
                }

                MessageBox.Show($"Статус заявки #{orderId} обновлен: {newStatus}", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadOrders();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка обновления статуса: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadOrders();
            MessageBox.Show("Список заказов обновлен", "Информация",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dataGridViewOrders_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewOrders.CurrentRow == null) return;

            string status = dataGridViewOrders.CurrentRow.Cells["Статус"].Value?.ToString();
            btnApprove.Enabled = (status == "Created");
            btnReject.Enabled = (status == "Created");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
