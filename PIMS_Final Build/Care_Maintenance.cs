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
    public partial class Care_Maintenance : Form
    {
        public Care_Maintenance()
        {
            InitializeComponent();
        }
        string MyConnectionString = "Server=localhost;Port=3306;database=final_pims;Uid=root;Pwd=''";
        public void register()
        {
            MySqlConnection connection = new MySqlConnection(MyConnectionString);
            MySqlCommand cmd;
            connection.Open();


            try
            {
                cmd = connection.CreateCommand();
                cmd.CommandText = @"INSERT INTO `typeofcare`(`careid`, `name`, `carecost`, `additionalnotes`) 
VALUES (@careid, @name, @carecost, @additionalnotes)";


                cmd.Parameters.AddWithValue("@careid", careid.Text.ToString());
                cmd.Parameters.AddWithValue("@name", name.Text.ToString());
                cmd.Parameters.AddWithValue("@carecost", carecost.Text.ToString());
                cmd.Parameters.AddWithValue("@additionalnotes", additionalnotes.Text.ToString());
                

                cmd.ExecuteNonQuery();
                MySqlDataAdapter sda2 = new MySqlDataAdapter(cmd);
                DataSet ds2 = new DataSet();
                sda2.Fill(ds2);
            }

            catch (Exception)
            {
                MessageBox.Show("New Care has bee save.");
            }
        }

        public void remove_search()
        {
            MySqlConnection MyConnection = new MySqlConnection(MyConnectionString);
            MySqlCommand cmd;
            MySqlDataReader mdr;

            MyConnection.Open();

            string selectQuery = "Select * from `typeofcare` where careid =" + int.Parse(rsearch.Text);
            cmd = new MySqlCommand(selectQuery, MyConnection);

            mdr = cmd.ExecuteReader();
            if (mdr.Read())
            {
                rcareid.Text = mdr.GetString("careid");
                rname.Text = mdr.GetString("name");
                rcost.Text = mdr.GetString("carecost");
                radditionalnotes.Text = mdr.GetString("additionalnotes");
            }
            else
            {
                MessageBox.Show("No Data Found For This Type of Care.");
            }
            MyConnection.Close();
        }
        public void remove_care()
        {
            MySqlConnection MyConnection = new MySqlConnection(MyConnectionString);
            MySqlCommand cmd;
            MySqlDataReader mdr;

            MyConnection.Open();

            string selectQuery = "DELETE FROM `typeofcare` where careid =" + int.Parse(rcareid.Text);
            cmd = new MySqlCommand(selectQuery, MyConnection);

            mdr = cmd.ExecuteReader();
            if (mdr.Read())
            {
                
            }
            else
            {
                MessageBox.Show("No Data Found For This Type of Care.");
            }
            MyConnection.Close();
        }

        public void changevalue_search()
        {
            MySqlConnection MyConnection = new MySqlConnection(MyConnectionString);
            MySqlCommand cmd;
            MySqlDataReader mdr;

            MyConnection.Open();

            string selectQuery = "Select * from `typeofcare` where careid =" + int.Parse(usearch.Text);
            cmd = new MySqlCommand(selectQuery, MyConnection);

            mdr = cmd.ExecuteReader();
            if (mdr.Read())
            {
                ucareid.Text = mdr.GetString("careid");
                uname.Text = mdr.GetString("name");
                ucost.Text = mdr.GetString("carecost");
                uadditionalnotes.Text = mdr.GetString("additionalnotes");
            }
            else
            {
                MessageBox.Show("No Data Found For This Type of Care.");
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
                cmd4.CommandText = @"UPDATE `typeofcare` SET `careid`=@careid,`name`=@name,`carecost`=@carecost,`additionalnotes`=@additionalnotes WHERE `careid`=@careid";


                cmd4.Parameters.AddWithValue("@careid", ucareid.Text.ToString());
                cmd4.Parameters.AddWithValue("@name", uname.Text.ToString());
                cmd4.Parameters.AddWithValue("@carecost", ucost.Text.ToString());
                cmd4.Parameters.AddWithValue("@additionalnotes", uadditionalnotes.Text.ToString());
                
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
                cmd.CommandText = "SELECT * FROM `typeofcare`";
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
        public void generator()
        {
            int a;
            MySqlConnection connection = new MySqlConnection(MyConnectionString);
            connection.Open();
            string query = "SELECT * FROM `typeofcare`";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                string val = dr[0].ToString();
                if (val == "")
                {
                    careid.Text = "1";
                }
                else
                {
                    a = Convert.ToInt32(dr[0].ToString());
                    a = a + 1;
                    careid.Text = a.ToString();
                }
            }
        }




        private void careid_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
        }
        private void rsearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
        }
        private void usearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
        }




        private void button1_Click(object sender, EventArgs e)
        {
            new1.Visible = true;
            remove1.Visible = false;
            changevalue.Visible = false;
            database.Visible = false;
        }





        private void button32_Click(object sender, EventArgs e)
        {
            register();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            new1.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            database.Visible = false;
            new1.Visible = false;
            remove1.Visible = true;
            changevalue.Visible = false;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(rsearch.Text))
            {

            }
            else
            {
                remove_search();
            }
            
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(rcareid.Text))
            {

            }
            else
            {
                DialogResult dr = MessageBox.Show("Are you sure you want to remove this Type Of Care", "Removing", MessageBoxButtons.YesNo);
                switch (dr)
                {
                    case DialogResult.Yes:
                        rcareid.Clear();
                        rname.Clear();
                        rcost.Clear();
                        radditionalnotes.Clear();
                        remove_care();
                        break;
                    case DialogResult.No:
                        break;
                }
                
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            remove1.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(usearch.Text))
            {

            }
            else
            {
                changevalue_search();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new1.Visible = false;
            remove1.Visible = false;
            database.Visible = false;
            changevalue.Visible = true;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(ucareid.Text) || String.IsNullOrEmpty(uname.Text) || String.IsNullOrEmpty(ucost.Text) || String.IsNullOrEmpty(uadditionalnotes.Text))
            {
                MessageBox.Show("Please fill up the form before Changing the Value");
            }
            else
            {
                changebasevalue();
                uadditionalnotes.Clear();
                ucost.Clear();
                uname.Clear();
                ucareid.Clear();
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            new1.Visible = false;
            remove1.Visible = false;
            changevalue.Visible = false;
            database.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            database.Visible = true;
            new1.Visible = false;
            remove1.Visible = false;
            changevalue.Visible = false;
        }

        private void Care_Maintenance_Load(object sender, EventArgs e)
        {
            generator();
            database_load();
        }

        private void ucost_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
        }

        

        

        




    }
}
