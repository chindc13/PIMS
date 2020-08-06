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
    public partial class Doctor_Maintenance : Form
    {
        public Doctor_Maintenance()
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
                cmd.CommandText = @"INSERT INTO `doctor_maintenance`(`doctorid`, `name`, `sex`, `specialization`, `doctorcharges`, `contact`, `licencenumber`)
VALUES (@doctorid, @name, @sex, @specialization, @doctorcharges, @contact, @licencenumber)";


                cmd.Parameters.AddWithValue("@doctorid", doctorid.Text.ToString());
                cmd.Parameters.AddWithValue("@name", fullname.Text.ToString());
                cmd.Parameters.AddWithValue("@sex", sex.Text.ToString());
                cmd.Parameters.AddWithValue("@specialization", specialization.Text.ToString());
                cmd.Parameters.AddWithValue("@doctorcharges", doctorcharge.Text.ToString());
                cmd.Parameters.AddWithValue("@contact", contactnumber.Text.ToString());
                cmd.Parameters.AddWithValue("@licencenumber", licencesnumber.Text.ToString());



                cmd.ExecuteNonQuery();
                MySqlDataAdapter sda2 = new MySqlDataAdapter(cmd);
                DataSet ds2 = new DataSet();
                sda2.Fill(ds2);
            }

            catch (Exception)
            {
                MessageBox.Show("New Doctor has been Registered");
            }
        }

        public void remove_search()
        {
            MySqlConnection MyConnection = new MySqlConnection(MyConnectionString);
            MySqlCommand cmd;
            MySqlDataReader mdr;

            MyConnection.Open();

            string selectQuery = "Select * from `doctor_maintenance` where doctorid =" + int.Parse(rsearch.Text);
            cmd = new MySqlCommand(selectQuery, MyConnection);

            mdr = cmd.ExecuteReader();
            if (mdr.Read())
            {
                rdoctorid1.Text = mdr.GetString("doctorid");
                rname.Text = mdr.GetString("name");
                rsex.Text = mdr.GetString("sex");
                rspecialization.Text = mdr.GetString("specialization");
                rdoctorcharge.Text = mdr.GetString("doctorcharges");
                rcontact.Text = mdr.GetString("contact");
                rlicences.Text = mdr.GetString("licencenumber");
            }
            else
            {
                MessageBox.Show("No Data Found For This Type of Care.");
            }
            MyConnection.Close();
        }
        public void remove_doctor2()
        {
            MySqlConnection MyConnection = new MySqlConnection(MyConnectionString);
            MySqlCommand cmd;
            MySqlDataReader mdr;

            MyConnection.Open();

            string selectQuery = "DELETE FROM `doctor_maintenance` WHERE doctorid =" + int.Parse(rdoctorid1.Text);
            cmd = new MySqlCommand(selectQuery, MyConnection);

            mdr = cmd.ExecuteReader();
            if (mdr.Read())
            {
                rdoctorid1.Clear();
            }
            else
            {
                MessageBox.Show("This Doctor has no records");
            }
            MyConnection.Close();
        }

        public void update_search()
        {
            MySqlConnection MyConnection = new MySqlConnection(MyConnectionString);
            MySqlCommand cmd;
            MySqlDataReader mdr;

            MyConnection.Open();

            string selectQuery = "Select * from `doctor_maintenance` where doctorid =" + int.Parse(csearch.Text);
            cmd = new MySqlCommand(selectQuery, MyConnection);

            mdr = cmd.ExecuteReader();
            if (mdr.Read())
            {
                cdoctorid.Text = mdr.GetString("doctorid");
                cname.Text = mdr.GetString("name");
                csex.Text = mdr.GetString("sex");
                cspecialization.Text = mdr.GetString("specialization");
                cdoctorcharge.Text = mdr.GetString("doctorcharges");
                ccontact.Text = mdr.GetString("contact");
                clicences.Text = mdr.GetString("licencenumber");
            }
            else
            {
                MessageBox.Show("No Data Found For This Type of Care.");
            }
            MyConnection.Close();
        }
        public void update()
        {
            MySqlConnection conn5 = new MySqlConnection(MyConnectionString);
            MySqlCommand cmd4;
            conn5.Open();
            try
            {

                cmd4 = conn5.CreateCommand();
                cmd4.CommandText = @"UPDATE `doctor_maintenance` SET `doctorcharges`=@doctorcharges WHERE `doctorid`=@doctorid";

                cmd4.Parameters.AddWithValue("@doctorid", cdoctorid.Text.ToString());
                cmd4.Parameters.AddWithValue("@doctorcharges", cdoctorcharge.Text.ToString());

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
                cmd.CommandText = "SELECT * FROM `doctor_maintenance`";
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
            string query = "SELECT `doctorid` FROM `doctor_maintenance`";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                string val = dr[0].ToString();
                if (val == "")
                {
                    doctorid.Text = "1";
                }
                else
                {
                    a = Convert.ToInt32(dr[0].ToString());
                    a = a + 1;
                    doctorid.Text = a.ToString();
                }
            }
        }





        private void doctor_TextChanged(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
        }
        private void rsearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
        }





        private void button6_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(doctorid.Text) || String.IsNullOrEmpty(fullname.Text) || String.IsNullOrEmpty(sex.Text) || 
                String.IsNullOrEmpty(specialization.Text) || String.IsNullOrEmpty(doctorcharge.Text) || String.IsNullOrEmpty(contactnumber.Text) ||
                String.IsNullOrEmpty(licencesnumber.Text))
            {
                MessageBox.Show("Please fill up the form !!");
            }
            else
            {
                register();
                doctorid.Clear();
                fullname.Clear();
                doctorcharge.Clear();
                specialization.Clear();
                licencesnumber.Clear();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel4.Visible = false;
            panel3.Visible = false;
            panel2.Visible = false;
            panel1.Visible = true;
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

        private void button7_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            rdoctorid1.Clear();
            rsearch.Clear();
            rname.Clear();
            rdoctorcharge.Clear();
            rspecialization.Clear();
            rcontact.Clear();
            rlicences.Clear();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(rdoctorid1.Text))
            {
                rdoctorid1.Text = "0";
            }
            else
            {
                DialogResult dr = MessageBox.Show("Are you sure you want to remove this Doctor ??", "Removing", MessageBoxButtons.YesNo);
                switch (dr)
                {
                    case DialogResult.Yes:
                        rsearch.Clear();
                        rname.Clear();
                        rdoctorcharge.Clear();
                        rspecialization.Clear();
                        rcontact.Clear();
                        rlicences.Clear();
                        remove_doctor2();
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

        private void button11_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
        }


        private void button3_Click_1(object sender, EventArgs e)
        {
            panel4.Visible = false;
            panel3.Visible = true;
            panel2.Visible = false;
            panel1.Visible = false;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(csearch.Text))
            {

            }
            else
            {
                update_search();
            }
        }

        private void button11_Click_1(object sender, EventArgs e)
        {
            panel3.Visible = false;
            csearch.Clear();
            cdoctorcharge.Clear();
            cdoctorid.Clear();
            cname.Clear();
            clicences.Clear();
            ccontact.Clear();
            cspecialization.Clear();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(cdoctorcharge.Text)){
                MessageBox.Show("Please put some value on Doctor Charges");

            }
            else
            {
                update();
            }
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel4.Visible = true;
            panel3.Visible = false;
            panel2.Visible = false;
            panel1.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Doctor_Maintenance_Load(object sender, EventArgs e)
        {
            generator();
            database_load();
        }


        

        









    }
}
