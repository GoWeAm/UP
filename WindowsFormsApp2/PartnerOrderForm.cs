using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace MasterPolApp
{
    public partial class PartnerOrderForm : Form
    {
        private string connectionString = @"Data Source=ADCLG1;Initial Catalog=MasterPol_2092_29;Integrated Security=True";
        private int partnerId;
        private string partnerName;
        private decimal currentDiscount = 0;

        public PartnerOrderForm(int partnerId, string partnerName)
        {
            InitializeComponent();
            this.partnerId = partnerId;
            this.partnerName = partnerName;
            lblPartnerInfo.Text = $"Партнер: {partnerName}";

            numericQuantity.Minimum = 1; // по умолчанию минимальное количество = 1
            numericQuantity.Maximum = 100000;

            LoadProducts();
            CalculateDiscount();
        }

        // ----------------- Загрузка и форматирование продуктов -----------------
        private void LoadProducts()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"
                        SELECT ProductID, Name, ProductType, MinPriceForPartner
                        FROM Products
                        WHERE MinPriceForPartner > 0";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    dataGridViewProducts.DataSource = table;
                    FormatProductsGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки продукции: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatProductsGrid()
        {
            dataGridViewProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewProducts.BackgroundColor = Color.White;
            dataGridViewProducts.GridColor = Color.FromArgb(244, 232, 211);
            dataGridViewProducts.RowHeadersVisible = false;
            dataGridViewProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            foreach (DataGridViewColumn column in dataGridViewProducts.Columns)
            {
                column.HeaderCell.Style.BackColor = Color.FromArgb(103, 186, 128);
                column.HeaderCell.Style.ForeColor = Color.White;
            }
        }

        // ----------------- Расчет скидки -----------------
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
                        currentDiscount = CalculateDiscountBySales(totalSales);
                        lblDiscount.Text = $"Ваша скидка: {currentDiscount}%";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка расчета скидки: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ----------------- Методы для тестов -----------------
        public static decimal CalculateDiscountBySales(decimal totalSales)
        {
            if (totalSales > 300000) return 15;
            if (totalSales > 50000) return 10;
            if (totalSales > 10000) return 5;
            return 0;
        }

        public bool ValidateQuantity(int quantity)
        {
            return quantity > 0;
        }

        // Для тестов: можно задать выбранный продукт
        public void SelectProductForTest(int productId, string name, decimal price)
        {
            lblSelectedProduct.Text = $"Выбрано: {name}";
            lblPrice.Text = $"Цена: {price:N2} руб.";
            decimal discountedPrice = price * (1 - currentDiscount / 100);
            lblDiscountedPrice.Text = $"Цена со скидкой {currentDiscount}%: {discountedPrice:N2} руб.";
        }

        // ----------------- Обработчики событий -----------------
        private void dataGridViewProducts_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewProducts.CurrentRow != null)
            {
                decimal price = Convert.ToDecimal(dataGridViewProducts.CurrentRow.Cells["MinPriceForPartner"].Value);
                string productName = dataGridViewProducts.CurrentRow.Cells["Name"].Value.ToString();
                lblSelectedProduct.Text = $"Выбрано: {productName}";
                lblPrice.Text = $"Цена: {price:N2} руб.";
                decimal discountedPrice = price * (1 - currentDiscount / 100);
                lblDiscountedPrice.Text = $"Цена со скидкой {currentDiscount}%: {discountedPrice:N2} руб.";
            }
        }

        private void btnCreateOrder_Click(object sender, EventArgs e)
        {
            if (dataGridViewProducts.CurrentRow == null)
            {
                MessageBox.Show("Выберите продукт для заказа", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int quantity = (int)numericQuantity.Value;
            if (!ValidateQuantity(quantity))
            {
                MessageBox.Show("Количество товара должно быть больше 0", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int productId = Convert.ToInt32(dataGridViewProducts.CurrentRow.Cells["ProductID"].Value);
            string productName = dataGridViewProducts.CurrentRow.Cells["Name"].Value.ToString();
            decimal price = Convert.ToDecimal(dataGridViewProducts.CurrentRow.Cells["MinPriceForPartner"].Value);
            decimal totalCost = price * quantity * (1 - currentDiscount / 100);

            CreateOrder(productId, productName, quantity, price, totalCost);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // ----------------- Создание заявки -----------------
        private void CreateOrder(int productId, string productName, int quantity, decimal unitPrice, decimal totalCost)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Получаем менеджера (для примера берем первого)
                    string managerQuery = "SELECT TOP 1 ManagerID FROM Managers";
                    int managerId = Convert.ToInt32(new SqlCommand(managerQuery, connection).ExecuteScalar());

                    // Создание заявки
                    string orderQuery = @"
                        INSERT INTO Orders (PartnerID, ManagerID, OrderDate, Status, TotalAmount)
                        VALUES (@PartnerID, @ManagerID, GETDATE(), @Status, @TotalAmount);
                        SELECT SCOPE_IDENTITY();";

                    int orderId;
                    using (SqlCommand cmd = new SqlCommand(orderQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@PartnerID", partnerId);
                        cmd.Parameters.AddWithValue("@ManagerID", managerId);
                        cmd.Parameters.AddWithValue("@Status", "Created");
                        cmd.Parameters.AddWithValue("@TotalAmount", totalCost);
                        orderId = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    // Добавляем позиции заказа
                    string orderItemQuery = @"
                        INSERT INTO OrderItems (OrderID, ProductID, Quantity, Price)
                        VALUES (@OrderID, @ProductID, @Quantity, @Price)";
                    using (SqlCommand cmd = new SqlCommand(orderItemQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@OrderID", orderId);
                        cmd.Parameters.AddWithValue("@ProductID", productId);
                        cmd.Parameters.AddWithValue("@Quantity", quantity);
                        cmd.Parameters.AddWithValue("@Price", unitPrice);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show($"Заявка #{orderId} успешно создана!\nСумма: {totalCost:N2} руб.\nСтатус: Created",
                        "Заявка создана", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ResetForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка создания заявки: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResetForm()
        {
            numericQuantity.Value = 1;
            dataGridViewProducts.ClearSelection();
            lblSelectedProduct.Text = "Выберите продукцию из таблицы";
            lblPrice.Text = "Цена:";
            lblDiscountedPrice.Text = "Цена со скидкой:";
        }
    }
}
