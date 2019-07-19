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
    public partial class Client : MetroFramework.Forms.MetroForm
    {
        String connectionString = "Data Source=DESKTOP-TL4IJDB;Initial Catalog=MedicalStore;User ID=SA;Password=12345";

        public Client()
        {
            InitializeComponent();
            button2.Enabled = false;
        }
        public void clear()
        {
            txtame.Text = "";
            email.Text = "";
            password.Text = "";
            phoneno.Text = "";
            securityquestion.ResetText(); 
            answer.Text = "";
            gender.ResetText();
            fullname.Text = "";
            confirmpassword.Text = "";

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
         public string username, strmenu;
         public Client(string uname, string strmnu)
        {
            username = uname;
            strmenu = strmnu;
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

        private int upperCase(string pass)
        {
            int num = 0;
            foreach (char ch in pass)
            {
                if (char.IsUpper(ch))
                {
                    num++;
                }
            }
            return num;
        }

        private void Client_Load(object sender, EventArgs e)
        {
            SqlConnection sql = new SqlConnection(connectionString);
            sql.Open();
            string query = "Select UserName,FullName,Email,PhoneNo,Status from Client  where Status='Verified'";
            SqlDataAdapter sda = new SqlDataAdapter(query, sql);
            //sda.SelectCommand.ExecuteNonQuery();
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
            sql.Close();

            sql.Open();
            string query2 = "Select UserName,FullName,Email,PhoneNo,Status from Client where Status='0'";
            SqlDataAdapter a = new SqlDataAdapter(query2, sql);
            DataTable dt2 = new DataTable();
            a.Fill(dt2);
            dataGridView3.DataSource = dt2;



        }
        public void load2()
        {
            try
            {
                SqlConnection sql = new SqlConnection(connectionString);
                sql.Open();
                string query = "Select * from Client where Status='Verified'";

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

        public void load()
        {
            try
            {
                SqlConnection sql = new SqlConnection(connectionString);
                sql.Open();
                string query = "Select * from Client where Status='0'";

                SqlDataAdapter sda = new SqlDataAdapter(query, sql);


                DataTable dt = new DataTable();
                sda.Fill(dt);
                dataGridView3.DataSource = dt;
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
                    if (txtame.Text == "" || fullname.Text == "" || gender.Text == "" || email.Text == "" || phoneno.Text == "" || password.Text == "" || securityquestion.Text == "" || answer.Text == "")
                    {
                        MessageBox.Show("Please fill up all the components!!!", "Warning");
                    }

                    else
                    {
                        if (password.Text == confirmpassword.Text)
                        {
                            const int MIN_LENGHTH = 5;
                            const int Phone_lenghth = 11;
                            string mobile = phoneno.Text;
                            string Password = password.Text;
                            if (password.Text.Length >= MIN_LENGHTH && numberPass(password.Text) >= 1 && upperCase(password.Text) >= 1)
                            {
                                if (phoneno.Text.Length == Phone_lenghth && numberPass(phoneno.Text) >= 1)
                                {
                                    string query = "Insert into Client (Username,Fullname,Gender,Email,Phoneno,Password,Sequrityquestion,Answer) values ('" + txtame.Text + "', '" + fullname.Text + "','" + gender.Text + "','" + email.Text + "','" + phoneno.Text + "','" + password.Text + "','" + securityquestion.Text + "','" + answer.Text + "') ";

                                    SqlCommand q1 = new SqlCommand(query, sql);

                                    q1.ExecuteNonQuery();
                                    MessageBox.Show("Registration Successful.  ");
                                    clear();
                                    load();
                                }
                                else
                                {
                                    MessageBox.Show("Please enter valid phone no!!!");

                                }
                            }
                            else
                            {
                                MessageBox.Show("Please enter minimum 1 uppercase and 1 number and at least 5 letter!!!", "Warning");
                            }

                        }
                        else
                        {
                            MessageBox.Show("Password & Confirm Password does not match!!!", "Warning");

                        }

                    }

                }
            }
            catch(Exception)
            {
                MessageBox.Show("UserName Already Taken", "Error");
                txtame.Text = "";

            }
        }

        private void tabPage4_Click(object sender, EventArgs e)
        {
           
            

            

        }

        private void button2_Click(object sender, EventArgs e)
        {

            SqlConnection sql = new SqlConnection(connectionString);
            sql.Open();
            string query = "Delete from Client where UserName= '" + textBox3.Text.Trim() + "'";
            SqlCommand q1 = new SqlCommand(query, sql);
            q1.ExecuteNonQuery();
            MessageBox.Show("User Succesfully Removed!!!!! ");
           // ((DataTable)dataGridView2.DataSource).Rows.Clear();
            textBox3.Text = "";
            button2.Enabled = false;
            dataGridView2.DataSource = null;
            load2();
            load();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection sql = new SqlConnection(connectionString);
            sql.Open();
            string query = "Select UserName,FullName,Email,PhoneNo,Status from Client where UserName= '" + textBox3.Text.Trim() + "'";
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
                MessageBox.Show("User not found!!!!! ");
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView2.DataSource = null;

           // ((DataTable)dataGridView2.DataSource).Rows.Clear();
            textBox3.Text = "";
            button2.Enabled = false;

        }

        private void verified_Click(object sender, EventArgs e)
        {
            //if (dataGridView3.CurrentRow.Index != -1)
            //{
            //    string name = Convert.ToString(dataGridView3.CurrentRow.Cells["UserName"].Value);
            //    SqlConnection sql = new SqlConnection(connectionString);
            //    sql.Open();
            //    string query = "Update Client Set Status = 'Verified' where UserName = 'name'";
            //    SqlCommand q = new SqlCommand(query, sql);

            //    q.ExecuteNonQuery();
                

            //}
        }

        private void dataGridView3_DoubleClick(object sender, EventArgs e)
        {
           
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (dataGridView3.Columns[e.ColumnIndex].Name == "Verified")
            //{
               
            //    SqlConnection sql = new SqlConnection(connectionString);
            //    sql.Open();
            //    string query = "Update Client Set Status = 'Verified' where UserName = ''";
            //    SqlCommand q = new SqlCommand(query, sql);

            //    q.ExecuteNonQuery();
            //}
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow sr = dataGridView3.Rows[index]; 
            textBox1.Text = sr.Cells[0].Value.ToString();
            button5.Enabled = true;
            button6.Enabled = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SqlConnection sql = new SqlConnection(connectionString);
            sql.Open();
            string query = "Update Client Set Status = 'Verified' where UserName = '"+textBox1.Text.Trim()+"'";
            SqlCommand q = new SqlCommand(query, sql);

            q.ExecuteNonQuery();
            MessageBox.Show("The Client Is Verified!!!!");
            sql.Close();
            textBox1.Text = "";
            load2();
            button5.Enabled = false;
            button6.Enabled = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SqlConnection sql = new SqlConnection(connectionString);
            sql.Open();
            string query = "Delete from Client where UserName = '" + textBox1.Text.Trim() + "'";
            SqlCommand q = new SqlCommand(query, sql);

            q.ExecuteNonQuery();
            MessageBox.Show("The Client Is Deleted!!!!");
            sql.Close();
            textBox1.Text = "";
            load();
            button6.Enabled = false;
            button5.Enabled = false;

        }
    }
}
