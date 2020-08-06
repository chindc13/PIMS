using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace PIMS_Final_Build
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            auser.Text = "";
        }



        private void button4_Click(object sender, EventArgs e)
        {
            if (auser.Text == "" || apass.Text == "")
            {
                MessageBox.Show("Please Provide Username and Password");
                return;
            }
            try
            {
                string lc = "Server=localhost;Port=3306;database=final_pims;Uid=root;Pwd=''";
                MySqlConnection con = new MySqlConnection(lc);
                MySqlDataReader mdr;
                MySqlCommand cmd = new MySqlCommand("Select * from `accounts` Where auser=@auser and apass=@apass", con);
                cmd.Parameters.AddWithValue("@auser", auser.Text);
                cmd.Parameters.AddWithValue("@apass", apass.Text);
                con.Open();
                MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                con.Close();
                int count = ds.Tables[0].Rows.Count;
                if (count == 1)
                {
                    this.Hide();
                    Form2 m = new Form2(auser.Text);
                    m.Show();
                }
                else
                {
                    MessageBox.Show("Login Failed");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        

     

       

        
    }
}
