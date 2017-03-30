using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using MySql.Data.Common;

namespace EPoller
{

    public partial class VoterRegistration : Form
    {
        MySqlCommand command;
        MySqlDataReader reader;
        String query;
        String connectionString;

        public VoterRegistration()
        {
            InitializeComponent();
            this.MaximizeBox = false;

            connectionString = "server=localhost;database=e_poller;userid = root;password = MSaa_1996";


            String electionName;
            MySqlConnection conn = new MySqlConnection(connectionString);
            command = new MySqlCommand("select name as ElectionName from election;", conn);
            try
            {
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = command;
                DataTable data = new DataTable();
                sda.Fill(data);

                foreach (DataRow dr in data.Rows)
                {
                    electionName = dr["ElectionName"].ToString();
                    comboename.Items.Add(electionName);
                }
            }
            catch (Exception ex)
            { MessageBox.Show("The page seems to have issues", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
          
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String electionName;
            String uniqueID = txtv_id.Text;
            String name = txtname.Text;
            String password = txtpassword.Text;
            String retypepass = txtpassword2.Text;
            if (comboename.SelectedItem != null)
                electionName = comboename.SelectedItem.ToString();
            else
                {MessageBox.Show("One or more required fields have been left empty. Please follow the instructions"); return;}
            
            int age = Int32.Parse(comboage.Text);
            Char sex;

            //Empty fields
            if (uniqueID.Length == 0 || name.Length == 0 || password.Length == 0 || retypepass.Length == 0 || electionName.Length == 0 || (radioF.Checked == false && radioM.Checked == false))
            {
                MessageBox.Show("One or more required fields have been left empty. Please follow the instructions");
                return;
            }

            //Pasword mismatch
            if(password.Equals(retypepass)==false)
            {
                MessageBox.Show("Password mismatch. Please follow the instructions");
                return;
            }

            //Set Gender
            if (radioM.Checked == true)
                sex = 'M';
            else
                sex = 'F';

            
            //To check if ID entered is unique or not
            int count = 0;
            MySqlConnection conn = new MySqlConnection(connectionString);
            query = "select count(v_id) AS total from voter where v_id = " + uniqueID + ";";
            command = new MySqlCommand(query, conn);
            conn.Open();
            reader = command.ExecuteReader();
            while(reader.Read())
                count = reader.GetInt32("total");
            conn.Close();

            if(count>0)
            {
                MessageBox.Show("This unique ID already exists, please enter a different ID");
                txtv_id.Text = "";
                return;
            }

            //To fix an election ID
            int e_id = 0;
            query = "select distinct(e_id) as ID from election where name = '" + electionName + "';";
            command = new MySqlCommand(query, conn);
            conn.Open();
            reader = command.ExecuteReader();
            while(reader.Read())
                e_id = reader.GetInt32("ID");
            conn.Close();

            //To insert a new voter into database
            try
            {
                query = "insert into voter values ('" + uniqueID + "', '" + password + "', '" + name + "', '" + sex + "', " + age + ", null, " + e_id + ");";
                command = new MySqlCommand(query, conn);
                conn.Open();
                reader = command.ExecuteReader();
                while(reader.Read())
                {}
                MessageBox.Show("Registration successful. Please wait till an administrator verifies your registration");
                conn.Close();
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("The page seems to have issues", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void VoterRegistration_Load(object sender, EventArgs e)
        {
          
        }
    }
}
