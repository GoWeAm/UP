using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace MasterPolApp
{
    public partial class MaterialCalculationForm : Form
    {
        private string connectionString = @"Data Source=ADCLG1;Initial Catalog=MasterPol_2092_29;Integrated Security=True";

        public MaterialCalculationForm()
        {
            InitializeComponent();
            LoadProducts();
            LoadMaterials();
        }

        private void LoadProducts()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT ProductID, Name FROM Products";  // Обновлено название таблицы и столбцов

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    comboBoxProducts.DisplayMember = "Name";  // Обновлено поле отображения
                    comboBoxProducts.ValueMember = "ProductID";  // Обновлено значение
                    comboBoxProducts.DataSource = table;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки продукции: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadMaterials()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"
                        SELECT 
                            m.MaterialID,
                            m.Name AS MaterialName,
                            m.QuantityOnStock,
                            m.CostPerUnit,
                            s.Name AS SupplierName
                        FROM Materials m
                        INNER JOIN Suppliers s ON m.SupplierID = s.SupplierID";  // Обновлены имена таблиц и полей

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    dataGridViewMaterials.DataSource = table;
                    FormatMaterialsGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки материалов: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatMaterialsGrid()
        {
            dataGridViewMaterials.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewMaterials.BackgroundColor = Color.White;
            dataGridViewMaterials.GridColor = Color.FromArgb(244, 232, 211);

            foreach (DataGridViewColumn column in dataGridViewMaterials.Columns)
            {
                column.HeaderCell.Style.BackColor = Color.FromArgb(103, 186, 128);
                column.HeaderCell.Style.ForeColor = Color.White;
                column.HeaderCell.Style.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            }
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            if (comboBoxProducts.SelectedValue == null)
            {
                MessageBox.Show("Выберите продукцию для расчета", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int productId = (int)comboBoxProducts.SelectedValue;
            CalculateMaterialRequirements(productId);
        }

        private void CalculateMaterialRequirements(int productId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"
                        SELECT 
                            m.Name AS MaterialName,
                            pm.MaterialQuantity,
                            m.QuantityOnStock,
                            m.CostPerUnit,
                            (pm.MaterialQuantity * m.CostPerUnit) AS MaterialCost
                        FROM ProductMaterials pm
                        INNER JOIN Materials m ON pm.MaterialID = m.MaterialID
                        WHERE pm.ProductID = @ProductId";  // Обновлены названия таблиц и полей

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@ProductId", productId);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    dataGridViewCalculation.DataSource = table;
                    FormatCalculationGrid();

                    // Расчет общей стоимости
                    decimal totalCost = 0;
                    foreach (DataRow row in table.Rows)
                    {
                        totalCost += Convert.ToDecimal(row["MaterialCost"]);
                    }

                    lblTotalCost.Text = $"Общая стоимость материалов: {totalCost:C}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка расчета материалов: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatCalculationGrid()
        {
            dataGridViewCalculation.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCalculation.BackgroundColor = Color.White;
            dataGridViewCalculation.GridColor = Color.FromArgb(244, 232, 211);

            foreach (DataGridViewColumn column in dataGridViewCalculation.Columns)
            {
                column.HeaderCell.Style.BackColor = Color.FromArgb(103, 186, 128);
                column.HeaderCell.Style.ForeColor = Color.White;
                column.HeaderCell.Style.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            }
        }
    }
}
