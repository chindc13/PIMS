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
    public partial class payment1 : Form
    {
        public payment1()
        {
            InitializeComponent();
        }
        string MyConnectionString = "Server=localhost;Port=3306;database=final_pims;Uid=root;Pwd=''";

        private void Form4_Load(object sender, EventArgs e)
        {
            GetData2();
            GetData1();
        }
        private void GetData1()
        {
            MySqlConnection connection = new MySqlConnection(MyConnectionString);
            connection.Open();
            try
            {
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM `outpatient_payment`";
                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adap.Fill(ds);
                payment.DataSource = ds.Tables[0].DefaultView;


            }
            catch (Exception ex3)
            {
                MessageBox.Show(ex3.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
        private void GetData2()
        {
            MySqlConnection connection = new MySqlConnection(MyConnectionString);
            connection.Open();
            try
            {
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM `outpatient_treatment`";
                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adap.Fill(ds);
                diagnosed.DataSource = ds.Tables[0].DefaultView;


            }
            catch (Exception ex3)
            {
                MessageBox.Show(ex3.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            diagnosed.Visible = false;
            payment.Visible = true;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            diagnosed.Visible = true;
            payment.Visible = false;
        }

        private void printToolStripButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Insert the printer");
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            diagnosed.CurrentRow.Visible = false;
        }

       
    }
}
