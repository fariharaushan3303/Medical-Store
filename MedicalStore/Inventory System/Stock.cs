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
    
    public partial class Stock : MetroFramework.Forms.MetroForm
    {
        String connectionString = "Data Source=DESKTOP-TL4IJDB;Initial Catalog=MedicalStore;User ID=SA;Password=12345";
        string imgLocation = "";
      
       
        
        
        public Stock()
        {
            InitializeComponent();
        }
        public void clear()
        {
            itemname.Text = "";
            groupname.Text = "";
            power.Text = "";
            itemprice.Text = "";
            quantity.Text = "";
            companyname.Text = "";
            expiredate.Text = "";
            pictureBox1.Image = null;


        }


        private void button4_Click(object sender, EventArgs e)
        {

            SqlConnection sql = new SqlConnection(connectionString);
            sql.Open();
            string query = "Select Quantity,Image from Medicine where ItemName= '" + textBox1.Text.Trim() + "'";
            SqlCommand cmd = new SqlCommand(query, sql);
            SqlDataReader a = cmd.ExecuteReader();

            a.Read();

          
           

            if (a.HasRows)
            {
                string q = a[0].ToString();
                textBox3.Text = q;

                

                byte[] img = ((byte[])a[1]);
                if (img == null)
                {
                    pictureBox2.Image = null;
                }
                else
                {
                    MemoryStream mstream = new MemoryStream(img);
                    pictureBox2.Image = Image.FromStream(mstream);
                }
                button2.Enabled = true;
                
            }
            else
            {
                MessageBox.Show("Medicine Not Found!!!","Sorry");
            }


            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection sql = new SqlConnection(connectionString);
                byte[] images = null;
                FileStream Stream = new FileStream(imgLocation, FileMode.Open, FileAccess.Read);
                BinaryReader brs = new BinaryReader(Stream);
                images = brs.ReadBytes((int)Stream.Length);


                sql.Open();

                if (sql.State == ConnectionState.Open)
                {
                    if (itemname.Text == "" || groupname.Text == "" || power.Text == "" || itemprice.Text == "" || quantity.Text == "" || companyname.Text == "" || expiredate.Text == "")
                    {
                        MessageBox.Show("Please fill up all the components!!!", "Warning");
                    }

                    else
                    {

                        string query = "Insert into Medicine (ItemName,GroupName,Power,Price,Quantity,CompanyName,ExpireDate,Image) values ('" + itemname.Text + "', '" + groupname.Text + "','" + power.Text + "','" + itemprice.Text + "','" + quantity.Text + "','" + companyname.Text + "','" + expiredate.Text + "',@images) ";

                        SqlCommand q1 = new SqlCommand(query, sql);
                        q1.Parameters.Add(new SqlParameter("@images", images));
                        q1.ExecuteNonQuery();
                        MessageBox.Show("Registration Successful.");
                        clear();
                        load();

                    }




                }
            }
            catch(Exception)
            {
                MessageBox.Show("Add All The Component First!!!");
            }

        }

        private void companyname_SelectedIndexChanged(object sender, EventArgs e)
        {
           

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {
        }

        private void Stock_Load(object sender, EventArgs e)
        {
            SqlConnection sql = new SqlConnection(connectionString);
            sql.Open();

            if (sql.State == ConnectionState.Open)
            {

                string query = "Select CompanyName from Company";
                SqlDataAdapter q1 = new SqlDataAdapter(query, sql);
                DataTable d = new DataTable();
                q1.Fill(d);

                foreach (DataRow dr in d.Rows)
                {
                    companyname.Items.Add(dr["CompanyName"].ToString());

                }
                sql.Close();
            }

            sql.Open();
            string query2 = "Select ItemName,GroupName,Power,Price,Quantity,CompanyName,ExpireDate from Medicine";
            SqlDataAdapter sda = new SqlDataAdapter(query2, sql);

            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
            sql.Close();
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialoge = new OpenFileDialog();
            dialoge.Filter = "*png files(*.png)|*.png|jpg files(*.jpg)|*.jpg|All files(*.*)|*.*";
            if (dialoge.ShowDialog() == DialogResult.OK)
            {
                imgLocation = dialoge.FileName.ToString();
                pictureBox1.ImageLocation = imgLocation;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SqlConnection sql = new SqlConnection(connectionString);
            sql.Open();
            string query1 = "Select ItemName,GroupName,Power,Price,Quantity,CompanyName,ExpireDate from Medicine where ItemName= '" + textBox2.Text.Trim() + "'";
            //SqlCommand cmd =new  SqlCommand(query1, sql);
            //SqlDataReader sda = cmd.ExecuteReader();
            SqlDataAdapter sda = new SqlDataAdapter(query1, sql);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            if (dt.Rows.Count == 1)
            {
                dataGridView2.DataSource = dt;

                string query2 = "Select Image from Medicine where ItemName= '" + textBox2.Text.Trim() + "'";
                SqlCommand cmd = new SqlCommand(query2, sql);
                SqlDataReader a = cmd.ExecuteReader();

                a.Read();


                if (a.HasRows)
                {

                    byte[] img = ((byte[])a[0]);
                    if (img == null)
                    {
                        pictureBox3.Image = null;
                    }
                    else
                    {
                        MemoryStream mstream = new MemoryStream(img);
                        pictureBox3.Image = Image.FromStream(mstream);
                    }
                }
                //a.Close();
                sql.Close();
                button3.Enabled = true;
            }
            else
            {
                MessageBox.Show("Medicine not found!!!!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection sql = new SqlConnection(connectionString);
            sql.Open();
            string query = "Delete from Medicine where ItemName= '" + textBox2.Text.Trim() + "'";
            SqlCommand q1 = new SqlCommand(query, sql);
            q1.ExecuteNonQuery();
            MessageBox.Show("Item Succesfully Removed!!!!! ");
            dataGridView2.DataSource = null;
            textBox2.Text = "";
            pictureBox3.Image = null;
            button3.Enabled = false;
            load();
        }
         public void load()
        {
            try
            {
                SqlConnection sql = new SqlConnection(connectionString);
                sql.Open();
                string query = "Select * from Medicine";

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
        
        private void button7_Click(object sender, EventArgs e)
        {
           // ((DataTable)dataGridView2.DataSource).Rows.Clear();
            textBox2.Text = "";
            pictureBox3.Image = null;
            dataGridView2.DataSource = null;
            button3.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection sql = new SqlConnection(connectionString);
            sql.Open();
            textBox4.Text = (int.Parse(textBox3.Text) + int.Parse(quant.Text)).ToString();
            string query3 = "Update Medicine set Quantity='" + textBox4.Text + "' where Itemname= '" + textBox1.Text.Trim() + "' ";

            SqlCommand q = new SqlCommand(query3, sql);

            q.ExecuteNonQuery();
            MessageBox.Show("Quantity Updated!!! ", "Successful");
                //Process.Start();
            sql.Close();
            clear1();
            button2.Enabled = false;
            load();
        }

       public void clear1()
        {
            textBox1.Text = "";
            textBox3.Text = "";
            pictureBox2.Image = null;
            quant.Text = "";
            textBox4.Text = "";
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            clear1();
            button2.Enabled = false;
        }

        private void quant_TextChanged(object sender, EventArgs e)
        {
            //textBox4.Text = (int.Parse(textBox3.Text) + int.Parse(quant.Text)).ToString();          
        }

        
        

    }
        }
         
    

