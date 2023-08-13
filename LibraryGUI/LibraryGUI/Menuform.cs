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
using System.Xml.Linq;

namespace LibraryGUI
{
    public partial class Menuform : Form
    {
        public Menuform()
        {
            InitializeComponent();
        }
       string connectionString = @"Data Source=DESKTOP-KQNL914\SQLEXPRESS;Initial Catalog=LibrarymanagementLogin;Integrated Security=True";

        private void Menuform_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM Books", conn);
                DataTable dtbl = new DataTable();
                sqlDa.Fill(dtbl);
                dgv1.DataSource = dtbl;


                dgv1.AllowUserToAddRows = false;
                DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
                dgv1.Columns.Insert(5, buttonColumn);
                buttonColumn.HeaderText = "Delete Row";
                buttonColumn.Width = 100;
                buttonColumn.Text = "Delete";
                buttonColumn.UseColumnTextForButtonValue = true;

            }
        }
                

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string BookName, AuthorID, Isbn, Quantity;
            BookName = bookname.Text;
            AuthorID = authorid.Text;
            Isbn = isbn.Text;
            Quantity = quantity.Text;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Books (Title, AuthorID, ISBN, Quantity) VALUES (@BookName, @AuthorID, @Isbn,@Quantity)";
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                command.Parameters.AddWithValue("@Bookname", BookName);
                    command.Parameters.AddWithValue("@AuthorID", AuthorID);
                    command.Parameters.AddWithValue("@Isbn", Isbn);
                    command.Parameters.AddWithValue("@Quantity", Quantity);
                    conn.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Data inserted successfully!");
                        // Clear the textboxes
                        bookname.Clear();
                        authorid.Clear();
                        isbn.Clear();
                        quantity.Clear();

                    }
                    else
                    {
                        MessageBox.Show("Failed to insert data.");
                    }
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
           
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void dgv1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5) 
            {
                DataGridViewRow row = dgv1.Rows[e.RowIndex];
                if (MessageBox.Show(string.Format("Do You Want to Delete this row?", row.Cells["BookID"].Value), "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes) 
                {
                    using (SqlConnection con1 = new SqlConnection(connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("Delete from Books where BookID=@BookID", con1))
                        {
                            cmd.Parameters.AddWithValue("BookID", row.Cells["BookID"].Value);
                            con1.Open();
                            cmd.ExecuteNonQuery();
                            con1.Close();
                                }
                    }
                }
            }
        }
    }
}
