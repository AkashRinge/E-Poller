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
    public partial class Validation : Form
    {
        String admin_id;
        MySqlCommand command;
        MySqlDataReader reader;
        String query;
        String connectionString;
        MySqlConnection conn;

        public Validation(String id)
        {
            InitializeComponent();
            this.MaximizeBox = false;
            admin_id = id;

            connectionString = "server=localhost;database=e_poller;userid = root;password = MSaa_1996";
            conn = new MySqlConnection(connectionString);
            
            //To fill the voter ID comboBox
            command = new MySqlCommand("select v_id from voter where admin_id is null;", conn);
            try
            {
                String voterName;
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = command;
                DataTable data = new DataTable();
                sda.Fill(data);

                foreach (DataRow dr in data.Rows)
                {
                    voterName = dr["v_id"].ToString();
                    comboVoterID.Items.Add(voterName);
                }
            }
            catch (Exception ex)
            { MessageBox.Show("The page seems to have issues", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }


            //To fill the candidate ID comboBox
            command = new MySqlCommand("select c_id from candidate where admin_id is null;", conn);
            try
            {
                String candID;
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = command;
                DataTable data = new DataTable();
                sda.Fill(data);

                foreach (DataRow dr in data.Rows)
                {
                    candID = dr["c_id"].ToString();
                    comboCandID.Items.Add(candID);
                }
            }
            catch (Exception ex)
            { MessageBox.Show("The page seems to have issues", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }



            //To fill datagrid with voter details
              command = new MySqlCommand("select v_id as ID, name as Name, sex as Gender, age as Age, e_id as ElectionID from voter where admin_id is null;", conn);

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
        

            //To fill datagrid with candidate details
            command = new MySqlCommand("select c_id as ID, name as Name, sex as Gender, age as Age, e_id as ElectionID from candidate where admin_id is null;", conn);

            try
            {
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = command;
                DataTable dbdataset = new DataTable();
                sda.Fill(dbdataset);
                BindingSource bSource = new BindingSource();

                bSource.DataSource = dbdataset;
                dataGridView2.DataSource = bSource;
                sda.Update(dbdataset);
            }
            catch (Exception ex)
            { MessageBox.Show("The page seems to have issues", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboCandID.SelectedItem == null)
            {
                MessageBox.Show("No ID selected");
                return;
            }

            int c_id = Int32.Parse(comboCandID.Text);

            //Validate candidate
            query = "update candidate set admin_id = '" + admin_id + "' where c_id = " + c_id + ";";
            command = new MySqlCommand(query, conn);
            conn.Open();
            reader = command.ExecuteReader();
            while (reader.Read()) { }
            conn.Close();

            //To fill datagrid with candidate details
            command = new MySqlCommand("select c_id as ID, name as Name, sex as Gender, age as Age, e_id as ElectionID from candidate where admin_id is null;", conn);

            try
            {
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = command;
                DataTable dbdataset = new DataTable();
                sda.Fill(dbdataset);
                BindingSource bSource = new BindingSource();

                bSource.DataSource = dbdataset;
                dataGridView2.DataSource = bSource;
                sda.Update(dbdataset);
            }
            catch (Exception ex)
            { MessageBox.Show("The page seems to have issues", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }


            //To fill the candidate ID comboBox
            command = new MySqlCommand("select c_id from candidate where admin_id is null;", conn);
            try
            {
                comboCandID.Items.Clear();
                String candID;
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = command;
                DataTable data = new DataTable();
                sda.Fill(data);

                foreach (DataRow dr in data.Rows)
                {
                    candID = dr["c_id"].ToString();
                    comboCandID.Items.Add(candID);
                }
            }
            catch (Exception ex)
            { MessageBox.Show("The page seems to have issues", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }

        }

        private void labelElection_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboVoterID.SelectedItem == null)
            {
                MessageBox.Show("No ID selected");
                return;
            }

            String v_id = comboVoterID.Text;

            //Validate voter
            query = "update voter set admin_id = '" + admin_id + "' where v_id = '" + v_id + "';";
            command = new MySqlCommand(query, conn);
            conn.Open();
            reader = command.ExecuteReader();
            while(reader.Read()){}
            conn.Close();


            //To fill datagrid with voter details
            command = new MySqlCommand("select v_id as ID, name as Name, sex as Gender, age as Age, e_id as ElectionID from voter where admin_id is null;", conn);

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

            //To fill the voter ID comboBox
            command = new MySqlCommand("select v_id from voter where admin_id is null;", conn);
            try
            {
                comboVoterID.Items.Clear();
                String voterName;
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = command;
                DataTable data = new DataTable();
                sda.Fill(data);

                foreach (DataRow dr in data.Rows)
                {
                    voterName = dr["v_id"].ToString();
                    comboVoterID.Items.Add(voterName);
                }
            }
            catch (Exception ex)
            { MessageBox.Show("The page seems to have issues", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboVoterID.SelectedItem == null)
            {
                MessageBox.Show("No ID selected");
                return;
            }

            String v_id = comboVoterID.Text;

            //Delete voter
            query = "delete from voter where v_id = '" + v_id + "';";
            command = new MySqlCommand(query, conn);
            conn.Open();
            reader = command.ExecuteReader();
            while (reader.Read()) { }
            conn.Close();

            //To fill datagrid with voter details
            command = new MySqlCommand("select v_id as ID, name as Name, sex as Gender, age as Age, e_id as ElectionID from voter where admin_id is null;", conn);

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

            //To fill the voter ID comboBox
            command = new MySqlCommand("select v_id from voter where admin_id is null;", conn);
            try
            {
                String voterName;
                comboVoterID.Items.Clear();
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = command;
                DataTable data = new DataTable();
                sda.Fill(data);

                foreach (DataRow dr in data.Rows)
                {
                    voterName = dr["v_id"].ToString();
                    comboVoterID.Items.Add(voterName);
                }
            }
            catch (Exception ex)
            { MessageBox.Show("The page seems to have issues", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (comboCandID.SelectedItem == null)
            {
                MessageBox.Show("No ID selected");
                return;
            }

            int c_id = Int32.Parse(comboCandID.Text);

            //Delete candidate
            query = "delete from candidate where c_id = " + c_id + ";";
            command = new MySqlCommand(query, conn);
            conn.Open();
            reader = command.ExecuteReader();
            while (reader.Read()) { }
            conn.Close();


            //To fill datagrid with candidate details
            command = new MySqlCommand("select c_id as ID, name as Name, sex as Gender, age as Age, e_id as ElectionID from candidate where admin_id is null;", conn);

            try
            {
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = command;
                DataTable dbdataset = new DataTable();
                sda.Fill(dbdataset);
                BindingSource bSource = new BindingSource();

                bSource.DataSource = dbdataset;
                dataGridView2.DataSource = bSource;
                sda.Update(dbdataset);
            }
            catch (Exception ex)
            { MessageBox.Show("The page seems to have issues", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }


            //To fill the candidate ID comboBox
            command = new MySqlCommand("select c_id from candidate where admin_id is null;", conn);
            try
            {
                comboCandID.Items.Clear();
                String candID;
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = command;
                DataTable data = new DataTable();
                sda.Fill(data);

                foreach (DataRow dr in data.Rows)
                {
                    candID = dr["c_id"].ToString();
                    comboCandID.Items.Add(candID);
                }
            }
            catch (Exception ex)
            { MessageBox.Show("The page seems to have issues", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }

        }

        private void linklabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        private void Validation_Load(object sender, EventArgs e)
        {

        }
    }
}
