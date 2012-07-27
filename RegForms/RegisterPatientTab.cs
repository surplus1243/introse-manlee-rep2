﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using introseHHC.Objects;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

namespace introseHHC.RegForms
{
    public partial class RegisterPatientTab : Form
    {
        private MySqlConnection conn;
        private MySqlCommand cmd;
        private MySqlDataReader read;
        private string server;
        private string database;
        private string user;
        private string password;

        private const byte PATIENT_TAB = 0;
        private const byte CLIENT_TAB =  1;
        private const byte REQUIREMENTS_TAB = 2;
        private const byte DETAILS_TAB = 3;

        private Patient patient;
        private Client  client;
        private FaceSheet fsheet;

        private string desig;
        private string fname;
        private string sname;
        private string mname;
        private string gender;
        private string nationality;
        private string religion;
        private string civstat;
        private string educattain;
        private string email;
        //holders for birthdate fields
        private DateTime birthdate;
        //holders for address fields
        private string addline;
        private UInt16 stnumber;
        private string region;
        private string city;
        //holders for contact number fields
        private UInt16 homeNum;
        private UInt16 workNum;
        private UInt16 mobNum;
        private UInt16 otherNum;
        
        public RegisterPatientTab()
        {
            InitializeComponent();

            this.tabPage1.Text = "Register Patient";
            this.tabPage2.Text = "Register Client";
            this.tabPage3.Text = "Requirements";
            this.tabPage4.Text = "Details";

            patient = new Patient();
            client = new Client();
            fsheet = new FaceSheet();

            server = "localhost";
            user = "root";
            database = "hhc-db";
            password = "root";

            string connString = "SERVER=" + server + ";" + "DATABASE=" +
                                database + ";" + "UID=" + user + ";" + 
                                "PASSWORD=" + password + ";";

            conn = new MySqlConnection(connString);
                     
          }


        private bool OpenConnection()
        {
            try
            {
                conn.Open();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        private bool CloseConnection()
        {
            try
            {
                conn.Close();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        private int checkName(string s, string f, string m)
        {
            

            return 0;
        }

        private void RegisterPerson(Person p)
        {
            if (OpenConnection())
            {
                string query = "INSERT INTO PERSON(DESIGNATION,SNAME,FNAME,MNAME,BDATE,GENDER,CIVSTAT,NATIONALITY,RELIGION," +
                 "EDUCATTAIN,EMAIL,HOMENUM,WORKNUM,MOBNUM,OTHERNUM,STNUM,ADDLINE,CITY,REGION) VALUES "
                 + "(@desig,@sur,@first,@mid,@bday,@gen,@cstat,@nat,@rel,@edatt,@mail,@hnum,@wnum,@mnum,@onum,@stno,@aline,@ct,@reg)";

                cmd = new MySqlCommand(query, conn);

                cmd.Prepare();

                cmd.Parameters.AddWithValue("@desig", patient.getDesig());
                cmd.Parameters.AddWithValue("@sur", patient.getSurname());
                cmd.Parameters.AddWithValue("@first", patient.getFirstName());
                cmd.Parameters.AddWithValue("@mid", patient.getMidName());
                cmd.Parameters.AddWithValue("@bday", patient.getBDay());
                cmd.Parameters.AddWithValue("@gen", patient.getGender());
                cmd.Parameters.AddWithValue("@cstat", patient.getCivilStatus());
                cmd.Parameters.AddWithValue("@nat", patient.getNationality());
                cmd.Parameters.AddWithValue("@rel", patient.getReligion());
                cmd.Parameters.AddWithValue("@edatt", patient.getEducAttainment());
                cmd.Parameters.AddWithValue("@mail", patient.getEmail());
                cmd.Parameters.AddWithValue("@hnum", patient.getHomeNum());
                cmd.Parameters.AddWithValue("@mnum", patient.getMobNum());
                cmd.Parameters.AddWithValue("@wnum", patient.getWorkNum());
                cmd.Parameters.AddWithValue("@onum", patient.getOtherNum());
                cmd.Parameters.AddWithValue("@stno", patient.getStNum());
                cmd.Parameters.AddWithValue("@aline", patient.getAddLine());
                cmd.Parameters.AddWithValue("@ct", patient.getCity());
                cmd.Parameters.AddWithValue("@reg", patient.getRegion());

                cmd.ExecuteNonQuery();

                CloseConnection();
            }
        }

        //save inputs to respective classes
        private void finishButton_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == PATIENT_TAB)
            {   
                
        
            }
            else if (tabControl1.SelectedIndex == CLIENT_TAB)
            {

            }
            else if (tabControl1.SelectedIndex == REQUIREMENTS_TAB)
            {
                //Requirements tab
            }
            else if (tabControl1.SelectedIndex == DETAILS_TAB)
            {
                //Details tab
            }
        }
        //runs when the Add button for Contact information is clicked.

        private void caseMgmtCB_CheckedChanged(object sender, EventArgs e)
        {
            if (caseMgmtCB.Checked == false)
            {
                caseMgmtBox.Enabled = false;
            }
            else
            {
                caseMgmtBox.Enabled = true;
            }

        }
        private void hvacCB_CheckedChanged(object sender, EventArgs e)
        {
            if (hvacCB.Checked == false)
            {
                hvacCoB.Enabled = false;
            }
            else
            {
                hvacCoB.Enabled = true;
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (primaryCB.Checked == true)
            {
                primaryIn.Enabled = false;
            }
            else
            {
                primaryIn.Enabled = true;
            }
        }

        private void acceptButton_Click(object sender, EventArgs e)
        {
           
        }

        private void RegisterPatientTab_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (CloseConnection())
                Console.WriteLine("SQL Connection Closed");

        }

        private void pNextButton_Click(object sender, EventArgs e)
        {

            //Patient Tab
            //get all the values in the fields & perform error checking
            //commands below execute only when the Patient Information Tab is selected.

            desig = pdesigCoB.Text;
            fname = pfnameIn.Text;
            sname = psnameIn.Text;
            mname = pmnameIn.Text;

            checkName(fname, sname, mname); //replace error checking with regular expressions.
            birthdate = pbdayPick.Value;             //Get data from DateTime Picker

            //following fields must not be blank
            try
            {
                gender = pgenCoB.Text;
                nationality = pnatIn.Text;
                religion = prelIn.Text;
                civstat = pcivStatCoB.Text;
                educattain = pedattCoB.Text;
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }

            //fields for address. must not be blank!
            addline = paddlineIn.Text;
            city = pcityIn.Text;
            region = pregIn.Text;

            try
            {
                stnumber = UInt16.Parse(pstnoIn.Text);
            }
            catch (FormatException err)
            {
                Console.WriteLine("Street #: " + err.Message);
            }
            catch (OverflowException of)
            {
                Console.WriteLine("Street #: " + of.Message);
            }

            try
            {
                workNum = UInt16.Parse(pWorkIn.Text);
                homeNum = UInt16.Parse(pHomeIn.Text);
                mobNum = UInt16.Parse(pMobileIn.Text);
                otherNum = UInt16.Parse(pOtherIn.Text);
            }
            catch (Exception err)
            {
            }
               


            //get email
            //needs regex based error checking
            email = pemailIn.Text;

            //add fields to Patient Object
            patient.setName(desig, fname, mname, sname);
            patient.setBday(birthdate);
            patient.setGender(gender);
            patient.setNationality(nationality);
            patient.setReligion(religion);
            patient.setCivilStatus(civstat);
            patient.setEducAttainment(educattain);
            patient.setEmail(email);
            patient.setAddress(stnumber, addline, city, region);
            patient.setNumbers(homeNum,workNum,mobNum,otherNum);
            
            //displayStuff(patient);

            //open connection to database
            if (true)
            {
                    RegisterPerson(patient);

                //insert into patient table

                OpenConnection();

                string query = "SELECT LAST_INSERT_ID() FROM PERSON;";
                cmd.CommandText = query;

                read = cmd.ExecuteReader();

                read.Read();

                Console.WriteLine(read.GetDecimal(0).ToString());

                UInt16 lastID = UInt16.Parse(read.GetDecimal(0).ToString());

                patient.setID(lastID);

                read.Close();

                query = "INSERT INTO PATIENT(PatID) VALUES(@pid);";

                cmd.CommandText = query;

                cmd.Prepare();

                cmd.Parameters.AddWithValue("@pid", lastID);

                cmd.ExecuteNonQuery();

                CloseConnection();

                tabControl1.SelectedIndex++;
               
            }
            else
            {

            }

            //move to next tab
            
        }
        private void cNextButton_Click(object sender, EventArgs e)
        {
            desig = cdesigCoB.Text;
            fname = cfnameIn.Text;
            sname = csnameIn.Text;
            mname = cmnameIn.Text;

            try
            {
                gender = cgenCoB.Text;
                nationality = cnatIn.Text;
                religion = crelIn.Text;
                civstat = ccivstatCoB.SelectedText;
                educattain = cedattCoB.Text;
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
            addline = caddIn.Text;
            city = ccityIn.Text;
            region = cregIn.Text;

            try
            {
                stnumber = UInt16.Parse(pstnoIn.Text);
            }
            catch (FormatException err)
            {
                Console.WriteLine("Street #: " + err.Message);
            }
            catch (OverflowException of)
            {
                Console.WriteLine("Street #: " + of.Message);
            }

            try
            {
                workNum = UInt16.Parse(cWorkIn.Text);
                homeNum = UInt16.Parse(cHomeIn.Text);
                mobNum = UInt16.Parse(cMobileIn.Text);
                otherNum = UInt16.Parse(cOtherIn.Text);
            }
            catch (Exception err)
            {
            }

            //get email
            //needs regex based error checking
            email = cemailIn.Text;

            client.setName(desig, fname, mname, sname);
            client.setBday(birthdate);
            client.setGender(gender);
            client.setNationality(nationality);
            client.setReligion(religion);
            client.setCivilStatus(civstat);
            client.setEducAttainment(educattain);
            client.setEmail(email);
            client.setAddress(stnumber, addline, city, region);
            client.setNumbers(homeNum, workNum, mobNum, otherNum);

            if (true)
            {
                RegisterPerson(client);

                OpenConnection();

                string query = "SELECT LAST_INSERT_ID() FROM PERSON;";
                cmd.CommandText = query;

                read = cmd.ExecuteReader();

                read.Read();

                Console.WriteLine(read.GetDecimal(0).ToString());

                UInt16 lastID = UInt16.Parse(read.GetDecimal(0).ToString());

                client.setID(lastID);

                read.Close();

                query = "INSERT INTO PATIENT(ClientID) VALUES(@cid);";

                cmd.CommandText = query;

                cmd.Prepare();

                cmd.Parameters.AddWithValue("@cid", lastID);

                cmd.ExecuteNonQuery();

                CloseConnection();

                tabControl1.SelectedIndex++;
            }
            else
            {
            }

            
        }
        private void rNextButton_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex++;
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == PATIENT_TAB)
            {

            }
        }

        private void RegisterPatientTab_Load(object sender, EventArgs e)
        {

        }


 


    }
}