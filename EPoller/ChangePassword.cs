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
    public partial class ChangePassword : Form
    {
        MySqlConnection conn;
        MySqlCommand command;
        MySqlDataReader reader;
        String query;
        String connectionString;
        String admin_id;

        public ChangePassword(String id)
        {
            admin_id = id;
            InitializeComponent();
            this.MaximizeBox = false;
            connectionString = "server=localhost;database=e_poller;userid = root;password = MSaa_1996";
            conn = new MySqlConnection(connectionString);
        }

        private void ChangePassword_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtNew.Text.Length == 0 || txtOld.Text.Length == 0 || txtRetype.Text.Length == 0)
            {
                MessageBox.Show("One or more fields empty");
                return;
            }

            String oldPass = txtOld.Text;
            String newPass = txtNew.Text;
            String retypePass = txtRetype.Text;
            String temp = "";

            query = "select password from admin where admin_id = '" + admin_id + "';";
            command = new MySqlCommand(query, conn);
            conn.Open();
            reader = command.ExecuteReader();
            while(reader.Read())
            {temp = reader.GetString("password");}
            conn.Close();

            if (oldPass.Equals(temp) == false || newPass.Equals(retypePass) == false)
            {
                MessageBox.Show("Password mismatch");
                txtOld.Text = "";
                txtNew.Text = "";
                txtRetype.Text = "";
                return;
            }

            query = "update admin set password = '" + newPass + "' where admin_id = '" + admin_id + "';";
            command = new MySqlCommand(query, conn);
            conn.Open();
            reader = command.ExecuteReader();
            while(reader.Read()){}
            conn.Close();

            MessageBox.Show("Password changed!");
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
