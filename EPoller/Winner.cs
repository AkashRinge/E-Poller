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
    public partial class Winner : Form
    {

        MySqlCommand command;
        String connectionString;

        public Winner()
        {
            InitializeComponent();
            this.MaximizeBox = false;

            connectionString = "server=localhost;database=e_poller;userid = root;password = MSaa_1996";
            MySqlConnection conn = new MySqlConnection(connectionString);

            command = new MySqlCommand("select ename as ElectionName, cname as WinningCandidate from winner;", conn);

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

        private void Winner_Load(object sender, EventArgs e)
        {

        }

        private void linklabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
