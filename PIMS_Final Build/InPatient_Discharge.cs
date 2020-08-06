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
    public partial class InPatient_Discharge : Form
    {
        public InPatient_Discharge()
        {
            InitializeComponent();
        }

        string MyConnectionString = "Server=localhost;Port=3306;database=final_pims;Uid=root;Pwd=''";
        public void admission_table()
        {
            
            MySqlConnection connection = new MySqlConnection(MyConnectionString);
            connection.Open();
            try
            {
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM `inpatients_admissions`";
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
        public void patient_number()
        {
            MySqlConnection MyConnection = new MySqlConnection(MyConnectionString);
            MySqlCommand cmd;
            MySqlDataReader mdr;

            MyConnection.Open();

            string selectQuery = "Select * from `inpatient_billing` where PatientNumber =" + int.Parse(patientnumber.Text);
            cmd = new MySqlCommand(selectQuery, MyConnection);

            mdr = cmd.ExecuteReader();
            if (mdr.Read())
            {
                nettotal.Text = mdr.GetString("totalcost");
                amountpaid.Text = mdr.GetString("discount");
                balance.Text = mdr.GetString("balance");
            }
            else
            {
                MessageBox.Show("No Data Found For This Patient");
            }
            MyConnection.Close();
        }
        public void search_admission() 
        {
            MySqlConnection connection = new MySqlConnection(MyConnectionString);
            connection.Open();
            try
            {
                MySqlCommand cmd = connection.CreateCommand();

                string value = label7.Text;
                switch (value)
                {
                    case "PatientNumber":
                        cmd.CommandText = "Select * from `inpatients_admissions` where PatientNumber LIKE @searchKey";
                        break;


                    case "":
                        cmd.CommandText = "Select * from `inpatients_admissions` ";
                        textBox2.SelectionStart = 0;
                        textBox2.SelectionLength = textBox2.Text.Length;
                        break;
                }
                cmd.Parameters.AddWithValue("@searchKey", "%" + textBox2.Text.ToString() + "%");
                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adap.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0].DefaultView;
            }
            catch (Exception)
            {
            }
            finally
            {

            }
        }
        public void discharge_treatment()
        {
            MySqlConnection connection = new MySqlConnection(MyConnectionString);
            MySqlCommand cmd;
            connection.Open();


            try
            {
                cmd = connection.CreateCommand();
                cmd.CommandText = @"INSERT INTO `inpatient_discharge`(`PatientNumber`, `lastname`, `firstname`, `middlename`, `admitnumber`, `nettotal`, `amountpaid`, `balance`, `time`, `date`) 
VALUES (@PatientNumber, @lastname, @firstname, @middlename, @admitnumber, @nettotal, @amountpaid, @balance, @time, @date) ";

                cmd.Parameters.AddWithValue("@PatientNumber", patientnumber.Text.ToString());
                cmd.Parameters.AddWithValue("@admitnumber", admitnumber.Text.ToString());
                cmd.Parameters.AddWithValue("@lastname", lastname.Text.ToString());
                cmd.Parameters.AddWithValue("@firstname", firstname.Text.ToString());
                cmd.Parameters.AddWithValue("@middlename", middlename.Text.ToString());
                cmd.Parameters.AddWithValue("@nettotal", nettotal.Text.ToString());
                cmd.Parameters.AddWithValue("@amountpaid", amountpaid.Text.ToString());
                cmd.Parameters.AddWithValue("@balance", balance.Text.ToString());

                cmd.Parameters.AddWithValue("@date", Datestatus.Text.ToString());
                cmd.Parameters.AddWithValue("@time", Timestatus.Text.ToString());



                cmd.ExecuteNonQuery();
                MySqlDataAdapter sda2 = new MySqlDataAdapter(cmd);
                DataSet ds2 = new DataSet();
                sda2.Fill(ds2);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public void discharge_treatment1()
        {
            MySqlConnection MyConnection = new MySqlConnection(MyConnectionString);
            MySqlCommand cmd;
            MySqlDataReader mdr;

            MyConnection.Open();

            string selectQuery = "DELETE FROM `inpatients_admissions` WHERE PatientNumber =" + int.Parse(patientnumber.Text);
            cmd = new MySqlCommand(selectQuery, MyConnection);

            mdr = cmd.ExecuteReader();
            if (mdr.Read())
            {
            }
            else
            {
                MessageBox.Show("No Data Found For This Patient");
            }
            MyConnection.Close();
        }





        private void textBox2_TextChanged(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
        }







        private void InPatient_Discharge_Load(object sender, EventArgs e)
        {
            timer1.Start();
            Timestatus.Text = DateTime.Now.ToLongTimeString();
            Datestatus.Text = DateTime.Now.ToLongDateString();
            admission_table();
        }

        private void button29_Click(object sender, EventArgs e)
        {
            patientnumber.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            lastname.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            firstname.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            middlename.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            admitnumber.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            admittime.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Start();
            Timestatus.Text = DateTime.Now.ToLongTimeString();
            Datestatus.Text = DateTime.Now.ToLongDateString();
        }

        private void patientnumber_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button32_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure to Discharge Patient ?", "Discharge!!", MessageBoxButtons.YesNo);

            switch (dr)
            {
                case DialogResult.Yes:
                    discharge_treatment();
                    discharge_treatment1();

                    patientnumber.Text = "0";
                    lastname.Clear();
                    firstname.Clear();
                    middlename.Clear();
                    admitnumber.Clear();
                    admittime.Clear();
                    textBox1.Clear();
                    balance.Text = "0";
                    amountpaid.Text = "0";
                    nettotal.Text = "0";
                    break;
                case DialogResult.No:
                    break;
            }
        }

        private void balance_TextChanged(object sender, EventArgs e)
        {
            if (balance.Text == "0")
            {
                button32.Enabled = true;
            }
            else
            {
                button32.Enabled = false;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox2.Text))
            {
                textBox2.Text = "0";
            }
            else
            {
                search_admission();
            }

        }

        private void button30_Click(object sender, EventArgs e)
        {
            patientnumber.Text = "0";
            lastname.Clear();
            firstname.Clear();
            middlename.Clear();
            admitnumber.Clear();
            admittime.Clear();
            textBox1.Clear();
            amountpaid.Text = "0";
            nettotal.Text = "0";
            balance.Clear();

        }

        private void button31_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(patientnumber.Text))
            {
                patientnumber.Text = "0";
            }
            else
            {
                patient_number();
            }
        }
    }
}

