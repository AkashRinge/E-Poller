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
using MySql.Data;

namespace EPoller
{
    public partial class CandidateRegistration : Form
    {
        MySqlCommand command;
        MySqlDataReader reader;
        String query;
        String connectionString;

        public CandidateRegistration()
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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CandidateRegistration_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            String election;
            String name = txtName.Text;
            int age = Int32.Parse(comboAge.Text);
            int count = 0;

            //Empty constraints
            if (comboename.SelectedItem != null)
                election = comboename.Text;
            else
            {
                MessageBox.Show("Election is a mandatory field");
                return;
            }
            Char sex;

            if (name.Length == 0 || (radioF.Checked == false && radioM.Checked == false))
            {
                MessageBox.Show("One or more mandatory fields have been left empty. Please follow the instructions");
                return;
            }
            
            //Gender allocation
            if (radioF.Checked)
                sex = 'F';
            else
                sex = 'M';
           
            //Candidate ID generation
            int flag = 1;
            int randomNumber=1;
            Random random;

            while (flag > 0)
            {
                random = new Random();
                randomNumber = random.Next(1, 1000);

                query = "select count(c_id) as total from candidate where c_id = " + randomNumber + ";";
                command = new MySqlCommand(query, conn);
                conn.Open();
                reader = command.ExecuteReader();
                while (reader.Read())
                {flag = reader.GetInt32("total");}
                conn.Close();
            }
            int c_id = randomNumber;

            //To fix an election ID
            int e_id = 0;
            query = "select distinct(e_id) as ID from election where name = '" + election + "';";
            command = new MySqlCommand(query, conn);
            conn.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
                e_id = reader.GetInt32("ID");
            conn.Close();
            
            //Insertion query
            try
            {
                query = "insert into candidate values (" + c_id + ", '" + name + "', '" + sex + "', " + age + ", null, " + e_id + ");";
                command = new MySqlCommand(query, conn);
                conn.Open();
                reader = command.ExecuteReader();
                while (reader.Read())
                { }
                MessageBox.Show("Registration successful. Your candidate ID has been generated: " + c_id + " Please wait till an administrator verifies your registration");
                conn.Close();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("The page seems to have issues", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
