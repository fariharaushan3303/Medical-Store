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
    public partial class ManageAdmin : MetroFramework.Forms.MetroForm
    {
        String connectionString = "Data Source=DESKTOP-TL4IJDB;Initial Catalog=MedicalStore;User ID=SA;Password=12345";
        public ManageAdmin()
        {
            InitializeComponent();
            button2.Enabled = false;
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
        public void clear()
        {
            txtname.Text = "";
            email.Text = "";
            password.Text = "";
            phoneno.Text = "";
           // securityquestion.Text = "";
           // answer.Text = "";
            confirmpassword.Text = "";
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection sql = new SqlConnection(connectionString);
                sql.Open();

                if (sql.State == ConnectionState.Open)
                {
                    if (txtname.Text == "" || email.Text == "" || phoneno.Text == "" || password.Text == "")                 
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
                                    string query = "Insert into Admin (UserName,Email,Phoneno,Password,SecurityQuestion,Answer) values ('" + txtname.Text + "', '" + email.Text + "','" + phoneno.Text + "','" + password.Text + "') ";

                                    SqlCommand q1 = new SqlCommand(query, sql);

                                    q1.ExecuteNonQuery();
                                    MessageBox.Show("Registration Successful.");
                                    clear();
                                    sql.Close();
                                    load2();
                                }
                                else
                                {
                                    MessageBox.Show("Please enter valid phone no|||");
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

            catch (Exception)
            {
                MessageBox.Show("UserName Already Taken", "Error");
                txtname.Text = "";
            }
        }
        public void load2()
        {
            try
            {
                SqlConnection sql = new SqlConnection(connectionString);
                sql.Open();
                string query = "Select * from Admin";

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
                string query = "Select * from ClientOrder";

                SqlDataAdapter sda = new SqlDataAdapter(query, sql);

              
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dataGridView3.DataSource = dt;
                sql.Close();
            }
            catch(Exception)
            {
                MessageBox.Show("Connection Problem!!", "Error");
            }
        }

        private void ManageAdmin_Load(object sender, EventArgs e)
        {
            SqlConnection sql = new SqlConnection(connectionString);
            sql.Open();
            string query = "Select UserName,Email,PhoneNo from Admin";
            SqlDataAdapter sda = new SqlDataAdapter(query, sql);
            //sda.SelectCommand.ExecuteNonQuery();
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
            sql.Close();

            SqlConnection sq = new SqlConnection(connectionString);
            sq.Open();
            string query2 = "Select * from ClientOrder";
            SqlDataAdapter sd = new SqlDataAdapter(query2, sq);
            //sda.SelectCommand.ExecuteNonQuery();
            DataTable d = new DataTable();
            sd.Fill(d);
            dataGridView3.DataSource = d;
            sq.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection sql = new SqlConnection(connectionString);
                sql.Open();
                string query = "Delete from Admin where UserName= '" + textBox1.Text.Trim() + "'";
                SqlCommand q1 = new SqlCommand(query, sql);

                q1.ExecuteNonQuery();

                MessageBox.Show("Admin Succesfully Deleted!!!!! ");
                //((DataTable)dataGridView2.DataSource).Rows.Clear();
                textBox1.Text = "";
                dataGridView2.DataSource = null;
                button2.Enabled = false;
                load2();
                sql.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : "+ex);
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection sql = new SqlConnection(connectionString);
            sql.Open();
            string query = "Select UserName,Email,PhoneNo from Admin where UserName= '" + textBox1.Text.Trim() + "'";
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
                MessageBox.Show("Admin not found!!!!! ");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ((DataTable)dataGridView2.DataSource).Rows.Clear();
            textBox1.Text = "";
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            textBox1.Text = "";
            
            dataGridView2.DataSource = null;
            button2.Enabled = false;
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Bitmap obj = new Bitmap(this.dataGridView3.Width,this.dataGridView3.Height);
            dataGridView3.DrawToBitmap(obj,new Rectangle(0,0,this.dataGridView3.Width,this.dataGridView3.Height));

            e.Graphics.DrawImage(obj,250,90);
            button7.Enabled = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow sr = dataGridView3.Rows[index];
            textBox2.Text = sr.Cells[0].Value.ToString();
            textBox3.Text = sr.Cells[3].Value.ToString();
            textBox4.Text = sr.Cells[2].Value.ToString();
            button6.Enabled = true;
            //button7.Enabled = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == " ")
            {
                delete_();
            }
            else
            {
                MessageBox.Show("No Row Is Selected!!!!");
            }

        }

        public void delete_()
        {
            try
            {
                SqlConnection sql = new SqlConnection(connectionString);
                sql.Open();
                string query = "Delete from ClientOrder where CustomerName = '" + textBox2.Text.Trim() + "'";
                SqlCommand q = new SqlCommand(query, sql);

                q.ExecuteNonQuery();
                MessageBox.Show("The Order Is Deleted!!!!");
                sql.Close();
                clear1();
                load();
            }
            catch(Exception)
            {
                MessageBox.Show("No Row Is Selected!!!!");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {

            try
            {

                SqlConnection sql = new SqlConnection(connectionString);
                sql.Open();
                for (int i = 0; i < dataGridView3.Rows.Count; i++)
                {
                    string query = "Select Quantity from Medicine where ItemName='" + textBox4.Text + "'";

                    ///SqlCommand q1 = new SqlCommand(query, sql);
                    SqlDataAdapter sda = new SqlDataAdapter(query, sql);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    string q = dt.Rows[0]["Quantity"].ToString();
                    //if (q == "0")
                    //{
                    textBox5.Text = (int.Parse(q) - int.Parse(textBox3.Text)).ToString();
                    //}
                    //else
                    //{
                    //    MessageBox.Show("Stock is Empty");
                    //    clear1();
                    //}
                }
                string query3 = "Update Medicine set Quantity='" + textBox5.Text + "' where ItemName= '" + textBox4.Text.Trim() + "' ";

                SqlCommand q2 = new SqlCommand(query3, sql);

                q2.ExecuteNonQuery();
                MessageBox.Show("Quantity Updated!!! ", "Successful");
                delete_();
                clear1();
                sql.Close();
                button6.Enabled = false;
            }
            catch(Exception)
            {
                MessageBox.Show("Row Not Selected!! ", "Error");
            }
                
            }


        public void clear1()
        {
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
        }

          
           
        

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


      
    }
}
