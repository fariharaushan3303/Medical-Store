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
using System.IO;

namespace InventorySystem
{
    public partial class Search : MetroFramework.Forms.MetroForm
    {
        String connectionString = "Data Source=DESKTOP-TL4IJDB;Initial Catalog=MedicalStore;User ID=SA;Password=12345";
        public Search()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f = new Form1();
            f.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection sql = new SqlConnection(connectionString);
            sql.Open();
            string query1 = "Select ItemName,GroupName,Power,Price,Quantity,CompanyName,ExpireDate from Medicine where ItemName= '" + textBox2.Text.Trim() + "'";
            
            SqlDataAdapter sda = new SqlDataAdapter(query1, sql);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            if (dt.Rows.Count == 1)
            {
                dataGridView1.DataSource = dt;

                string query2 = "Select Image from Medicine where ItemName= '" + textBox2.Text.Trim() + "'";
                SqlCommand cmd = new SqlCommand(query2, sql);
                SqlDataReader a = cmd.ExecuteReader();

                a.Read();


                if (a.HasRows)
                {

                    byte[] img = ((byte[])a[0]);
                    if (img == null)
                    {
                        pictureBox1.Image = null;
                    }
                    else
                    {
                        MemoryStream mstream = new MemoryStream(img);
                        pictureBox1.Image = Image.FromStream(mstream);
                    }
                }
                sql.Close();
                button1.Enabled = false;
                
               
            }
            else
            {
                MessageBox.Show("Medicine not found!!!!");
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            textBox1.Text = "";
            pictureBox1.Image = null;
            dataGridView1.DataSource = null;
            button1.Enabled = true;
            button2.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection sql = new SqlConnection(connectionString);
            sql.Open();
            string query2 = "Select ItemName,GroupName,Power,Price,Quantity,CompanyName,ExpireDate from Medicine where GroupName='"+textBox1.Text.Trim()+"'";
            SqlDataAdapter sda = new SqlDataAdapter(query2, sql);

            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Medicine not found!!!!");
            }
            else
            {

                dataGridView1.DataSource = dt;
                sql.Close();
                button2.Enabled = false;
            }
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
