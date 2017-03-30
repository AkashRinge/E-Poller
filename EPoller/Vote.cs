using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace EPoller
{
    public partial class Vote : Form
    {
        MySqlCommand command;
        MySqlDataReader reader;
        String query;
        String connectionString;
        String v_id;

        public Vote(String id)
        {
            InitializeComponent();
            this.MaximizeBox = false;
            
            connectionString = "server=localhost;database=e_poller;userid = root;password = MSaa_1996";
            v_id = id;

            MySqlConnection conn = new MySqlConnection(connectionString);
            command = new MySqlCommand("select name from Candidate where e_id in (select e_id from voter where v_id = '" + v_id + "') and admin_id is not null;", conn);
            try
            {
                String name;
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = command;
                DataTable data = new DataTable();
                sda.Fill(data);

                foreach (DataRow dr in data.Rows)
                {
                    name = dr["name"].ToString();
                    comboCandidate.Items.Add(name);
                }
            }
            catch (Exception ex)
            { MessageBox.Show("The page seems to have issues", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
          
        }
        

        private void Vote_Load(object sender, EventArgs e)
        {

        }

        private void comboCandidate_SelectedIndexChanged(object sender, EventArgs e)
        {
            String candidate = comboCandidate.Text;
            MySqlConnection conn = new MySqlConnection(connectionString);

            //ID
            query = "select c_id from candidate where name = '" + candidate + "';";
            command = new MySqlCommand(query, conn);

            conn.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            { labelID.Text = reader.GetString("c_id"); }
            conn.Close();


            //Name
            labelName.Text = candidate;


            //Sex
            query = "select sex from candidate where name = '" + candidate + "';";
            command = new MySqlCommand(query, conn);

            conn.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            { labelSex.Text = reader.GetString("sex"); }
            conn.Close();


            //Age
            query = "select age from candidate where name = '" + candidate + "';";
            command = new MySqlCommand(query, conn);

            conn.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            { labelAge.Text = reader.GetString("age"); }
            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            String candidate;
            int count = 1;
            
        
            //If a particular voter tries to vote more than once
            query = "select count(v_id) as total from votes where v_id = '" + v_id + "';";
            command = new MySqlCommand(query, conn);

            conn.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            { count = reader.GetInt32("total"); }
            conn.Close();

            if (count != 0)
            {
                MessageBox.Show("Each voter is only allowed to vote once");
                return;
            }




            //If no candidate selected
            if (comboCandidate.SelectedItem == null)
            {
                DialogResult result = MessageBox.Show("Are you sure you do not wish to participate in this election?", "Confirmation", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                {
                    query = "insert into votes values('" + v_id + "', null);";
                    command = new MySqlCommand(query, conn);
                    conn.Open();
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    { }
                    conn.Close();
                    this.Close();
                }
                else
                    return;
                return;
            }

            
            //Get candidate ID
            int c_id = 0;
            candidate = comboCandidate.Text;
            query = "select c_id from candidate where name = '" + candidate + "';";
            command = new MySqlCommand(query, conn);
            conn.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            {c_id = reader.GetInt32("c_id");}
            conn.Close();

            //Update database
            query = "insert into votes values('" + v_id + "', " + c_id + ");";
            command = new MySqlCommand(query, conn);
            conn.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            { }
            conn.Close();
            MessageBox.Show("You have successfully casted your vote");
            this.Close();
        }

        private void linklabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            int e_id = 0;
            query = "select e_id from voter where v_id = '" + v_id + "';";
            command = new MySqlCommand(query, conn);

            conn.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            { e_id = reader.GetInt32("e_id"); }
            conn.Close();

            this.Hide();
            new CandidateStandings(e_id).ShowDialog();
            this.Close();
        }
    }
}
