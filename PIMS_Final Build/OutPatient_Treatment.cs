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
    public partial class OutPatient_Treatment : Form
    {
        public OutPatient_Treatment()
        {
            InitializeComponent();
        }


        string MyConnectionString = "Server=localhost;Port=3306;database=final_pims;Uid=root;Pwd=''";
        public void doctor_name_id()
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
            }
            else
            {
            }
            MyConnection.Close();
        }
        public void medicine_name_id()
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
            }
            else
            {
            }
            MyConnection.Close();
        }
        public void typeofcare1()
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
            }
            else
            {
            }
            MyConnection.Close();
        }
        public void save()
        {
            MySqlConnection connection = new MySqlConnection(MyConnectionString);
            MySqlCommand cmd;
            connection.Open();


            try
            {
                cmd = connection.CreateCommand();
                cmd.CommandText = @"INSERT INTO `outpatient_treatment`(`outpatientid`, `lastname`, `firstname`, `middlename`, `sex`, `status`, `contact`, `birth`, `age`, `doctorid`, `doctorname`, `careid`, `typeofcare`, `medicineid`, `medicinename`, `date`, `time`) 
VALUES (@outpatientid, @lastname, @firstname, @middlename, @sex, @status, @contact, @birth, @age, @doctorid, @doctorname, @careid, @typeofcare, @medicineid, @medicinename, @date, @time)";


                cmd.Parameters.AddWithValue("@date", Datestatus.Text.ToString());
                cmd.Parameters.AddWithValue("@time", Timestatus.Text.ToString());


                cmd.Parameters.AddWithValue("@outpatientid", outpatientid.Text.ToString());
                cmd.Parameters.AddWithValue("@firstname", firstname.Text.ToString());
                cmd.Parameters.AddWithValue("@lastname", lastname.Text.ToString());
                cmd.Parameters.AddWithValue("@middlename", middlename.Text.ToString());
                cmd.Parameters.AddWithValue("@sex", sex.Text.ToString());
                cmd.Parameters.AddWithValue("@status", status.Text.ToString());
                cmd.Parameters.AddWithValue("@contact", contact.Text.ToString());
                cmd.Parameters.AddWithValue("@birth", birth.Text.ToString());
                cmd.Parameters.AddWithValue("@age", age.Text.ToString());

                cmd.Parameters.AddWithValue("@doctorid", doctorid.Text.ToString());
                cmd.Parameters.AddWithValue("@doctorname", doctorname.Text.ToString());
                cmd.Parameters.AddWithValue("@careid", careid.Text.ToString());
                cmd.Parameters.AddWithValue("@typeofcare", typeofcare.Text.ToString());
                cmd.Parameters.AddWithValue("@medicineid", medicineid.Text.ToString());
                cmd.Parameters.AddWithValue("@medicinename", medicinename.Text.ToString());

                cmd.ExecuteNonQuery();
                MySqlDataAdapter sda2 = new MySqlDataAdapter(cmd);
                DataSet ds2 = new DataSet();
                sda2.Fill(ds2);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            outpatientid.Text = "0";
            contact.Text = "0";
            doctorid.Text = "0";
            careid.Text = "0";
            medicineid.Text = "0";
            lastname.Clear();
            firstname.Clear();
            middlename.Clear();
            age.Clear();
            doctorname.Clear();
            typeofcare.Clear();
            medicinename.Clear();
        }
        public void load()
        {

            MySqlConnection connection = new MySqlConnection(MyConnectionString);
            connection.Open();
            try
            {
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM `outpatient_treatment`";
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
            string query = "SELECT outpatientid FROM `outpatient_treatment`";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                string val = dr[0].ToString();
                if (val == "")
                {
                    outpatientid.Text = "1";
                }
                else
                {
                    a = Convert.ToInt32(dr[0].ToString());
                    a = a + 2;
                    outpatientid.Text = a.ToString();
                }
            }
        }
        public void changebasevalue()
        {
            MySqlConnection conn5 = new MySqlConnection(MyConnectionString);
            MySqlCommand cmd4;
            conn5.Open();
            try
            {

                cmd4 = conn5.CreateCommand();
                cmd4.CommandText = @"UPDATE `outpatient_treatment` SET `lastname`=@lastname,`firstname`=@firstname,`middlename`=@middlename,`sex`=@sex,`status`=@status,`contact`=@contact,
`birth`=@birth,`age`=@age WHERE `outpatientid`=@outpatientid";


                cmd4.Parameters.AddWithValue("@outpatientid", upoutpatientid.Text.ToString());
                cmd4.Parameters.AddWithValue("@lastname", uplastname.Text.ToString());
                cmd4.Parameters.AddWithValue("@firstname", upfirstname.Text.ToString());
                cmd4.Parameters.AddWithValue("@middlename", upmiddlename.Text.ToString());
                cmd4.Parameters.AddWithValue("@sex", upsex.Text.ToString());
                cmd4.Parameters.AddWithValue("@status", status.Text.ToString());
                cmd4.Parameters.AddWithValue("@contact", upcontact.Text.ToString());
                cmd4.Parameters.AddWithValue("@birth", upbirth.Text.ToString());
                cmd4.Parameters.AddWithValue("@age", upage.Text.ToString());


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
                    upoutpatientid.Clear();
                    upfirstname.Clear();
                    uplastname.Clear();
                    upmiddlename.Clear();
                    upage.Clear();
                    upcontact.Clear();
                    MessageBox.Show("Outpatient Record has been Change Successfully.");
                }
            }
        }
        










        private void medicineid_TextChanged(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
        }
        private void careid_TextChanged(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
        }
        private void doctorid_TextChanged(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
        }
        private void outpatientid_TextChanged(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
        }
        private void age_TextChanged(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
        }
        private void contact_TextChanged(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
        }
        private void lastname_TextChanged(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsLetter(e.KeyChar) || e.KeyChar == 8 ? false : true;
        }
        private void firstname_TextChanged(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsLetter(e.KeyChar) || e.KeyChar == 8 ? false : true;
        }
        private void middlename_TextChanged(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsLetter(e.KeyChar) || e.KeyChar == 8 ? false : true;
        }
    






        private void button1_Click(object sender, EventArgs e)
        {
            if (doctorid.Text == "")
            {

            }
            else
            {
                doctor_name_id();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (medicineid.Text == "")
            {

            }
            else
            {
                medicine_name_id();
            }
        }

        private void button29_Click(object sender, EventArgs e)
        {
            if (careid.Text == "")
            {

            }
            else
            {
                typeofcare1();
            }
        }

        private void button32_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(outpatientid.Text) || String.IsNullOrEmpty(lastname.Text) || String.IsNullOrEmpty(firstname.Text) ||
                String.IsNullOrEmpty(middlename.Text) || String.IsNullOrEmpty(sex.Text) || String.IsNullOrEmpty(status.Text) ||
                String.IsNullOrEmpty(contact.Text) || String.IsNullOrEmpty(age.Text) || String.IsNullOrEmpty(doctorid.Text) ||
                String.IsNullOrEmpty(doctorname.Text) || String.IsNullOrEmpty(careid.Text) || String.IsNullOrEmpty(typeofcare.Text) ||
                String.IsNullOrEmpty(medicineid.Text) || String.IsNullOrEmpty(medicinename.Text))
            {
                MessageBox.Show("Please fill up the form !!");
            }
            else
            {
                DialogResult dr = MessageBox.Show("Save out patient ?", "Out Patient Save !! ", MessageBoxButtons.YesNo);
                switch (dr)
                {
                    case DialogResult.Yes:
                        save();
                        break;
                    case DialogResult.No:
                        break;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            contact.Clear();
            doctorid.Clear();
            careid.Clear();
            medicineid.Clear();
            lastname.Clear();
            firstname.Clear();
            middlename.Clear();
            age.Clear();
            doctorname.Clear();
            typeofcare.Clear();
            medicinename.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OutPatient_Billing opb = new OutPatient_Billing();
            opb.Show();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OutPatient_Payment opp = new OutPatient_Payment();
            opp.Show();
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure you want to Cancel ?", "CANCEL !! ", MessageBoxButtons.YesNo);
            switch (dr)
            {
                case DialogResult.Yes:
                    this.Close();
                    break;
                case DialogResult.No:
                    break;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = true;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            upoutpatientid.Clear();
            upcontact.Clear();
            updoctorid.Clear();
            upcareid.Clear();
            upmedicineid.Clear();
            uplastname.Clear();
            upfirstname.Clear();
            upmiddlename.Clear();
            upage.Clear();
            updoctorname.Clear();
            uptypeofcare.Clear();
            upmedicinename.Clear();
            panel1.Visible = true;
            panel2.Visible = false;
        }









        private void updoctorid_TextChanged(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
        }
        private void upcareid_TextChanged(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
        }
        private void upmedicineid_TextChanged(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
        }
        private void upage_TextChanged(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
        }
        private void upoutpatientid_TextChanged(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
        }
        private void upcontact_TextChanged(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
        }
        private void uplastname_TextChanged(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsLetter(e.KeyChar) || e.KeyChar == 8 ? false : true;
        }
        private void upfirstname_TextChanged(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsLetter(e.KeyChar) || e.KeyChar == 8 ? false : true;
        }
        private void upmiddlename_TextChanged(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsLetter(e.KeyChar) || e.KeyChar == 8 ? false : true;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            searchpatientupdate.Visible = true;
            uppatienttreatment.Visible = false;

        }

        private void button15_Click(object sender, EventArgs e)
        {
            searchpatientupdate.Visible = false;
            uppatienttreatment.Visible = true;
        }

        private void OutPatient_Treatment_Load(object sender, EventArgs e)
        {
             timer1.Start();
            Timestatus.Text = DateTime.Now.ToLongTimeString();
            Datestatus.Text = DateTime.Now.ToLongDateString();
            outpatientid.ReadOnly = true;
            generator();
            load();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            searchpatientupdate.Visible = false;
            uppatienttreatment.Visible = true;
            upoutpatientid.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            upcontact.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            updoctorid.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
            upcareid.Text = dataGridView1.CurrentRow.Cells[11].Value.ToString();
            upmedicineid.Text = dataGridView1.CurrentRow.Cells[13].Value.ToString();
            uplastname.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            upfirstname.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            upmiddlename.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            upage.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            updoctorname.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
            uptypeofcare.Text = dataGridView1.CurrentRow.Cells[12].Value.ToString();
            upmedicinename.Text = dataGridView1.CurrentRow.Cells[14].Value.ToString();
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            if (updoctorid.Text == "")
            {

            }
            else
            {

            }
        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            if (upcareid.Text == "")
            {

            }
            else
            {

            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (upmedicineid.Text == "")
            {

            }
            else
            {

            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(upoutpatientid.Text) || String.IsNullOrEmpty(uplastname.Text) || String.IsNullOrEmpty(upfirstname.Text) || String.IsNullOrEmpty(upmiddlename.Text) ||
                String.IsNullOrEmpty(upage.Text) || String.IsNullOrEmpty(upsex.Text) || String.IsNullOrEmpty(upcontact.Text) || String.IsNullOrEmpty(upstatus.Text))
            {
                MessageBox.Show("Please fill up the form");
                }
            else
            {
                changebasevalue();
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            searchpatientupdate.Visible = true;
            uppatienttreatment.Visible = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Start();
            Timestatus.Text = DateTime.Now.ToLongTimeString();
            Datestatus.Text = DateTime.Now.ToLongDateString();
        }

        

        

       

        

        
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    }
}