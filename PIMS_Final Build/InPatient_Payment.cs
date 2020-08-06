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
    public partial class InPatient_Payment : Form
    {
        public InPatient_Payment()
        {
            InitializeComponent();
        }
        string MyConnectionString = "Server=localhost;Port=3306;database=final_pims;Uid=root;Pwd=''";
        Double pcash;
        Double pamount;
        Double pchanged;

        public void admission_search()
        {
            MySqlConnection MyConnection = new MySqlConnection(MyConnectionString);
            MySqlCommand cmd;
            MySqlDataReader mdr;

            MyConnection.Open();

            string selectQuery = "Select * from `inpatients_admissions` where PatientNumber=" + int.Parse(patientnumber.Text);
            cmd = new MySqlCommand(selectQuery, MyConnection);

            mdr = cmd.ExecuteReader();
            if (mdr.Read())
            {
                lastname.Text = mdr.GetString("lastname");
                firstname.Text = mdr.GetString("firstname");
                middlename.Text = mdr.GetString("middlename");
                admitnumber.Text = mdr.GetString("admitnumber");

            }
            else
            {
                MessageBox.Show("No Data Found For This PatientNumber");
            }
            MyConnection.Close();
        }
        public void payment_save()
        {
            MySqlConnection connection = new MySqlConnection(MyConnectionString);
            MySqlCommand cmd;
            connection.Open();


            try
            {
                cmd = connection.CreateCommand();
                cmd.CommandText = @"INSERT INTO `inpatient_payment`(`PatientNumber`, `admitnumber`, `lastname`, `firstname`, `middlename`, `paymentnumber`, `cash`, `amouttopay`, `change`, `payby`, `time`, `date`) 
                                                            VALUES (@PatientNumber, @admitnumber, @lastname, @firstname, @middlename, @paymentnumber, @cash, @amouttopay, @change, @payby, @time, @date)";


                cmd.Parameters.AddWithValue("@date", Datestatus.Text.ToString());
                cmd.Parameters.AddWithValue("@time", Timestatus.Text.ToString());

                cmd.Parameters.AddWithValue("@PatientNumber", patientnumber.Text.ToString());
                cmd.Parameters.AddWithValue("@firstname", firstname.Text.ToString());
                cmd.Parameters.AddWithValue("@lastname", lastname.Text.ToString());
                cmd.Parameters.AddWithValue("@middlename", middlename.Text.ToString());
                cmd.Parameters.AddWithValue("@admitnumber", admitnumber.Text.ToString());
                cmd.Parameters.AddWithValue("@paymentnumber", paymentnumber.Text.ToString());
                cmd.Parameters.AddWithValue("@cash", cash.Text.ToString());
                cmd.Parameters.AddWithValue("@amouttopay", amounttopay.Text.ToString());
                cmd.Parameters.AddWithValue("@change", change.Text.ToString());
                cmd.Parameters.AddWithValue("@payby", payby.Text.ToString());
                
                cmd.ExecuteNonQuery();
                MySqlDataAdapter sda2 = new MySqlDataAdapter(cmd);
                DataSet ds2 = new DataSet();
                sda2.Fill(ds2);
            }

            catch (Exception ex)
            {
                patientnumber.Clear();
                lastname.Clear();
                firstname.Clear();
                middlename.Clear();
                admitnumber.Clear();
                paymentnumber.Clear();
                cash.Clear();
                amounttopay.Clear();
                change.Clear();
                payby.Clear();
                MessageBox.Show(ex.Message);
            }
        }





        private void patientnumber_TextChanged(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
        }
        private void paymentnumber_TextChanged(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
        }
        private void cash_TextChanged(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
        }
        private void amounttopay_TextChanged(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
        }
        private void change_TextChanged(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
        }






        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Start();
            Timestatus.Text = DateTime.Now.ToLongTimeString();
            Datestatus.Text = DateTime.Now.ToLongDateString();
        }

        private void InPatient_Payment_Load(object sender, EventArgs e)
        {
            timer1.Start();
            Timestatus.Text = DateTime.Now.ToLongTimeString();
            Datestatus.Text = DateTime.Now.ToLongDateString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button29_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(patientnumber.Text))
            {

            }
            else
            {
                admission_search();
            }
        }

        private void cash_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(cash.Text) || String.IsNullOrEmpty(amounttopay.Text))
            {

            }
            else
            {
                Double pcash1;
                Double pamount1;
                Double pchanged1;

                pcash1 = Double.Parse(cash.Text);
                pamount1 = Double.Parse(amounttopay.Text);

                pchanged = pcash1 - pamount1;

                change.Text = System.Convert.ToString(pchanged);
            }
        }

        private void amounttopay_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(cash.Text) || String.IsNullOrEmpty(amounttopay.Text))
            {

            }
            else
            {
                Double pcash1;
                Double pamount1;
                Double pchanged1;

                pcash1 = Double.Parse(cash.Text);
                pamount1 = Double.Parse(amounttopay.Text);

                pchanged = pcash1 - pamount1;

                change.Text = System.Convert.ToString(pchanged);
            }
        }

        private void button32_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(paymentnumber.Text) || String.IsNullOrEmpty(cash.Text) || String.IsNullOrEmpty(amounttopay.Text) ||
                String.IsNullOrEmpty(payby.Text))
            {
                MessageBox.Show("Please fill up the form !");
            }
            else
            {
                payment_save();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            patientnumber.Clear();
            lastname.Clear();
            firstname.Clear();
            middlename.Clear();
            admitnumber.Clear();
            paymentnumber.Clear();
            cash.Clear();
            amounttopay.Clear();
            change.Clear();
            payby.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataTable1BindingNavigator.Visible = true;
            closereceipt.Visible = true;
            reciept.Visible = true;


            reciept.AppendText("\t\t\t\t" + "             " + "PASCUAL GENERAL HOSPITAL");
            reciept.AppendText(Environment.NewLine + "\t\t\t\t" + "  " + Datestatus.Text + "\t" + Timestatus.Text);
            reciept.AppendText(Environment.NewLine + "******************************************************************************************************************************");
            reciept.AppendText(Environment.NewLine + "Out Patient ID : " + "\t\t" + patientnumber.Text + "\t\t\t\t" + "Admit Number : " + "\t\t" + admitnumber.Text);
            reciept.AppendText(Environment.NewLine + "Full Name : " + "\t\t" + lastname.Text + ", " + firstname.Text + " " + middlename.Text + Environment.NewLine);
            reciept.AppendText(Environment.NewLine + "******************************************************************************************************************************");
            reciept.AppendText(Environment.NewLine + "******************************************************************************************************************************");
            reciept.AppendText(Environment.NewLine + "Cash :  : " + "\t\t\t\t\t\t" + cash.Text + ".00 Php" + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
            reciept.AppendText(Environment.NewLine + "******************************************************************************************************************************");
            reciept.AppendText(Environment.NewLine + "This is the Official Reciept of Pascual General Hospital" + "\t\t\t\t\t" + paymentnumber.Text);
        }

        private void closereceipt_Click(object sender, EventArgs e)
        {
            dataTable1BindingNavigator.Visible = false;
            closereceipt.Visible = false;
            reciept.Visible = false;
        }

        private void printToolStripButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Please insert the Printer");
        }


        

        
    }
}
