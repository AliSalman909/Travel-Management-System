using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace TravelEaseDB
{
    public partial class SignINForm : Form
    {
        public string LoggedInUserRole { get; private set; }
        public string LoggedInUserSubRole { get; private set; }
        public static int LoggedInUserID;

        private AppUser_Form _parentForm;  // Declare a variable to hold the parent Form1 instance
        string connectionString = "Data Source=CODE-TECH\\SQLEXPRESS;Initial Catalog=TravelEaseDB;Integrated Security=True;";
        

        public SignINForm(AppUser_Form parent)
        {
            InitializeComponent();

            this.Activated += (s, e) => textBox1.Focus();

            textBox1.TabIndex = 0;
            textBox4.TabIndex = 1;
            comboBox4.TabIndex = 2;

            this._parentForm = parent;

            this.WindowState = FormWindowState.Maximized;
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.UserPaint, true);
            this.UpdateStyles();



            this.MinimizeBox = false; // Disable minimize button
            this.MaximizeBox = false; // Disable maximize button

            comboBox4.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox4.MaxDropDownItems = 5;
            comboBox4.DropDownWidth = 300;
            comboBox4.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBox4.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            comboBox4.Items.Clear();
            comboBox4.Items.Add("-- Select User Role");
            comboBox4.Items.AddRange(new string[]
            {
        "ADMIN", "SERVICE PROVIDER HOTEL","SERVICE PROVIDER GUIDE","SERVICE PROVIDER TRANSPORT", "TOUR OPERATOR","TRAVELER"  });
            comboBox4.SelectedIndex = 0;


        }


        private void ClearForm()
        {
            foreach (Control control in this.Controls)
            {
                if (control is TextBox)
                {
                    ((TextBox)control).Text = "";
                    control.BackColor = Color.White;
                }
            }

        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // WS_EX_COMPOSITED
                return cp;
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            if (textBox1.Text.Length < 8 || textBox1.Text.Length > 20)
            {
                textBox1.BackColor = Color.LightCoral;
            }
            else
            {
                textBox1.BackColor = Color.White;

            }
        }

        private void SignINForm_Load(object sender, EventArgs e)
        {
            ClearForm(); // Clear form fields recursively
            textBox1.Focus();  // Set initial focus to AdminName textbox
            BeginInvoke((MethodInvoker)delegate
            {
                textBox1.Focus();
            });
            textBox4.UseSystemPasswordChar = true;
            comboBox4.SelectedIndex = 0;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (textBox4.Text.Length < 8 || textBox4.Text.Length > 15)
            {
                textBox4.BackColor = Color.LightCoral;
            }
            else
            {
                textBox4.BackColor = Color.White;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only lowercase letters (a-z), digits (0-9), or control characters (like Backspace)
            if (!char.IsControl(e.KeyChar) && !char.IsLower(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Block invalid input
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar))
            {
                e.Handled = true; // Block non-alphanumeric characters
            }
        }

        private bool ContainsFormData(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is TextBox textBox && !string.IsNullOrEmpty(textBox.Text))
                    return true;
                if (control is ComboBox comboBox && comboBox.SelectedIndex != 0)
                    return true;
                if (control is CheckBox checkBox && checkBox.Checked)
                    return true;
                if (control is RadioButton radioButton && radioButton.Checked)
                    return true;
                if (control.HasChildren && ContainsFormData(control))
                    return true;
            }
            return false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool hasData = ContainsFormData(this);

            if (hasData)
            {
                DialogResult result = MessageBox.Show(
                   "THIS WILL CLEAR THE FORM.DO YOU WANT TO CONTINUE ? ",
                    "CONFIRM CLEAR",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result != DialogResult.Yes)
                    return;
            }

            ClearForm();
            comboBox4.SelectedIndex = 0;
            _parentForm.Show();
            this.Close();
        }

        private string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2")); // Convert to hex
                }
                return builder.ToString();
            }
        }

private void button2_Click(object sender, EventArgs e)
        {
            // Validate inputs
            if (string.IsNullOrWhiteSpace(textBox1.Text) || textBox1.Text.Trim().Length < 8 || textBox1.Text.Trim().Length > 20 || textBox1.BackColor == Color.LightCoral)
            {
                MessageBox.Show("USERNAME IS INVALID. IT SHOULD BE 8-20 CHARACTERS.", "INVALID INPUT", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox4.Text) || textBox4.Text.Length < 8 || textBox4.Text.Length > 15 || textBox4.BackColor == Color.LightCoral)
            {
                MessageBox.Show("PASSWORD IS INVALID. IT SHOULD BE BETWEEN 8 AND 15 CHARACTERS.", "INVALID INPUT", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (comboBox4.SelectedIndex <= 0)
            {
                MessageBox.Show("PLEASE SELECT A USER ROLE.", "INVALID INPUT", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string username = textBox1.Text.Trim();
            string password = textBox4.Text;
            string selectedRole = comboBox4.SelectedItem.ToString();

            // Map comboBox4 items to database UserRole values
            string userRole;
            if (selectedRole == "ADMIN")
                userRole = "Admin";
            else if (selectedRole == "SERVICE PROVIDER")
                userRole = "ServiceProvider";
            else if (selectedRole == "TOUR OPERATOR")
                userRole = "TourOperator";
            else if (selectedRole == "TRAVELER")
                userRole = "Traveler";
            else
            {
                MessageBox.Show("INVALID USER ROLE SELECTED.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Compute SHA-256 hash of the entered password
            string hashedPassword = ComputeSha256Hash(password);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"
                SELECT UserID, UserPassword, UserStatus
                FROM AppUsers
                WHERE UserName = @username AND UserRole = @userRole";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@username", SqlDbType.VarChar, 20).Value = username;
                        cmd.Parameters.Add("@userRole", SqlDbType.VarChar, 20).Value = userRole;

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                MessageBox.Show("INVALID USERNAME OR ROLE.", "LOGIN FAILED", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            int userId = Convert.ToInt32(reader["UserID"]);
                            string storedHash = reader["UserPassword"].ToString();
                            int userStatus = Convert.ToInt32(reader["UserStatus"]);
                            reader.Close();

                            // Verify password
                            if (!storedHash.Equals(hashedPassword, StringComparison.OrdinalIgnoreCase))
                            {
                                MessageBox.Show("INVALID PASSWORD.", "LOGIN FAILED", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            // Check UserStatus
                            if (userStatus == 0)
                            {
                                MessageBox.Show("ACCOUNT REQUEST IS PENDING FOR APPROVAL.", "PENDING APPROVAL", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            else if (userStatus == 1)
                            {
                                // Log login attempt
                                //string logQuery = "INSERT INTO UserActivityLog (UserID, Action, ActionDate) VALUES (@userId, 'Login', GETDATE())";
                                //using (SqlCommand logCmd = new SqlCommand(logQuery, conn))
                                //{
                                //    logCmd.Parameters.AddWithValue("@userId", userId);
                                //    logCmd.ExecuteNonQuery();
                                //}

                                MessageBox.Show("LOGIN SUCCESSFULLY!", "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information);



                                //// Redirect based on UserRole
                                LoggedInUserID= userId;
                                this.LoggedInUserRole = userRole;  // Assign it here

                                this.DialogResult = DialogResult.OK;
                                this.Close();

                                //if (userRole == "Admin")
                                //{
                                //    form1.Show();
                                //  //  _parentForm.Hide();
                                //    this.Close();
                                //}                                //else if (userRole == "Traveler")
                                //    new TravelerDashboardForm(userId).Show();
                                //else if (userRole == "TourOperator")
                                //    new TourOperatorDashboardForm(userId).Show();
                                //else if (userRole == "ServiceProvider")
                                //    new ServiceProviderDashboardForm(userId).Show();

                            }
                            else
                            {
                                MessageBox.Show("INVALID ACCOUNT STATUS.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"LOGIN FAILED: {ex.Message}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
