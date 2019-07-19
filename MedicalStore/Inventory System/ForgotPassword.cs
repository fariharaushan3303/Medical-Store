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
    public partial class ForgotPassword :MetroFramework.Forms.MetroForm
    {
        String connectionString = "Data Source=DESKTOP-TL4IJDB;Initial Catalog=MedicalStore;User ID=SA;Password=12345";
        public ForgotPassword()
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
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection sql = new SqlConnection(connectionString);
            sql.Open();

            if (sql.State == ConnectionState.Open)
            {

                string query = "Select * from Client where Username ='" + textBox1.Text.Trim() + "' ";
                SqlDataAdapter q1 = new SqlDataAdapter(query, sql);
                DataTable d = new DataTable();
                q1.Fill(d);

                if (d.Rows.Count == 1)
                {
                    string query1 = "Select Sequrityquestion,Answer from Client where Username ='" + textBox1.Text.Trim() + "' ";

                    SqlCommand cmd = new SqlCommand(query1, sql);
                    SqlDataReader a = cmd.ExecuteReader();

                    a.Read();

                    string question = a[0].ToString();
                    string ans = a[1].ToString();

                    if (question == comboBox1.Text && ans == textBox2.Text)
                    {
                        a.Close();
                        if (password.Text == confirmpassword.Text)
                        {
                            const int MIN_LENGHTH = 5;
                            //string Password = password.Text;
                           
                            if (password.Text.Length >= MIN_LENGHTH && numberPass(password.Text) >= 1 && upperCase(password.Text) >= 1)
                            {


                                string query3 = "Update Client set Password='"+password.Text+"' where Username= '"+ textBox1.Text.Trim() + "' ";

                                SqlCommand q = new SqlCommand(query3, sql);
                                
                                q.ExecuteNonQuery();
                                MessageBox.Show("Password successfully changed ");
                                this.Hide();
                                SignIn f = new SignIn();
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
                    else
                    {
                        MessageBox.Show("Your security question or answer may wrong", "Warning");
                    }

                }
                else
                {
                    MessageBox.Show("Your Username doesn't match!! plz enter valid username", "Warning");
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            SignIn s = new SignIn();
            s.ShowDialog();
        }
    }
}
