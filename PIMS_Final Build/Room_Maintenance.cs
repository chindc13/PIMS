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
    public partial class Room_Maintenance : Form
    {
        public Room_Maintenance()
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
                cmd.CommandText = @"INSERT INTO `room_maintenance`(`roomid`, `roomnumber`, `department`, `roomcost`) 
VALUES (@roomid, @roomnumber, @department, @roomcost)";


                cmd.Parameters.AddWithValue("@roomid", roomid.Text.ToString());
                cmd.Parameters.AddWithValue("@roomnumber", roomnumber.Text.ToString());
                cmd.Parameters.AddWithValue("@department", department.Text.ToString());
                cmd.Parameters.AddWithValue("@roomcost", roomcost.Text.ToString());


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

            string selectQuery = "SELECT * FROM `room_maintenance` WHERE roomid =" + int.Parse(rsearch.Text);
            cmd = new MySqlCommand(selectQuery, MyConnection);

            mdr = cmd.ExecuteReader();
            if (mdr.Read())
            {
                rroomid.Text = mdr.GetString("roomid");
                rnumber.Text = mdr.GetString("roomnumber");
                rcost.Text = mdr.GetString("roomcost");
                rderpartment.Text = mdr.GetString("department");
            }
            else
            {
                MessageBox.Show("No Data Found For This Room.");
            }
            MyConnection.Close();
        }
        public void remove_care()
        {
            MySqlConnection MyConnection = new MySqlConnection(MyConnectionString);
            MySqlCommand cmd;
            MySqlDataReader mdr;

            MyConnection.Open();

            string selectQuery = "DELETE FROM `room_maintenance` where roomid =" + int.Parse(rroomid.Text);
            cmd = new MySqlCommand(selectQuery, MyConnection);

            mdr = cmd.ExecuteReader();
            if (mdr.Read())
            {

            }
            else
            {
                rnumber.Clear();
                roomcost.Clear();
                department.Clear();
                MessageBox.Show("No Data Found For This Room.");
            }
            MyConnection.Close();
        }
        public void changebasevalue()
        {
            MySqlConnection MyConnection = new MySqlConnection(MyConnectionString);
            MySqlCommand cmd;
            MySqlDataReader mdr;

            MyConnection.Open();

            string selectQuery = "SELECT * FROM `room_maintenance` WHERE roomid =" + int.Parse(csearch.Text);
            cmd = new MySqlCommand(selectQuery, MyConnection);

            mdr = cmd.ExecuteReader();
            if (mdr.Read())
            {
                croomid.Text = mdr.GetString("roomid");
                cnumber.Text = mdr.GetString("roomnumber");
                ccost.Text = mdr.GetString("roomcost");
                cdepartment.Text = mdr.GetString("department");
            }
            else
            {
                MessageBox.Show("No Data Found For This Room.");
            }
            MyConnection.Close();
        }
        public void changebasevalue_update()
        {
            MySqlConnection conn5 = new MySqlConnection(MyConnectionString);
            MySqlCommand cmd4;
            conn5.Open();
            try
            {

                cmd4 = conn5.CreateCommand();
                cmd4.CommandText = @"UPDATE `room_maintenance` SET `roomcost`=@roomcost WHERE `roomid`=@roomid";


                cmd4.Parameters.AddWithValue("@roomid", croomid.Text.ToString());
                cmd4.Parameters.AddWithValue("@roomcost", ccost.Text.ToString());
                

                cmd4.ExecuteNonQuery();
                MySqlDataAdapter sda4 = new MySqlDataAdapter(cmd4);
                DataSet ds4 = new DataSet();
                sda4.Fill(ds4);

            }
            catch (Exception ex)
            {
                croomid.Clear();
                cnumber.Clear();
                cdepartment.Clear();
                ccost.Clear();
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (conn5.State == ConnectionState.Open)
                {
                    croomid.Clear();
                    cnumber.Clear();
                    cdepartment.Clear();
                    ccost.Clear();
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
                cmd.CommandText = "SELECT * FROM `room_maintenance`";
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
            string query = "SELECT `roomid` FROM `room_maintenance` ";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                string val = dr[0].ToString();
                if (val == "")
                {
                    roomid.Text = "1";
                }
                else
                {
                    a = Convert.ToInt32(dr[0].ToString());
                    a = a + 1;
                    roomid.Text = a.ToString();
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

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            roomid.Clear();
            roomnumber.Clear();
            roomcost.Clear();
            department.Clear();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(roomid.Text) || String.IsNullOrEmpty(roomnumber.Text) || String.IsNullOrEmpty(roomcost.Text) ||
                String.IsNullOrEmpty(department.Text))
            {
                MessageBox.Show("Please fill up the form.");
            }
            else
            {
                register();
                roomid.Clear();
                roomnumber.Clear();
                roomcost.Clear();
                department.Clear();
            }

                
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel4.Visible = false;
            panel3.Visible = false;
            panel2.Visible = true;
            panel1.Visible = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(rroomid.Text))
            {
                MessageBox.Show("Please fill up the form.");
            }
            else
            {
                remove_care();
                
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

        private void button8_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            roomnumber.Clear();
            roomid.Clear();
            roomcost.Clear();
            rderpartment.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel4.Visible = false;
            panel3.Visible = true;
            panel2.Visible = false;
            panel1.Visible = false;
        }

        private void csearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(csearch.Text))
            {

            }
            else
            {
                changebasevalue();
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
            csearch.Clear();
            croomid.Clear();
            cnumber.Clear();
            cdepartment.Clear();
            ccost.Clear();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(croomid.Text))
            {

            }
            else
            {
                changebasevalue_update();

            }
        }

        private void Room_Maintenance_Load(object sender, EventArgs e)
        {
            generator();
            database_load();
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

