using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Drawing.Printing;
using System.IO;

namespace PIMS_Final_Build
{
    public partial class OutPatient_Payment : Form
    {

        private Button printButton = new Button();
        
        public OutPatient_Payment()
        {
            InitializeComponent();
        }

        string MyConnectionString = "Server=localhost;Port=3306;database=final_pims;Uid=root;Pwd=''";
        public void outpatientid()
        {
            MySqlConnection MyConnection = new MySqlConnection(MyConnectionString);
            MySqlCommand cmd;
            MySqlDataReader mdr;

            MyConnection.Open();

            string selectQuery = "Select * from `outpatient_treatment` where outpatientid =" + int.Parse(outpatientnumber.Text);
            cmd = new MySqlCommand(selectQuery, MyConnection);

            mdr = cmd.ExecuteReader();
            if (mdr.Read())
            {
                lastname.Text = mdr.GetString("lastname");
                firstname.Text = mdr.GetString("firstname");
                middlename.Text = mdr.GetString("middlename");
            }
            else
            {
                lastname.Clear();
                firstname.Clear();
                middlename.Clear();
                MessageBox.Show("No Data Found For This Patient");
            }
            MyConnection.Close();
        }
        private void paymentnumber_TextChanged(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
        }
        private void cash_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
        }
        private void amounttopay_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
        }
        public void register2()
        {
            MySqlConnection connection = new MySqlConnection(MyConnectionString);
            MySqlCommand cmd;
            connection.Open();


            try
            {
                cmd = connection.CreateCommand();
                cmd.CommandText = @"INSERT INTO `outpatient_payment`(`outpatientid`, `lastname`, `firstname`, `middlename`, `paymentnumber`, `cash`, `amountpaid`, `change`, `payby`, `time`, `date`) 
VALUES (@outpatientid, @lastname, @firstname, @middlename, @paymentnumber, @cash, @amountpaid, @change, @payby, @time, @date)";


                cmd.Parameters.AddWithValue("@date", Datestatus.Text.ToString());
                cmd.Parameters.AddWithValue("@time", Timestatus.Text.ToString());
                cmd.Parameters.AddWithValue("@outpatientid", outpatientnumber.Text.ToString());
                cmd.Parameters.AddWithValue("@firstname", firstname.Text.ToString());
                cmd.Parameters.AddWithValue("@lastname", lastname.Text.ToString());
                cmd.Parameters.AddWithValue("@middlename", middlename.Text.ToString());
                cmd.Parameters.AddWithValue("@paymentnumber", paymentnumber.Text.ToString());
                cmd.Parameters.AddWithValue("@cash", cash.Text.ToString());
                cmd.Parameters.AddWithValue("@amountpaid", amounttopay.Text.ToString());
                cmd.Parameters.AddWithValue("@change", change.Text.ToString());
                cmd.Parameters.AddWithValue("@payby", payby.Text.ToString());


                cmd.ExecuteNonQuery();
                MySqlDataAdapter sda2 = new MySqlDataAdapter(cmd);
                DataSet ds2 = new DataSet();
                sda2.Fill(ds2);
            }

            catch (Exception)
            {
                outpatientnumber.Clear();
                lastname.Clear();
                firstname.Clear();
                middlename.Clear();
                paymentnumber.Clear();
                cash.Text = "0";
                amounttopay.Text = "0";
                change.Text = "0";
                payby.Clear();
                MessageBox.Show("Patient has been Registered.");
            }
        }








        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Start();
            Timestatus.Text = DateTime.Now.ToLongTimeString();
            Datestatus.Text = DateTime.Now.ToLongDateString();
        }

        private void OutPatient_Payment_Load(object sender, EventArgs e)
        {
            timer1.Start();
            Timestatus.Text = DateTime.Now.ToLongTimeString();
            Datestatus.Text = DateTime.Now.ToLongDateString();
        }

        private void button29_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(outpatientnumber.Text))
            {

            }
            else
            {
                outpatientid();
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            outpatientnumber.Clear();
            lastname.Clear();
            firstname.Clear();
            middlename.Clear();
            paymentnumber.Clear();
            cash.Text = "0";
            amounttopay.Text = "0";
            change.Text = "0";
            payby.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            outpatientnumber.Clear();
            lastname.Clear();
            firstname.Clear();
            middlename.Clear();
            paymentnumber.Clear();
            cash.Text = "0";
            amounttopay.Text = "0";
            change.Text = "0";
            payby.Clear();
        }

        private void button32_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(outpatientnumber.Text) || String.IsNullOrEmpty(lastname.Text) || String.IsNullOrEmpty(firstname.Text) ||
                String.IsNullOrEmpty(middlename.Text) || String.IsNullOrEmpty(paymentnumber.Text) || String.IsNullOrEmpty(cash.Text) ||
                String.IsNullOrEmpty(amounttopay.Text) || String.IsNullOrEmpty(change.Text) || String.IsNullOrEmpty(payby.Text))
            {
                MessageBox.Show("Please fill up the form !!");
            }
            else
            {
                
                register2();
            }
               
        }

        private void amounttopay_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(amounttopay.Text))
            {
                amounttopay.Text = "0";
            }
            else
            {
                Double amount;
                Double cash1;
                Double total1;


                cash1 = Double.Parse(cash.Text);
                amount = Double.Parse(amounttopay.Text);

                total1 = cash1 - amount;

                change.Text = System.Convert.ToString(total1);
            }
        }

        private void cash_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(cash.Text))
            {
                cash.Text = "0";
            }
        }

        private void change_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(change.Text))
            {
                change.Text = "0";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            closereceipt.Visible = true;
            dataTable1BindingNavigator.Visible = true;
            reciept.Visible = true;

            reciept.AppendText("\t\t\t\t" + "             " + "PASCUAL GENERAL HOSPITAL");
            reciept.AppendText(Environment.NewLine + "\t\t\t\t" + "  " + Datestatus.Text + "\t" + Timestatus.Text);
            reciept.AppendText(Environment.NewLine + "******************************************************************************************************************************");
            reciept.AppendText(Environment.NewLine + "Out Patient ID : " + "\t\t" + outpatientnumber.Text);
            reciept.AppendText(Environment.NewLine + "Full Name : " + "\t\t" + lastname.Text + ", " + firstname.Text + " " + middlename.Text + Environment.NewLine);
            reciept.AppendText(Environment.NewLine + "******************************************************************************************************************************");
            reciept.AppendText(Environment.NewLine + "******************************************************************************************************************************");
            reciept.AppendText(Environment.NewLine + "Cash :  : " + "\t\t\t\t\t\t" + cash.Text + ".00 Php" + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine);
            reciept.AppendText(Environment.NewLine + "******************************************************************************************************************************");
            reciept.AppendText(Environment.NewLine + "This is the Official Reciept of Pascual General Hospital" + "\t\t\t\t\t" + paymentnumber.Text);
        }

        private void closereceipt_Click_1(object sender, EventArgs e)
        {
            dataTable1BindingNavigator.Visible = false;
            reciept.Clear();
            closereceipt.Visible = false;
            reciept.Visible = false;
        }


        private void printToolStripButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Please insert the printer");
        }

       

       

        
  
        
    
    
    
    
    
    
    
    
    
    
    }
}
