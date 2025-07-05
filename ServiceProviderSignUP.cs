using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Runtime.ConstrainedExecution;


namespace TravelEaseDB
{
    public partial class ServiceProviderSignUP : Form
    {
        private AppUser_Form _parentForm;  // Declare a variable to hold the parent Form1 instance
        string connectionString = "Data Source=CODE-TECH\\SQLEXPRESS;Initial Catalog=TravelEaseDB;Integrated Security=True;";


        public ServiceProviderSignUP(AppUser_Form parent)
        {
            InitializeComponent();
            this.Activated += (s, e) => textBox7.Focus();

            comboBox6.Items.AddRange(new object[] {
    "-- Select Vehicle Type","Car", "Bus", "Van", "Bike", "Jeep",
    "SUV", "Mini Bus", "Rickshaw", "Pickup", "Truck",
    "Electric Car", "Tempo", "Scooter", "Convertible", "Limousine"
});


            comboBox4.Items.AddRange(new object[] {
    "-- Select Vehicle Capacity","4",      // Car
    "50",     // Bus
    "8",      // Van
    "2",      // Bike
    "5",      // Jeep
    "7",      // SUV
    "20",     // Mini Bus
    "3",      // Rickshaw
    "6",      // Tempo
    "10",      // Limousine
    "30",
    "25",
    "Other"
});

            checkedListBox1.Items.Clear();
            checkedListBox1.Items.AddRange(new string[]
            {
    "Urdu", "English", "Punjabi", "Pashto", "Sindhi", "Balochi", "Saraiki", "Hindko", "Arabic",
    "Kashmiri", "Shina", "Persian", "Mandarin", "Wakhi", "Balti", "Gujari", "Kohistani", "Torwali",
    "Kalasha", "Dameli", "Pothwari", "Marwari", "Spanish", "Italian", "Russian", "Japanese",
    "Korean", "Turkish", "Portuguese", "Dutch", "Swedish", "Polish", "Thai", "Vietnamese", "Malay",
    "Swahili", "Bengali", "Tamil", "Telugu", "Kurdish", "Gujarati", "French", "German", "Greek",
    "Hebrew", "Filipino (Tagalog)", "Finnish", "Czech", "Danish", "Other"
            });

            // Add this block here:
            comboBox3.Items.Clear();
            comboBox3.Items.Add("Select Guide Specialization");
            comboBox3.Items.AddRange(new string[]
            {
        "Adventure", "Cultural", "Luxury", "Budget", "Wildlife", "Hiking",
        "Beach", "Historical", "Religious", "Culinary", "Photography", "Snow/Skiing",
        "Wellness", "Road Trips", "SoloTravel", "Family Friendly", "Others"
            });
            comboBox3.SelectedIndex = 0;


            // Set tab order explicitly
            textBox7.TabIndex = 0;
            textBox6.TabIndex = 1;
            textBox1.TabIndex = 2;
            textBox2.TabIndex = 3;
            textBox3.TabIndex = 4;
            textBox4.TabIndex = 5;
            textBox5.TabIndex = 6;
            comboBox1.TabIndex = 7;
            comboBox2.TabIndex = 8;
            // ... continue with other controls
            this._parentForm = parent;
            this.WindowState = FormWindowState.Maximized;
            // Attach the Form2_Load event handler
            // Enable Double Buffering to prevent flickering
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.UserPaint, true);
            this.UpdateStyles();

            this.MinimizeBox = false; // Disable minimize button
            this.MaximizeBox = false; // Disable maximize button


            // Vertical scroll: only 5 items before scrolling
            comboBox1.MaxDropDownItems = 5;
            comboBox2.MaxDropDownItems = 5;

            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.DropDownWidth = 300; // Adjust width as needed
            comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.DropDownWidth = 300; // Adjust width as needed
            comboBox2.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBox2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox3.MaxDropDownItems = 5;
            comboBox3.DropDownWidth = 300;
            comboBox3.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBox3.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            comboBox4.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox4.MaxDropDownItems = 5;
            comboBox4.DropDownWidth = 300;
            comboBox4.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBox4.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            comboBox6.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox6.MaxDropDownItems = 5;
            comboBox6.DropDownWidth = 300;
            comboBox6.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBox6.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            //comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            //comboBox2.DropDownWidth = 300; // Adjust width as needed
            //comboBox2.AutoCompleteSource = AutoCompleteSource.ListItems;
            //comboBox2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            comboBox6.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
            LoadProviderTypes();

        }

        //private void LockFormExceptGuidePanel()
        //{
        //    foreach (Control ctrl in this.Controls)
        //    {
        //        if (ctrl != GuidePanel)
        //        {
        //            ctrl.Enabled = false;
        //        }
        //    }
        //    // Ensure GuidePanel and its children are enabled
        //    GuidePanel.Enabled = true;
        //    foreach (Control child in GuidePanel.Controls)
        //    {
        //        child.Enabled = true;
        //    }
        //}

        //private void LockFormExceptGuidePanel()
        //{
        //    // Disable all controls except GuidePanel
        //    foreach (Control ctrl in this.Controls)
        //    {
        //        if (ctrl != GuidePanel)
        //        {
        //            ctrl.Enabled = false;
        //        }
        //    }

        //    // Ensure GuidePanel and its children are enabled
        //    GuidePanel.Enabled = true;
        //    foreach (Control child in GuidePanel.Controls)
        //    {
        //        child.Enabled = true;
        //    }

        //    // Disable the close button (X button) in the title bar
        //    this.ControlBox = false; // Hides minimize, maximize, and close buttons
        //                             // OR (if you want to keep minimize/maximize but disable close)
        //                             // this.CloseButtonEnabled = false; // (Requires P/Invoke or custom form behavior)
        //}
        private void LockFormExceptGuidePanel()
        {
            this.SuspendLayout();

            foreach (Control ctrl in this.Controls)
            {
                ctrl.Enabled = (ctrl == GuidePanel);
            }

            // Enable all children of GuidePanel
            foreach (Control child in GuidePanel.Controls)
            {
                child.Enabled = true;
            }

            this.ControlBox = false;
            this.ResumeLayout(true);
        }

        private void LockFormExceptTransportPanel()
        {
            this.SuspendLayout();

            foreach (Control ctrl in this.Controls)
            {
                ctrl.Enabled = (ctrl == TransportPanel);
            }

            // Enable all children of TransportPanel
            foreach (Control child in TransportPanel.Controls)
            {
                child.Enabled = true;
            }

            this.ControlBox = false;
            this.ResumeLayout(true);
        }

        private void LockFormExceptHotelPanel()
        {
            this.SuspendLayout();

            foreach (Control ctrl in this.Controls)
            {
                ctrl.Enabled = (ctrl == HotelPanel);
            }

            // Enable all children of HotelPanel
            foreach (Control child in HotelPanel.Controls)
            {
                child.Enabled = true;
            }

            this.ControlBox = false;
            this.ResumeLayout(true);
        }

        //private void UnlockForm()
        //{
        //    foreach (Control ctrl in this.Controls)
        //    {
        //        ctrl.Enabled = true;
        //        this.ControlBox = true;
        //    }
        //}

        private void UnlockForm()
        {
            this.SuspendLayout();
            this.ControlBox = true;

            // Bulk enable all controls (faster than recursive)
            foreach (Control c in this.Controls)
            {
                c.Enabled = true;
            }

            this.ResumeLayout(true);
        }

        private void LoadProviderTypes()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ProviderType", typeof(string));
            dt.Rows.Add("-- Select Provider Type --"); // Placeholder
            dt.Rows.Add("HOTEL");
            dt.Rows.Add("TRANSPORT");
            dt.Rows.Add("GUIDE");

            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "ProviderType";
            comboBox1.SelectedIndex = 0; // Show placeholder
        }


        private void comboBox2_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            if (e.Index < 0) return;
            string text = comboBox2.GetItemText(comboBox2.Items[e.Index]);
            SizeF size = e.Graphics.MeasureString(text, comboBox2.Font);
            e.ItemHeight = (int)size.Height;
            e.ItemWidth = (int)size.Width;
        }

        private void comboBox2_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            if (e.Index >= 0)
            {
                string text = comboBox2.GetItemText(comboBox2.Items[e.Index]);
                using (SolidBrush brush = new SolidBrush(e.ForeColor))
                {
                    e.Graphics.DrawString(text, comboBox2.Font, brush, e.Bounds.Left, e.Bounds.Top);
                }
            }
            e.DrawFocusRectangle();
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

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            // Heavy operations here instead of Load
        }


        private void ClearForm()
        {
            ClearControlsRecursive(this);
        }

        //private void ClearControlsRecursive(System.Windows.Forms.Control parent)
        //{
        //    foreach (System.Windows.Forms.Control control in parent.Controls)
        //    {
        //        if (control is System.Windows.Forms.TextBox textBox)
        //        {
        //            textBox.Text = "";
        //            textBox.BackColor = System.Drawing.Color.White;
        //        }
        //        else if (control is System.Windows.Forms.ComboBox comboBox)
        //        {
        //            if (comboBox == comboBox1 || comboBox == comboBox2)
        //            {
        //                comboBox.SelectedIndex = 0; // Keep placeholder
        //            }
        //            else
        //            {
        //                comboBox.SelectedIndex = -1; // Clear others
        //            }
        //            comboBox.BackColor = System.Drawing.Color.White;
        //        }
        //        else if (control.HasChildren)
        //        {
        //            ClearControlsRecursive(control); // Recursively check children
        //        }
        //    }
        //}

        private void ClearControlsRecursive(System.Windows.Forms.Control parent)
        {
            foreach (System.Windows.Forms.Control control in parent.Controls)
            {
                if (control is System.Windows.Forms.TextBox textBox)
                {
                    textBox.Text = "";
                    textBox.BackColor = System.Drawing.Color.White;
                }
                else if (control is System.Windows.Forms.ComboBox comboBox)
                {
                    if (comboBox == comboBox1 || comboBox == comboBox2)
                    {
                        comboBox.SelectedIndex = 0; // Keep placeholder
                    }
                    else
                    {
                        comboBox.SelectedIndex = -1; // Clear others
                    }
                    comboBox.BackColor = System.Drawing.Color.White;
                }
                else if (control is System.Windows.Forms.Button button)
                {
                    // Reset button appearance (remove highlight)
                  //  button.FlatStyle = FlatStyle.Standard; // Reset if using FlatStyle
                  //  button.BackColor = SystemColors.Control; // Default WinForms button color
                    button.UseVisualStyleBackColor = true; // Revert to system style
                    button.FlatAppearance.BorderSize = 1; // Reset border if modified
                //    button.FlatAppearance.MouseOverBackColor = SystemColors.Control; // Reset hover effect
                 //   button.FlatAppearance.MouseDownBackColor = SystemColors.Control; // Reset click effect
                }
                else if (control.HasChildren)
                {
                    ClearControlsRecursive(control); // Recursively check children
                }
            }
        }

        private void LoadLocations()
        {
            string query = "SELECT LocationID, City, Country FROM LOCATION"; // Removed Description from the query

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Add column for combined text
                    dt.Columns.Add("DisplayText", typeof(string));
                    foreach (DataRow row in dt.Rows)
                    {
                        // Update to display only City and Country
                        row["DisplayText"] = $"{row["City"]}, {row["Country"]}";
                    }

                    // Insert placeholder
                    DataRow placeholder = dt.NewRow();
                    placeholder["LocationID"] = DBNull.Value;
                    placeholder["DisplayText"] = "-- Select Location --";
                    dt.Rows.InsertAt(placeholder, 0);

                    comboBox2.DisplayMember = "DisplayText";
                    comboBox2.ValueMember = "LocationID";
                    comboBox2.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("FAILED TO LOAD LOCATIONS:\n" + ex.Message, "DATABASE ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }




        private void Form1_Load(object sender, EventArgs e)
        {
            BeginInvoke((MethodInvoker)delegate 
            {
                textBox7.Focus();
                comboBox4.SelectedIndex = 0;
                comboBox6.SelectedIndex = 0;
            });
            //  TransportPanel.Visible = false;
            GuidePanel.Visible = false;
            HotelPanel.Visible = false;
            TransportPanel.Visible = false;
            
            comboBox3.SelectedIndex = 0; // Set placeholder as default
            comboBox4.SelectedIndex = 0;
            comboBox6.SelectedIndex = 0;
            //  comboBox1.Items.Clear();
            // comboBox1.Items.AddRange(new[] { "Hotel", "Guide", "Transport" });
            // comboBox1.SelectedIndex = -1;
            //  checkedListBox1.Items.Clear();
            //  checkedListBox1.Items.AddRange(new object[]
            //    {
            //"English", "Spanish", "French", "German", "Italian",
            //"Mandarin Chinese", "Arabic", "Russian", "Portuguese",
            //"Japanese", "Hindi", "Urdu", "Bengali", "Turkish",
            //"Korean", "Thai", "Dutch", "Greek", "Hebrew", "Malay",
            //"Vietnamese", "Swahili", "Filipino", "Farsi", "Polish",
            //"Other"
            //    });

            //    textBox8.Visible = false;      // hide the “Other language” textbox
            LoadLocations();
            ClearForm();
            textBox7.Focus();  // Set initial focus to  textbox
            textBox4.UseSystemPasswordChar = true;
            textBox5.UseSystemPasswordChar = true;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.DoubleBuffered = true; // Reduces flickering
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

        private void ValidatePasswords()
        {
            if (textBox4.Text != textBox5.Text)
            {
                textBox5.BackColor = Color.LightCoral;
            }
            else
            {
                textBox5.BackColor = Color.White;
            }

            if (textBox4.Text.Length < 8 || textBox4.Text.Length > 15)
            {
                textBox4.BackColor = Color.LightCoral;
            }
            else
            {
                textBox4.BackColor = Color.White;
            }
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            string input = textBox7.Text.Trim();

            if (input.Length < 8 || input.Length > 20)
            {
                textBox7.BackColor = Color.LightCoral;
            }
            else
            {
                textBox7.BackColor = Color.White;
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            string input = textBox6.Text.Trim();

            if (input.Length < 8 || input.Length > 25)
            {
                textBox6.BackColor = Color.LightCoral;
                return;
            }

            // Check uniqueness from database
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM SERVICE_PROVIDER WHERE LicenseNo = @license";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@license", input);

                try
                {
                    conn.Open();
                    int count = (int)cmd.ExecuteScalar();
                    if (count > 0)
                    {
                        textBox6.BackColor = Color.LightCoral;
                        MessageBox.Show("THIS LICENSE NUMBER ALREADY EXISTS. PLEASE ENTER  A UNIQUE LICENSENO.", "DUPLICATE LICENSENO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        textBox6.BackColor = Color.White;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR CHECKING LICENSENO: " + ex.Message);
                }
            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) &&
       !char.IsLetterOrDigit(e.KeyChar) &&
       e.KeyChar != '-')
            {
                e.Handled = true; // Block character
            }
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only letters, space, or control keys (e.g., Backspace)
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true; // Reject character
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string username = textBox1.Text.Trim();
            string userRole = "ServiceProvider"; // or dynamically set if needed

            if (username.Length < 8 || username.Length > 20)
            {
                textBox1.BackColor = Color.LightCoral;
                return;
            }

            // Check uniqueness
            using (SqlConnection conn = new SqlConnection("Data Source=CODE-TECH\\SQLEXPRESS;Initial Catalog=TravelEaseDB;Integrated Security=True;"))
            {
                string query = "SELECT COUNT(*) FROM AppUsers WHERE UserName = @UserName AND UserRole = @UserRole";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserName", username);
                    cmd.Parameters.AddWithValue("@UserRole", userRole);

                    conn.Open();
                    int count = (int)cmd.ExecuteScalar();

                    if (count > 0)
                    {
                        textBox1.BackColor = Color.LightCoral;
                        MessageBox.Show("USERNAME ALREADY EXISTS FOR ROLE SERVICE PROVIDER", "DUPLICATE USERNAME", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        textBox1.BackColor = Color.White;
                    }
                }
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

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string email = textBox2.Text.Trim();

            if (email.Length > 20 || email.Length < 8 || !email.Contains("@") || !email.Contains("."))
            {
                textBox2.BackColor = Color.LightCoral;
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM AppUsers WHERE Email = @Email";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    conn.Open();
                    int count = (int)cmd.ExecuteScalar();

                    if (count > 0)
                    {
                        textBox2.BackColor = Color.LightCoral;
                        MessageBox.Show("EMAIL IS ALREADY REGISTERED FOR ROLE SERVICE PROVIDER.", "DUPLICATE EMAIL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        textBox2.BackColor = Color.White;
                    }
                }
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            // Allow only:
            // - lowercase letters a-z
            // - digits 0-9
            // - @ and .
            // - control keys like Backspace
            if (!char.IsControl(ch) &&
                !(ch >= 'a' && ch <= 'z') &&
                !(ch >= '0' && ch <= '9') &&
                ch != '@' && ch != '.')
            {
                e.Handled = true; // Reject invalid characters
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            string contact = textBox3.Text.Trim();

            if (contact.Length != 11)
            {
                textBox3.BackColor = Color.LightCoral;
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM AppUsers WHERE ContactNumber = @ContactNumber";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ContactNumber", contact);
                    conn.Open();
                    int count = (int)cmd.ExecuteScalar();

                    if (count > 0)
                    {
                        textBox3.BackColor = Color.LightCoral;
                        MessageBox.Show("CONTACT NUMBER IS ALREADY IN USE FOR ROLE SERVICE PROVIDER.", "DUPLICATE CONTACT", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        textBox3.BackColor = Color.White;
                    }
                }
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only digits or backspace
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Reject input
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            ValidatePasswords();
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar))
            {
                e.Handled = true; // Block non-alphanumeric characters
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            ValidatePasswords();
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar))
            {
                e.Handled = true; // Block non-alphanumeric characters
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //private void button1_Click_1(object sender, EventArgs e)
        //{
        //    // Reset comboBox1 to placeholder
        //    comboBox1.SelectedIndex = 0;
        //    comboBox3.SelectedIndex = 0;

        //    // Clear all checked items in checkedListBox1
        //    for (int i = 0; i < checkedListBox1.Items.Count; i++)
        //    {
        //        checkedListBox1.SetItemChecked(i, false);
        //    }

        //    ClearForm(); // Clear form fields recursively
        //    _parentForm.Show();  // Show Form1
        //    this.Close();
        //}

        private void button1_Click_1(object sender, EventArgs e)
        {
            // Check if any fields have data
            bool hasData = false;

            // Check text boxes (assuming you want to check for non-empty text)
            foreach (Control control in this.Controls)
            {
                if (control is System.Windows.Forms.TextBox textBox && !string.IsNullOrEmpty(textBox.Text))
                {
                    hasData = true;
                    break;
                }

                if (control is System.Windows.Forms.ComboBox comboBox && comboBox.SelectedIndex > 0) // Assuming 0 is the placeholder
                {
                    hasData = true;
                    break;
                }
            }

            // Check checkedListBox1 for any checked items
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.GetItemChecked(i))
                {
                    hasData = true;
                    break;
                }
            }

            // If there's data, show confirmation dialog
            if (hasData)
            {
                DialogResult result = MessageBox.Show(
                    "THIS WILL CLEAR THE FORM.DO YOU WANT TO CONTINUE ? ",
                    "CONFIRM CLEAR",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning);

                if (result != DialogResult.OK)
                {
                    return; // User canceled, do nothing
                }
            }

            // Proceed with clearing if no data or user confirmed
            comboBox1.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;

            // Clear all checked items in checkedListBox1
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, false);
            }

            ClearForm(); // Clear form fields recursively
            _parentForm.Show();  // Show Form1
            this.Close();
        }



        private void button2_Click(object sender, EventArgs e)
        {
            // Validate each field with specific feedback
            if (textBox7.BackColor == Color.LightCoral || string.IsNullOrWhiteSpace(textBox7.Text) || textBox7.Text.Trim().Length < 8 || textBox7.Text.Trim().Length > 20)
            {
                MessageBox.Show("PROVIDER NAME IS INVALID. IT SHOULD BE 8-20 CHARACTERS LONG.", "INVALID PROVIDER NAME", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (textBox6.BackColor == Color.LightCoral || string.IsNullOrWhiteSpace(textBox6.Text) || textBox6.Text.Trim().Length < 8)
            {
                MessageBox.Show("LICENSE NUMBER IS INVALID. IT SHOULD BE AT LEAST 8 CHARACTERS.", "INVALID LICENSE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (textBox1.BackColor == Color.LightCoral || string.IsNullOrWhiteSpace(textBox1.Text) || textBox1.Text.Trim().Length < 8 || textBox1.Text.Trim().Length > 20)
            {
                MessageBox.Show("USERNAME IS INVALID. IT SHOULD BE 8-20 CHARACTERS LONG AND UNIQUE.", "INVALID USERNAME", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (textBox2.BackColor == Color.LightCoral || string.IsNullOrWhiteSpace(textBox2.Text) || !IsValidEmail(textBox2.Text.Trim()))
            {
                MessageBox.Show("EMAIL IS INVALID. PLEASE USE A VALID EMAIL FORMAT LIKE USER@EXAMPLE.COM.", "INVALID EMAIL", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (textBox3.BackColor == Color.LightCoral || string.IsNullOrWhiteSpace(textBox3.Text) || textBox3.Text.Trim().Length != 11 || !textBox3.Text.Trim().All(char.IsDigit))
            {
                MessageBox.Show("CONTACT NUMBER IS INVALID. IT MUST BE 11 DIGITS LONG.", "INVALID CONTACT", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (textBox4.BackColor == Color.LightCoral || string.IsNullOrWhiteSpace(textBox4.Text) || textBox4.Text.Trim().Length < 8)
            {
                MessageBox.Show("PASSWORD IS INVALID. IT SHOULD BE AT LEAST 8 CHARACTERS.", "INVALID PASSWORD", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (textBox5.BackColor == Color.LightCoral || string.IsNullOrWhiteSpace(textBox5.Text))
            {
                MessageBox.Show("CONFIRM PASSWORD IS INVALID. IT SHOULD MATCH THE PASSWORD.", "INVALID CONFIRM PASSWORD", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (textBox4.Text.Trim() != textBox5.Text.Trim())
            {
                MessageBox.Show("PASSWORD AND CONFIRM PASSWORD DO NOT MATCH.", "MISMATCHED PASSWORDS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (comboBox1.SelectedIndex == 0 || string.IsNullOrWhiteSpace(comboBox1.Text))
            {
                MessageBox.Show("PLEASE SELECT A VALID PROVIDER TYPE (HOTEL, TRANSPORT, GUIDE).", "MISSING PROVIDER TYPE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (comboBox2.SelectedIndex == 0 || comboBox2.SelectedValue == null)
            {
                MessageBox.Show("PLEASE SELECT A LOCATION.", "MISSING LOCATION", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Additional validation for HOTEL provider type
            string providerType = comboBox1.Text.ToUpper(); // Normalize to handle case sensitivity
            if (providerType == "HOTEL")
            {
                if (string.IsNullOrWhiteSpace(textBox9.Text) || textBox9.Text.Trim().Length < 3)
                {
                    MessageBox.Show("HOTEL NAME IS INVALID. IT MUST BE AT LEAST 3 CHARACTERS LONG.", "INVALID HOTEL NAME", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // Additional validation for GUIDE provider type
            if (providerType == "GUIDE")
            {
                if (comboBox3.SelectedItem == null || string.IsNullOrWhiteSpace(comboBox3.SelectedItem.ToString()))
                {
                    MessageBox.Show("GUIDE SPECIALIZATION IS INVALID. PLEASE SELECT A VALID SPECIALIZATION.", "INVALID SPECIALIZATION", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (checkedListBox1.CheckedItems.Count == 0)
                {
                    MessageBox.Show("GUIDE LANGUAGES ARE INVALID. PLEASE SELECT AT LEAST ONE LANGUAGE.", "INVALID LANGUAGES", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string languages = string.Join(", ", checkedListBox1.CheckedItems.Cast<string>());
                if (languages.Length < 2)
                {
                    MessageBox.Show("GUIDE LANGUAGES ARE INVALID. THE COMBINED LANGUAGE STRING MUST BE AT LEAST 2 CHARACTERS LONG.", "INVALID LANGUAGES", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // All validations passed
            string username = textBox1.Text.Trim();
            string email = textBox2.Text.Trim();
            string contact = textBox3.Text.Trim();
            string password = textBox4.Text.Trim();
            string licenseNo = textBox6.Text.Trim();
            string providerName = textBox7.Text.Trim();
            int locationID;
            try
            {
                locationID = Convert.ToInt32(comboBox2.SelectedValue);
            }
            catch
            {
                MessageBox.Show("INVALID LOCATION SELECTED.", "INVALID LOCATION", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string hashedPassword = ComputeSha256Hash(password);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    Console.WriteLine($"Connection State: {conn.State}");
                    SqlTransaction transaction = conn.BeginTransaction(); // BEGIN TRANSACTION

                    try
                    {
                        // Insert into AppUsers
                        string insertUserQuery = @"
                    INSERT INTO AppUsers (UserName, UserPassword, ContactNumber, Email, UserRole)
                    VALUES (@username, @password, @contact, @email, 'ServiceProvider');
                    SELECT SCOPE_IDENTITY();";

                        SqlCommand cmd = new SqlCommand(insertUserQuery, conn, transaction);
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", hashedPassword);
                        cmd.Parameters.AddWithValue("@contact", contact);
                        cmd.Parameters.AddWithValue("@email", email);

                        object result = cmd.ExecuteScalar();
                        if (result == null)
                            throw new Exception("Failed to insert AppUser.");

                        int newUserId = Convert.ToInt32(result);
                        Console.WriteLine($"Inserted AppUser with UserID: {newUserId}");

                        // Insert into SERVICE_PROVIDER
                        string insertProviderQuery = @"
                    INSERT INTO SERVICE_PROVIDER (LicenseNo, ProviderName, ProviderType, UserID, LocationID)
                    VALUES (@license, @name, @type, @userid, @locationid);";

                        SqlCommand spCmd = new SqlCommand(insertProviderQuery, conn, transaction);
                        spCmd.Parameters.AddWithValue("@license", licenseNo);
                        spCmd.Parameters.AddWithValue("@name", providerName);
                        spCmd.Parameters.AddWithValue("@type", providerType);
                        spCmd.Parameters.AddWithValue("@userid", newUserId);
                        spCmd.Parameters.AddWithValue("@locationid", locationID);

                        Console.WriteLine($"SERVICE_PROVIDER Query: {insertProviderQuery}");
                        Console.WriteLine($"Parameters: license={licenseNo}, name={providerName}, type={providerType}, userid={newUserId}, locationid={locationID}");

                        int providerRows = spCmd.ExecuteNonQuery();
                        Console.WriteLine($"SERVICE_PROVIDER Rows Affected: {providerRows}");
                        if (providerRows == 0)
                            throw new Exception("FAILED TO INSERT SERVICE PROVIDER.");

                        bool additionalTableInserted = false;

                        // Guide-specific logic
                        if (providerType == "GUIDE")
                        {
                            string specialization = comboBox3.SelectedItem.ToString();
                            List<string> selectedLanguages = checkedListBox1.CheckedItems.Cast<string>().ToList();
                            string languages = string.Join(", ", selectedLanguages);

                            string insertGuideQuery = @"
                        INSERT INTO GUIDE (Specialization, Language, LicenseNo)
                        VALUES (@specialization, @languages, @license);";

                            SqlCommand guideCmd = new SqlCommand(insertGuideQuery, conn, transaction);
                            guideCmd.Parameters.AddWithValue("@specialization", specialization);
                            guideCmd.Parameters.AddWithValue("@languages", languages);
                            guideCmd.Parameters.AddWithValue("@license", licenseNo);

                            Console.WriteLine($"GUIDE Query: {insertGuideQuery}");
                            Console.WriteLine($"Parameters: specialization={specialization}, languages={languages}, license={licenseNo}");

                            int guideRows;
                            try
                            {
                                guideRows = guideCmd.ExecuteNonQuery();
                                Console.WriteLine($"GUIDE Rows Affected: {guideRows}");
                            }
                            catch (SqlException sqlEx)
                            {
                                throw new Exception($"SQL Error inserting GUIDE details: {sqlEx.Message}", sqlEx);
                            }
                            if (guideRows == 0)
                                throw new Exception("FAILED TO INSERT GUIDE DETAILS.");

                            additionalTableInserted = true;
                        }
                        else if (providerType == "HOTEL")
                        {
                            string hotelName = textBox9.Text.Trim();

                            string insertHotelQuery = @"
                        INSERT INTO HOTEL (HotelName, LicenseNo)
                        VALUES (@hotelName, @license);";

                            SqlCommand hotelCmd = new SqlCommand(insertHotelQuery, conn, transaction);
                            hotelCmd.Parameters.AddWithValue("@hotelName", hotelName);
                            hotelCmd.Parameters.AddWithValue("@license", licenseNo);

                            Console.WriteLine($"HOTEL Query: {insertHotelQuery}");
                            Console.WriteLine($"Parameters: hotelName={hotelName}, license={licenseNo}");

                            int hotelRows;
                            try
                            {
                                hotelRows = hotelCmd.ExecuteNonQuery();
                                Console.WriteLine($"HOTEL Rows Affected: {hotelRows}");
                            }
                            catch (SqlException sqlEx)
                            {
                                throw new Exception($"SQL Error inserting HOTEL details: {sqlEx.Message}", sqlEx);
                            }
                            if (hotelRows == 0)
                                throw new Exception("FAILED TO INSERT HOTEL DETAILS.");

                            additionalTableInserted = true;
                        }
                        else if (providerType == "TRANSPORT")
                        {
                            string vehicleType = comboBox6.SelectedItem.ToString();
                            int capacity = int.Parse(comboBox4.SelectedItem.ToString());

                            string insertTransportQuery = @"
        INSERT INTO TRANSPORT (VehicleType, Capacity, LicenseNo)
        VALUES (@vehicleType, @capacity, @license);";

                            SqlCommand transportCmd = new SqlCommand(insertTransportQuery, conn, transaction);
                            transportCmd.Parameters.AddWithValue("@vehicleType", vehicleType);
                            transportCmd.Parameters.AddWithValue("@capacity", capacity);
                            transportCmd.Parameters.AddWithValue("@license", licenseNo);

                            Console.WriteLine($"TRANSPORT Query: {insertTransportQuery}");
                            Console.WriteLine($"Parameters: vehicleType={vehicleType}, capacity={capacity}, license={licenseNo}");

                            int transportRows;
                            try
                            {
                                transportRows = transportCmd.ExecuteNonQuery();
                                Console.WriteLine($"TRANSPORT Rows Affected: {transportRows}");
                            }
                            catch (SqlException sqlEx)
                            {
                                throw new Exception($"SQL ERROR INSERTING TRANSPORT DETAILS: {sqlEx.Message}", sqlEx);
                            }

                            if (transportRows == 0)
                                throw new Exception("FAILED TO INSERT TRANSPORT DETAILS.");

                            additionalTableInserted = true;
                        }

                        // Ensure all required tables were updated
                        if ((providerType == "HOTEL" || providerType == "GUIDE") && !additionalTableInserted)
                        {
                            throw new Exception($"FAILED TO INSERT DATA INTO {(providerType == "HOTEL" ? "HOTEL" : "GUIDE")} TABLE.");
                        }

                        transaction.Commit(); // ✅ COMMIT if everything is successful
                        Console.WriteLine("TRANSACTION COMMITTED.");
                        MessageBox.Show("SERVICE PROVIDER REGISTERED SUCCESSFULLY! PENDING FOR APPROAL", "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback(); // ❌ ROLLBACK on any error
                        Console.WriteLine($"TRANSACTION ROLLED BACK. ERROR: {ex.Message}");
                        MessageBox.Show($"TRANSACTION FAILED:\n{ex.Message}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"DATABASE CONNECTION FAILED:\n{ex.Message}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Helper method for email validation
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void ServiceProviderSignUP_Leave_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label26_Click(object sender, EventArgs e)
        {

        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        //private void textBox8_TextChanged(object sender, EventArgs e)
        //{
        //    string input = textBox8.Text.Trim();

        //    if (input.Length < 8 || input.Length > 20)
        //    {
        //        textBox8.BackColor = Color.LightCoral; // Invalid length
        //    }
        //    else
        //    {
        //        textBox8.BackColor = Color.White; // Valid
        //    }
        //}

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true; // Reject the input
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Hide all panels
          //  HotelPanel.Visible = false;
            //GuidePanel.Visible = false;
            //TransportPanel.Visible = false;
            //textBox8.Text = "";
            //textBox8.BackColor = Color.White; // Optional: reset background color

            // Re-enable the rest of the form
            foreach (Control ctrl in this.Controls)
            {
                ctrl.Enabled = true;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Hide GuidePanel and unlock form by default
            GuidePanel.Visible = false;
            HotelPanel.Visible = false;
            TransportPanel.Visible = false;
            UnlockForm();

            // Check if a valid item is selected (not null and not the placeholder)
            if (comboBox1.SelectedItem != null && comboBox1.SelectedIndex > 0)
            {
                DataRowView selectedRow = comboBox1.SelectedItem as DataRowView;
                if (selectedRow != null)
                {
                    string selectedType = selectedRow["ProviderType"].ToString();
                    if (selectedType == "GUIDE")
                    {
                        GuidePanel.Visible = true;
                        LockFormExceptGuidePanel();
                    }
                    else if(selectedType=="HOTEL")
                    {
                        HotelPanel.Visible=true;
                        textBox9.Focus(); // Keep focus on the textbox
                        LockFormExceptHotelPanel();
                    }
                    else if(selectedType=="TRANSPORT")
                    {
                        TransportPanel.Visible = true;
                        LockFormExceptTransportPanel();
                    }
                }
            }
        }


        private void HotelPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        

        private void button8_Click(object sender, EventArgs e)
        {
            // Hide all panels
         //   HotelPanel.Visible = false;
         //   GuidePanel.Visible = false;
         //   TransportPanel.Visible = false;

         //   comboBox5.SelectedIndex = -1;
         //   comboBox6.SelectedIndex = -1;

            // Re-enable the rest of the form
            foreach (Control ctrl in this.Controls)
            {
                ctrl.Enabled = true;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //HotelPanel.Visible = false;
            //GuidePanel.Visible = false;
            //TransportPanel.Visible = false;

            // Re-enable the rest of the form
            foreach (Control ctrl in this.Controls)
            {
                ctrl.Enabled = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //HotelPanel.Visible = false;
            //GuidePanel.Visible = false;
            //TransportPanel.Visible = false;

            // Re-enable the rest of the form
            foreach (Control ctrl in this.Controls)
            {
                ctrl.Enabled = true;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //HotelPanel.Visible = false;
            //GuidePanel.Visible = false;
            //TransportPanel.Visible = false;

            // Re-enable the rest of the form
            foreach (Control ctrl in this.Controls)
            {
                ctrl.Enabled = true;
            }
        }


        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void button6_Click(object sender, EventArgs e)
        {
            bool hasComboBoxSelection = comboBox3.SelectedIndex > 0;
            bool hasCheckedLanguages = checkedListBox1.CheckedItems.Count > 0;

            bool shouldConfirm = hasComboBoxSelection || hasCheckedLanguages;

            DialogResult result = DialogResult.OK;

            if (shouldConfirm)
            {
                result = MessageBox.Show(
                    "THIS WILL CLEAR THE FORM. DO YOU WANT TO CONTINUE?",
                    "CONFIRM CLEAR",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning
                );
            }

            if (result == DialogResult.OK)
            {
                GuidePanel.Visible = false;

                UnlockForm();

                // Reset combo boxes
                comboBox1.SelectedIndex = 0;
               // comboBox2.SelectedIndex = 0;
                comboBox3.SelectedIndex = 0;

                //// Clear textboxes
                //foreach (TextBox tb in this.Controls.OfType<TextBox>())
                //{
                //    tb.Text = "";
                //    tb.BackColor = Color.White;
                //}

                // Clear checked items
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    checkedListBox1.SetItemChecked(i, false);
                }
                checkedListBox1.ClearSelected();
            }
            else
            {
                return;
            }
        }



        private void GuidePanel_Paint(object sender, PaintEventArgs e)
        {

        }


        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                e.Handled = true; // Reject non-letter input
            }
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click_2(object sender, EventArgs e)
        {
            bool hasChecked = checkedListBox1.CheckedItems.Count > 0;
            bool hasValidComboSelection = comboBox3.SelectedIndex > 0; // assuming index 0 is placeholder

            checkedListBox1.ClearSelected();

            if (!hasChecked && !hasValidComboSelection)
            {
                MessageBox.Show("PLEASE SELECT AT LEAST ONE LANGUAGE AND ONE SPECIALIZATION.", "INPUT REQUIRED", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!hasChecked)
            {
                MessageBox.Show("PLEASE SELECT AT LEAST ONE LANGUAGE.", "LANGUAGE REQUIRED", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!hasValidComboSelection)
            {
                MessageBox.Show("PLEASE SELECT A SPECIALIZATION FROM THE LIST.", "SPECIALIZATION REQUIRED", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // If everything is valid, proceed
            checkedListBox1.ClearSelected();
            comboBox4.SelectedIndex = 0;
            comboBox6.SelectedIndex = 0;
            textBox9.Text = "";
            GuidePanel.Visible = false;
            UnlockForm();
        }


        private void checkedListBox1_ItemCheck_1(object sender, ItemCheckEventArgs e)
        {
            // Count how many items are already checked
            int checkedCount = checkedListBox1.CheckedItems.Count;

            // If user is trying to check a new item (not unchecking), increase count
            if (e.NewValue == CheckState.Checked)
                checkedCount++;

            // If the count exceeds 5, cancel the check and show an error
            if (checkedCount > 5)
            {
                MessageBox.Show("You can select a maximum of 5 languages.", "Selection Limit", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.NewValue = CheckState.Unchecked;
            }
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            string hotelName = textBox9.Text.Trim();
            string licenseNo = textBox6.Text.Trim(); // Assuming textBox6 holds LicenseNo

            // Check minimum length
            if (hotelName.Length < 8 || hotelName.Length>20)
            {
                textBox9.BackColor = Color.LightCoral;
                return;
            }
            else
            {
                textBox9.BackColor = Color.White;
            }

            // Check if both HotelName and LicenseNo are present before querying
            if (hotelName.Length >= 8 && licenseNo.Length <= 20)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();

                        string checkQuery = @"
SELECT COUNT(*) FROM HOTEL
WHERE HotelName = @hotelName AND LicenseNo = @licenseNo;";

                        using (SqlCommand cmd = new SqlCommand(checkQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@hotelName", hotelName);
                            cmd.Parameters.AddWithValue("@licenseNo", licenseNo);

                            int count = (int)cmd.ExecuteScalar();

                            if (count > 0)
                            {
                                textBox9.BackColor = Color.LightCoral;
                                MessageBox.Show(
                                    "THIS HOTEL NAME ALREADY EXISTS FOR THE PROVIDED LICENSE NUMBER.",
                                    "DUPLICATE HOTEL ENTRY",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning
                                );
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Database error during hotel validation:\n" + ex.Message,
                            "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }


        private void button3_Click_1(object sender, EventArgs e)
        {
            // Check if textBox9 has any text (after trimming whitespace)
            if (!string.IsNullOrWhiteSpace(textBox9.Text))
            {
                // Show confirmation dialog
                DialogResult result = MessageBox.Show(
                    "DO YOU WANT TO CLEAR THE ENTERED TEXT?",
                    "CLEAR CONFIRMATION",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                // If user clicks Yes, clear the textbox
                // Consider focusing the textbox after clearing
                if (result == DialogResult.Yes)
                {
                    textBox9.Text = "";
                    textBox9.BackColor = Color.White;
                    HotelPanel.Visible = false;
                    UnlockForm();
                    comboBox1.SelectedIndex = 0;
                }
                else
                {
                    textBox9.Focus(); // Keep focus on the textbox
                    return;
                }
            }
            else
            {
                HotelPanel.Visible = false;
                comboBox1.SelectedIndex = 0;
                UnlockForm();
            }
          
        }

        private void textBox9_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) &&
       !char.IsControl(e.KeyChar) &&
       e.KeyChar != ' ')
            {
                e.Handled = true; // Block the input
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox9.Text) || textBox9.BackColor == Color.LightCoral)
            {
                MessageBox.Show("PLEASE ENTER VALID TEXT IN THE FIELD (8-20 ALPHABETIC CHARACTERS WITH SPACES ONLY).",
                               "INVALID INPUT",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Warning);
                textBox9.Focus(); // Set focus back to the textbox
                return;
            }
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, false);
            }
            checkedListBox1.ClearSelected();
            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
            comboBox6.SelectedIndex = 0;
            HotelPanel.Visible = false;
            UnlockForm();
        }

        private void HotelPanel_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void TransportPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            // Check if either comboBox has a selected value
            if (comboBox4.SelectedIndex != 0 || comboBox6.SelectedIndex != 0)
            {
                // Show warning message
                DialogResult result = MessageBox.Show(
                    "ALL INFORMATION WILL BE CLEARED IF YOU PROCEED. CONTINUE?",
                    "WARNING",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.No)
                {
                    return; // User canceled the operation
                }
                else
                {
                    comboBox4.SelectedIndex = 0; 
                    comboBox6.SelectedIndex = 0;
                    comboBox1.SelectedIndex = 0;

                    TransportPanel.Visible = false;
                    UnlockForm();
                }
                // If user clicks Yes, continue with clearing operation
                // (Add your clearing logic here)
            }
            else
            {
                comboBox4.SelectedIndex = 0;
                comboBox6.SelectedIndex = 0;
                comboBox1.SelectedIndex = 0;

                TransportPanel.Visible = false;
                UnlockForm();
                // No values in combo boxes, just return
            }
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            bool hasChecked_1 = comboBox4.SelectedIndex > 0; // assuming index 0 is placeholder
            bool hasChecked_2 = comboBox6.SelectedIndex > 0; // assuming index 0 is placeholder

            checkedListBox1.ClearSelected();

            if (!hasChecked_1 && !hasChecked_2)
            {
                MessageBox.Show("PLEASE SELECT AT LEAST ONE VEHICLE TYPE AND VEHICLE CAPACITY.", "INPUT REQUIRED", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!hasChecked_2)
            {
                MessageBox.Show("PLEASE SELECT AT LEAST ONE VEHICLE TYPE.", "VEHICLE TYPE REQUIRED", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!hasChecked_1)
            {
                MessageBox.Show("PLEASE SELECT A VEHICLE CAPACITY FROM THE LIST.", "VEHICLE CAPACITY REQUIRED", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            comboBox3.SelectedIndex = 0;
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, false);
            }
            checkedListBox1.ClearSelected();
            textBox9.Text = "";
            TransportPanel.Visible = false;
            UnlockForm();
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
