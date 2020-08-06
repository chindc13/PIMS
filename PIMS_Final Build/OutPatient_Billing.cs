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
    public partial class OutPatient_Billing : Form
    {
        public OutPatient_Billing()
        {
            InitializeComponent();
        }
        Double total1;

        string MyConnectionString = "Server=localhost;Port=3306;database=final_pims;Uid=root;Pwd=''";
        public void doctor_charges()
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
                doctorname.Text = mdr.GetString("name");
                doctorcharges.Text = mdr.GetString("doctorcharges");
            }
            else
            {
            }
            MyConnection.Close();
        }
        public void out_patient_details()
        {
            MySqlConnection MyConnection = new MySqlConnection(MyConnectionString);
            MySqlCommand cmd;
            MySqlDataReader mdr;

            MyConnection.Open();

            string selectQuery = "Select * from `outpatient_treatment` where outpatientid =" + int.Parse(textBox1.Text);
            cmd = new MySqlCommand(selectQuery, MyConnection);

            mdr = cmd.ExecuteReader();
            if (mdr.Read())
            {
                outpatientid.Text = mdr.GetString("outpatientid");
                lastname.Text = mdr.GetString("lastname");
                firstname.Text = mdr.GetString("firstname");
                middlename.Text = mdr.GetString("middlename");
                doctorid.Text = mdr.GetString("doctorid");
                medicineid.Text = mdr.GetString("medicineid");
                careid.Text = mdr.GetString("careid");
            }
            else
            {
            }
            MyConnection.Close();
        }
        public void care_charges()
        {
            MySqlConnection MyConnection = new MySqlConnection(MyConnectionString);
            MySqlCommand cmd;
            MySqlDataReader mdr;

            MyConnection.Open();

            string selectQuery = "Select * from `typeofcare` where careid =" + int.Parse(careid.Text);
            cmd = new MySqlCommand(selectQuery, MyConnection);

            mdr = cmd.ExecuteReader();
            if (mdr.Read())
            {
                typeofcare.Text = mdr.GetString("name");
                carecharges.Text = mdr.GetString("carecost");
            }
            else
            {
            }
            MyConnection.Close();
        }
        public void medicine_charges()
        {
            MySqlConnection MyConnection = new MySqlConnection(MyConnectionString);
            MySqlCommand cmd;
            MySqlDataReader mdr;

            MyConnection.Open();

            string selectQuery = "Select * from `medical_maintenance` where medicineid =" + int.Parse(medicineid.Text);
            cmd = new MySqlCommand(selectQuery, MyConnection);

            mdr = cmd.ExecuteReader();
            if (mdr.Read())
            {
                medicinename.Text = mdr.GetString("medicine_name");
                medicinecharges.Text = mdr.GetString("unitprice");
            }
            else
            {
            }
            MyConnection.Close();
        }
        public void payment()
        {
            MySqlConnection MyConnection = new MySqlConnection(MyConnectionString);
            MySqlCommand cmd;
            MySqlDataReader mdr;

            MyConnection.Open();

            string selectQuery = "Select * from `outpatient_payment` where outpatientid =" + int.Parse(textBox1.Text);
            cmd = new MySqlCommand(selectQuery, MyConnection);

            mdr = cmd.ExecuteReader();
            if (mdr.Read())
            {
                amountpaid.Text = mdr.GetString("amountpaid");
            }
            else
            {
            }
            MyConnection.Close();
        }
        public void register()
        {
            MySqlConnection connection = new MySqlConnection(MyConnectionString);
            MySqlCommand cmd;
            connection.Open();


            try
            {
                cmd = connection.CreateCommand();
                cmd.CommandText = @"INSERT INTO `outpatient_billing`(`outpatientid`, `lastname`, `firstname`, `middlename`, `total`, `discount`, `nettotal`, `amountpaid`, `balance`) 
VALUES (@outpatientid, @lastname, @firstname, @middlename, @total, @discount, @nettotal, @amountpaid, @balance) ";


                cmd.Parameters.AddWithValue("@outpatientid", outpatientid.Text.ToString());
                cmd.Parameters.AddWithValue("@lastname", lastname.Text.ToString());
                cmd.Parameters.AddWithValue("@firstname", firstname.Text.ToString());
                cmd.Parameters.AddWithValue("@middlename", middlename.Text.ToString());
                cmd.Parameters.AddWithValue("@total", total.Text.ToString());
                cmd.Parameters.AddWithValue("@discount", discount.Text.ToString());
                cmd.Parameters.AddWithValue("@nettotal", nettotal.Text.ToString());
                cmd.Parameters.AddWithValue("@amountpaid", amountpaid.Text.ToString());
                cmd.Parameters.AddWithValue("@balance", balance.Text.ToString());



                cmd.ExecuteNonQuery();
                MySqlDataAdapter sda2 = new MySqlDataAdapter(cmd);
                DataSet ds2 = new DataSet();
                sda2.Fill(ds2);
            }

            catch (Exception)
            {
                MessageBox.Show("Out Patient Bills Has been save.");
            }
        }




        private void textBox1_TextChanged(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
        }






        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox1.Text))
            {

            }
            
            
        }

        private void button29_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox1.Text))
            {

            }
            else
            {
                amountpaid.Text = "0";
                balance.Text = "0";
                discount.Text = "100";
                payment();
                out_patient_details();
                if (String.IsNullOrEmpty(medicinecharges.Text) || String.IsNullOrEmpty(carecharges.Text) || String.IsNullOrEmpty(doctorcharges.Text))
                {

                }
                else
                {
                    Double doctor;
                    Double care3;
                    Double medicine;
                    Double nettotal3;


                    doctor = Double.Parse(doctorcharges.Text);
                    care3 = Double.Parse(carecharges.Text);
                    medicine = Double.Parse(medicinecharges.Text);

                    total1 = care3 + medicine + doctor;
                    nettotal3 = care3 + medicine + doctor;

                    nettotal.Text = System.Convert.ToString(nettotal3);
                    total.Text = System.Convert.ToString(total1);


                    Double bdiscount2;
                    Double bnettotal2;
                    Double bamountpaid2;
                    Double bbalance2;
                    Double btotal2;

                    bdiscount2 = Double.Parse(discount.Text);
                    bnettotal2 = Double.Parse(nettotal.Text);
                    bamountpaid2 = Double.Parse(amountpaid.Text);
                    bbalance2 = Double.Parse(balance.Text);
                    btotal2 = Double.Parse(total.Text);
                    bnettotal2 = Double.Parse(nettotal.Text);



                    bnettotal2 = btotal2 * (bdiscount2 / 100);
                    bbalance2 = bnettotal2 - bamountpaid2;

                    balance.Text = System.Convert.ToString(bbalance2);
                    discount.Text = System.Convert.ToString(bdiscount2);
                    nettotal.Text = System.Convert.ToString(bnettotal2);
                    if (bbalance2 <= 0)
                    {
                        balance.Text = "0";
                    }



                }
            }
            
        }

        private void doctorid_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(doctorid.Text))
            {

            }
            else
            {
                doctor_charges();
            }
            
        }

        private void careid_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(careid.Text))
            {

            }
            else
            {
                care_charges();
            }
            
        }

        private void OutPatient_Billing_Load(object sender, EventArgs e)
        {
            timer1.Start();
            Timestatus.Text = DateTime.Now.ToLongTimeString();
            Datestatus.Text = DateTime.Now.ToLongDateString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Start();
            Timestatus.Text = DateTime.Now.ToLongTimeString();
            Datestatus.Text = DateTime.Now.ToLongDateString();
        }

        private void medicineid_TextChanged_1(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(medicineid.Text))
            {

            }
            else
            {
                medicine_charges();
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            outpatientid.Clear();
            lastname.Clear();
            firstname.Clear();
            middlename.Clear();
            doctorid.Clear();
            medicineid.Clear();
            careid.Clear();
            doctorname.Clear();
            medicinename.Clear();
            typeofcare.Clear();
            doctorcharges.Text = "0";
            medicinecharges.Text = "0";
            carecharges.Text = "0";
            discount.Text = "100";
            total.Text = "0";
            nettotal.Text = "0";
            amountpaid.Text = "0";
            balance.Text = "0";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OutPatient_Payment opp = new OutPatient_Payment();
            opp.Show();
            this.Close();
        }
        Double bbalance2;
        private void discount_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(discount.Text))
            {

            }
            else
            {
                Double bdiscount2;
                Double bnettotal2;
                Double bamountpaid2;
                Double btotal2;

                bdiscount2 = Double.Parse(discount.Text);
                bnettotal2 = Double.Parse(nettotal.Text);
                bamountpaid2 = Double.Parse(amountpaid.Text);
                bbalance2 = Double.Parse(balance.Text);
                btotal2 = Double.Parse(total.Text);
                bnettotal2 = Double.Parse(nettotal.Text);

                bnettotal2 = btotal2 * (bdiscount2 / 100);
                bbalance2 = bnettotal2 - bamountpaid2;

                amountpaid.Text = System.Convert.ToString(bamountpaid2);
                discount.Text = System.Convert.ToString(bdiscount2);
                nettotal.Text = System.Convert.ToString(bnettotal2);
                balance.Text = System.Convert.ToString(bbalance2);
                if (bbalance2 <= 0)
                {
                    balance.Text = "0";
                }
            }
        }

        private void balance_TextChanged(object sender, EventArgs e)
        {

        }

        private void button32_Click(object sender, EventArgs e)
        {
            if (bbalance2 >= 0)
            {
                MessageBox.Show("Please pay for your bill thank you!");
            }
            else
            {
                register();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }


        




    }
}
