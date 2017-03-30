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
    public partial class CandidateStandings : Form
    {
        MySqlCommand command;
        MySqlDataReader reader;
        String query;
        String connectionString;
        int e_id;

        public CandidateStandings(int id)
        {
            InitializeComponent();
            this.MaximizeBox = false;
            e_id = id;
            connectionString = "server=localhost;database=e_poller;userid = root;password = MSaa_1996";


            query = "select c_id as ID, name as Name, no_of_Votes as NumberOfVotes from count natural join candidate where e_id = " + e_id + " and admin_id is not null;";

            MySqlConnection connection = new MySqlConnection(connectionString);
            command = new MySqlCommand(query, connection);

            try
            {
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = command;
                DataTable dbdataset = new DataTable();
                sda.Fill(dbdataset);
                BindingSource bSource = new BindingSource();

                bSource.DataSource = dbdataset;
                dataGridView1.DataSource = bSource;
                sda.Update(dbdataset);
            }
            catch (Exception ex)
            { MessageBox.Show("The page seems to have issues", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
                   

        private void CandidateStandings_Load(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            query = "select distinct(name) as n from election where e_id = " + e_id + ";";
            command = new MySqlCommand(query, conn);
            conn.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            { labelElection.Text = reader.GetString("n"); }

        }

        private void linklabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }
    }
}
