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
    public partial class ForgotPassword : Form
    {
        MySqlCommand command;
        MySqlDataReader reader;
        String query;
        String connectionString;
        MySqlConnection conn;

        public ForgotPassword()
        {
            InitializeComponent();
            this.MaximizeBox = false;

            connectionString = "server=localhost;database=e_poller;userid = root;password = MSaa_1996";
            conn = new MySqlConnection(connectionString);
        }

        private void ForgotPassword_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Empty fields
            if (txtv_id.Text.Length == 0 || txtname.Text.Length == 0 || comboage.Text.Length == 0 || (radioF.Checked == false && radioM.Checked == false))
            {
                MessageBox.Show("One or more required fields have been left empty. Please enter all fields");
                return;
            }
            
            String id = txtv_id.Text;
            String name = txtname.Text;
            int age = Int32.Parse(comboage.Text);
            char sex;
            if (radioF.Checked)
                sex = 'F';
            else
                sex = 'M';
            int count = 0;

            //Checking for valid details
            query = "select count(*) as tot from voter where v_id = '" + id + "' and age = " + age + " and name = '" + name + "' and sex = '" + sex + "';";
            command = new MySqlCommand(query, conn);
            conn.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            {  count = reader.GetInt32("tot");  }
            conn.Close();

            if (count == 0)
            {
                MessageBox.Show("Invalid details, password retrieval impossible");
                return;
            }

            //Retrieve password
            String password = "";
            query = "select password from voter where v_id = '" + id + "';";
            command = new MySqlCommand(query, conn);
            conn.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            { password = reader.GetString("password"); }
            conn.Close();

            this.Hide();
            new QRCode(password).ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
