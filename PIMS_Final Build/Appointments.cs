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
    public partial class Appointments : Form
    {
        public Appointments()
        {
            InitializeComponent();
        }

        string MyConnectionString = "Server=localhost;Port=3306;database=final_pims;Uid=root;Pwd=''";
        public void doctortable1()
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
                doctortable.DataSource = ds.Tables[0].DefaultView;
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
        public void search123()
        {
            MySqlConnection MyConnection = new MySqlConnection(MyConnectionString);
            MySqlCommand cmd;
            MySqlDataReader mdr;

            MyConnection.Open();

            string selectQuery = "SELECT * FROM `new_patient` WHERE PatientNumber =" + int.Parse(search.Text);
            cmd = new MySqlCommand(selectQuery, MyConnection);

            mdr = cmd.ExecuteReader();
            if (mdr.Read())
            {
                lastname.Text = mdr.GetString("lastname");
                firstname.Text = mdr.GetString("firstname");
                middlename.Text = mdr.GetString("middlename");
                search.Clear();
            }
            else
            {
                MessageBox.Show("No data found for this patient.");
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
                cmd.CommandText = @"INSERT INTO `doctor_appointments`(`lastname`, `firstname`, `middlename`, `starts`, `ends`, `day`, `doctorid`, `doctorname`, `contact`) 
                                                                VALUES (@lastname, @firstname, @middlename, @starts, @ends, @day, @doctorid, @doctorname, @contact)";


                cmd.Parameters.AddWithValue("@lastname", lastname.Text.ToString());
                cmd.Parameters.AddWithValue("@firstname", firstname.Text.ToString());
                cmd.Parameters.AddWithValue("@middlename", middlename.Text.ToString());
                cmd.Parameters.AddWithValue("@starts", starts.Text.ToString());
                cmd.Parameters.AddWithValue("@ends", ends.Text.ToString());
                cmd.Parameters.AddWithValue("@day", day.Text.ToString());
                cmd.Parameters.AddWithValue("@doctorid", doctorid.Text.ToString());
                cmd.Parameters.AddWithValue("@doctorname", doctorname.Text.ToString());
                cmd.Parameters.AddWithValue("@contact", contact.Text.ToString());



                cmd.ExecuteNonQuery();
                MySqlDataAdapter sda2 = new MySqlDataAdapter(cmd);
                DataSet ds2 = new DataSet();
                sda2.Fill(ds2);
            }

            catch (Exception)
            {
                MessageBox.Show("Appointment has been save.");
                lastname.Clear();
                firstname.Clear();
                middlename.Clear();
                doctorid.Clear();
                doctorname.Clear();
                contact.Clear();
            }
        }
        public void register2()
        {
            MySqlConnection connection = new MySqlConnection(MyConnectionString);
            MySqlCommand cmd;
            connection.Open();


            try
            {
                cmd = connection.CreateCommand();
                cmd.CommandText = @"INSERT INTO `hospital_appointment`(`PatientNumber`, `lastname`, `firstname`, `middlename`, `starts`, `ends`, `day`, `note`, `topic`) 
VALUES (@PatientNumber, @lastname, @firstname, @middlename, @starts, @ends, @day, @note, @topic) ";

                cmd.Parameters.AddWithValue("@PatientNumber", hid.Text.ToString());
                cmd.Parameters.AddWithValue("@lastname", hlastname.Text.ToString());
                cmd.Parameters.AddWithValue("@firstname", hfirstname.Text.ToString());
                cmd.Parameters.AddWithValue("@middlename", hmiddlename.Text.ToString());
                cmd.Parameters.AddWithValue("@starts", hstarts.Text.ToString());
                cmd.Parameters.AddWithValue("@ends", hends.Text.ToString());
                cmd.Parameters.AddWithValue("@day", hday.Text.ToString());
                cmd.Parameters.AddWithValue("@note", hnote.Text.ToString());
                cmd.Parameters.AddWithValue("@topic", htopic.Text.ToString());



                cmd.ExecuteNonQuery();
                MySqlDataAdapter sda2 = new MySqlDataAdapter(cmd);
                DataSet ds2 = new DataSet();
                sda2.Fill(ds2);
            }

            catch (Exception)
            {
                MessageBox.Show("Appointment has been save.");
                hlastname.Clear();
                hfirstname.Clear();
                hmiddlename.Clear();
                htopic.Clear();
                hid.Clear();
                hnote.Clear();
            }
        }
        public void search_hospital()
        {
            MySqlConnection MyConnection = new MySqlConnection(MyConnectionString);
            MySqlCommand cmd;
            MySqlDataReader mdr;

            MyConnection.Open();

            string selectQuery = "SELECT * FROM `new_patient` WHERE PatientNumber =" + int.Parse(search.Text);
            cmd = new MySqlCommand(selectQuery, MyConnection);

            mdr = cmd.ExecuteReader();
            if (mdr.Read())
            {
                hlastname.Text = mdr.GetString("lastname");
                hfirstname.Text = mdr.GetString("firstname");
                hmiddlename.Text = mdr.GetString("middlename");
                hid.Text = mdr.GetString("PatientNumber");
                search.Clear();
            }
            else
            {
                MessageBox.Show("No data found for this patient.");
            }
            MyConnection.Close();
        }





        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            hlastname.Clear();
            hmiddlename.Clear();
            hfirstname.Clear();
            groupBox4.Visible = false;
            groupBox1.Visible = true;
        }

        private void Appointments_Load(object sender, EventArgs e)
        {
            doctortable1();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            panel4.Visible = false;
            panel3.Visible = false;
            panel2.Visible = false;
            panel1.Visible = true;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            lastname.Clear();
            middlename.Clear();
            firstname.Clear();
            groupBox4.Visible = true;
            groupBox1.Visible = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            panel4.Visible = false;
            panel3.Visible = false;
            panel2.Visible = true;
            panel1.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            doctorid.Text = doctortable.CurrentRow.Cells[0].Value.ToString();
            doctorname.Text = doctortable.CurrentRow.Cells[1].Value.ToString();
            contact.Text = doctortable.CurrentRow.Cells[5].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            lastname.Clear();
            firstname.Clear();
            middlename.Clear();
            doctorid.Clear();
            doctorname.Clear();
            contact.Clear();
        }

        private void search_TextChanged(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(search.Text))
            {
                MessageBox.Show("Please Enter the PATIENTNUMBER of the Patient");
                lastname.Clear();
                firstname.Clear();
                middlename.Clear();
            }
            else
            {
                if (radioButton5.Checked)
                {
                    search123();
                }
                if (radioButton6.Checked)
                {
                    search_hospital();
                }
                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(lastname.Text) || String.IsNullOrEmpty(firstname.Text) || String.IsNullOrEmpty(middlename.Text) ||
                String.IsNullOrEmpty(doctorid.Text) || String.IsNullOrEmpty(doctorname.Text) || String.IsNullOrEmpty(contact.Text))
            {
                MessageBox.Show("Please fill up the form");
                }
            else
            {
                register();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            hid.Clear();
            hlastname.Clear();
            hfirstname.Clear();
            hmiddlename.Clear();
            hnote.Clear();
            htopic.Clear();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(hlastname.Text) || String.IsNullOrEmpty(hfirstname.Text) || String.IsNullOrEmpty(hmiddlename.Text) ||
                 String.IsNullOrEmpty(hnote.Text) || String.IsNullOrEmpty(htopic.Text))
            {
                MessageBox.Show("Please fill up the form");
            }
            else
            {
                register2();
            }
        }






        public void search_appointments_doctor_cancel()
        {
            MySqlConnection MyConnection = new MySqlConnection(MyConnectionString);
            MySqlCommand cmd;
            MySqlDataReader mdr;

            MyConnection.Open();

            string selectQuery = "SELECT * FROM `doctor_appointments` WHERE doctorid =" + int.Parse(textBox1.Text);
            cmd = new MySqlCommand(selectQuery, MyConnection);

            mdr = cmd.ExecuteReader();
            if (mdr.Read())
            {
                rlastname.Text = mdr.GetString("lastname");
                rfirstname.Text = mdr.GetString("firstname");
                rmiddlename.Text = mdr.GetString("middlename");
                rstarts.Text = mdr.GetString("starts");
                rends.Text = mdr.GetString("ends");
                rday.Text = mdr.GetString("day");
                rdoctorid.Text = mdr.GetString("doctorid");
                rdoctorname.Text = mdr.GetString("doctorname");
                rcontact.Text = mdr.GetString("contact");
                textBox1.Clear();
            }
            else
            {
                textBox1.Clear();
                MessageBox.Show("No data found for this patient.");
            }
            MyConnection.Close();
        }
        public void cancel_appointment_doctor()
        {
            MySqlConnection MyConnection = new MySqlConnection(MyConnectionString);
            MySqlCommand cmd;
            MySqlDataReader mdr;

            MyConnection.Open();

            string selectQuery = "DELETE FROM `doctor_appointments`  WHERE doctorid =" + int.Parse(rdoctorid.Text);
            cmd = new MySqlCommand(selectQuery, MyConnection);

            mdr = cmd.ExecuteReader();
            if (mdr.Read())
            {

            }
            else
            {
                rlastname.Clear();
                rfirstname.Clear();
                rmiddlename.Clear();
                rstarts.Clear();
                rends.Clear();
                rday.Clear();
                rdoctorid.Clear();
                rdoctorname.Clear();
                rcontact.Clear();
                MessageBox.Show("No Data Found For This Appointment");
            }
            MyConnection.Close();
        }
        public void load_doctor_appointment()
        {
            MySqlConnection connection = new MySqlConnection(MyConnectionString);
            connection.Open();
            try
            {
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM `doctor_appointments` ";
                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adap.Fill(ds);
                appointmentsfordoctor.DataSource = ds.Tables[0].DefaultView;
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
        public void load_hospital_appointment()
        {
            MySqlConnection connection = new MySqlConnection(MyConnectionString);
            connection.Open();
            try
            {
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM `hospital_appointment` ";
                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adap.Fill(ds);
                loadpatienthospital.DataSource = ds.Tables[0].DefaultView;
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
        public void search_appointments_hospital_cancel()
        {
            MySqlConnection MyConnection = new MySqlConnection(MyConnectionString);
            MySqlCommand cmd;
            MySqlDataReader mdr;

            MyConnection.Open();

            string selectQuery = "SELECT * FROM `hospital_appointment` WHERE PatientNumber =" + int.Parse(textBox1.Text);
            cmd = new MySqlCommand(selectQuery, MyConnection);

            mdr = cmd.ExecuteReader();
            if (mdr.Read())
            {
                rlastname1.Text = mdr.GetString("lastname");
                rfirstname1.Text = mdr.GetString("firstname");
                rmiddlename1.Text = mdr.GetString("middlename");
                rstarts1.Text = mdr.GetString("starts");
                rends1.Text = mdr.GetString("ends");
                rday1.Text = mdr.GetString("day");
                rid1.Text = mdr.GetString("PatientNumber");
                textBox1.Text = "";
            }
            else
            {
                textBox1.Clear();
                MessageBox.Show("No data found for this patient.");
            }
            MyConnection.Close();
        }
        public void cancel_appointment_hospital()
        {
            MySqlConnection MyConnection = new MySqlConnection(MyConnectionString);
            MySqlCommand cmd;
            MySqlDataReader mdr;

            MyConnection.Open();

            string selectQuery = "DELETE FROM `hospital_appointment`  WHERE PatientNumber =" + int.Parse(rid1.Text);
            cmd = new MySqlCommand(selectQuery, MyConnection);

            mdr = cmd.ExecuteReader();
            if (mdr.Read())
            {

            }
            else
            {
                rlastname1.Clear();
                rfirstname1.Clear();
                rmiddlename1.Clear();
                rstarts1.Clear();
                rends1.Clear();
                rday1.Clear();
                rid1.Clear();
                MessageBox.Show("No Data Found For This Appointment");
            }
            MyConnection.Close();
        }








        private void rsearch_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Please Enter the DoctorID of the Patient");
                textBox1.Clear();
                rlastname.Clear();
                rfirstname.Clear();
                rmiddlename.Clear();
                rstarts.Clear();
                rends.Clear();
                rdoctorid.Clear();
                rdoctorname.Clear();
                rday.Clear();
                rcontact.Clear();
            }
            else
            {
                if (radioButton8.Checked)
                {
                    search_appointments_doctor_cancel();
                }
                if (radioButton7.Checked)
                {
                    search_appointments_hospital_cancel();
                }

            }
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            load_doctor_appointment();
            groupBox8.Visible = false;
            groupBox6.Visible = true;
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            groupBox8.Visible = true;
            load_hospital_appointment();
            groupBox6.Visible = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(rdoctorid.Text))
            {
                MessageBox.Show("Please enter Doctor ID.");
            }
            else
            {
                DialogResult dr = MessageBox.Show("Are you sure you want to Cancel this Appointment  ??", "Cancelling", MessageBoxButtons.YesNo);
                switch (dr)
                {
                    case DialogResult.Yes:
                        cancel_appointment_doctor();
                        load_doctor_appointment();
                        break;
                    case DialogResult.No:
                        break;
                }
                
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            rlastname.Clear();
            rfirstname.Clear();
            rmiddlename.Clear();
            rstarts.Clear();
            rends.Clear();
            rdoctorid.Clear();
            rdoctorname.Clear();
            rday.Clear();
            rcontact.Clear();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            rlastname1.Clear();
            rfirstname1.Clear();
            rmiddlename1.Clear();
            rid1.Clear();
            rstarts1.Clear();
            rends1.Clear();
            rday1.Clear();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure you want to Cancel this Appointment  ??", "Cancelling", MessageBoxButtons.YesNo);
            switch (dr)
            {
                case DialogResult.Yes:
                    cancel_appointment_hospital();
                    load_hospital_appointment();
                    break;
                case DialogResult.No:
                    break;
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            panel4.Visible = false;
            panel3.Visible = true;
            panel2.Visible = false;
            panel1.Visible = false;
        }








        public void reschedule()
        {
            MySqlConnection conn5 = new MySqlConnection(MyConnectionString);
            MySqlCommand cmd4;
            conn5.Open();
            try
            {

                cmd4 = conn5.CreateCommand();
                cmd4.CommandText = @"UPDATE `doctor_appointments` SET `starts`=@starts,`ends`=@ends,`day`=@day, `doctorid`=@doctorid WHERE `doctorid`=@doctorid";

                cmd4.Parameters.AddWithValue("@doctorid", rsdoctorid.Text.ToString());
                cmd4.Parameters.AddWithValue("@starts", rsstarts.Text.ToString());
                cmd4.Parameters.AddWithValue("@ends", rsends.Text.ToString());
                cmd4.Parameters.AddWithValue("@day", rsstarts.Text.ToString());


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
                    rslastname.Clear();
                    rsfirstname.Clear();
                    rsmiddlename.Clear();
                    rsdoctorid.Clear();
                    rsdoctorname.Clear();
                    rscontact.Clear();   
                    MessageBox.Show("Reschedulling has been Successful.");
                }
            }
        }
        public void search_appointments_doctor_rs()
        {
            MySqlConnection MyConnection = new MySqlConnection(MyConnectionString);
            MySqlCommand cmd;
            MySqlDataReader mdr;

            MyConnection.Open();

            string selectQuery = "SELECT * FROM `doctor_appointments` WHERE doctorid =" + int.Parse(textBox2.Text);
            cmd = new MySqlCommand(selectQuery, MyConnection);

            mdr = cmd.ExecuteReader();
            if (mdr.Read())
            {
                rslastname.Text = mdr.GetString("lastname");
                rsfirstname.Text = mdr.GetString("firstname");
                rsmiddlename.Text = mdr.GetString("middlename");
                rsdoctorid.Text = mdr.GetString("doctorid");
                rsdoctorname.Text = mdr.GetString("doctorname");
                rscontact.Text = mdr.GetString("contact");
                textBox2.Clear();
            }
            else
            {
                textBox1.Clear();
                MessageBox.Show("No data found for this patient.");
            }
            MyConnection.Close();
        }
        public void search_appointments_hospital_rs()
        {
            MySqlConnection MyConnection = new MySqlConnection(MyConnectionString);
            MySqlCommand cmd;
            MySqlDataReader mdr;

            MyConnection.Open();

            string selectQuery = "SELECT * FROM `hospital_appointment` WHERE PatientNumber =" + int.Parse(textBox2.Text);
            cmd = new MySqlCommand(selectQuery, MyConnection);

            mdr = cmd.ExecuteReader();
            if (mdr.Read())
            {
                rslastname1.Text = mdr.GetString("lastname");
                rsfirstname1.Text = mdr.GetString("firstname");
                rsmiddlename1.Text = mdr.GetString("middlename");
                rspatientnumber1.Text = mdr.GetString("PatientNumber");
                textBox2.Clear();
            }
            else
            {
                textBox2.Clear();
                MessageBox.Show("No data found for this patient.");
            }
            MyConnection.Close();
        }
        public void reschedule1()
        {
            MySqlConnection conn5 = new MySqlConnection(MyConnectionString);
            MySqlCommand cmd4;
            conn5.Open();
            try
            {

                cmd4 = conn5.CreateCommand();
                cmd4.CommandText = @"UPDATE `doctor_appointments` SET `PatientNumber`=@PatientNumber,`starts`=@starts,`ends`=@ends,`day`=@day WHERE `PatientNumber`=@PatientNumber";

                cmd4.Parameters.AddWithValue("@PatientNumber", rspatientnumber1.Text.ToString());
                cmd4.Parameters.AddWithValue("@starts", rsstarts1.Text.ToString());
                cmd4.Parameters.AddWithValue("@ends", rsends1.Text.ToString());
                cmd4.Parameters.AddWithValue("@day", rsstarts1.Text.ToString());


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
                    rslastname1.Clear();
                    rsfirstname1.Clear();
                    rsmiddlename1.Clear();
                    rspatientnumber1.Clear();
                    MessageBox.Show("Reschedulling has been Successful.");
                }
            }
        }





        private void radioButton10_CheckedChanged(object sender, EventArgs e)
        {
            rshospital.Visible = false;
            rsdoctor.Visible = true;
        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            rshospital.Visible = true;
            rsdoctor.Visible = false;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Please fill up the form");
            }
            else
            {
                if (radioButton10.Checked)
                {
                    search_appointments_doctor_rs();
                }
                if (radioButton9.Checked)
                {
                    search_appointments_hospital_rs();
                }
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            rslastname.Clear();
            rsfirstname.Clear();
            rsmiddlename.Clear();
            rsdoctorid.Clear();
            rsdoctorname.Clear();
            rscontact.Clear();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure you want to reschedule this Appointment  ??", "Cancelling", MessageBoxButtons.YesNo);
            switch (dr)
            {
                case DialogResult.Yes:
                    reschedule();
                    break;
                case DialogResult.No:
                    break;
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            rslastname1.Clear();
            rsfirstname1.Clear();
            rsmiddlename1.Clear();
            rspatientnumber1.Clear();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure you want to reschedule this Appointment  ??", "Cancelling", MessageBoxButtons.YesNo);
            switch (dr)
            {
                case DialogResult.Yes:
                    reschedule1();
                    break;
                case DialogResult.No:
                    break;
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            panel4.Visible = true;
            panel3.Visible = false;
            panel2.Visible = false;
            panel1.Visible = false;
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }



        public void load_doctor_appointment2()
        {
            MySqlConnection connection = new MySqlConnection(MyConnectionString);
            connection.Open();
            try
            {
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM `doctor_appointments` ";
                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adap.Fill(ds);
                loadappointment.DataSource = ds.Tables[0].DefaultView;
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
        public void load_doctor_appointment3()
        {
            MySqlConnection connection = new MySqlConnection(MyConnectionString);
            connection.Open();
            try
            {
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM `hospital_appointment` ";
                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adap.Fill(ds);
                loadappointment1.DataSource = ds.Tables[0].DefaultView;
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


        private void radioButton12_CheckedChanged(object sender, EventArgs e)
        {
            load_doctor_appointment2();
            groupBox11.Visible = true;
            groupBox12.Visible = false;
        }

        private void radioButton11_CheckedChanged(object sender, EventArgs e)
        {
            load_doctor_appointment3();
            groupBox12.Visible = true;
            groupBox11.Visible = false;
        }
    
    
    
    }
}
