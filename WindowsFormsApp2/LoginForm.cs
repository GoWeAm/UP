using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace MasterPolApp
{
    public partial class LoginForm : Form
    {
        private string connectionString = @"Data Source=ADCLG1;Initial Catalog=MasterPol_2092_29;Integrated Security=True";

        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите логин и пароль", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // --- Специальный логин для partner ---
            if (username == "partner" && password == "partner")
            {
                this.Hide();
                PartnerMainForm mainForm = new PartnerMainForm(1, "Партнер");
                mainForm.ShowDialog();
                this.Close();
                return;
            }

            // --- Специальный логин для partner1 ---
            if (username == "partner1" && password == "hash_partner1")
            {
                this.Hide();
                int partnerId = 1;
                string partnerName = "Партнер1";

                PartnerMainForm mainForm = new PartnerMainForm(partnerId, partnerName);
                PartnerOrderForm orderForm = new PartnerOrderForm(partnerId, partnerName);
                PartnerOrdersViewForm ordersViewForm = new PartnerOrdersViewForm(partnerId, partnerName);

                // Открываем все формы немодально
                mainForm.Show();
                orderForm.Show();
                ordersViewForm.Show();

                // При закрытии главной формы закрываем приложение
                mainForm.FormClosed += (s, ev) =>
                {
                    Application.Exit();
                };

                return;
            }

            // --- Проверка через базу данных ---
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                        SELECT 
                            u.UserID,
                            u.Username,
                            r.RoleName
                        FROM Users u 
                        INNER JOIN Roles r ON u.RoleID = r.RoleID 
                        WHERE u.Username = @Username 
                        AND u.PasswordHash = @Password";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", password);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int userId = reader.GetInt32(0);
                                string usernameDb = reader.GetString(1);
                                string role = reader.GetString(2);

                                this.Hide();
                                MainForm mainForm = new MainForm(role, userId);
                                mainForm.ShowDialog();
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Неверный логин или пароль", "Ошибка авторизации",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка подключения к базе данных: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnLogin_Click(sender, e);
            }
        }
    }
}
