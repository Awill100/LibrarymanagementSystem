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

namespace LibraryGUI
{
    public partial class user : Form
    {
        public user()
        {
            InitializeComponent();
        }
        string connectionString = @"Data Source=DESKTOP-KQNL914\SQLEXPRESS;Initial Catalog=LibrarymanagementLogin;Integrated Security=True";

        private void button1_Click(object sender, EventArgs e)
        {
            string Username;
            string Password;
            string Name;
            string Dob;
            string Address;
            Name = name.Text;
            Username = username.Text;
            Password = password.Text;
            
            Dob = dob.Text;
            Address = address.Text;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Login_new  (username, password, name, dob, address) VALUES (@Username, @Password, @Name, @Dob,@Address)";
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@Username", Username);
                    command.Parameters.AddWithValue ("@Password", Password);
                    command.Parameters.AddWithValue("@Name", Name);
                    command.Parameters.AddWithValue("@Dob", Dob);
                    command.Parameters.AddWithValue("Address", Address);
                    conn.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Data inserted successfully!");
                        // Clear the textboxes
                        name.Clear();
                        username.Clear();
                        password.Clear();
                        dob.Clear();
                        address.Clear();

                    }
                    else
                    {
                        MessageBox.Show("Failed to insert data.");
                    }
                }
            }

        }
    }
}
