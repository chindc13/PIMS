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
    public partial class InPatient_Billing : Form
    {


        Double bperday;
        Double bstay;
        Double bdoctor;
        Double broom;
        Double bmedical;
        Double bservice;
        Double btotal;
        Double bdiscount;
        Double bnettotal;
        Double bamountpaid;
        Double bbalance;





        public InPatient_Billing()
        {
            InitializeComponent();
        }
        string MyConnectionString = "Server=localhost;Port=3306;database=final_pims;Uid=root;Pwd=''";

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
                doctorid.Text = mdr.GetString("doctorid");
                doctor.Text = mdr.GetString("doctor");
                textBox1.Text = mdr.GetString("roomid");
                room.Text = mdr.GetString("room");
                admitdate.Text = mdr.GetString("date");
                
                
            }
            else
            {
                MessageBox.Show("No Data Found For This PatientNumber");
            }
            MyConnection.Close();
        }
        public void billing_room()
        {
            MySqlConnection MyConnection = new MySqlConnection(MyConnectionString);
            MySqlCommand cmd;
            MySqlDataReader mdr;

            MyConnection.Open();

            string selectQuery = "Select * from `room_maintenance` where roomid =" + int.Parse(textBox1.Text);
            cmd = new MySqlCommand(selectQuery, MyConnection);

            mdr = cmd.ExecuteReader();
            if (mdr.Read())
            {
                roomcharge.Text = mdr.GetString("roomcost");
            }
            else
            {
            }
            MyConnection.Close();
        }
        public void billing_doctor()
        {
            MySqlConnection MyConnection = new MySqlConnection(MyConnectionString);
            MySqlCommand cmd;
            MySqlDataReader mdr;

            MyConnection.Open();

            string selectQuery = "Select * from `doctor_maintenance` where doctorid =" + int.Parse(doctorid.Text);
            cmd = new MySqlCommand(selectQuery, MyConnection);

            mdr = cmd.ExecuteReader();
            if (mdr.Read())
            {
                doctorcharge.Text = mdr.GetString("doctorcharges");
            }
            else
            {
            }
            MyConnection.Close();
        }
        public void billing_medicalcharges()
        {
            MySqlConnection MyConnection = new MySqlConnection(MyConnectionString);
            MySqlCommand cmd;
            MySqlDataReader mdr;

            MyConnection.Open();

            string selectQuery = "Select * from `medical_treatment` where PatientNumber =" + int.Parse(patientnumber.Text);
            cmd = new MySqlCommand(selectQuery, MyConnection);

            mdr = cmd.ExecuteReader();
            if (mdr.Read())
            {
                medicalcharge.Text = mdr.GetString("total");
            }
            else
            {
            }
            MyConnection.Close();
        }
        public void billing_servicecharges()
        {
            MySqlConnection MyConnection = new MySqlConnection(MyConnectionString);
            MySqlCommand cmd;
            MySqlDataReader mdr;

            MyConnection.Open();

            string selectQuery = "Select * from `service_treatment` where PatientNumber =" + int.Parse(patientnumber.Text);
            cmd = new MySqlCommand(selectQuery, MyConnection);

            mdr = cmd.ExecuteReader();
            if (mdr.Read())
            {
                servicecharge.Text = mdr.GetString("servicecharge");
            }
            else
            {
            }
            MyConnection.Close();
        }
        public void paid()
        {
            MySqlConnection MyConnection = new MySqlConnection(MyConnectionString);
            MySqlCommand cmd;
            MySqlDataReader mdr;

            MyConnection.Open();

            string selectQuery = "Select * from `inpatient_payment` where PatientNumber =" + int.Parse(patientnumber.Text);
            cmd = new MySqlCommand(selectQuery, MyConnection);

            mdr = cmd.ExecuteReader();
            if (mdr.Read())
            {
                amountpaid.Text = mdr.GetString("amouttopay");
                discount.Text = "100";
            }
            else
            {
            }
            MyConnection.Close();
        }
        public void billing_save()
        {
            MySqlConnection connection = new MySqlConnection(MyConnectionString);
            MySqlCommand cmd;
            connection.Open();


            try
            {
                cmd = connection.CreateCommand();
                cmd.CommandText = @"INSERT INTO `inpatient_billing`(`PatientNumber`, `admitnumber`, `lastname`, `firstname`, `middlename`, `totalcost`, `discount`, `totalpayable`, `totalpaidsofar`, `balance`, `time`, `date`) 
VALUES (@PatientNumber, @admitnumber, @lastname, @firstname, @middlename, @totalcost, @discount, @totalpayable, @totalpaidsofar, @balance, @time, @date)";


                cmd.Parameters.AddWithValue("@date", Datestatus.Text.ToString());
                cmd.Parameters.AddWithValue("@time", Timestatus.Text.ToString());


                cmd.Parameters.AddWithValue("@admitnumber", admitnumber.Text.ToString());
                cmd.Parameters.AddWithValue("@PatientNumber", patientnumber.Text.ToString());
                cmd.Parameters.AddWithValue("@firstname", firstname.Text.ToString());
                cmd.Parameters.AddWithValue("@lastname", lastname.Text.ToString());
                cmd.Parameters.AddWithValue("@middlename", middlename.Text.ToString());
                cmd.Parameters.AddWithValue("@totalcost", total.Text.ToString());
                cmd.Parameters.AddWithValue("@discount", discount.Text.ToString());
                cmd.Parameters.AddWithValue("@totalpayable", nettotal.Text.ToString());
                cmd.Parameters.AddWithValue("@totalpaidsofar", amountpaid.Text.ToString());
                cmd.Parameters.AddWithValue("@balance", balance.Text.ToString());

                cmd.ExecuteNonQuery();
                MySqlDataAdapter sda2 = new MySqlDataAdapter(cmd);
                DataSet ds2 = new DataSet();
                sda2.Fill(ds2);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            patientnumber.Clear();
            lastname.Clear();
            firstname.Clear();
            middlename.Clear();
            total.Text = "0";
            discount.Text = "0";
            amountpaid.Text = "0";
            balance.Text = "0";
            doctor.Clear();
            doctorid.Text = "0";
            room.Clear();
            textBox1.Text = "0";
            lenghtofstay.Clear();
            doctorcharge.Text = "0";
            roomcharge.Text = "0";
            roomperdaycharges.Text = "0";
            medicalcharge.Text = "0"; 
            servicecharge.Text = "0";
            admitnumber.Text = "0";
            connection.Close();

        }

        












        private void InPatient_Billing_Load(object sender, EventArgs e)
        {
            timer1.Start();
            Timestatus.Text = DateTime.Now.ToLongTimeString();
            Datestatus.Text = DateTime.Now.ToLongDateString();
            datetoday.Text = DateTime.Now.ToLongDateString();
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Start();
            Timestatus.Text = DateTime.Now.ToLongTimeString();
            Datestatus.Text = DateTime.Now.ToLongDateString();
            datetoday.Text = DateTime.Now.ToLongDateString();
        }







        private void patientnumber_TextChanged(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
        }
        private void lenghtofstay_TextChanged(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
        }
        private void firstname_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(lastname.Text) || String.IsNullOrEmpty(firstname.Text) || String.IsNullOrEmpty(middlename.Text)) { }
            else
            {
                lenghtofstay.ReadOnly = false;

            }
        }
        private void middlename_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(lastname.Text) || String.IsNullOrEmpty(firstname.Text) || String.IsNullOrEmpty(middlename.Text)) { }
            else
            {
                lenghtofstay.ReadOnly = false;

            }
        }
        private void lastname_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(lastname.Text) || String.IsNullOrEmpty(firstname.Text) || String.IsNullOrEmpty(middlename.Text)) { }
            else
            {
                lenghtofstay.ReadOnly = false;

            }
        }
        private void discount_TextChanged(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar)  || e.KeyChar == 8 ? false : true;
        }





        private void button29_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(patientnumber.Text))
            {

            }

            else
            {
            paid();
            billing_servicecharges();
            admission_search();
            billing_medicalcharges();
            }
            if (String.IsNullOrEmpty(admitnumber.Text))
            {
                amountpaid.Text = "0";
                medicalcharge.Text = "0";
                servicecharge.Text = "0";
            }
            
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            billing_room();
        }

        private void doctorid_TextChanged(object sender, EventArgs e)
        {
            billing_doctor();
        }

        private void lenghtofstay_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(lenghtofstay.Text))
            {
                lenghtofstay.Text = "0";
            }
            else
            {
                Double perday1 = 1000;
                Double stay1;

                stay1 = Double.Parse(lenghtofstay.Text);

                bperday = perday1 * stay1;

                roomperdaycharges.Text = System.Convert.ToString(bperday);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void roomperdaycharges_TextChanged(object sender, EventArgs e)
        {
            
            Double bstay1;
            Double bdoctor1;
            Double broom1;
            Double bmedical1;
            Double bservice1;

            bstay1 = Double.Parse(roomperdaycharges.Text);
            bdoctor1 = Double.Parse(doctorcharge.Text);
            broom1 = Double.Parse(roomcharge.Text);
            bmedical1 = Double.Parse(medicalcharge.Text);
            bservice1 = Double.Parse(servicecharge.Text);

            btotal = bdoctor1 + broom1 + bmedical1 + bservice1 + bstay1;

            total.Text = System.Convert.ToString(btotal);

        }

        private void discount_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(discount.Text))
            {
                discount.Text = "0";
            }
            else
            {
                Double bdiscount1;
                Double bnettotal1;
                Double bamountpaid1;
                Double bbalance1;
                Double btotal1;
                Double bbalance2;

                bdiscount1 = Double.Parse(discount.Text);
                bnettotal1 = Double.Parse(nettotal.Text);
                bamountpaid1 = Double.Parse(amountpaid.Text);
                bbalance1 = Double.Parse(balance.Text);
                btotal1 = Double.Parse(total.Text);
                bnettotal1 = Double.Parse(nettotal.Text);



                bnettotal = btotal1 * (bdiscount1 / 100);
                bbalance2 = bnettotal - bamountpaid1;
                

                balance.Text = System.Convert.ToString(bbalance2);
                nettotal.Text = System.Convert.ToString(bnettotal);
                if (bbalance2 <= 0)
                {
                    balance.Text = "0";
                }
                
                
            }
            

        }

        private void nettotal_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(nettotal.Text))
            {

            }
            else
            {
                
            }
        }

        private void button32_Click(object sender, EventArgs e)
        {
            billing_save();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            patientnumber.Clear();
            lastname.Clear();
            firstname.Clear();
            middlename.Clear();
            total.Text = "0";
            discount.Text = "0";
            amountpaid.Text = "0";
            balance.Text = "0";
            doctor.Clear();
            doctorid.Text = "0";
            room.Clear();
            textBox1.Text = "0";
            lenghtofstay.Clear();
            doctorcharge.Text = "0";
            roomcharge.Text = "0";
            roomperdaycharges.Text = "0";
            medicalcharge.Text = "0";
            servicecharge.Text = "0";
            admitnumber.Text = "0";
        }

        private void admitnumber_TextChanged(object sender, EventArgs e)
        {
            
        }

        

        

        

       









    }
}
