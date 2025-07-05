using Microsoft.Reporting.WinForms;
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
using Microsoft.Reporting.WinForms;


namespace TravelEaseDB
{
    public partial class TravelerInterface : Form
    {
        public class TicketData
        {
            public int BookingID { get; set; }
            public DateTime IssueDate { get; set; }
            public DateTime ExpiryDate { get; set; }
        }

        private AppUser_Form _parentForm;  // Declare a variable to hold the parent Form1 instance
        string connectionString = "Data Source=CODE-TECH\\SQLEXPRESS;Initial Catalog=TravelEaseDB;Integrated Security=True;";


        public TravelerInterface(AppUser_Form parent)
        {
            InitializeComponent();
            this._parentForm = parent;

            this.WindowState = FormWindowState.Maximized;
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.UserPaint, true);
            this.UpdateStyles();

            panel1.Visible = false; // Hide the panel by default
            panel2.Visible = false; // Hide the panel by default
            panel3.Visible = false;

            this.MinimizeBox = false; // Disable minimize button
            this.MaximizeBox = false; // Disable maximize button

            reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer
            {
                Dock = DockStyle.Fill,
                Location = new Point(0, 0),
                ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local
            };

            // Add to panel4
            panel4.Controls.Add(reportViewer1);


        }

        private void TravelerInterface_Load(object sender, EventArgs e)
        {
            loadBookedTrips();
        }


        private void loadTrips()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT TripID, Title, TripDate, Price FROM TRIP WHERE TripDate >= GETDATE() ORDER BY TripDate";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable tripsTable = new DataTable();
                    adapter.Fill(tripsTable);

                    // Clear existing items
                    comboBox1.Items.Clear();
                    comboBox1.Items.Add("Select a trip..."); // Optional default item
                    comboBox1.SelectedIndex = 0;

                    // Store TripID as the value and display formatted string
                    foreach (DataRow row in tripsTable.Rows)
                    {
                        string displayText = $"{row["Title"]} - {Convert.ToDateTime(row["TripDate"]).ToString("MM/dd/yyyy")} - ${row["Price"]}";
                        comboBox1.Items.Add(new KeyValuePair<int, string>((int)row["TripID"], displayText));
                    }

                    // Configure ComboBox to show display text and store TripID
                    comboBox1.DisplayMember = "Value";
                    comboBox1.ValueMember = "Key";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading trips: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void button5_Click(object sender, EventArgs e)
        {
            _parentForm.Show();
            this.Close();
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

        private void button4_Click(object sender, EventArgs e)
        {
            panel3.Visible = true; // Show the panel
            LockFormExceptPanel3(); // Lock the form except for panel1
            loadTrips(); // Load trips into the comobox1
        }

        private void button17_Click(object sender, EventArgs e)
        {
            panel3.Visible = false; // Hide the panel
            UnlockForm(); // Unlock the form
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Visible = false; // Show the panel
            UnlockForm(); // Unlock the form
        }

        private void button3_Click(object sender, EventArgs e)
        {
            loadBookedTrips();
            panel1.Visible = true; // Show the panel
            LockFormExceptPanel1(); // Lock the form except for panel1
        }

        //private void button16_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        // Check if a valid trip is selected
        //        if (comboBox1.SelectedIndex <= 0 || !(comboBox1.SelectedItem is KeyValuePair<int, string>))
        //        {
        //            MessageBox.Show("Please select a valid trip.", "Invalid Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            return;
        //        }

        //        // Get selected TripID
        //        int tripID = ((KeyValuePair<int, string>)comboBox1.SelectedItem).Key;

        //        using (SqlConnection conn = new SqlConnection(connectionString))
        //        {
        //            conn.Open();

        //            // Begin transaction to ensure atomicity
        //            using (SqlTransaction transaction = conn.BeginTransaction())
        //            {
        //                try
        //                {
        //                    // Insert into BOOKINGS and get the new BookingID
        //                    string bookingQuery = @"
        //                INSERT INTO BOOKINGS (TripID, UserID, BookDate, BookingStatus) 
        //                OUTPUT INSERTED.BookingID
        //                VALUES (@TripID, @UserID, GETDATE(), 0)";

        //                    int bookingID;
        //                    using (SqlCommand cmd = new SqlCommand(bookingQuery, conn, transaction))
        //                    {
        //                        cmd.Parameters.AddWithValue("@TripID", tripID);
        //                        cmd.Parameters.AddWithValue("@UserID", SignINForm.LoggedInUserID);
        //                        bookingID = (int)cmd.ExecuteScalar();
        //                    }

        //                    // Insert into TICKET using the new BookingID
        //                    string ticketQuery = @"
        //                INSERT INTO TICKET (BookingID, IssueDate, ExpiryDate) 
        //                VALUES (@BookingID, GETDATE(), DATEADD(day, 15, GETDATE()))";

        //                    using (SqlCommand cmd = new SqlCommand(ticketQuery, conn, transaction))
        //                    {
        //                        cmd.Parameters.AddWithValue("@BookingID", bookingID);
        //                        cmd.ExecuteNonQuery();
        //                    }

        //                    // Commit the transaction
        //                    transaction.Commit();

        //                    MessageBox.Show("Booking and ticket created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

        //                    // Reset form or close panel
        //                    panel3.Visible = false;
        //                    UnlockForm(); // Implement this if you have a corresponding unlock method
        //                }
        //                catch (Exception ex)
        //                {
        //                    // Roll back transaction on error
        //                    transaction.Rollback();
        //                    throw new Exception($"Error creating booking or ticket: {ex.Message}", ex);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        private void button16_Click(object sender, EventArgs e)
        {
            try
            {
                // Check if a valid trip is selected
                if (comboBox1.SelectedIndex <= 0 || !(comboBox1.SelectedItem is KeyValuePair<int, string>))
                {
                    MessageBox.Show("Please select a valid trip.", "Invalid Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Get selected TripID
                int tripID = ((KeyValuePair<int, string>)comboBox1.SelectedItem).Key;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Begin transaction to ensure atomicity
                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            // Insert into BOOKINGS and get the new BookingID
                            string bookingQuery = @"
                        INSERT INTO BOOKINGS (TripID, UserID, BookDate, BookingStatus) 
                        OUTPUT INSERTED.BookingID
                        VALUES (@TripID, @UserID, GETDATE(), 1)";

                            int bookingID;
                            using (SqlCommand cmd = new SqlCommand(bookingQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@TripID", tripID);
                                cmd.Parameters.AddWithValue("@UserID", SignINForm.LoggedInUserID);
                                bookingID = (int)cmd.ExecuteScalar();
                            }

                            // Insert into TICKET using the new BookingID
                            string ticketQuery = @"
                        INSERT INTO TICKET (BookingID, IssueDate, ExpiryDate) 
                        VALUES (@BookingID, GETDATE(), DATEADD(day, 15, GETDATE()))";

                            using (SqlCommand cmd = new SqlCommand(ticketQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@BookingID", bookingID);
                                cmd.ExecuteNonQuery();
                            }

                            // Commit the transaction
                            transaction.Commit();

                            MessageBox.Show("Booking and ticket created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Refresh comboBox2 and comboBox3 with updated bookings
                            loadBookedTrips();

                            // Reset form or close panel
                            panel3.Visible = false;
                            UnlockForm();
                        }
                        catch (Exception ex)
                        {
                            // Roll back transaction on error
                            transaction.Rollback();
                            throw new Exception($"Error creating booking or ticket: {ex.Message}", ex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(textBox1.Text.Length <0 || textBox1.Text.Length > 5)
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
            // Allow only digits or backspace
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Reject input
            }
        }

        private void loadBookedTrips()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                SELECT B.BookingID, T.Title 
                FROM BOOKINGS B
                INNER JOIN TRIP T ON B.TripID = T.TripID
                WHERE B.UserID = @UserID 
                AND B.BookingID NOT IN (SELECT BookingID FROM REVIEW)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", SignINForm.LoggedInUserID);
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable bookedTripsTable = new DataTable();
                        adapter.Fill(bookedTripsTable);

                        // Clear existing items
                        comboBox2.Items.Clear();
                        comboBox2.Items.Add("Select a booked trip..."); // Optional default item
                        comboBox2.SelectedIndex = 0;

                        comboBox3.Items.Clear();
                        comboBox3.Items.Add("Select a booked trip..."); // Optional default item
                        comboBox3.SelectedIndex = 0;


                        // Store BookingID as value and display Title
                        foreach (DataRow row in bookedTripsTable.Rows)
                        {
                            comboBox2.Items.Add(new KeyValuePair<int, string>((int)row["BookingID"], row["Title"].ToString()));
                            comboBox3.Items.Add(new KeyValuePair<int, string>((int)row["BookingID"], row["Title"].ToString()));
                        }

                        // Configure ComboBox to show Title and store BookingID
                        comboBox2.DisplayMember = "Value";
                        comboBox2.ValueMember = "Key";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading booked trips: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate inputs
                if (comboBox2.SelectedIndex <= 0 || !(comboBox2.SelectedItem is KeyValuePair<int, string>))
                {
                    MessageBox.Show("Please select a valid booked trip.", "Invalid Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(textBox1.Text, out int rating) || rating < 1 || rating > 5)
                {
                    MessageBox.Show("Please enter a valid rating between 1 and 5.", "Invalid Rating", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string comment = textBox2.Text.Trim();
                if (string.IsNullOrEmpty(comment))
                {
                    MessageBox.Show("Please enter a comment.", "Invalid Comment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Get selected BookingID
                int bookingID = ((KeyValuePair<int, string>)comboBox2.SelectedItem).Key;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                INSERT INTO REVIEW (BookingID, Rating, Comment, ReviewDate, ApproveStatus) 
                VALUES (@BookingID, @Rating, @Comment, GETDATE(), 0)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@BookingID", bookingID);
                        cmd.Parameters.AddWithValue("@Rating", rating);
                        cmd.Parameters.AddWithValue("@Comment", comment);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Review submitted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Reset form
                comboBox2.SelectedIndex = 0;
                textBox1.Clear();
                textBox2.Clear();
                loadBookedTrips(); // Refresh comboBox2 to remove reviewed booking
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error submitting review: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //private void button7_Click(object sender, EventArgs e)
        //{
        //    panel2.Visible = true; // Show the panel
        //    LockFormExceptPanel2(); // Lock the form except for panel2
        //    try
        //    {


        //        // Get selected BookingID
        //        int bookingID = ((KeyValuePair<int, string>)comboBox2.SelectedItem).Key;

        //        using (SqlConnection conn = new SqlConnection(connectionString))
        //        {
        //            conn.Open();
        //            string query = @"
        //        SELECT T.BookingID, T.IssueDate, T.ExpiryDate
        //        FROM TICKET T
        //        INNER JOIN BOOKINGS B ON T.BookingID = B.BookingID
        //        WHERE T.BookingID = @BookingID AND B.UserID = @UserID";

        //            using (SqlCommand cmd = new SqlCommand(query, conn))
        //            {
        //                cmd.Parameters.AddWithValue("@BookingID", bookingID);
        //                cmd.Parameters.AddWithValue("@UserID", SignINForm.LoggedInUserID);

        //                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        //                DataTable ticketTable = new DataTable();
        //                adapter.Fill(ticketTable);

        //                // Check if data was found
        //                if (ticketTable.Rows.Count == 0)
        //                {
        //                    MessageBox.Show("No ticket found for this booking.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                    return;
        //                }

        //                // Configure ReportViewer
        //                reportViewer1.LocalReport.ReportEmbeddedResource = "TravelEaseDB.Ticket.rdlc"; // Update with your namespace and report name
        //                reportViewer1.LocalReport.DataSources.Clear();
        //                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("TicketDataSet", ticketTable));
        //                reportViewer1.RefreshReport();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Error generating report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        //private void button7_Click(object sender, EventArgs e)
        //{
        //    loadBookedTrips();
        //    try
        //    {
        //        // Check if a valid booking is selected
        //        //if (comboBox3.SelectedIndex <= 0 || !(comboBox3.SelectedItem is KeyValuePair<int, string>))
        //        //{
        //        //    MessageBox.Show("Please select a valid booked trip.", "Invalid Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        //    return;
        //        //}

        //        // Get selected BookingID
        //        int bookingID = ((KeyValuePair<int, string>)comboBox3.SelectedItem).Key;

        //        using (SqlConnection conn = new SqlConnection(connectionString))
        //        {
        //            conn.Open();
        //            string query = @"
        //        SELECT T.BookingID, T.IssueDate, T.ExpiryDate
        //        FROM TICKET T
        //        INNER JOIN BOOKINGS B ON T.BookingID = B.BookingID
        //        WHERE T.Booking voelID = @BookingID AND B.UserID = @UserID";

        //            using (SqlCommand cmd = new SqlCommand(query, conn))
        //            {
        //                cmd.Parameters.AddWithValue("@BookingID", bookingID);
        //                cmd.Parameters.AddWithValue("@UserID", SignINForm.LoggedInUserID);

        //                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        //                DataTable ticketTable = new DataTable();
        //                adapter.Fill(ticketTable);

        //                // Check if data was found
        //                if (ticketTable.Rows.Count == 0)
        //                {
        //                    MessageBox.Show("No ticket found for this booking.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                    return;
        //                }

        //                // Configure ReportViewer
        //                reportViewer1.LocalReport.ReportEmbeddedResource = "TravelEaseDB.Ticket.rdlc"; // Update with your namespace and report name
        //                reportViewer1.LocalReport.DataSources.Clear();
        //                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("TicketDataSet", ticketTable));
        //                reportViewer1.RefreshReport();
        //            }
        //        }

        //        panel2.Visible = true; // Show the panel
        //        LockFormExceptPanel2(); // Lock the form except for panel2
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Error generating report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                loadBookedTrips(); // Load booked trips into comboBox3
                panel2.Visible = true; // Show panel2 containing comboBox3
                LockFormExceptPanel2(); // Lock the form except for panel2
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading booked trips: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            UnlockForm(); // Unlock the form
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                // Check if a valid booking is selected
                if (comboBox3.SelectedIndex <= 0 || !(comboBox3.SelectedItem is KeyValuePair<int, string>))
                {
                    MessageBox.Show("Please select a valid booked trip.", "Invalid Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Get selected BookingID
                int bookingID = ((KeyValuePair<int, string>)comboBox3.SelectedItem).Key;

                List<TicketData> ticketList = new List<TicketData>();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                SELECT T.BookingID, T.IssueDate, T.ExpiryDate
                FROM TICKET T
                INNER JOIN BOOKINGS B ON T.BookingID = B.BookingID
                WHERE T.BookingID = @BookingID AND B.UserID = @UserID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@BookingID", bookingID);
                        cmd.Parameters.AddWithValue("@UserID", SignINForm.LoggedInUserID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ticketList.Add(new TicketData
                                {
                                    BookingID = reader.GetInt32(0),
                                    IssueDate = reader.GetDateTime(1),
                                    ExpiryDate = reader.GetDateTime(2)
                                });
                            }
                        }

                        // Check if data was found
                        if (ticketList.Count == 0)
                        {
                            MessageBox.Show("No ticket found for this booking.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        // Ensure reportViewer1 is a child of panel4
                        if (reportViewer1.Parent != panel4)
                        {
                            panel4.Controls.Add(reportViewer1);
                            reportViewer1.Dock = DockStyle.Fill;
                        }

                        // Configure ReportViewer
                        reportViewer1.LocalReport.ReportEmbeddedResource = "TravelEaseDB.Ticket.rdlc";
                        reportViewer1.LocalReport.DataSources.Clear();
                        reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("TicketDataSet", ticketList));
                        reportViewer1.RefreshReport();

                        // Hide button6
                        button6.Visible = false;

                        // Optionally hide panel2 and unlock form
                        panel2.Visible = false;
                        UnlockForm();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ( comboBox3.SelectedIndex <= 0 || !(comboBox3.SelectedItem is KeyValuePair<int, string>))
                return;

            try
            {
                // Get selected BookingID
                int bookingID = ((KeyValuePair<int, string>)comboBox3.SelectedItem).Key;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                SELECT T.BookingID, T.IssueDate, T.ExpiryDate
                FROM TICKET T
                INNER JOIN BOOKINGS B ON T.BookingID = B.BookingID
                WHERE T.BookingID = @BookingID AND B.UserID = @UserID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@BookingID", bookingID);
                        cmd.Parameters.AddWithValue("@UserID", SignINForm.LoggedInUserID);

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable ticketTable = new DataTable();
                        adapter.Fill(ticketTable);

                        if (ticketTable.Rows.Count == 0)
                        {
                            MessageBox.Show("No ticket found for this booking.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        reportViewer1.LocalReport.ReportEmbeddedResource = "TravelEaseDB.Ticket.rdlc";
                        reportViewer1.LocalReport.DataSources.Clear();
                        reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("TicketDataSet", ticketTable));
                        reportViewer1.RefreshReport();
                    }
                }

                panel2.Visible = false; // Hide panel2 after generating the report
                UnlockForm(); // Unlock the form
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            panel4.Visible = false;
            UnlockForm();
        }
    }
}
