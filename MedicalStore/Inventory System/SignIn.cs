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
    public partial class SignIn : MetroFramework.Forms.MetroForm
    {
        String connectionString = "Data Source=DESKTOP-TL4IJDB;Initial Catalog=MedicalStore;User ID=SA;Password=12345";
        public SignIn()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f = new Form1();
            f.ShowDialog();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            SqlConnection sql = new SqlConnection(connectionString);
            sql.Open();

            if (sql.State == ConnectionState.Open)
            {

                string query = "Select * from Client where Username ='" + username.Text.Trim() + "' and Password ='" + password.Text.Trim() + "'";
                SqlDataAdapter q1 = new SqlDataAdapter(query, sql);
                DataTable d = new DataTable();
                q1.Fill(d);
                
                if (d.Rows.Count == 1)
                {
                    foreach (DataRow dr in d.Rows)
                    {
                        if (dr["Status"].ToString() == "0")
                        {
                            MessageBox.Show("Wait Admin Verification.", "Warning");
                            username.Text = "";
                            password.Text = "";
                        }
                        else
                        {
                            this.Hide();
                            Sell sp = new Sell();
                            sp.ShowDialog();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Your Username Or Password Is Incorrect", "Warning");
                }
            }

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            ForgotPassword f1 = new ForgotPassword();
            f1.ShowDialog();
        }
    }
}
