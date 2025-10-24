using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MasterPolApp
{
    public partial class PartnerForm : Form
    {
        private string connectionString = @"Data Source=ADCLG1;Initial Catalog=MasterPol_2092_29;Integrated Security=True";
        private int? partnerId;
        private bool isEditMode;

        public PartnerForm()
        {
            InitializeComponent();
            isEditMode = false;
            InitializePartnerTypeCombo();
        }

        public PartnerForm(int id) : this()
        {
            partnerId = id;
            isEditMode = true;
            this.Text = "Редактирование партнера";
            btnSave.Text = "Сохранить";
            LoadPartnerData();
        }

        private void InitializePartnerTypeCombo()
        {
            cmbPartnerType.Items.AddRange(new string[]
            {
                "Поставщик",
                "Клиент",
                "Дилер",
                "Дистрибьютор",
                "Розничный покупатель",
                "Обычный"
            });
            cmbPartnerType.SelectedIndex = 0;
        }

        private void LoadPartnerData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Partners WHERE PartnerID = @PartnerId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PartnerId", partnerId);
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtName.Text = reader["Name"].ToString();
                                txtAddress.Text = reader["LegalAddress"].ToString();
                                txtINN.Text = reader["INN"].ToString();
                                txtDirector.Text = reader["DirectorFullName"].ToString();
                                txtPhone.Text = reader["Phone"].ToString();
                                txtEmail.Text = reader["Email"].ToString();
                                numRating.Value = Convert.ToDecimal(reader["Rating"]);
                                cmbPartnerType.SelectedItem = reader["PartnerType"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query;

                    if (isEditMode)
                    {
                        query = @"
                            UPDATE Partners 
                            SET Name = @Name,
                                LegalAddress = @Address,
                                INN = @INN,
                                DirectorFullName = @Director,
                                Phone = @Phone,
                                Email = @Email,
                                Rating = @Rating,
                                PartnerType = @PartnerType
                            WHERE PartnerID = @PartnerId";
                    }
                    else
                    {
                        query = @"
                            INSERT INTO Partners 
                            (Name, LegalAddress, INN, DirectorFullName, Phone, Email, Rating, PartnerType)
                            VALUES (@Name, @Address, @INN, @Director, @Phone, @Email, @Rating, @PartnerType);
                            SELECT SCOPE_IDENTITY();";
                    }

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", txtName.Text.Trim());
                        command.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
                        command.Parameters.AddWithValue("@INN", txtINN.Text.Trim());
                        command.Parameters.AddWithValue("@Director", txtDirector.Text.Trim());
                        command.Parameters.AddWithValue("@Phone", txtPhone.Text.Trim());
                        command.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                        command.Parameters.AddWithValue("@Rating", numRating.Value);
                        command.Parameters.AddWithValue("@PartnerType", cmbPartnerType.SelectedItem?.ToString() ?? "Обычный");

                        if (isEditMode)
                        {
                            command.Parameters.AddWithValue("@PartnerId", partnerId);
                            command.ExecuteNonQuery();
                        }
                        else
                        {
                            object newId = command.ExecuteScalar();
                            partnerId = Convert.ToInt32(newId);
                        }
                    }
                }

                MessageBox.Show("Данные партнера успешно сохранены", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения данных: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        // ----------------- Методы для тестов -----------------
        /// <summary>
        /// Проверка корректности введенных данных
        /// </summary>
        public bool ValidateInputForTest()
        {
            return ValidateInput();
        }

        /// <summary>
        /// Метод для установки данных партнера из теста
        /// </summary>
        public void SetPartnerData(string name, string inn, string phone, string address, string director, string email, decimal rating)
        {
            txtName.Text = name;
            txtINN.Text = inn;
            txtPhone.Text = phone;
            txtAddress.Text = address;
            txtDirector.Text = director;
            txtEmail.Text = email;
            numRating.Value = rating;
        }

        /// <summary>
        /// Приватная валидация для использования в тестах
        /// </summary>
        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
                return false;

            if (string.IsNullOrWhiteSpace(txtINN.Text) || (txtINN.Text.Length != 10 && txtINN.Text.Length != 12))
                return false;

            if (string.IsNullOrWhiteSpace(txtPhone.Text))
                return false;

            if (string.IsNullOrWhiteSpace(txtEmail.Text) || !txtEmail.Text.Contains("@"))
                return false;

            if (cmbPartnerType.SelectedItem == null)
                return false;

            return true;
        }
    }
}
