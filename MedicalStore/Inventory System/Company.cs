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


namespace InventorySystem
{
    public partial class Company : MetroFramework.Forms.MetroForm
    {
        String connectionString = "Data Source=DESKTOP-TL4IJDB;Initial Catalog=MedicalStore;User ID=SA;Password=12345";
        public Company()
        {
            InitializeComponent();
        }
        private int numberPass(string pass)
        {
            int num = 0;
            foreach (char ch in pass)
            {
                if (char.IsDigit(ch))
                {
                    num++;
                }
            }
            return num;
        }

       


        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        public void clear()
        {
            txtame.Text = "";
             txtadd.Text ="" ;
            cmphone.Text = "" ;
            dbphone.Text ="";
        }
        public void load()
        {
            try
            {
                SqlConnection sql = new SqlConnection(connectionString);
                sql.Open();
                string query = "Select * from Company";

                SqlDataAdapter sda = new SqlDataAdapter(query, sql);


                DataTable dt = new DataTable();
                sda.Fill(dt);
                dataGridView1.DataSource = dt;
                sql.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Connection Problem!!", "Error");
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection sql = new SqlConnection(connectionString);
                sql.Open();

                if (sql.State == ConnectionState.Open)
                {
                    if (txtame.Text == "" || txtadd.Text == "" || cmphone.Text == "" || dbphone.Text == "")
                    {
                        MessageBox.Show("Please fill up all the components!!!", "Warning");
                    }

                    else
                    {

                        const int Phone_lenghth = 11;

                        if (cmphone.Text.Length == Phone_lenghth && numberPass(cmphone.Text) >= 1 && dbphone.Text.Length == Phone_lenghth && numberPass(dbphone.Text) >= 1)
                        {

                            string query = "Insert into Company (CompanyName,Address,CompanyMobile,DistributorMobile) values ('" + txtame.Text + "', '" + txtadd.Text + "','" + cmphone.Text + "','" + dbphone.Text + "') ";

                            SqlCommand q1 = new SqlCommand(query, sql);

                            q1.ExecuteNonQuery();
                            MessageBox.Show("Registration Successful.");
                            clear();
                            load();
                        }
                        else
                        {
                            MessageBox.Show("Enter Valid Phone number");
                        }
                    }




                }
            }
            catch(Exception)
            {
                MessageBox.Show("Company Name Is Already Taken", "Error");
                txtame.Text = "";
            }


        }

        private void Company_Load(object sender, EventArgs e)
        {
            SqlConnection sql = new SqlConnection(connectionString);
            sql.Open();
            string query = "Select CompanyName,Address,CompanyMObile,DistributorMobile from Company";
            SqlDataAdapter sda = new SqlDataAdapter(query, sql);
            //sda.SelectCommand.ExecuteNonQuery();
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
            sql.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection sql = new SqlConnection(connectionString);
            sql.Open();
            string query = "Delete from Company where CompanyName= '" + textBox1.Text.Trim() + "'";
            SqlCommand q1 = new SqlCommand(query, sql);
            q1.ExecuteNonQuery();
            MessageBox.Show("Company Succesfully Removed!!!!! ");
            //((DataTable)dataGridView2.DataSource).Rows.Clear();
            textBox1.Text = "";
            dataGridView2.DataSource = null;
            button2.Enabled = false;
            load();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //((DataTable)dataGridView2.DataSource).Rows.Clear();
            textBox1.Text = "";
            dataGridView2.DataSource = null;
            button2.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection sql = new SqlConnection(connectionString);
            sql.Open();
            string query = "Select CompanyName,Address,CompanyMobile,DistributorMobile from Company where CompanyName= '" + textBox1.Text.Trim() + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, sql);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count == 1)
            {

                dataGridView2.DataSource = dt;
                sql.Close();
                button2.Enabled = true;
            }
            else
            {
                MessageBox.Show("Company not found");
            }
            
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}