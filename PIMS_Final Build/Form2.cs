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

    public partial class Form2 : Form
    {

        Double atotal;
        Double anettotal;

        Double atotal1;
        Double anettotal1;

        public Form2 auser { get; set; }

    
        public Form2(string designation)
        {
            InitializeComponent();
            User.Text = designation;
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
                cmd.CommandText = @"INSERT INTO `new_patient`(`date`,`time`,`PatientNumber`, `firstname`, `lastname`, `middlename`, `age`, `birth`, `sex`,`status`,`address`,`city`,`contact`,`telephone`,`email`,`grelationship`,`gname`,`gnumber`,`diagnosed`,`allergie`) 
                                                    VALUES (@date,@time,@PatientNumber,@firstname,@lastname,@middlename,@age,@birth,@sex,@status,@address,@city,@contact,@telephone,@email,@grelationship,@gname,@gnumber,@diagnosed,@allergie)";


                cmd.Parameters.AddWithValue("@date", Datestatus.Text.ToString());
                cmd.Parameters.AddWithValue("@time", Timestatus.Text.ToString());
                cmd.Parameters.AddWithValue("@PatientNumber", PatientNumber.Text.ToString());
                cmd.Parameters.AddWithValue("@firstname", firstname.Text.ToString());
                cmd.Parameters.AddWithValue("@lastname", lastname.Text.ToString());
                cmd.Parameters.AddWithValue("@middlename", middlename.Text.ToString());
                cmd.Parameters.AddWithValue("@age", age.Text.ToString());
                cmd.Parameters.AddWithValue("@birth", birth.Text.ToString());
                cmd.Parameters.AddWithValue("@sex", sex.Text.ToString());
                cmd.Parameters.AddWithValue("@status", status.Text.ToString());
                cmd.Parameters.AddWithValue("@address", address.Text.ToString());
                cmd.Parameters.AddWithValue("@city", city.Text.ToString());
                cmd.Parameters.AddWithValue("@contact", contact.Text.ToString());
                cmd.Parameters.AddWithValue("@telephone", telephone.Text.ToString());
                cmd.Parameters.AddWithValue("@email", email.Text.ToString());
                cmd.Parameters.AddWithValue("@grelationship", relationship.Text.ToString());
                cmd.Parameters.AddWithValue("@gname", gname.Text.ToString());
                cmd.Parameters.AddWithValue("@gnumber", gnumber.Text.ToString());
                cmd.Parameters.AddWithValue("@diagnosed", diagnosed.Text.ToString());
                cmd.Parameters.AddWithValue("@allergie", allergie.Text.ToString());

                cmd.ExecuteNonQuery();
                MySqlDataAdapter sda2 = new MySqlDataAdapter(cmd);
                DataSet ds2 = new DataSet();
                sda2.Fill(ds2);
            }

            catch (Exception)
            {
                MessageBox.Show("Patient has been Registered.");
            }
            PatientNumber.Clear();
            lastname.Clear();
            firstname.Clear();
            middlename.Clear();
            age.Clear();
            address.Clear();
            city.Clear();
            contact.Clear();
            telephone.Clear();
            gname.Clear();
            email.Clear();
            gnumber.Clear();
            diagnosed.Clear();
            allergie.Clear();
            generator3();
            username.Text = User.Text;
            outpatientsrecordsoverallbill2();
            outpatientsrecords2();
            user1();
            generator1();
            generator();
            service_treatment_load();
            service_treatment_information_table();
            admitsearch();
            medicine_treatment_load();
            medicine_table_load();

        }
        public void admitsearch()
        {
            MySqlConnection connection = new MySqlConnection(MyConnectionString);
            connection.Open();
            try
            {
                MySqlCommand cmd = connection.CreateCommand();

                string value = comboBox1.Text;
                switch (value)
                {
                    case "Lastname":
                        cmd.CommandText = "Select * from `new_patient` where lastname LIKE @searchKey";
                        break;

                    case "Firstname":
                        cmd.CommandText = "Select * from `new_patient` where firstname LIKE @searchKey";
                        break;

                    case "PatientNumber":
                        cmd.CommandText = "Select * from `new_patient` where PatientNumber LIKE @searchKey";
                        break;


                    case "":
                        cmd.CommandText = "Select * from `new_patient`";
                        searchpatient.SelectionStart = 0;
                        searchpatient.SelectionLength = apatientnumber.Text.Length;
                        break;
                }
                cmd.Parameters.AddWithValue("@searchKey", "%" + searchpatient.Text.ToString() + "%");
                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adap.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0].DefaultView;
                connection.Close();
            }
            catch (Exception)
            {
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
        public void admitpatient()
        {
            MySqlConnection connection = new MySqlConnection(MyConnectionString);
            MySqlCommand cmd;
            connection.Open();


            try
            {
                cmd = connection.CreateCommand();
                cmd.CommandText = @"INSERT INTO `Inpatients_Admissions`(`date`, `time`, `PatientNumber`, `admitnumber`, `lastname`, `firstname`, `middlename`, `roomid`, `doctorid`, `doctor`, `room`, `additionalnote`) 
                                                                VALUES (@date, @time, @PatientNumber, @admitnumber, @lastname, @firstname, @middlename, @roomid, @doctorid, @doctor, @room, @additionalnote) ";


                cmd.Parameters.AddWithValue("@roomid", textBox1.Text.ToString());
                cmd.Parameters.AddWithValue("@doctorid", textBox2.Text.ToString());

                cmd.Parameters.AddWithValue("@date", Datestatus.Text.ToString());
                cmd.Parameters.AddWithValue("@time", Timestatus.Text.ToString());
                cmd.Parameters.AddWithValue("@PatientNumber", apatientnumber.Text.ToString());
                cmd.Parameters.AddWithValue("@admitnumber", admitnumber.Text.ToString());
                cmd.Parameters.AddWithValue("@firstname", afirstname.Text.ToString());
                cmd.Parameters.AddWithValue("@lastname", alastname.Text.ToString());
                cmd.Parameters.AddWithValue("@middlename", amiddlename.Text.ToString());
                cmd.Parameters.AddWithValue("@doctor", adoctor.Text.ToString());
                cmd.Parameters.AddWithValue("@room", aroom.Text.ToString());
                cmd.Parameters.AddWithValue("@additionalnote", anote.Text.ToString());

                cmd.ExecuteNonQuery();
                MySqlDataAdapter sda2 = new MySqlDataAdapter(cmd);
                DataSet ds2 = new DataSet();
                sda2.Fill(ds2);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            

            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                    apatientnumber.Clear();
                    admitnumber.Clear();
                    alastname.Clear();
                    afirstname.Clear();
                    amiddlename.Clear();
                    adoctor.Clear();
                    aroom.Clear();
                    anote.Clear();
                }
            }
        }

        public void inpatientmedicalsearch()
        {
            MySqlConnection MyConnection = new MySqlConnection(MyConnectionString);
            MySqlCommand cmd;
            MySqlDataReader mdr;

            MyConnection.Open();

            string selectQuery = "Select * from `inpatients_admissions` where PatientNumber=" + int.Parse(msearch.Text);
            cmd = new MySqlCommand(selectQuery, MyConnection);

            mdr = cmd.ExecuteReader();
            if (mdr.Read())
            {
                mlastname.Text = mdr.GetString("lastname");
                mfirstname.Text = mdr.GetString("firstname");
                mmiddlename.Text = mdr.GetString("middlename");
                mpatientnumber.Text = mdr.GetString("PatientNumber");
                msearch.Text = "0";
            }
            else
            {
                MessageBox.Show("No Data Found For This PatientNumber");
            }
            MyConnection.Close();
        }
        public void admit_doctorid_search()
        {
            MySqlConnection MyConnection = new MySqlConnection(MyConnectionString);
            MySqlCommand cmd;
            MySqlDataReader mdr;

            MyConnection.Open();

            string selectQuery = "Select * from `doctor_maintenance` where doctorid =" + int.Parse(textBox2.Text);
            cmd = new MySqlCommand(selectQuery, MyConnection);

            mdr = cmd.ExecuteReader();
            if (mdr.Read())
            {
                adoctor.Text = mdr.GetString("name");
            }
            else
            {
                adoctor.Clear();
                textBox2.Clear();
                MessageBox.Show("No Data Found For This Doctor");
            }
            MyConnection.Close();
        }
        public void admit_roomid_searh()
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
                aroom.Text = mdr.GetString("department");
            }
            else
            {
                adoctor.Clear();
                textBox2.Clear();
                MessageBox.Show("No Data Found For This Room");
            }
            MyConnection.Close();
        }
        public void medicine_maintenance()
        {
            MySqlConnection MyConnection = new MySqlConnection(MyConnectionString);
            MySqlCommand cmd;
            MySqlDataReader mdr;

            MyConnection.Open();

            string selectQuery = "Select * from `medical_maintenance` where medicineid =" + int.Parse(medicinenumber.Text);
            cmd = new MySqlCommand(selectQuery, MyConnection);

            mdr = cmd.ExecuteReader();
            if (mdr.Read())
            {
                medicinename.Text = mdr.GetString("medicine_name");
                unitprice.Text = mdr.GetString("unitprice");
                
            }
            else
            {
                MessageBox.Show("No Data Found For This Medicine");
            }
            MyConnection.Close();
        }
        public void medidvalidation()
        {
            if (String.IsNullOrEmpty(mlastname.Text) || String.IsNullOrEmpty(mfirstname.Text) || String.IsNullOrEmpty(mmiddlename.Text))
            {
                button13.Enabled = true;
            }
        }
        public void medicine_treatment()
        {
            MySqlConnection connection = new MySqlConnection(MyConnectionString);
            MySqlCommand cmd;
            connection.Open();


            try
            {
                cmd = connection.CreateCommand();
                cmd.CommandText = @"INSERT INTO `medical_treatment`(`PatientNumber`, `treatmentid`, `lastname`, `firstname`, `middlename`, `medicineid`, `medicinename`, `dateofissue`, `unitprice`, `qty`, `total`, `nettotal`, `time`, `date`) 
                        VALUES (@PatientNumber, @treatmentid, @lastname, @firstname, @middlename, @medicineid, @medicinename, @dateofissue, @unitprice, @qty, @total, @nettotal, @time, @date)";

                cmd.Parameters.AddWithValue("@PatientNumber", mpatientnumber.Text.ToString());
                cmd.Parameters.AddWithValue("@treatmentid", mtreatmentid.Text.ToString());
                cmd.Parameters.AddWithValue("@lastname", mlastname.Text.ToString());
                cmd.Parameters.AddWithValue("@firstname", mfirstname.Text.ToString());
                cmd.Parameters.AddWithValue("@middlename", mmiddlename.Text.ToString());
                cmd.Parameters.AddWithValue("@medicineid", medicinenumber.Text.ToString());
                cmd.Parameters.AddWithValue("@medicinename", medicinename.Text.ToString());
                cmd.Parameters.AddWithValue("@dateofissue", dateofissue.Text.ToString());
                cmd.Parameters.AddWithValue("@unitprice", unitprice.Text.ToString());
                cmd.Parameters.AddWithValue("@qty", qty.Text.ToString());
                cmd.Parameters.AddWithValue("@total", total.Text.ToString());
                cmd.Parameters.AddWithValue("@nettotal", nettotal.Text.ToString());

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
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            

        }
        public void medicine_table_load()
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
                MedicineID.DataSource = ds.Tables[0].DefaultView;
                

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
        public void medicine_treatment_load()
        {

            MySqlConnection connection = new MySqlConnection(MyConnectionString);
            connection.Open();
            try
            {
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM `medical_treatment`";
                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adap.Fill(ds);
                mtreatmentload.DataSource = ds.Tables[0].DefaultView;
                

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
        public void outpatientsrecords2()
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
                outpatients.DataSource = ds.Tables[0].DefaultView;


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
        public void medicine_treatment_search_update()
        {
            MySqlConnection connection = new MySqlConnection(MyConnectionString);
            connection.Open();
            try
            {
                MySqlCommand cmd = connection.CreateCommand();

                string value = label61.Text;
                switch (value)
                {
                    case "TreatmentID":
                        cmd.CommandText = "Select * from `medical_treatment`  where TreatmentID LIKE @searchKey";
                        break;


                    case "":
                        cmd.CommandText = "Select * from `medical_treatment` ";
                        searchpatient.SelectionStart = 0;
                        searchpatient.SelectionLength = apatientnumber.Text.Length;
                        break;
                }
                cmd.Parameters.AddWithValue("@searchKey", "%" + umsearch.Text.ToString() + "%");
                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adap.Fill(ds);
                mtreatmentload.DataSource = ds.Tables[0].DefaultView;
            }
            catch (Exception)
            {
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
        public void medicine_treatment1()
        {
            MySqlConnection MyConnection = new MySqlConnection(MyConnectionString);
            MySqlCommand cmd;
            MySqlDataReader mdr;

            MyConnection.Open();

            string selectQuery = "Select * from `medical_maintenance` where medicineid =" + int.Parse(ummedicinenumber.Text);
            cmd = new MySqlCommand(selectQuery, MyConnection);

            mdr = cmd.ExecuteReader();
            if (mdr.Read())
            {
                medicinename.Text = mdr.GetString("medicine_name");
                unitprice.Text = mdr.GetString("unitprice");
                
            }
            else
            {
                MessageBox.Show("No Data Found For This Medicine");
            }
            MyConnection.Close();
        }
        public void medicine_maintenance_update()
        {
            MySqlConnection conn5 = new MySqlConnection(MyConnectionString);
            MySqlCommand cmd4;
            conn5.Open();
            try
            {

                cmd4 = conn5.CreateCommand();
                cmd4.CommandText = @"UPDATE `medical_treatment` SET `treatmentid`=@treatmentid,`lastname`=@lastname,`firstname`=@firstname,`middlename`=@middlename,`medicineid`=@medicineid,`medicinename`=@medicinename,`dateofissue`=@dateofissue,`unitprice`=@unitprice,`qty`=@qty,`total`=@total,`nettotal`=@nettotal 
                                    WHERE `treatmentid`=@treatmentid";


                cmd4.Parameters.AddWithValue("@treatmentid", umtreatmentid.Text.ToString());
                cmd4.Parameters.AddWithValue("@lastname", umlastname.Text.ToString());
                cmd4.Parameters.AddWithValue("@firstname", umfirstname.Text.ToString());
                cmd4.Parameters.AddWithValue("@middlename", ummiddlename.Text.ToString());
                cmd4.Parameters.AddWithValue("@medicineid", ummedicinenumber.Text.ToString());
                cmd4.Parameters.AddWithValue("@medicinename", ummedicinename.Text.ToString());
                cmd4.Parameters.AddWithValue("@dateofissue", umdateofissue.Text.ToString());
                cmd4.Parameters.AddWithValue("@unitprice", umunitprice.Text.ToString());
                cmd4.Parameters.AddWithValue("@qty", umqty.Text.ToString());
                cmd4.Parameters.AddWithValue("@total", umtotal.Text.ToString());
                cmd4.Parameters.AddWithValue("@nettotal", umnettotal.Text.ToString());



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
                    medicine_treatment_load();
                    MessageBox.Show("Record Successfuly Updated");
                    umtreatmentid.Clear();
                    umlastname.Clear();
                    umfirstname.Clear();
                    umpatientnumber.Clear();
                    ummiddlename.Clear();
                    ummedicinenumber.Clear();
                    ummedicinename.Clear();
                    umqty.Text = "0";
                    umunitprice.Text = "0";
                    umtotal.Text = "0";
                    umnettotal.Text = "0";

                }
            }

        }
        public void service_treatment_information_table()
        {

            MySqlConnection connection = new MySqlConnection(MyConnectionString);
            connection.Open();
            try
            {
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM `service_maintenance` ";
                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adap.Fill(ds);
                servicetreatmenttable.DataSource = ds.Tables[0].DefaultView;
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
        public void service_treatment_searchpatient()
        {
            MySqlConnection MyConnection = new MySqlConnection(MyConnectionString);
            MySqlCommand cmd;
            MySqlDataReader mdr;

            MyConnection.Open();

            string selectQuery = "Select * from `inpatients_admissions` where PatientNumber =" + int.Parse(ssearch.Text);
            cmd = new MySqlCommand(selectQuery, MyConnection);

            mdr = cmd.ExecuteReader();
            if (mdr.Read())
            {
                slastname.Text = mdr.GetString("lastname");
                sfirstname.Text = mdr.GetString("firstname");
                smiddlename.Text = mdr.GetString("middlename");
                spatientnumber.Text = mdr.GetString("PatientNumber");
                ssearch.Text = "0";
            }
            else
            {
                ssearch.Clear();
                MessageBox.Show("No Data Found For This Patient");
            }
            MyConnection.Close();
        }
        public void service_maintenance_search()
        {
            MySqlConnection MyConnection = new MySqlConnection(MyConnectionString);
            MySqlCommand cmd;
            MySqlDataReader mdr;

            MyConnection.Open();

            string selectQuery = "Select * from `service_maintenance` where service_id =" + int.Parse(sserviceid.Text);
            cmd = new MySqlCommand(selectQuery, MyConnection);

            mdr = cmd.ExecuteReader();
            if (mdr.Read())
            {
                sservicename.Text = mdr.GetString("service_name");
                sservicecharge.Text = mdr.GetString("amount");
            }
            else
            {
                ssearch.Clear();
                MessageBox.Show("No Data Found For This Service");
            }
            MyConnection.Close();
        }
        public void service_treatment()
        {
            MySqlConnection connection = new MySqlConnection(MyConnectionString);
            MySqlCommand cmd;
            connection.Open();


            try
            {
                cmd = connection.CreateCommand();
                cmd.CommandText = @"INSERT INTO `service_treatment`(`PatientNumber`, `treatmentid`, `lastname`, `firstname`, `middlename`, `serviceid`, `servicename`, `treatmentdate`, `servicecharge`, `date`, `time`) 
        VALUES (@PatientNumber,@treatmentid, @lastname, @firstname, @middlename, @serviceid, @servicename, @treatmentdate, @servicecharge, @date, @time) ";

                cmd.Parameters.AddWithValue("@PatientNumber", spatientnumber.Text.ToString());
                cmd.Parameters.AddWithValue("@treatmentid", streatmentid.Text.ToString());
                cmd.Parameters.AddWithValue("@lastname", slastname.Text.ToString());
                cmd.Parameters.AddWithValue("@firstname", sfirstname.Text.ToString());
                cmd.Parameters.AddWithValue("@middlename", smiddlename.Text.ToString());
                cmd.Parameters.AddWithValue("@serviceid", sserviceid.Text.ToString());
                cmd.Parameters.AddWithValue("@servicename", sservicename.Text.ToString());
                cmd.Parameters.AddWithValue("@treatmentdate", streatmentdate.Text.ToString());
                cmd.Parameters.AddWithValue("@servicecharge", sservicecharge.Text.ToString());

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
        public void service_treatment_load()
        {
            MySqlConnection connection = new MySqlConnection(MyConnectionString);
            connection.Open();
            try
            {
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM `service_treatment`";
                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adap.Fill(ds);
                servicetreatmentupdate.DataSource = ds.Tables[0].DefaultView;
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
        public void service_treatment_searchpatient_update()
        {
            MySqlConnection connection = new MySqlConnection(MyConnectionString);
            connection.Open();
            try
            {
                MySqlCommand cmd = connection.CreateCommand();

                string value = label86.Text;
                switch (value)
                {
                    case "TreatmentID":
                        cmd.CommandText = "Select * from `service_treatment` where TreatmentID LIKE @searchKey";
                        break;


                    case "":
                        cmd.CommandText = "Select * from `service_treatment` ";
                        streatmentpatientsearch.SelectionStart = 0;
                        streatmentpatientsearch.SelectionLength = streatmentpatientsearch.Text.Length;
                        break;
                }
                cmd.Parameters.AddWithValue("@searchKey", "%" + streatmentpatientsearch.Text.ToString() + "%");
                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adap.Fill(ds);
                servicetreatmentupdate.DataSource = ds.Tables[0].DefaultView;
            }
            catch (Exception)
            {
            }
            finally
            {

            }
        }
        public void service_id_search()
        {
            MySqlConnection MyConnection = new MySqlConnection(MyConnectionString);
            MySqlCommand cmd;
            MySqlDataReader mdr;

            MyConnection.Open();

            string selectQuery = "Select * from `service_maintenance`  where service_id =" + int.Parse(usserviceid.Text);
            cmd = new MySqlCommand(selectQuery, MyConnection);

            mdr = cmd.ExecuteReader();
            if (mdr.Read())
            {
                usservicename.Text = mdr.GetString("service_name");
                usservicecharge.Text = mdr.GetString("amount");
            }
            else
            {
                ssearch.Clear();
                MessageBox.Show("No Data Found For This Patient");
            }
            MyConnection.Close();
        }
        public void service_treatment_update()
        {
            MySqlConnection conn5 = new MySqlConnection(MyConnectionString);
            MySqlCommand cmd4;
            conn5.Open();
            try
            {

                cmd4 = conn5.CreateCommand();
                cmd4.CommandText = @"UPDATE `service_treatment` SET `treatmentid`=@treatmentid,`lastname`=@lastname,`firstname`=@firstname,`middlename`=@middlename,`serviceid`=@serviceid,`servicename`=@servicename,`treatmentdate`=@treatmentdate,`servicecharge`=@servicecharge 
                                    WHERE `treatmentid`=@treatmentid";


                cmd4.Parameters.AddWithValue("@treatmentid", ustreatmentid.Text.ToString());
                cmd4.Parameters.AddWithValue("@lastname", uslastname.Text.ToString());
                cmd4.Parameters.AddWithValue("@firstname", usfirstname.Text.ToString());
                cmd4.Parameters.AddWithValue("@middlename", usmiddlename.Text.ToString());
                cmd4.Parameters.AddWithValue("@serviceid", usserviceid.Text.ToString());
                cmd4.Parameters.AddWithValue("@servicename", usservicename.Text.ToString());
                cmd4.Parameters.AddWithValue("@treatmentdate", ustreatmentdate.Text.ToString());
                cmd4.Parameters.AddWithValue("@servicecharge", usservicecharge.Text.ToString());

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
                    service_treatment_load();
                    MessageBox.Show("Record Successfuly Updated");
                    uslastname.Clear();
                    usfirstname.Clear();
                    usmiddlename.Clear();
                    ustreatmentid.Clear();
                    usserviceid.Clear();
                    usservicecharge.Clear();
                    usmiddlename.Clear();
                    uspatientnumber.Clear();
                    usservicename.Clear();


                }
            }
        }
        public void generator()
        {
            int a;
            MySqlConnection connection = new MySqlConnection(MyConnectionString);
            connection.Open();
            string query = "SELECT admitnumber FROM `inpatients_admissions`";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                string val = dr[0].ToString();
                if (val == "")
                {
                    admitnumber.Text = "1";
                }
                else
                {
                    a = Convert.ToInt32(dr[0].ToString());
                    a = a + 3;
                    a = a + 1;
                    admitnumber.Text = a.ToString();
                    
                }
            }
        }
        public void generator1()
        {
            int a;
            MySqlConnection connection = new MySqlConnection(MyConnectionString);
            connection.Open();
            string query = "SELECT PatientNumber FROM `new_patient` ";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                string val = dr[0].ToString();
                if (val == "")
                {
                    PatientNumber.Text = "1";
                }
                else
                {
                    a = Convert.ToInt32(dr[0].ToString());
                    a = a + 4;
                    a = a + 1;
                    PatientNumber.Text = a.ToString();
                    
                }
            }
        }
        public void user1()
        {
            if (User.Text == "Cashier" || User.Text == "cashier" || User.Text == "CASHIER")
            {
                ManageUserAccount.Enabled = false;
                maintinanceToolStripMenuItem.Enabled = false;
                reportsToolStripMenuItem.Enabled = false;
                channellingToolStripMenuItem.Enabled = false;
                OutpatientSignUp.Enabled = false;
                InPatientDischarge.Enabled = false;
                PatientAdmission.Enabled = false;
                toolStripMenuItem4.Enabled = false;
                PatientAdmission.Enabled = false;
                patientToolStripMenuItem.Enabled = false;
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;
                User.Text = "Cashier";
            }
            if (User.Text == "InPatient" || User.Text == "inpatient" || User.Text == "INPATIENT")
            {
                ManageUserAccount.Enabled = false;
                maintinanceToolStripMenuItem.Enabled = false;
                outPatientsToolStripMenuItem.Enabled = false;
                toolStripMenuItem24.Enabled = false;
                OutPatientReport.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;
                User.Text = "InPatient";
            }
            if (User.Text == "OutPatient" || User.Text == "outpatient" || User.Text == "OUTPATIENT")
            {
                InPatientReport.Enabled = false;
                toolStripMenuItem23.Enabled = false;
                channellingToolStripMenuItem.Enabled = false;
                inPatientToolStripMenuItem.Enabled = false;
                patientToolStripMenuItem.Enabled = false;
                maintinanceToolStripMenuItem.Enabled = false;
                ManageUserAccount.Enabled = false;
                button1.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;
                button6.Enabled = false;
                button7.Enabled = false;
                User.Text = "OutPatient";
            }
            if (User.Text == "admin" || User.Text == "ADMIN" || User.Text == "Admin")
            {
                User.Text = "Administrator";
            }
            
        }
        public void outpatientsrecords3()
        {
            MySqlConnection connection = new MySqlConnection(MyConnectionString);
            connection.Open();
            try
            {
                MySqlCommand cmd = connection.CreateCommand();

                string value = comboBox3.Text;
                switch (value)
                {
                    case "lastname":
                        cmd.CommandText = "Select * from `outpatient_treatment` where lastname LIKE @searchKey";
                        break;

                    case "firstname":
                        cmd.CommandText = "Select * from `outpatient_treatment` where firstname LIKE @searchKey";
                        break;

                    case "outpatientsid":
                        cmd.CommandText = "Select * from `outpatient_treatment` where outpatientsid LIKE @searchKey";
                        break;


                    case "":
                        cmd.CommandText = "Select * from `outpatient_treatment`";
                        outpatientsearach.SelectionStart = 0;
                        outpatientsearach.SelectionLength = outpatientsearach.Text.Length;
                        break;
                }
                cmd.Parameters.AddWithValue("@searchKey", "%" + outpatientsearach.Text.ToString() + "%");
                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adap.Fill(ds);
                outpatients.DataSource = ds.Tables[0].DefaultView;
            }
            catch (Exception)
            {
            }
            finally
            {

            }
        }
        public void outpatientsrecordsoverallbill()
        {
            MySqlConnection connection = new MySqlConnection(MyConnectionString);
            connection.Open();
            try
            {
                MySqlCommand cmd = connection.CreateCommand();

                string value = comboBox3.Text;
                switch (value)
                {
                    case "lastname":
                        cmd.CommandText = "Select * from `outpatient_billing` where lastname LIKE @searchKey";
                        break;

                    case "firstname":
                        cmd.CommandText = "Select * from `outpatient_billing` where firstname LIKE @searchKey";
                        break;

                    case "outpatientsid":
                        cmd.CommandText = "Select * from `outpatient_billing` where outpatientsid LIKE @searchKey";
                        break;


                    case "":
                        cmd.CommandText = "Select * from `outpatient_billing`";
                        outpatientsearach.SelectionStart = 0;
                        outpatientsearach.SelectionLength = outpatientsearach.Text.Length;
                        break;
                }
                cmd.Parameters.AddWithValue("@searchKey", "%" + outpatientsearach.Text.ToString() + "%");
                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adap.Fill(ds);
                overallbill.DataSource = ds.Tables[0].DefaultView;
            }
            catch (Exception)
            {
            }
            finally
            {

            }
        }
        public void outpatientsrecordsoverallbill2()
        {

            MySqlConnection connection = new MySqlConnection(MyConnectionString);
            connection.Open();
            try
            {
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM `outpatient_billing`";
                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adap.Fill(ds);
                overallbill.DataSource = ds.Tables[0].DefaultView;


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
        public void changepassword1()
        {
            MySqlConnection conn5 = new MySqlConnection(MyConnectionString);
            MySqlCommand cmd4;
            conn5.Open();
            try
            {

                cmd4 = conn5.CreateCommand();
                cmd4.CommandText = @"UPDATE `accounts` SET `auser`=@auser,`apass`=@apass WHERE `auser`=@auser";


                cmd4.Parameters.AddWithValue("@auser", username.Text.ToString());
                cmd4.Parameters.AddWithValue("@apass", apass.Text.ToString());

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
                    service_treatment_load();
                    MessageBox.Show("Password has been changed");
                    username.Clear();
                    apass.Clear();
                }
            }
        }
        public void generator3()
        {
            int a;
            MySqlConnection connection = new MySqlConnection(MyConnectionString);
            connection.Open();
            string query = "SELECT user_id FROM `accounts`";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                string val = dr[0].ToString();
                if (val == "")
                {
                    id.Text = "1";
                }
                else
                {
                    a = Convert.ToInt32(dr[0].ToString());
                    a = a + 2;
                    id.Text = a.ToString();
                }
            }
        }
        


            
        








        private void Form2_Load(object sender, EventArgs e)
        {
            generator3();
            username.Text = User.Text;
            outpatientsrecordsoverallbill2();
            outpatientsrecords2();
            user1();
            generator1();
            generator();
            service_treatment_load();
            service_treatment_information_table();
            admitsearch();
            medicine_treatment_load();
            medicine_table_load();
            timer1.Start();
            Timestatus.Text = DateTime.Now.ToLongTimeString();
            Datestatus.Text = DateTime.Now.ToLongDateString();
            dateofissue.Text = DateTime.Now.ToLongDateString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = true;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            panel6.Visible = false;
            panel7.Visible = false;
            panel8.Visible = false;
        }

        private void channellingToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem27_Click(object sender, EventArgs e)
        {

        }

        private void NewPatients_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure to Cancel ?", "Cancel !!", MessageBoxButtons.YesNo);

            switch (dr)
            {
                case DialogResult.Yes:
                    panel1.Visible = false;
                    break;
                case DialogResult.No:
                    break;
            }
        }

        private void RegisterPatient_Click(object sender, EventArgs e)
        {
            panel8.Visible = false;
            panel1.Visible = true;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            panel6.Visible = false;
            panel7.Visible = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Start();
            Timestatus.Text = DateTime.Now.ToLongTimeString();
            Datestatus.Text = DateTime.Now.ToLongDateString();
        }

        private void Save_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(PatientNumber.Text) || String.IsNullOrEmpty(lastname.Text) || String.IsNullOrEmpty(firstname.Text) ||
                String.IsNullOrEmpty(age.Text) || String.IsNullOrEmpty(address.Text) || String.IsNullOrEmpty(city.Text) ||
                String.IsNullOrEmpty(contact.Text) || String.IsNullOrEmpty(telephone.Text) || String.IsNullOrEmpty(gname.Text) || 
                String.IsNullOrEmpty(gnumber.Text) || String.IsNullOrEmpty(email.Text) || String.IsNullOrEmpty(diagnosed.Text) ||
                String.IsNullOrEmpty(allergie.Text) || String.IsNullOrEmpty(middlename.Text))
            {
                MessageBox.Show("Please Fill up all the form");
            }
            else
            {
                generator1();
                register();
            }

            

        } 

        private void Clear_Click(object sender, EventArgs e)
        {
            PatientNumber.Clear();
            lastname.Clear();
            firstname.Clear();
            middlename.Clear();
            age.Clear();
            address.Clear();
            city.Clear();
            contact.Clear();
            telephone.Clear();
            gname.Clear();
            gnumber.Clear();
            diagnosed.Clear();
            email.Clear();
            allergie.Clear();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure to Cancel ?", "Cancel !!", MessageBoxButtons.YesNo);

            switch (dr)
            {
                case DialogResult.Yes:
                    panel2.Visible = false;
                    break;
                case DialogResult.No:
                    break;
            }
        }

        

        private void PatientAdmission_Click(object sender, EventArgs e)
        {
            panel8.Visible = false;
            panel1.Visible = false;
            panel2.Visible = true;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            panel6.Visible = false;
            panel7.Visible = false;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            panel7.Visible = false;
            panel6.Visible = false;
            panel5.Visible = false;
            panel4.Visible = false;
            panel3.Visible = false;
            panel2.Visible = false;
            panel1.Visible = true;
        }


        
        private void textBox1_TextChanged(object sender, EventArgs e)
        {


        }






        private void PatientNumber_TextChanged(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
        }
        private void contact_TextChanged(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
        }
        private void telephone_TextChanged(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
        }
        private void gnumber_TextChanged(object sender, KeyPressEventArgs e)
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
        private void age_TextChanged(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
        }
        private void address_TextChanged(object sender, KeyPressEventArgs e)
        {
            
        }
        private void city_TextChanged(object sender, KeyPressEventArgs e)
        {
            
        }
        private void email_TextChanged(object sender, KeyPressEventArgs e)
        {
            
        }
        private void gname_TextChanged(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsLetter(e.KeyChar) || e.KeyChar == 8 ? false : true;
        }
        private void diagnosed_TextChanged(object sender, KeyPressEventArgs e)
        {
           
        }
        private void allergie_TextChanged(object sender, KeyPressEventArgs e)
        {
          
        }
        private void admitnumber_TextChanged(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
        }
        private void dateofissue_TextChanged(object sender, EventArgs e)
        {
            timer1.Start();
            dateofissue.Text = DateTime.Now.ToLongDateString();
        }
        private void medicinenumber_TextChanged(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
        }
        private void qty_TextChanged(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
        }
        private void mlastname_TextChanged(object sender, EventArgs e)
        {
            medidvalidation();
        }
        private void mfirstname_TextChanged(object sender, EventArgs e)
        {
            medidvalidation();
        }
        private void mmiddlename_TextChanged(object sender, EventArgs e)
        {
            medidvalidation();
        }
        private void mtreatmentid_TextChanged(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
        }
        private void textBox2_TextChanged(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
        }
        private void textBox1_TextChanged(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
        }
        private void umsearch_TextChanged(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
        }
        private void ummedicinenumber_TextChanged(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
        }
        private void ssearch_TextChanged(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
        }
        private void sserviceid_TextChanged(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
        }
        private void streatmentpatientsearch_TextChanged(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
        }
        private void usserviceid_TextChange(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || e.KeyChar == 8 ? false : true;
        }
        private void ufirstname_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsLetter(e.KeyChar) || e.KeyChar == 8 ? false : true;
        }








        private void searchpatient_TextChanged(object sender, EventArgs e)
        {
            admitsearch();
        }
        private void Select_Click(object sender, EventArgs e)
        {
            apatientnumber.Text = dataGridView1.CurrentRow.Cells[15].Value.ToString();
            alastname.Text = dataGridView1.CurrentRow.Cells[13].Value.ToString();
            afirstname.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
            amiddlename.Text = dataGridView1.CurrentRow.Cells[14].Value.ToString();
           
        }

        private void AddMedicalTreatment_Click(object sender, EventArgs e)
        {
            panel8.Visible = false;
            panel7.Visible = false;
            panel6.Visible = false;
            panel5.Visible = false;
            panel4.Visible = false;
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = true;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure to Cancel ?", "Cancel !!", MessageBoxButtons.YesNo);

            switch (dr)
            {
                case DialogResult.Yes:
                    panel7.Visible = false;
                    panel6.Visible = false;
                    panel5.Visible = false;
                    panel2.Visible = false;
                    panel1.Visible = false;
                    panel3.Visible = false;
                    panel4.Visible = false;
                    break;
                case DialogResult.No:
                    break;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(apatientnumber.Text) || String.IsNullOrEmpty(admitnumber.Text) || String.IsNullOrEmpty(alastname.Text) ||
                String.IsNullOrEmpty(afirstname.Text) || String.IsNullOrEmpty(amiddlename.Text) || String.IsNullOrEmpty(adoctor.Text) ||
                String.IsNullOrEmpty(aroom.Text) || String.IsNullOrEmpty(anote.Text))
            {
                MessageBox.Show("Please Fill It Up");
            }
            else
            {
                generator();
                admitpatient();
            }
            
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(mtreatmentid.Text) || String.IsNullOrEmpty(mlastname.Text) || String.IsNullOrEmpty(mfirstname.Text) ||
                String.IsNullOrEmpty(medicinenumber.Text) || String.IsNullOrEmpty(medicinename.Text) || String.IsNullOrEmpty(dateofissue.Text) ||
                String.IsNullOrEmpty(qty.Text) || String.IsNullOrEmpty(unitprice.Text) || String.IsNullOrEmpty(total.Text) ||
                String.IsNullOrEmpty(nettotal.Text) || String.IsNullOrEmpty(mmiddlename.Text))
            {
                MessageBox.Show("Please Fill up all the form");
            }
            else
            {

                DialogResult dr = MessageBox.Show("Add another Medicine Treatment ?", "Cancel !!", MessageBoxButtons.YesNo);

                switch (dr)
                {
                    case DialogResult.Yes:
                        panel3.Visible = true;
                        medicine_treatment();
                        mtreatmentid.Clear();
                        mlastname.Clear();
                        mfirstname.Clear();
                        mmiddlename.Clear();
                        medicinenumber.Clear();
                        medicinename.Clear();
                        qty.Text = "0";
                        unitprice.Text = "0";
                        total.Text = "0";
                        nettotal.Text = "0";
                        break;
                    case DialogResult.No:
                        medicine_treatment();
                        mtreatmentid.Clear();
                        mlastname.Clear();
                        mfirstname.Clear();
                        mmiddlename.Clear();
                        medicinenumber.Clear();
                        medicinename.Clear();
                        qty.Text = "0";
                        unitprice.Text = "0";
                        total.Text = "0";
                        nettotal.Text = "0";
                        break;
                }
            }


            
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(msearch.Text))
            {

            }
            else
            {
                inpatientmedicalsearch();
            }
            
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(medicinenumber.Text))
            {
                MessageBox.Show("Please inpute Medicine Number ..");
            }
            else
            {
                medicine_maintenance();
            }
            
        }

        


        private void qty_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(qty.Text))
            {
                qty.Text = "0";
            }
            else
            {
                Double mqty;
                Double munitprice;
                

                mqty = Double.Parse(qty.Text);
                munitprice = Double.Parse(unitprice.Text);

                atotal = mqty * munitprice;
                anettotal = mqty * munitprice;

                unitprice.Text = System.Convert.ToString(munitprice);
                qty.Text = System.Convert.ToString(mqty);
                total.Text = System.Convert.ToString(atotal);
                nettotal.Text = System.Convert.ToString(anettotal);
            }
            

        }

        private void label36_Click(object sender, EventArgs e)
        {

        }

        private void unitprice_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(unitprice.Text))
            {
                unitprice.Text = "0";
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            mpatientnumber.Text = "0";
            mtreatmentid.Text = "0";
            mlastname.Clear();
            mfirstname.Clear();
            mmiddlename.Clear();
            medicinenumber.Text = "0";
            medicinename.Clear();
            qty.Text = "0";
            unitprice.Text = "0";
            total.Text = "0";
            nettotal.Text = "0";
        }

        private void button20_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure to Cancel ?", "Cancel !!", MessageBoxButtons.YesNo);

            switch (dr)
            {
                case DialogResult.Yes:
                    panel5.Visible = false;
                    panel2.Visible = false;
                    panel1.Visible = false;
                    panel3.Visible = false;
                    panel4.Visible = false;
                    panel6.Visible = false;
                    panel7.Visible = false;
                    break;
                case DialogResult.No:
                    break;
            }
        }

        private void EditMedicalTreatment_Click(object sender, EventArgs e)
        {
            panel8.Visible = false;
            panel7.Visible = false;
            panel5.Visible = false;
            panel2.Visible = false;
            panel1.Visible = false;
            panel3.Visible = false;
            panel4.Visible = true;
            panel6.Visible = false;
        }

        private void button17_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox2.Text))
            {

            }
            else
            {
                admit_doctorid_search();
            }
            
        }

        private void button18_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox2.Text))
            {

            }
            else
            {
                admit_roomid_searh();
            }
        }

        private void umsearch_TextChanged(object sender, EventArgs e)
        {
            medicine_treatment_search_update();
        }

        private void button23_Click(object sender, EventArgs e)
        {
            umpatientnumber.Text = mtreatmentload.CurrentRow.Cells[0].Value.ToString();
            umlastname.Text = mtreatmentload.CurrentRow.Cells[2].Value.ToString();
            umfirstname.Text = mtreatmentload.CurrentRow.Cells[3].Value.ToString();
            ummiddlename.Text = mtreatmentload.CurrentRow.Cells[4].Value.ToString();
            umtreatmentid.Text = mtreatmentload.CurrentRow.Cells[1].Value.ToString();
            ummedicinenumber.Text = mtreatmentload.CurrentRow.Cells[5].Value.ToString();
            ummedicinename.Text = mtreatmentload.CurrentRow.Cells[6].Value.ToString();
            umdateofissue.Text = mtreatmentload.CurrentRow.Cells[7].Value.ToString();
            umunitprice.Text = mtreatmentload.CurrentRow.Cells[8].Value.ToString();
            umqty.Text = mtreatmentload.CurrentRow.Cells[9].Value.ToString();

        }

        private void button22_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(umtreatmentid.Text))
            {

            }
            else
            {
                medicine_treatment1();
            }
        }

        private void umqty_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(umqty.Text))
            {
                umqty.Text = "0";
            }
            else
            {
                Double mqty1;
                Double munitprice1;


                mqty1 = Double.Parse(umqty.Text);
                munitprice1 = Double.Parse(umunitprice.Text);

                atotal1 = mqty1 * munitprice1;
                anettotal1 = mqty1 * munitprice1;

                umunitprice.Text = System.Convert.ToString(munitprice1);
                umqty.Text = System.Convert.ToString(mqty1);
                umtotal.Text = System.Convert.ToString(atotal1);
                umnettotal.Text = System.Convert.ToString(anettotal1);
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            umtreatmentid.Clear();
            umlastname.Clear();
            umfirstname.Clear();
            ummiddlename.Clear();
            ummedicinenumber.Clear();
            ummedicinename.Clear();
            umqty.Text = "0";
            umunitprice.Text = "0";
            umtotal.Text = "0";
            umnettotal.Text = "0";
        }

        private void button21_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Update Medical Treatment Now?", "Updating Record", MessageBoxButtons.YesNo);
            switch (dr)
            {
                case DialogResult.Yes:
                    medicine_maintenance_update();
                    break;
                case DialogResult.No:
                    break;
            }
        }

        private void AddServiceTreatment_Click(object sender, EventArgs e)
        {
            panel8.Visible = false;
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = true;
            panel6.Visible = false;
            panel7.Visible = false;
        }

        private void button26_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure to Cancel ?", "Cancel !!", MessageBoxButtons.YesNo);

            switch (dr)
            {
                case DialogResult.Yes:
                    panel5.Visible = false;
                    break;
                case DialogResult.No:
                    break;
            }
        }

        private void button24_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(ssearch.Text))
            {

            }
            else
            {
                service_treatment_searchpatient();
            }
        }

        private void button28_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(sserviceid.Text))
            {

            }
            else
            {
                service_maintenance_search();
            }
        }

        private void button27_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(streatmentid.Text) || String.IsNullOrEmpty(slastname.Text) || String.IsNullOrEmpty(sfirstname.Text) ||
                String.IsNullOrEmpty(sservicecharge.Text) || String.IsNullOrEmpty(sserviceid.Text) || String.IsNullOrEmpty(sservicename.Text) ||
                String.IsNullOrEmpty(smiddlename.Text))
            {
                MessageBox.Show("Please Fill up all the form");
            }
            else
            {

                DialogResult dr = MessageBox.Show("Add another Service Treatment ?", "Service Treatment !!", MessageBoxButtons.YesNo);

                switch (dr)
                {
                    case DialogResult.Yes:
                        panel5.Visible = true;
                        service_treatment();
                        streatmentid.Clear();
                        slastname.Clear();
                        sfirstname.Clear();
                        smiddlename.Clear();
                        sserviceid.Clear();
                        sservicename.Clear();
                        sservicecharge.Clear();
                        break;
                    case DialogResult.No:
                        service_treatment();
                        streatmentid.Clear();
                        slastname.Clear();
                        sfirstname.Clear();
                        smiddlename.Clear();
                        sserviceid.Clear();
                        sservicename.Clear();
                        sservicecharge.Clear();
                        panel5.Visible = false;
                        break;
                }
            }
            
        }

        private void button25_Click(object sender, EventArgs e)
        {
            spatientnumber.Text = "0";
            streatmentid.Clear();
            slastname.Clear();
            sfirstname.Clear();
            smiddlename.Clear();
            sserviceid.Clear();
            sservicename.Clear();
            sservicecharge.Text = "0";
        }

        private void OutPatientEditServiceTreatment_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            panel6.Visible = true;
            panel7.Visible = false;
        }

        private void streatmentpatientsearch_TextChanged(object sender, EventArgs e)
        {
            service_treatment_searchpatient_update();
        }

        private void button33_Click(object sender, EventArgs e)
        {
            uspatientnumber.Text = servicetreatmentupdate.CurrentRow.Cells[0].Value.ToString();
            ustreatmentid.Text = servicetreatmentupdate.CurrentRow.Cells[1].Value.ToString();
            uslastname.Text = servicetreatmentupdate.CurrentRow.Cells[2].Value.ToString();
            usfirstname.Text = servicetreatmentupdate.CurrentRow.Cells[3].Value.ToString();
            usmiddlename.Text = servicetreatmentupdate.CurrentRow.Cells[4].Value.ToString();
            usserviceid.Text = servicetreatmentupdate.CurrentRow.Cells[5].Value.ToString();
            usservicename.Text = servicetreatmentupdate.CurrentRow.Cells[6].Value.ToString();
            usservicecharge.Text = servicetreatmentupdate.CurrentRow.Cells[8].Value.ToString();

        }

        private void button29_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(usserviceid.Text))
            {

            }
            else
            {
                service_id_search();
            }

        }

        private void EditServiceTreatment_Click(object sender, EventArgs e)
        {
            panel8.Visible = false;
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            panel6.Visible = true;
            panel7.Visible = false;

        }

        private void button31_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure to Cancel ?", "Cancel !!", MessageBoxButtons.YesNo);

            switch (dr)
            {
                case DialogResult.Yes:
                    panel6.Visible = false;
                    break;
                case DialogResult.No:
                    break;
            }
        }

        private void button30_Click(object sender, EventArgs e)
        {
            uspatientnumber.Clear();
            uslastname.Clear();
            usfirstname.Clear();
            usmiddlename.Clear();
            ustreatmentid.Clear();
            usserviceid.Clear();
            usservicecharge.Clear();
            usservicename.Clear();

        }

        private void button32_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(ustreatmentid.Text) || String.IsNullOrEmpty(uslastname.Text) || String.IsNullOrEmpty(usfirstname.Text) ||
                String.IsNullOrEmpty(usservicecharge.Text) || String.IsNullOrEmpty(usserviceid.Text) || String.IsNullOrEmpty(usservicename.Text) ||
                String.IsNullOrEmpty(usmiddlename.Text))
            {
                MessageBox.Show("Please Fill up all the form");
            }
            else
            {

                DialogResult dr = MessageBox.Show("Update Another Patient Service Treatment ?", "Service Treatment !!", MessageBoxButtons.YesNo);

                switch (dr)
                {
                    case DialogResult.Yes:
                        panel6.Visible = true;
                        service_treatment_update();
                        
                        break;
                    case DialogResult.No:
                        panel6.Visible = false;
                        service_treatment_update();
                         break;
                }
            }
        }

        private void InPatientBill_Click(object sender, EventArgs e)
        {
            panel8.Visible = false;
            InPatient_Billing ipb = new InPatient_Billing();
            ipb.Show();
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            panel6.Visible = false;
            panel7.Visible = false;
        }
        private void InPaymentSearch_Click(object sender, EventArgs e)
        {
            panel8.Visible = false;
            InPatient_Payment inp = new InPatient_Payment();
            inp.Show();
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            panel6.Visible = false;
            panel7.Visible = false;
        }
        private void button6_Click(object sender, EventArgs e)
        {
            panel8.Visible = false;
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            panel6.Visible = false;
            panel7.Visible = false;
            InPatient_Billing ipb = new InPatient_Billing();
            ipb.Show();
        }
        private void button7_Click(object sender, EventArgs e)
        {
            panel8.Visible = false;
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            panel6.Visible = false;
            panel7.Visible = false;
            InPatient_Payment inp = new InPatient_Payment();
            inp.Show();
        }
        private void InPatientDischarge_Click(object sender, EventArgs e)
        {
            panel8.Visible = false;
            InPatient_Discharge ipd = new InPatient_Discharge();
            ipd.Show();
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            panel6.Visible = false;
            panel7.Visible = false;
        }
        private void OutpatientSignUp_Click(object sender, EventArgs e)
        {
            panel8.Visible = false;
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            panel6.Visible = false;
            panel7.Visible = false;
            OutPatient_Treatment opt = new OutPatient_Treatment();
            opt.Show();
        }
        private void OutPatientBill_Click(object sender, EventArgs e)
        {
            panel8.Visible = false;
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            panel6.Visible = false;
            panel7.Visible = false;
            OutPatient_Billing opb = new OutPatient_Billing();
            opb.Show();
        }
        private void OutSearchPayment_Click(object sender, EventArgs e)
        {
            panel8.Visible = false;
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            panel6.Visible = false;
            panel7.Visible = false;
            OutPatient_Payment opp = new OutPatient_Payment();
            opp.Show();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            panel8.Visible = false;
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            panel6.Visible = false;
            panel7.Visible = false;
            OutPatient_Treatment opt = new OutPatient_Treatment();
            opt.Show();
        }
        private void Doctormaintenance_Click(object sender, EventArgs e)
        {
            panel8.Visible = false;
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            panel6.Visible = false;
            panel7.Visible = false;
            Doctor_Maintenance dm = new Doctor_Maintenance();
            dm.Show();
        }
        private void typeofcare_Click(object sender, EventArgs e)
        {
            panel8.Visible = false;
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            panel6.Visible = false;
            panel7.Visible = false;
            Care_Maintenance cm = new Care_Maintenance();
            cm.Show();
        }
        private void medicinemaintenance_Click(object sender, EventArgs e)
        {
            panel8.Visible = false;
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            panel6.Visible = false;
            panel7.Visible = false;
            Medicine_Maintenance mm = new Medicine_Maintenance();
            mm.Show();
        }
        private void RoomMaintenance_Click(object sender, EventArgs e)
        {
            panel8.Visible = false;
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            panel6.Visible = false;
            panel7.Visible = false;
            Room_Maintenance rm = new Room_Maintenance();
            rm.Show();
        }
        private void servicemaintenance_Click(object sender, EventArgs e)
        {
            panel8.Visible = false;
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            panel6.Visible = false;
            panel7.Visible = false;
            Service_Maintenance sm = new Service_Maintenance();
            sm.Show();
        }




        







        private void inpatientrecords_Click(object sender, EventArgs e)
        {
            panel8.Visible = false;
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            panel6.Visible = false;
            panel7.Visible = true;
            inpatient_records.Visible = true;
        }

        private void outpatientsrecords_Click(object sender, EventArgs e)
        {
            panel8.Visible = false;
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            panel6.Visible = false;
            panel7.Visible = true;
            inpatient_records.Visible = false;
        }

        public void patients1()
        {
            MySqlConnection connection = new MySqlConnection(MyConnectionString);
            connection.Open();
            try
            {
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM `new_patient`";
                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adap.Fill(ds);
                Patients.DataSource = ds.Tables[0].DefaultView;
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
        public void patients_search()
        {
            MySqlConnection connection = new MySqlConnection(MyConnectionString);
            connection.Open();
            try
            {
                MySqlCommand cmd = connection.CreateCommand();

                string value = searchby.Text;
                switch (value)
                {
                    case "PatientNumber":
                        cmd.CommandText = "Select * from `new_patient` where PatientNumber LIKE @searchKey";
                        break;

                    case "lastname":
                        cmd.CommandText = "Select * from `new_patient` where lastname LIKE @searchKey";
                        break;

                    case "firstname":
                        cmd.CommandText = "Select * from `new_patient` where firstname LIKE @searchKey";
                        break;

                    case "middlename":
                        cmd.CommandText = "Select * from `new_patient` where middlename LIKE @searchKey";
                        break;

                    case "":
                        cmd.CommandText = "Select * from `new_patient`";
                        search_records.SelectionStart = 0;
                        search_records.SelectionLength = search_records.Text.Length;
                        break;
                }
                cmd.Parameters.AddWithValue("@searchKey", "%" + search_records.Text.ToString() + "%");
                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adap.Fill(ds);
                Patients.DataSource = ds.Tables[0].DefaultView;
            }
            catch (Exception)
            {
            }
            finally
            {

            }
        }

        public void admit1()
        {
            MySqlConnection connection = new MySqlConnection(MyConnectionString);
            connection.Open();
            try
            {
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM `inpatients_admissions";
                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adap.Fill(ds);
                admit.DataSource = ds.Tables[0].DefaultView;
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
        public void admit1_search()
        {
            MySqlConnection connection = new MySqlConnection(MyConnectionString);
            connection.Open();
            try
            {
                MySqlCommand cmd = connection.CreateCommand();

                string value = searchby.Text;
                switch (value)
                {
                    case "PatientNumber":
                        cmd.CommandText = "Select * from `inpatients_admissions` where PatientNumber LIKE @searchKey";
                        break;

                    case "lastname":
                        cmd.CommandText = "Select * from `inpatients_admissions` where lastname LIKE @searchKey";
                        break;

                    case "firstname":
                        cmd.CommandText = "Select * from `inpatients_admissions` where firstname LIKE @searchKey";
                        break;

                    case "middlename":
                        cmd.CommandText = "Select * from `inpatients_admissions` where middlename LIKE @searchKey";
                        break;

                    case "":
                        cmd.CommandText = "Select * from `inpatients_admissions`";
                        search_records.SelectionStart = 0;
                        search_records.SelectionLength = search_records.Text.Length;
                        break;
                }
                cmd.Parameters.AddWithValue("@searchKey", "%" + search_records.Text.ToString() + "%");
                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adap.Fill(ds);
                admit.DataSource = ds.Tables[0].DefaultView;
            }
            catch (Exception)
            {
            }
            finally
            {

            }
        }

        public void discharge1()
        {
            MySqlConnection connection = new MySqlConnection(MyConnectionString);
            connection.Open();
            try
            {
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM `inpatient_discharge`";
                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adap.Fill(ds);
                discharge.DataSource = ds.Tables[0].DefaultView;
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
        public void discharge123_search()
        {
            MySqlConnection connection = new MySqlConnection(MyConnectionString);
            connection.Open();
            try
            {
                MySqlCommand cmd = connection.CreateCommand();

                string value = searchby.Text;
                switch (value)
                {
                    case "PatientNumber":
                        cmd.CommandText = "Select * from `inpatient_discharge` where PatientNumber LIKE @searchKey";
                        break;

                    case "lastname":
                        cmd.CommandText = "Select * from `inpatient_discharges` where lastname LIKE @searchKey";
                        break;

                    case "firstname":
                        cmd.CommandText = "Select * from `inpatient_discharge` where firstname LIKE @searchKey";
                        break;

                    case "middlename":
                        cmd.CommandText = "Select * from `inpatient_discharge` where middlename LIKE @searchKey";
                        break;

                    case "":
                        cmd.CommandText = "Select * from `inpatient_discharge`";
                        search_records.SelectionStart = 0;
                        search_records.SelectionLength = search_records.Text.Length;
                        break;
                }
                cmd.Parameters.AddWithValue("@searchKey", "%" + search_records.Text.ToString() + "%");
                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adap.Fill(ds);
                discharge.DataSource = ds.Tables[0].DefaultView;
            }
            catch (Exception)
            {
            }
            finally
            {

            }
        }

        public void medical_treatment3()
        {
            MySqlConnection connection = new MySqlConnection(MyConnectionString);
            connection.Open();
            try
            {
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM `medical_treatment`";
                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adap.Fill(ds);
                medicaltreatment3.DataSource = ds.Tables[0].DefaultView;
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
        public void medical_search3()
        {
            MySqlConnection connection = new MySqlConnection(MyConnectionString);
            connection.Open();
            try
            {
                MySqlCommand cmd = connection.CreateCommand();

                string value = searchby.Text;
                switch (value)
                {
                    case "PatientNumber":
                        cmd.CommandText = "Select * from `medical_treatment` where PatientNumber LIKE @searchKey";
                        break;

                    case "lastname":
                        cmd.CommandText = "Select * from `medical_treatment` where lastname LIKE @searchKey";
                        break;

                    case "firstname":
                        cmd.CommandText = "Select * from `medical_treatment` where firstname LIKE @searchKey";
                        break;

                    case "middlename":
                        cmd.CommandText = "Select * from `medical_treatment` where middlename LIKE @searchKey";
                        break;

                    case "":
                        cmd.CommandText = "Select * from `medical_treatment`";
                        search_records.SelectionStart = 0;
                        search_records.SelectionLength = search_records.Text.Length;
                        break;
                }
                cmd.Parameters.AddWithValue("@searchKey", "%" + search_records.Text.ToString() + "%");
                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adap.Fill(ds);
                medicaltreatment3.DataSource = ds.Tables[0].DefaultView;
            }
            catch (Exception)
            {
            }
            finally
            {

            }
        }

        public void service_treatment3()
        {
            MySqlConnection connection = new MySqlConnection(MyConnectionString);
            connection.Open();
            try
            {
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM `service_treatment` ";
                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adap.Fill(ds);
                servicetreatment.DataSource = ds.Tables[0].DefaultView;
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
        public void service_search3()
        {
            MySqlConnection connection = new MySqlConnection(MyConnectionString);
            connection.Open();
            try
            {
                MySqlCommand cmd = connection.CreateCommand();

                string value = searchby.Text;
                switch (value)
                {
                    case "PatientNumber":
                        cmd.CommandText = "Select * from `service_treatment`  where PatientNumber LIKE @searchKey";
                        break;

                    case "lastname":
                        cmd.CommandText = "Select * from `service_treatment`  where lastname LIKE @searchKey";
                        break;

                    case "firstname":
                        cmd.CommandText = "Select * from `service_treatment`  where firstname LIKE @searchKey";
                        break;

                    case "middlename":
                        cmd.CommandText = "Select * from `service_treatment`  where middlename LIKE @searchKey";
                        break;

                    case "":
                        cmd.CommandText = "Select * from `service_treatment` ";
                        search_records.SelectionStart = 0;
                        search_records.SelectionLength = search_records.Text.Length;
                        break;
                }
                cmd.Parameters.AddWithValue("@searchKey", "%" + search_records.Text.ToString() + "%");
                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adap.Fill(ds);
                servicetreatment.DataSource = ds.Tables[0].DefaultView;
            }
            catch (Exception)
            {
            }
            finally
            {

            }
        }

        public void overall_bill()
        {
            MySqlConnection connection = new MySqlConnection(MyConnectionString);
            connection.Open();
            try
            {
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM `inpatient_billing`";
                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adap.Fill(ds);
                bill3.DataSource = ds.Tables[0].DefaultView;
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
        public void overall_search_bill()
        {
            MySqlConnection connection = new MySqlConnection(MyConnectionString);
            connection.Open();
            try
            {
                MySqlCommand cmd = connection.CreateCommand();

                string value = searchby.Text;
                switch (value)
                {
                    case "PatientNumber":
                        cmd.CommandText = "Select * from `inpatient_billing`  where PatientNumber LIKE @searchKey";
                        break;

                    case "lastname":
                        cmd.CommandText = "Select * from `inpatient_billing`  where lastname LIKE @searchKey";
                        break;

                    case "firstname":
                        cmd.CommandText = "Select * from `inpatient_billing`  where firstname LIKE @searchKey";
                        break;

                    case "middlename":
                        cmd.CommandText = "Select * from `inpatient_billing`  where middlename LIKE @searchKey";
                        break;

                    case "":
                        cmd.CommandText = "Select * from `inpatient_billing`";
                        search_records.SelectionStart = 0;
                        search_records.SelectionLength = search_records.Text.Length;
                        break;
                }
                cmd.Parameters.AddWithValue("@searchKey", "%" + search_records.Text.ToString() + "%");
                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adap.Fill(ds);
                bill3.DataSource = ds.Tables[0].DefaultView;
            }
            catch (Exception)
            {
            }
            finally
            {

            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (criteria.Text == "Patients")
            {
                Patients.Visible = true;
                patients1();
            }
            else
            {
                Patients.Visible = false;
            }

            if (criteria.Text == "Admit Patients")
            {
                admit.Visible = true;
                admit1();
            }
            else
            {
                admit.Visible = false;
            }
            if (criteria.Text == "Discharges")
            {
                discharge.Visible = true;
                discharge1();
            }
            else
            {
                discharge.Visible = false;
            }
            if (criteria.Text == "Medical Treatment")
            {
                medicaltreatment3.Visible = true;
                medical_treatment3();
            }
            else
            {
                medicaltreatment3.Visible = false;
            }
            if (criteria.Text == "Service Treatment")
            {
                servicetreatment.Visible = true;
                service_treatment3();
            }
            else
            {
                servicetreatment.Visible = false;
            }
            if (criteria.Text == "Overall Bills")
            {
                bill3.Visible = true;
                overall_bill();
            }
            else
            {
                bill3.Visible = false;
            }





        }

        private void search_records_TextChanged(object sender, EventArgs e)
        {
            overall_search_bill();
            service_search3();
            medical_search3();
            discharge123_search();
            admit1_search();
            patients_search();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel8.Visible = false;
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            panel6.Visible = false;
            panel7.Visible = true;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            outpatientrecords.Visible = false;
            inpatient_records.Visible = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            outpatientrecords.Visible = true;
            inpatient_records.Visible = false;
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 f1 = new Form1();
            f1.Show();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ManageAppointments_Click(object sender, EventArgs e)
        {
            panel8.Visible = false;
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            panel6.Visible = false;
            panel7.Visible = false;
            Appointments a = new Appointments();
            a.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel8.Visible = false;
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            panel6.Visible = false;
            panel7.Visible = false;
            Appointments a = new Appointments();
            a.Show();
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text == "Out Patients")
            {
                outpatients.Visible = true;
            }
            else
            {
                outpatients.Visible = false;
            }
            if (comboBox2.Text == "OverAll Bill")
            {
                overallbill.Visible = true;
            }
            else
            {
                overallbill.Visible = false;
            }
            
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text == "Out Patients")
            {
                outpatientsrecords3();
            }
            else
            {
                outpatients.Visible = false;
            }
            if (comboBox2.Text == "OverAll Bill")
            {
                outpatientsrecordsoverallbill();
            }
            else
            {
                overallbill.Visible = false;
            }

        }

        private void ChangePass_Click(object sender, EventArgs e)
        {
            registeraccount.Visible = false;
            changepassword.Visible = true;
            panel8.Visible = true;
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            panel6.Visible = false;
            panel7.Visible = false;
        }

        private void button35_Click(object sender, EventArgs e)
        {
            panel8.Visible = false;
        }

        private void button34_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure you want to change your password ?", "Changing Password !!", MessageBoxButtons.YesNo);

            switch (dr)
            {
                case DialogResult.Yes:
                    changepassword1();
                    break;
                case DialogResult.No:
                    break;
            }
        }

        private void ManageUserAccount_Click(object sender, EventArgs e)
        {
            registeraccount.Visible = true;
            changepassword.Visible = false;
            panel8.Visible = true;
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            panel6.Visible = false;
            panel7.Visible = false;

        }

        public void register2()
        {
            MySqlConnection connection = new MySqlConnection(MyConnectionString);
            MySqlCommand cmd;
            connection.Open();
            try
            {
                cmd = connection.CreateCommand();
                cmd.CommandText = @"INSERT INTO `accounts`(`user_id`, `firstname`, `lastname`, `email`, `designation`, `auser`, `apass`) 
VALUES (@user_id, @firstname, @lastname, @email, @designation, @auser, @apass)";


                cmd.Parameters.AddWithValue("@user_id", id.Text.ToString());
                cmd.Parameters.AddWithValue("@lastname", ulastname.Text.ToString());
                cmd.Parameters.AddWithValue("@firstname", ufirstname.Text.ToString());
                cmd.Parameters.AddWithValue("@email", uemail.Text.ToString());
                cmd.Parameters.AddWithValue("@designation", udesignation.Text.ToString());
                cmd.Parameters.AddWithValue("@auser", uuser.Text.ToString());
                cmd.Parameters.AddWithValue("@apass", upass.Text.ToString());

                cmd.ExecuteNonQuery();
                MySqlDataAdapter sda2 = new MySqlDataAdapter(cmd);
                DataSet ds2 = new DataSet();
                sda2.Fill(ds2);
            }

            catch (Exception)
            {
                id.Clear();
                ufirstname.Clear();
                ulastname.Clear();
                uemail.Clear();
                upass.Clear();
                urpass.Clear();
                uuser.Clear();
            }
        }


        private void button36_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(uuser.Text) || String.IsNullOrEmpty(upass.Text) || String.IsNullOrEmpty(urpass.Text) ||
                String.IsNullOrEmpty(id.Text) || String.IsNullOrEmpty(ufirstname.Text) || String.IsNullOrEmpty(ulastname.Text) ||
                String.IsNullOrEmpty(udesignation.Text) || String.IsNullOrEmpty(uemail.Text))
            {
                MessageBox.Show("Please Fill up the form.");
            }
            else
            {
                if (upass.Text != urpass.Text || urpass.Text != upass.Text)
                {
                    MessageBox.Show("Password Doesn't Match");
                    upass.Clear();
                    urpass.Clear();
                }
                else
                {
                    register2();
                    MessageBox.Show("Register Account Success !!");
                }
            }
            
        }

        private void udesignation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (udesignation.Text == "Cashier")
            {
                uuser.Text = "Cashier";
            }
            if (udesignation.Text == "Inpatient")
            {
                uuser.Text = "Inpatient";
            }
            if (udesignation.Text == "Outpatient")
            {
                uuser.Text = "Outpatient";
            }
        }

        private void about_Click(object sender, EventArgs e)
        {
        }

        private void InPatientReport_Click(object sender, EventArgs e)
        {
            Form3 f = new Form3();
            f.Show();
        }

        private void OutPatientReport_Click(object sender, EventArgs e)
        {
            payment1 p1 = new payment1();
            p1.Show();
        }



        

        


        

        
        







       
    }
}
