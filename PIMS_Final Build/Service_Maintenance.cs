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
    public partial class Service_Maintenance : Form
    {
        public Service_Maintenance()
        {
            InitializeComponent();
        }
        string MyConnectionString = "Server=localhost;Port=3306;database=final_pims;Uid=root;Pwd=''";


        public void generator()
        {
            int a;
            MySqlConnection connection = new MySqlConnection(MyConnectionString);
            connection.Open();
            string query = "Select `service_id` from `service_maintenance`";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                string val = dr[0].ToString();
                if (val == "")
                {
                    serviceid.Text = "1";
                }
                else
                {
                    a = Convert.ToInt32(dr[0].ToString());
                    a = a + 1;
                    serviceid.Text = a.ToString();
                }
            }
        }
        public void register()
        {
            MySqlConnection connection = new MySqlConnection(MyConnectionString);
            MySqlCommand cmd;
            connection.Open();


            try
            {
                cmd = connection.CreateCommand();
                cmd.CommandText = @"INSERT INTO `service_maintenance`(`service_id`, `service_name`, `amount`, `averageduration`, `additionalnotes`) 
VALUES (@service_id, @service_name, @amount, @averageduration, @additionalnotes)";


                cmd.Parameters.AddWithValue("@service_id", serviceid.Text.ToString());
                cmd.Parameters.AddWithValue("@service_name", name.Text.ToString());
                cmd.Parameters.AddWithValue("@amount", amount.Text.ToString());
                cmd.Parameters.AddWithValue("@averageduration", duration.Text.ToString());
                cmd.Parameters.AddWithValue("@additionalnotes", notes.Text.ToString());



                cmd.ExecuteNonQuery();
                MySqlDataAdapter sda2 = new MySqlDataAdapter(cmd);
                DataSet ds2 = new DataSet();
                sda2.Fill(ds2);
            }

            catch (Exception)
            {
                MessageBox.Show("New Service has bee save.");
            }
        }
        public void remove_service()
        {
            MySqlConnection MyConnection = new MySqlConnection(MyConnectionString);
            MySqlCommand cmd;
            MySqlDataReader mdr;

            MyConnection.Open();

            string selectQuery = "DELETE FROM `service_maintenance` WHERE service_id =" + int.Parse(rid.Text);
            cmd = new MySqlCommand(selectQuery, MyConnection);

            mdr = cmd.ExecuteReader();
            if (mdr.Read())
            {

            }
            else
            {
                rsearch.Clear();
                rid.Text = "0";
                rname.Clear();
                ramount.Clear();
                rnotes.Clear();
                rduration.Clear();
                MessageBox.Show("No Data Found For This Room.");
            }
            MyConnection.Close();
        }
        public void remove_search()
        {
            MySqlConnection MyConnection = new MySqlConnection(MyConnectionString);
            MySqlCommand cmd;
            MySqlDataReader mdr;

            MyConnection.Open();

            string selectQuery = "SELECT * FROM `service_maintenance` WHERE service_id =" + int.Parse(rsearch.Text);
            cmd = new MySqlCommand(selectQuery, MyConnection);

            mdr = cmd.ExecuteReader();
            if (mdr.Read())
            {
                rid.Text = mdr.GetString("service_id");
                rname.Text = mdr.GetString("service_name");
                ramount.Text = mdr.GetString("amount");
                rduration.Text = mdr.GetString("averageduration");
                rnotes.Text = mdr.GetString("additionalnotes");
            }
            else
            {
                MessageBox.Show("No Data Found For This Room.");
            }
            MyConnection.Close();
        }
        public void change_search()
        {
            MySqlConnection MyConnection = new MySqlConnection(MyConnectionString);
            MySqlCommand cmd;
            MySqlDataReader mdr;

            MyConnection.Open();

            string selectQuery = "SELECT * FROM `service_maintenance` WHERE service_id =" + int.Parse(csearch.Text);
            cmd = new MySqlCommand(selectQuery, MyConnection);

            mdr = cmd.ExecuteReader();
            if (mdr.Read())
            {
                cid.Text = mdr.GetString("service_id");
                cname.Text = mdr.GetString("service_name");
                camount.Text = mdr.GetString("amount");
                cduration.Text = mdr.GetString("averageduration");
                cnotes.Text = mdr.GetString("additionalnotes");
            }
            else
            {
                MessageBox.Show("No Data Found For This Room.");
            }
            MyConnection.Close();
        }
        public void changebasevalue()
        {
            MySqlConnection conn5 = new MySqlConnection(MyConnectionString);
            MySqlCommand cmd4;
            conn5.Open();
            try
            {

                cmd4 = conn5.CreateCommand();
                cmd4.CommandText = @"UPDATE `service_maintenance` SET `amount`=@amount WHERE `service_id`=@serviceid";


                cmd4.Parameters.AddWithValue("@amount", camount.Text.ToString());
                cmd4.Parameters.AddWithValue("@serviceid", cid.Text.ToString());
                

                cmd4.ExecuteNonQuery();
                MySqlDataAdapter sda4 = new MySqlDataAdapter(cmd4);
                DataSet ds4 = new DataSet();
                sda4.Fill(ds4);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (conn5.State == ConnectionState.Open)
                {
                    cid.Text = "0";
                    cname.Clear();
                    camount.Clear();
                    cduration.Clear();
                    cnotes.Clear();
                    camount.Clear();
                    MessageBox.Show("Value has been Change Successfully.");
                }
            }
        }
        public void database_load()
        {
            MySqlConnection connection = new MySqlConnection(MyConnectionString);
            connection.Open();
            try
            {
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM `service_maintenance`";
                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adap.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0].DefaultView;
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





        private void button1_Click(object sender, EventArgs e)
        {
            panel4.Visible = false;
            panel3.Visible = false;
            panel2.Visible = false;
            panel1.Visible = true;

        }

        private void button11_Click(object sender, EventArgs e)
        {
            register();
        }

        private void Service_Maintenance_Load(object sender, EventArgs e)
        {
            database_load();
            generator();

        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(rid.Text))
            {
                MessageBox.Show("Please fill up the form");
            }
            else
            {
                DialogResult dr = MessageBox.Show("Are you sure you want to remove this Service", "Removing", MessageBoxButtons.YesNo);
                switch (dr)
                {
                    case DialogResult.Yes:
                        remove_service();
                        break;
                    case DialogResult.No:
                        break;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel4.Visible = false;
            panel3.Visible = false;
            panel2.Visible = true;
            panel1.Visible = false;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            remove_search();
        }

        private void amount_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
        }

        private void rsearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel4.Visible = false;
            panel3.Visible = true;
            panel2.Visible = false;
            panel1.Visible = false;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            change_search();
        }

        private void camount_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            changebasevalue();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel4.Visible = true;
            panel3.Visible = false;
            panel2.Visible = false;
            panel1.Visible = false;
        }

        
    }
}
