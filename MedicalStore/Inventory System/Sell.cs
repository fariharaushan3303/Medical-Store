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

namespace InventorySystem
{
    public partial class Sell : MetroFramework.Forms.MetroForm
    {
        String connectionString = "Data Source=DESKTOP-TL4IJDB;Initial Catalog=MedicalStore;User ID=SA;Password=12345";
        public Sell()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
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
        

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection sql = new SqlConnection(connectionString);
            sql.Open();

            if (sql.State == ConnectionState.Open)
            {

                string query = "Select * from Medicine where ItemName ='" + txtiname.Text.Trim() + "' ";
                SqlCommand cmd = new SqlCommand(query, sql);
                SqlDataReader a = cmd.ExecuteReader();
                a.Read();
                if (a.HasRows)
                {
                    string q1 = a[4].ToString();
                    string a1 = a[3].ToString();

                    qnt.Text = q1;
                    qprice.Text = a1;
                }
                else
                {
                    MessageBox.Show("Product Not Found!!", "Warning");
                }

                

            }

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
        private void txtq_TextChanged(object sender, EventArgs e)
        {


            try
            {
                SqlConnection sql = new SqlConnection(connectionString);
                sql.Open();

                if (sql.State == ConnectionState.Open)
                {
                    if (numberPass(txtq.Text) >= 1)
                    {
                        string query = "Select * from Medicine where ItemName ='" + txtiname.Text.Trim() + "' ";
                        SqlCommand cmd = new SqlCommand(query, sql);
                        SqlDataReader o = cmd.ExecuteReader();
                        o.Read();
                        string q1 = o[0].ToString();
                        string q2 = o[3].ToString();


                        //gvsales.Rows.Add(q1, q2, txtq.Text, (int.Parse(qprice.Text) * int.Parse(txtq.Text)).ToString());
                        gvsales.Rows.Add(q1, q2, txtq.Text, (int.Parse(qprice.Text) * int.Parse(txtq.Text)).ToString());
                        sql.Close();
                    }
                    else
                    {
                        MessageBox.Show("Please Enter Valid Number");
                    }
                    //button1.Enabled = true;
                }
            }
            catch(Exception)
            {
                MessageBox.Show("Search Iteam First!!!","Error");
            }

        }
        public void Clear1()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            txtiname.Text = "";
            gvsales.Rows.Clear();
            txtq.Text = "";
            qprice.Text = "0";
            qnt.Text = "0";
           


        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "" && textBox2.Text == "" && txtiname.Text == "" && txtq.Text =="")
                {
                    MessageBox.Show("Please Fill All The Component");
                }
                else
                {
                    try
                    {
                        SqlConnection sql = new SqlConnection(connectionString);
                        sql.Open();

                        if (sql.State == ConnectionState.Open)
                        {
                            for (int i = 0; i < gvsales.Rows.Count - 1; i++)
                            {
                                string query = "Insert into ClientOrder (CustomerName,Address,ItemName,Quantity,Price) Values ('" + textBox1.Text + "','" + textBox2.Text + "','" + gvsales.Rows[i].Cells[0].Value + "','" + gvsales.Rows[i].Cells[2].Value + "','" + gvsales.Rows[i].Cells[3].Value + "')";

                                SqlCommand q1 = new SqlCommand(query, sql);
                                q1.ExecuteNonQuery();
                                sql.Close();



                                MessageBox.Show("Order Placed!!!");
                                
                            }

                            Clear1();

                            //((DataTable)gvsales.DataSource).Rows.Clear();

                        }
                    }

                    catch (Exception)
                    {
                        MessageBox.Show("Insert Iteam First!!!","Error");
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("You Have Already Submit An Order!!! Wait Confirm that First");
            }
               
               
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //SqlConnection sql = new SqlConnection(connectionString);
            //sql.Open();
            //if (sql.State == ConnectionState.Open)
            //{
            //    textBox3.Text = (int.Parse(qnt.Text) - int.Parse(txtq.Text)).ToString();

            //    string query = "Update Medicine set Quantity='" + textBox3.Text + "' where ItemName= '" + textBox4.Text+ "' ";

            //    SqlCommand q = new SqlCommand(query, sql);
            //    //SqlDataReader o = q.ExecuteReader();
            //    q.ExecuteNonQuery();

            //    MessageBox.Show("Request Order Successful!!! ", "Successful");

            //    sql.Close();
            //}
        }

        private void txtiname_TextChanged(object sender, EventArgs e)
        {
            //button3.Enabled = true;
                
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f1 = new Form1();
            f1.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Clear1();
        }

        private void gvsales_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (gvsales.Columns[e.ColumnIndex].Name == "REMOVE")
            //{
                
            //}
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                gvsales.Rows.RemoveAt(gvsales.SelectedRows[0].Index);
                txtq.Text = "";
                qprice.Text = "0";
                qnt.Text = "0";
                txtiname.Text = "";
            }
            catch(Exception)
            {
                MessageBox.Show("Row Does Not Selected Yet!!!");
            }
        }
    }
}
