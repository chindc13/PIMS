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
    public partial class Medicine_Maintenance : Form
    {

        public Medicine_Maintenance()
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
                cmd.CommandText = @"INSERT INTO `medical_maintenance`(`medicineid`, `medicine_name`, `dosageform`, `unitprice`, `unitstock`, `additionalnotes`) 
VALUES (@medicineid, @medicine_name, @dosageform, @unitprice, @unitstock, @additionalnotes)";


                cmd.Parameters.AddWithValue("@medicineid", medicineid.Text.ToString());
                cmd.Parameters.AddWithValue("@medicine_name", name.Text.ToString());
                cmd.Parameters.AddWithValue("@dosageform", dosage.Text.ToString());
                cmd.Parameters.AddWithValue("@unitprice", price.Text.ToString());
                cmd.Parameters.AddWithValue("@unitstock", stock.Text.ToString());
                cmd.Parameters.AddWithValue("@additionalnotes", notes.Text.ToString());


                cmd.ExecuteNonQuery();
                MySqlDataAdapter sda2 = new MySqlDataAdapter(cmd);
                DataSet ds2 = new DataSet();
                sda2.Fill(ds2);
            }

            catch (Exception)
            {
                MessageBox.Show("New Medicine has bee save.");
            }
        }

        public void remove_search()
        {
            MySqlConnection MyConnection = new MySqlConnection(MyConnectionString);
            MySqlCommand cmd;
            MySqlDataReader mdr;

            MyConnection.Open();

            string selectQuery = "Select * from `medical_maintenance` where medicineid =" + int.Parse(rsearch.Text);
            cmd = new MySqlCommand(selectQuery, MyConnection);

            mdr = cmd.ExecuteReader();
            if (mdr.Read())
            {
                rmedicineid.Text = mdr.GetString("medicineid");
                rname.Text = mdr.GetString("medicine_name");
                rdosage.Text = mdr.GetString("dosageform");
                rprice.Text = mdr.GetString("unitprice");
                rstock.Text = mdr.GetString("unitstock");
                rnotes.Text = mdr.GetString("additionalnotes");
            }
            else
            {
                MessageBox.Show("No Data Found For This Medicine.");
            }
            MyConnection.Close();
        }
        public void remove_care()
        {
            MySqlConnection MyConnection = new MySqlConnection(MyConnectionString);
            MySqlCommand cmd;
            MySqlDataReader mdr;

            MyConnection.Open();

            string selectQuery = "DELETE FROM `medical_maintenance` where medicineid =" + int.Parse(rmedicineid.Text);
            cmd = new MySqlCommand(selectQuery, MyConnection);

            mdr = cmd.ExecuteReader();
            if (mdr.Read())
            {

            }
            else
            {
                MessageBox.Show("No Data Found For This Medicine.");
                rmedicineid.Text = "0";
            }
            MyConnection.Close();
        }

        public void change_search()
        {
            MySqlConnection MyConnection = new MySqlConnection(MyConnectionString);
            MySqlCommand cmd;
            MySqlDataReader mdr;

            MyConnection.Open();

            string selectQuery = "Select * from `medical_maintenance` where medicineid =" + int.Parse(csearch.Text);
            cmd = new MySqlCommand(selectQuery, MyConnection);

            mdr = cmd.ExecuteReader();
            if (mdr.Read())
            {
                cmedicineid.Text = mdr.GetString("medicineid");
                cname.Text = mdr.GetString("medicine_name");
                cdosage.Text = mdr.GetString("dosageform");
                cprice.Text = mdr.GetString("unitprice");
                cstock.Text = mdr.GetString("unitstock");
                cnotes.Text = mdr.GetString("additionalnotes");
            }
            else
            {
                MessageBox.Show("No Data Found For This Medicine.");
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
                cmd4.CommandText = @"UPDATE `medical_maintenance` SET `unitprice`=@unitprice WHERE `medicineid`=@medicineid";


                cmd4.Parameters.AddWithValue("@medicineid", cmedicineid.Text.ToString());
                cmd4.Parameters.AddWithValue("@unitprice", cprice.Text.ToString());

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
                cmd.CommandText = "SELECT * FROM `medical_maintenance`";
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
            string query = "SELECT medicineid FROM `medical_maintenance`";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                string val = dr[0].ToString();
                if (val == "")
                {
                    medicineid.Text = "1";
                }
                else
                {
                    a = Convert.ToInt32(dr[0].ToString());
                    a = a + 1;
                    medicineid.Text = a.ToString();
                }
            }
        }  




        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel4.Visible = false;
            panel3.Visible = false;
            panel2.Visible = false;
            panel1.Visible = true;

        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            register();
            medicineid.Clear();
            name.Clear();
            dosage.Clear();
            price.Clear();
            stock.Clear();
            notes.Clear();
            
        }

        private void stock_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(medicineid.Text) || String.IsNullOrEmpty(name.Text) || String.IsNullOrEmpty(dosage.Text) ||
                String.IsNullOrEmpty(price.Text) || String.IsNullOrEmpty(stock.Text) || String.IsNullOrEmpty(notes.Text))
            {
                MessageBox.Show("Please fill up the formm");
            }
            else
            {
                register();
                medicineid.Clear();
                name.Clear();
                dosage.Clear();
                price.Clear();
                stock.Clear();
                notes.Clear();
            }
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

        private void rsearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure you want to remove this Type Of Care", "Removing", MessageBoxButtons.YesNo);
            switch (dr)
            {
                case DialogResult.Yes:
                    rname.Clear();
                    rdosage.Clear();
                    rprice.Clear();
                    rstock.Clear();
                    rnotes.Clear();
                    remove_care();
                    break;
                case DialogResult.No:
                    break;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            rmedicineid.Clear();
            rname.Clear();
            rdosage.Clear();
            rprice.Clear();
            rstock.Clear();
            rnotes.Clear();
            panel2.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel4.Visible = false;
            panel3.Visible = false;
            panel2.Visible = true;
            panel1.Visible = false;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            cname.Clear();
            cdosage.Clear();
            cprice.Clear();
            cstock.Clear();
            cnotes.Clear();
            panel3.Visible = false;

        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(csearch.Text))
            {

            }
            else
            {
                change_search();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel4.Visible = false;
            panel3.Visible = true;
            panel2.Visible = false;
            panel1.Visible = false;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(cprice.Text))
            {
                MessageBox.Show("Please put the new Base Value");
            }
            else
            {
                changebasevalue();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel4.Visible = true;
            panel3.Visible = false;
            panel2.Visible = false;
            panel1.Visible = false;
        }

        private void Medicine_Maintenance_Load(object sender, EventArgs e)
        {
            generator();
            database_load();
        }
    }
}
