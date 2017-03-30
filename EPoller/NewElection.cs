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
    public partial class NewElection : Form
    {
        String admin_id;
        String connectionString;
        MySqlConnection conn;

        MySqlCommand command;
        MySqlDataReader reader;
        String query;
        int e_id;

        public NewElection(String s)
        {
            InitializeComponent();
            this.MaximizeBox = false;
            admin_id = s;
            connectionString = "server=localhost;database=e_poller;userid = root;password = MSaa_1996";
            conn = new MySqlConnection(connectionString);

            query = "select max(e_id) as total from election;";
            command = new MySqlCommand(query, conn);
            conn.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            { e_id = reader.GetInt32("total"); }
            conn.Close();

            e_id++;
            labelID.Text = e_id + "";
        }

        private void NewElection_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Length == 0 || txtArea.Text.Length == 0)
            {
                MessageBox.Show("One or more required fields empty");
                return;
            }

            String name, area;
            name = txtName.Text;
            area = txtArea.Text;
            int count = 1;

            //To check if an election of the same name already exists
            query = "select count(e_id) as cunt from election where name like '" + name + "';";
            command = new MySqlCommand(query, conn);
            conn.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            { count = reader.GetInt32("cunt"); }
            conn.Close();

            if (count != 0)
            {
                MessageBox.Show("An election with a name similar to that of provided already exists. Please give a different name");
                return;
            }

            query = "insert into election values (" + e_id + ", '" + name + "', '" + area + "', '" + admin_id + "');";
            command = new MySqlCommand(query, conn);
            conn.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            { }
            conn.Close();

            MessageBox.Show("Election successfully created!");
            return;
        }

        private void linklabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }
    }
}
