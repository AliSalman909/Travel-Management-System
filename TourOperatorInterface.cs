using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TravelEaseDB
{
    public partial class TourOperatorInterface : Form
    {


        private AppUser_Form _parentForm;  // Declare a variable to hold the parent Form1 instance
        string connectionString = "Data Source=CODE-TECH\\SQLEXPRESS;Initial Catalog=TravelEaseDB;Integrated Security=True;";

        public TourOperatorInterface(AppUser_Form parent)
        {
            InitializeComponent();

            this._parentForm = parent;

          //  SignINForm form1= new SignINForm(this._parentForm);
            this.WindowState = FormWindowState.Maximized;
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.UserPaint, true);
            this.UpdateStyles();

            dateTimePicker1.MinDate = DateTime.Today; // Restrict to future dates

            this.MinimizeBox = false; // Disable minimize button
            this.MaximizeBox = false; // Disable maximize button

            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            panel6.Visible = false;
            panel7.Visible = false;

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToResizeRows = false; // Lock row height
            dataGridView1.AllowUserToResizeColumns = false; // Lock column width
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells; // Auto-size columns
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells; // Auto-size rows
            dataGridView1.RowHeadersVisible = false; // Hide row header arrow

            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.ReadOnly = true;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.AllowUserToResizeRows = false; // Lock row height
            dataGridView2.AllowUserToResizeColumns = false; // Lock column width
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells; // Auto-size columns
            dataGridView2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells; // Auto-size rows
            dataGridView2.RowHeadersVisible = false; // Hide row header arrow

            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.ReadOnly = true;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.AllowUserToResizeRows = false; // Lock row height
            dataGridView2.AllowUserToResizeColumns = false; // Lock column width
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells; // Auto-size columns
            dataGridView2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells; // Auto-size rows
            dataGridView2.RowHeadersVisible = false; // Hide row header arrow


            dataGridView3.AllowUserToAddRows = false;
            dataGridView3.ReadOnly = true;
            dataGridView3.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView3.AllowUserToResizeRows = false; // Lock row height
            dataGridView3.AllowUserToResizeColumns = false; // Lock column width
            dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells; // Auto-size columns
            dataGridView3.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells; // Auto-size rows
            dataGridView3.RowHeadersVisible = false; // Hide row header arrow


            dataGridView4.AllowUserToAddRows = false;
            dataGridView4.ReadOnly = true;
            dataGridView4.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView4.AllowUserToResizeRows = false; // Lock row height
            dataGridView4.AllowUserToResizeColumns = false; // Lock column width
            dataGridView4.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells; // Auto-size columns
            dataGridView4.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells; // Auto-size rows
            dataGridView4.RowHeadersVisible = false; // Hide row header arrow

        }

        private void LoadCategories()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Query to select CategoryID and CategoryName
                    string query = "SELECT CategoryID, CategoryName FROM CATEGORY ORDER BY CategoryName";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);

                    // Create a DataTable to hold the results
                    DataTable categories = new DataTable();
                    adapter.Fill(categories);

                    // Bind data to ComboBox
                    comboBox1.DataSource = categories;
                    comboBox1.DisplayMember = "CategoryName"; // What users see
                    comboBox1.ValueMember = "CategoryID";    // Underlying value
                    comboBox1.SelectedIndex = -1;            // Optional: No default selection
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void TourOperatorInterface_Load(object sender, EventArgs e)
        {
            ClearForm();
            LoadCategories();
            LoadServiceProviders();
        }

        private void LoadServiceProviders()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT UserID, ProviderName FROM SERVICE_PROVIDER ORDER BY ProviderName";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable providers = new DataTable();
                    adapter.Fill(providers);

                    comboBox2.DataSource = providers;
                    comboBox2.DisplayMember = "ProviderName";
                    comboBox2.ValueMember = "UserID";
                    comboBox2.SelectedIndex = -1;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void button5_Click(object sender, EventArgs e)
        {
            _parentForm.Show();
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string input = textBox1.Text.Trim();

            if (input.Length < 8 || input.Length > 20)
            {
                textBox1.BackColor = Color.LightCoral;
            }
            else
            {
                textBox1.BackColor = Color.White;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only letters, space, or control keys (e.g., Backspace)
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true; // Reject character
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string input = textBox1.Text.Trim();

            if (input.Length < 0 || input.Length > 10)
            {
                textBox1.BackColor = Color.LightCoral;
            }
            else
            {
                textBox1.BackColor = Color.White;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only letters, space, or control keys (e.g., Backspace)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true; // Reject character
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

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
                else if (control is DateTimePicker)
                {
                    ((DateTimePicker)control).Value = DateTime.Today; // Reset to today's date
                }
                else if (control is ComboBox)
                {
                    ((ComboBox)control).SelectedIndex = -1; // Clear selection
                                                            // Optional: ((ComboBox)control).DataSource = null; // Clear items if needed
                }
            }
        }



        private void button9_Click(object sender, EventArgs e)
        {
            ClearForm();
            UnlockForm();
            panel1.Visible = false;

        }

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
        private void LockFormExceptPanel1()
        {
            this.SuspendLayout();

            foreach (Control ctrl in this.Controls)
            {
                ctrl.Enabled = (ctrl == panel1);
            }

            // Enable all children of TransportPanel
            foreach (Control child in panel1.Controls)
            {
                child.Enabled = true;
            }

            this.ControlBox = false;
            this.ResumeLayout(true);
        }


        private void LockFormExceptPanel3()
        {
            this.SuspendLayout();

            foreach (Control ctrl in this.Controls)
            {
                ctrl.Enabled = (ctrl == panel3);
            }

            // Enable all children of TransportPanel
            foreach (Control child in panel3.Controls)
            {
                child.Enabled = true;
            }

            this.ControlBox = false;
            this.ResumeLayout(true);
        }

        private void LockFormExceptPanel7()
        {
            this.SuspendLayout();

            foreach (Control ctrl in this.Controls)
            {
                ctrl.Enabled = (ctrl == panel7);
            }

            // Enable all children of TransportPanel
            foreach (Control child in panel7.Controls)
            {
                child.Enabled = true;
            }

            this.ControlBox = false;
            this.ResumeLayout(true);
        }


        private void LockFormExceptPanel2()
        {
            this.SuspendLayout();

            foreach (Control ctrl in this.Controls)
            {
                ctrl.Enabled = (ctrl == panel2);
            }

            // Enable all children of TransportPanel
            foreach (Control child in panel2.Controls)
            {
                child.Enabled = true;
            }

            this.ControlBox = false;
            this.ResumeLayout(true);
        }

        private void LockFormExceptPanel4()
        {
            this.SuspendLayout();

            foreach (Control ctrl in this.Controls)
            {
                ctrl.Enabled = (ctrl == panel4);
            }

            // Enable all children of TransportPanel
            foreach (Control child in panel4.Controls)
            {
                child.Enabled = true;
            }

            this.ControlBox = false;
            this.ResumeLayout(true);
        }

        private void LockFormExceptPanel5()
        {
            this.SuspendLayout();

            foreach (Control ctrl in this.Controls)
            {
                ctrl.Enabled = (ctrl == panel5);
            }

            // Enable all children of TransportPanel
            foreach (Control child in panel5.Controls)
            {
                child.Enabled = true;
            }

            this.ControlBox = false;
            this.ResumeLayout(true);
        }



        private void LockFormExceptPanel6()
        {
            this.SuspendLayout();

            foreach (Control ctrl in this.Controls)
            {
                ctrl.Enabled = (ctrl == panel6);
            }

            // Enable all children of TransportPanel
            foreach (Control child in panel6.Controls)
            {
                child.Enabled = true;
            }

            this.ControlBox = false;
            this.ResumeLayout(true);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            LockFormExceptPanel1();
        }

        private void LoadBookings()
        {
            int tourOperatorId = SignINForm.LoggedInUserID;

            if (tourOperatorId <= 0)
            {
                MessageBox.Show("Invalid Tour Operator ID. Please log in again.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"SELECT b.BookingID, t.TripID, t.Title 
                            FROM BOOKINGS b 
                            INNER JOIN TRIP t ON b.TripID = t.TripID 
                            WHERE t.TourOperatorID = @TourOperatorID 
                            ORDER BY b.BookingID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TourOperatorID", tourOperatorId);
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable bookings = new DataTable();
                            adapter.Fill(bookings);

                            dataGridView4.DataSource = bookings;

                            // Customize column headers
                            dataGridView4.Columns["BookingID"].HeaderText = "Booking ID";
                            dataGridView4.Columns["TripID"].HeaderText = "Trip ID";
                            dataGridView4.Columns["Title"].HeaderText = "Trip Name";

                            // Ensure the Cancel button column is visible
                            if (!dataGridView4.Columns.Contains("CancelButton"))
                            {
                                DataGridViewButtonColumn cancelButtonColumn = new DataGridViewButtonColumn
                                {
                                    Name = "CancelButton",
                                    HeaderText = "Action",
                                    Text = "Cancel",
                                    UseColumnTextForButtonValue = true
                                };
                                dataGridView4.Columns.Add(cancelButtonColumn);
                            }

                            dataGridView4.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            int UserID= SignINForm.LoggedInUserID;

            // Step 1: Retrieve and validate input
            string title = textBox1.Text.Trim();
            if (string.IsNullOrEmpty(title) || textBox1.BackColor==Color.LightCoral)
            {
                MessageBox.Show("Title cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (title.Length > 100 || textBox1.BackColor == Color.LightCoral)
            {
                MessageBox.Show("Title cannot exceed 20 characters.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!decimal.TryParse(textBox2.Text.Trim(), out decimal price) || price < 0 || textBox2.BackColor == Color.LightCoral)
            {
                MessageBox.Show("Price must be a valid non-negative number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DateTime tripDate = dateTimePicker1.Value;

            if (comboBox1.SelectedValue == null || !int.TryParse(comboBox1.SelectedValue.ToString(), out int categoryId))
            {
                MessageBox.Show("Please select a valid category.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Step 2: Insert data into TRIP table
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO TRIP (Title, Price, TripDate, CategoryID,TourOperatorID) VALUES (@Title, @Price, @TripDate, @CategoryID,@UserID)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Title", title);
                        command.Parameters.AddWithValue("@Price", price);
                        command.Parameters.AddWithValue("@TripDate", tripDate);
                        command.Parameters.AddWithValue("@CategoryID", categoryId);
                        command.Parameters.AddWithValue("@UserID", UserID); // OR better: update the query to match this name

                        command.ExecuteNonQuery();
                    }
                }

                // Step 3: Show success message and optionally clear form
                MessageBox.Show("Trip added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm(); // Call the ClearForm method from your previous question
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            UnlockForm();
            panel1.Visible = false;
            ClearForm();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LockFormExceptPanel4();
            panel4.Visible = true;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            LockFormExceptPanel2();

            int UserID = SignINForm.LoggedInUserID;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT TripID, Title, Price, TripDate FROM TRIP WHERE TourOperatorID = @UserID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserID", UserID);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable tripTable = new DataTable();
                            adapter.Fill(tripTable);
                            dataGridView1.DataSource = tripTable;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void button11_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            UnlockForm();
            
        }

        private void button14_Click(object sender, EventArgs e)
        {
            panel4.Visible = false;
            UnlockForm();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox9.Text.Trim(), out int tripId) || textBox9.BackColor == Color.LightCoral)
            {
                MessageBox.Show("Please enter a valid Trip ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int userId = SignINForm.LoggedInUserID;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Check if the trip belongs to the logged-in tour operator
                    string checkQuery = "SELECT COUNT(*) FROM TRIP WHERE TripID = @TripID AND TourOperatorID = @UserID";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, connection))
                    {
                        checkCmd.Parameters.AddWithValue("@TripID", tripId);
                        checkCmd.Parameters.AddWithValue("@UserID", userId);

                        int count = (int)checkCmd.ExecuteScalar();
                        if (count == 0)
                        {
                            MessageBox.Show("Trip not found or you are not authorized to delete it.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    // Proceed with deletion
                    string deleteQuery = "DELETE FROM TRIP WHERE TripID = @TripID AND TourOperatorID = @UserID";
                    using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, connection))
                    {
                        deleteCmd.Parameters.AddWithValue("@TripID", tripId);
                        deleteCmd.Parameters.AddWithValue("@UserID", userId);

                        int rowsAffected = deleteCmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Trip deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            textBox9.Text = "";
                        }
                        else
                        {
                            MessageBox.Show("Trip deletion failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            UnlockForm();
            panel4.Visible = false;
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            string input = textBox9.Text.Trim();

            if (input.Length < 3)
            {
                textBox9.BackColor = Color.LightCoral;
            }
            else
            {
                textBox9.BackColor = Color.White;
            }
        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Reject character
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel3.Visible = true;
            LockFormExceptPanel3();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            ClearForm();
            UnlockForm();
            panel3.Visible = false;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            string input = textBox9.Text.Trim();

            if (input.Length < 3)
            {
                textBox9.BackColor = Color.LightCoral;
            }
            else
            {
                textBox9.BackColor = Color.White;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Reject character
            }
        }


        private void button16_Click(object sender, EventArgs e)
        {
            // Step 1: Validate input
            if (!int.TryParse(textBox3.Text.Trim(), out int tripId))
            {
                MessageBox.Show("Please enter a valid Trip ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Verify TripID exists in TRIP table
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string checkQuery = "SELECT COUNT(*) FROM TRIP WHERE TripID = @TripID";
                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@TripID", tripId);
                        int count = (int)checkCommand.ExecuteScalar();
                        if (count == 0)
                        {
                            MessageBox.Show("The specified Trip ID does not exist.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Database error while validating Trip ID: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int tourOperatorId = SignINForm.LoggedInUserID;
            // Verify TOUR_OPERATOR_ID exists
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string checkQuery = "SELECT COUNT(*) FROM TOUR_OPERATOR WHERE UserID = @UserID";
                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@UserID", tourOperatorId);
                        int count = (int)checkCommand.ExecuteScalar();
                        if (count == 0)
                        {
                            MessageBox.Show("Invalid Tour Operator ID. Please log in again.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Database error while validating Tour Operator ID: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (comboBox2.SelectedValue == null || !int.TryParse(comboBox2.SelectedValue.ToString(), out int serviceProviderId))
            {
                MessageBox.Show("Please select a valid service provider.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DateTime assignedDate = dateTimePicker2.Value;
            // Optional: Ensure AssignedDate is not in the past
            if (assignedDate < DateTime.Today)
            {
                MessageBox.Show("Assigned date cannot be in the past.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Step 2: Insert into TRIP_SERVICE_ASSIGNMENT
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO TRIP_SERVICE_ASSIGNMENT (TripID, TOUR_OPERATOR_ID, SERVICE_PROVIDER_ID, AssignedDate, AssignmentStatus) " +
                                  "VALUES (@TripID, @TourOperatorID, @ServiceProviderID, @AssignedDate, @AssignmentStatus)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TripID", tripId);
                        command.Parameters.AddWithValue("@TourOperatorID", tourOperatorId);
                        command.Parameters.AddWithValue("@ServiceProviderID", serviceProviderId);
                        command.Parameters.AddWithValue("@AssignedDate", assignedDate);
                        command.Parameters.AddWithValue("@AssignmentStatus", 0); // Pending

                        command.ExecuteNonQuery();
                    }
                }

                // Step 3: Show success message and clear form
                MessageBox.Show("Service provider assigned successfully! Status: Pending", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            UnlockForm();
            panel3.Visible = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            panel5.Visible = true;
            LockFormExceptPanel5();
            LoadCancellations();
        }

        private void LoadCancellations()
        {
            int tourOperatorId = SignINForm.LoggedInUserID;

            // Optional: Validate tourOperatorId
            if (tourOperatorId <= 0)
            {
                MessageBox.Show("Invalid Tour Operator ID. Please log in again.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    // Query to get cancellation data for the logged-in tour operator
                    string query = @"SELECT c.BookingID, c.TourOperatorID, t.CompanyName, c.CancelDate, c.Reason 
                           FROM CANCELLATION c 
                           LEFT JOIN TOUR_OPERATOR t ON c.TourOperatorID = t.UserID 
                           WHERE c.TourOperatorID = @TourOperatorID 
                           ORDER BY c.CancelDate DESC";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TourOperatorID", tourOperatorId);
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable cancellations = new DataTable();
                            adapter.Fill(cancellations);

                            // Bind data to gridView2
                            dataGridView2.DataSource = cancellations;

                            // Customize column headers and visibility
                            dataGridView2.Columns["BookingID"].HeaderText = "Booking ID";
                            dataGridView2.Columns["TourOperatorID"].Visible = false; // Hide ID, show OperatorName
                            dataGridView2.Columns["OperatorName"].HeaderText = "Tour Operator";
                            dataGridView2.Columns["CancelDate"].HeaderText = "Cancellation Date";
                            dataGridView2.Columns["Reason"].HeaderText = "Reason";

                            // Adjust column widths
                            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            panel5.Visible = false;
            UnlockForm();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel6.Visible = true;
            LockFormExceptPanel6();
            LoadRefunds();
        }

        private void LoadRefunds()
        {
            int tourOperatorId = SignINForm.LoggedInUserID;

            // Validate tourOperatorId
            if (tourOperatorId <= 0)
            {
                MessageBox.Show("Invalid Tour Operator ID. Please log in again.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"SELECT r.BookingID, r.TourOperatorID, t.CompanyName, r.RefundDate, r.Amount 
                               FROM REFUND r 
                               LEFT JOIN TOUR_OPERATOR t ON r.TourOperatorID = t.UserID 
                               WHERE r.TourOperatorID = @TourOperatorID 
                               ORDER BY r.RefundDate DESC";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TourOperatorID", tourOperatorId);
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable refunds = new DataTable();
                            adapter.Fill(refunds);

                            dataGridView3.DataSource = refunds;

                            dataGridView3.Columns["BookingID"].HeaderText = "Booking ID";
                            dataGridView3.Columns["TourOperatorID"].Visible = false;
                            dataGridView3.Columns["OperatorName"].HeaderText = "Tour Operator";
                            dataGridView3.Columns["RefundDate"].HeaderText = "Refund Date";
                            dataGridView3.Columns["Amount"].HeaderText = "Refund Amount";

                            dataGridView3.Columns["Amount"].DefaultCellStyle.Format = "C2";
                            dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            panel6.Visible = false;
            UnlockForm();
        }

        private void button12_Click(object sender, EventArgs e)
        {

        }

        private void button12_Click_1(object sender, EventArgs e)
        {
            panel7.Visible = true;
            LockFormExceptPanel7();
            LoadBookings();
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button15_Click(object sender, EventArgs e)
        {
            panel7.Visible = false;
            UnlockForm();
        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the clicked cell is in the CancelButton column and not the header row
            if (e.ColumnIndex == dataGridView4.Columns["CancelButton"].Index && e.RowIndex >= 0)
            {
                // Get the BookingID from the selected row
                int bookingId = Convert.ToInt32(dataGridView4.Rows[e.RowIndex].Cells["BookingID"].Value);
                int tourOperatorId = SignINForm.LoggedInUserID;

                // Get the TripID to fetch the Price for the refund
                int tripId = Convert.ToInt32(dataGridView4.Rows[e.RowIndex].Cells["TripID"].Value);
                decimal tripPrice = 0;

                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        // Step 1: Get the trip price for the refund amount
                        string priceQuery = "SELECT Price FROM TRIP WHERE TripID = @TripID";
                        using (SqlCommand priceCommand = new SqlCommand(priceQuery, connection))
                        {
                            priceCommand.Parameters.AddWithValue("@TripID", tripId);
                            object result = priceCommand.ExecuteScalar();
                            if (result != null)
                            {
                                tripPrice = Convert.ToDecimal(result);
                            }
                            else
                            {
                                MessageBox.Show("Trip not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }

                        // Step 2: Begin transaction to ensure data consistency
                        using (SqlTransaction transaction = connection.BeginTransaction())
                        {
                            try
                            {
                                // Step 3: Insert into CANCELLATION table
                                string cancelQuery = @"INSERT INTO CANCELLATION (BookingID, TourOperatorID, CancelDate, Reason) 
                                              VALUES (@BookingID, @TourOperatorID, @CancelDate, @Reason)";
                                using (SqlCommand cancelCommand = new SqlCommand(cancelQuery, connection, transaction))
                                {
                                    cancelCommand.Parameters.AddWithValue("@BookingID", bookingId);
                                    cancelCommand.Parameters.AddWithValue("@TourOperatorID", tourOperatorId);
                                    cancelCommand.Parameters.AddWithValue("@CancelDate", DateTime.Today);
                                    cancelCommand.Parameters.AddWithValue("@Reason", "Cancelled by Tour Operator");
                                    cancelCommand.ExecuteNonQuery();
                                }

                                // Step 4: Insert into REFUND table
                                string refundQuery = @"INSERT INTO REFUND (BookingID, TourOperatorID, RefundDate, Amount) 
                                              VALUES (@BookingID, @TourOperatorID, @RefundDate, @Amount)";
                                using (SqlCommand refundCommand = new SqlCommand(refundQuery, connection, transaction))
                                {
                                    refundCommand.Parameters.AddWithValue("@BookingID", bookingId);
                                    refundCommand.Parameters.AddWithValue("@TourOperatorID", tourOperatorId);
                                    refundCommand.Parameters.AddWithValue("@RefundDate", DateTime.Today);
                                    refundCommand.Parameters.AddWithValue("@Amount", tripPrice);
                                    refundCommand.ExecuteNonQuery();
                                }

                                // Step 5: Delete from BOOKINGS table (this will cascade delete from TICKET)
                                string deleteQuery = "DELETE FROM BOOKINGS WHERE BookingID = @BookingID";
                                using (SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection, transaction))
                                {
                                    deleteCommand.Parameters.AddWithValue("@BookingID", bookingId);
                                    int rowsAffected = deleteCommand.ExecuteNonQuery();
                                    if (rowsAffected == 0)
                                    {
                                        throw new Exception("Booking not found or already deleted.");
                                    }
                                }

                                // Commit the transaction
                                transaction.Commit();

                                // Step 6: Refresh the grid
                                LoadBookings();

                                MessageBox.Show("Booking cancelled successfully. Refund processed.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            catch (Exception ex)
                            {
                                // Rollback the transaction on error
                                transaction.Rollback();
                                MessageBox.Show($"Error during cancellation: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
    
}
