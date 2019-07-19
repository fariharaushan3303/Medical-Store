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
    public partial class Registration : MetroFramework.Forms.MetroForm
    {
        String connectionString = "Data Source=DESKTOP-TL4IJDB;Initial Catalog=MedicalStore;User ID=SA;Password=12345";
        public Registration()
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





        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Registration_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
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
                        if (username.Text == "" || fullname.Text == "" || gender.Text == "" || email.Text == "" || phoneno.Text == "" || password.Text == "" || sequrityquestion.Text == "" || answer.Text == "")
                        {
                            MessageBox.Show("Please fill up all the components!!!", "Warning");
                        }

                        else
                        {




                            if (password.Text == confirmpassword.Text)
                            {
                                const int MIN_LENGHTH = 5;
                                string Password = password.Text;
                                //const int Phone_lenghth = 11;
                                //string mobile = phoneno.Text;
                                if (password.Text.Length >= MIN_LENGHTH && numberPass(password.Text) >= 1 && upperCase(password.Text) >= 1)
                                {


                                    string query = "Insert into Client (Username,Fullname,Gender,Email,Phoneno,Password,Sequrityquestion,Answer) values ('" + username.Text + "', '" + fullname.Text + "','" + gender.Text + "','" + email.Text + "','" + phoneno.Text + "','" + password.Text + "','" + sequrityquestion.Text + "','" + answer.Text + "') ";

                                    SqlCommand q1 = new SqlCommand(query, sql);

                                    q1.ExecuteNonQuery();
                                    MessageBox.Show("Registration Successful. Please wait for admin verification!!! ");
                                    this.Hide();
                                    Form1 f = new Form1();
                                    f.ShowDialog();
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
                    MessageBox.Show("UserName Already Taken","Error");
                    username.Text = "";
                }

                
            }
        

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f = new Form1();
            f.ShowDialog();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
    }
}
