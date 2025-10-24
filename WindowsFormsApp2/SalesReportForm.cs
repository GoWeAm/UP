using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace MasterPolApp
{
    public partial class SalesReportForm : Form
    {
        private string connectionString = @"Data Source=ADCLG1;Initial Catalog=MasterPol_2092_29;Integrated Security=True";

        public SalesReportForm()
        {
            InitializeComponent();
            LoadSalesReport();
        }

        private void LoadSalesReport()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"
                        SELECT 
                            p.Name AS [Партнер],
                            ISNULL(SUM(ph.Amount), 0) AS [Общая сумма продаж],
                            COUNT(ph.SaleID) AS [Количество продаж],
                            ISNULL(AVG(ph.Amount), 0) AS [Средняя продажа],
                            MAX(ph.SaleDate) AS [Последняя продажа]
                        FROM Partners p
                        LEFT JOIN PartnerSalesHistory ph ON p.PartnerID = ph.PartnerID
                        GROUP BY p.Name
                        ORDER BY [Общая сумма продаж] DESC";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    dataGridViewSales.DataSource = table;
                    FormatDataGridView();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки отчета: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatDataGridView()
        {
            dataGridViewSales.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewSales.BackgroundColor = Color.White;
            dataGridViewSales.GridColor = Color.FromArgb(244, 232, 211);

            foreach (DataGridViewColumn column in dataGridViewSales.Columns)
            {
                column.HeaderCell.Style.BackColor = Color.FromArgb(103, 186, 128);
                column.HeaderCell.Style.ForeColor = Color.White;
                column.HeaderCell.Style.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "CSV файл (*.csv)|*.csv",
                Title = "Экспорт отчета по продажам"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                ExportToCsv(saveFileDialog.FileName);
            }
        }

        private void ExportToCsv(string fileName)
        {
            try
            {
                using (System.IO.StreamWriter writer = new System.IO.StreamWriter(fileName, false, System.Text.Encoding.UTF8))
                {
                    // Заголовки
                    string headers = "";
                    foreach (DataGridViewColumn column in dataGridViewSales.Columns)
                    {
                        headers += column.HeaderText + ";";
                    }
                    writer.WriteLine(headers.TrimEnd(';'));

                    // Данные
                    foreach (DataGridViewRow row in dataGridViewSales.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            string line = "";
                            foreach (DataGridViewCell cell in row.Cells)
                            {
                                line += cell.Value?.ToString() + ";";
                            }
                            writer.WriteLine(line.TrimEnd(';'));
                        }
                    }
                }

                MessageBox.Show("Отчет успешно экспортирован", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка экспорта: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
