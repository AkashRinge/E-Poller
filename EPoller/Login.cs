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
    public partial class Login : Form
    {
        MySqlCommand command;
        MySqlDataReader reader;
        String query;
        String connectionString;

        public Login()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            connectionString = "server=localhost;database=e_poller;userid = root;password = MSaa_1996";
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            String username = txtv_id.Text;
            String password = txtpassword.Text;
            String admin_id = "";
            int count = 0;
 
            if(username.Length == 0)
            {
                MessageBox.Show("Please enter your Voter ID");
                return;
            }

            if(password.Length == 0)
            {
                MessageBox.Show("Please enter your password");
                return;
            }

            query = "select count(v_id) as total from voter where v_id = '" + username + "' and password = '" + password + "'";
     
            command = new MySqlCommand(query, conn);
            conn.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            { count = reader.GetInt32("total"); }

            if (count == 0)
            {
                MessageBox.Show("Invalid username or password");
                return;
            }
            conn.Close();

            query = "select admin_id from voter where v_id = '" + username + "'";

            command = new MySqlCommand(query, conn);
            conn.Open();
            reader = command.ExecuteReader();
            
            while (reader.Read())
            {admin_id = reader.IsDBNull(reader.GetOrdinal("admin_id")) ? null : reader.GetString("admin_id");}

            if (admin_id == null)
            {
                MessageBox.Show("You have not yet been authenticated by the administrator. Sorry!");
                return;
            }
            else
            {
                this.Hide();
                new Vote(username).ShowDialog();
                this.Close();
            }
            
        }

        private void linklabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            new ForgotPassword().ShowDialog();
            this.Close();
        }
    }
}
