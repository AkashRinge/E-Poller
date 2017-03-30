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
    public partial class LoginAdmin : Form
    {
        MySqlCommand command;
        MySqlDataReader reader;
        String query;
        String connectionString;

        public LoginAdmin()
        {
            InitializeComponent();
             this.MaximizeBox = false;
            connectionString = "server=localhost;database=e_poller;userid = root;password = MSaa_1996";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int count = 1;
            String admin_id = txtID.Text;
            String password = txtPass.Text;
            MySqlConnection conn = new MySqlConnection(connectionString);

            if (admin_id.Length == 0)
            {
                MessageBox.Show("Please enter Admin ID");
                return;
            }

            if (password.Length == 0)
            {
                MessageBox.Show("Please enter corresponding password");
                return;
            }

            query = "select count(admin_id) as total from admin where admin_id = '" + admin_id + "' and password = '" + password + "';";

            command = new MySqlCommand(query, conn);
            conn.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            { count = reader.GetInt32("total"); }
            conn.Close();

            if (count == 0)
            {
                MessageBox.Show("You are not permitted to administration priviledges");
                this.Close();
            }
            else
            {
                MessageBox.Show("Login successful");
                this.Hide();
                new AdministratorOperations(admin_id).ShowDialog();
                this.Close();
            }
        }

        private void LoginAdmin_Load(object sender, EventArgs e)
        {

        }
    }
}
