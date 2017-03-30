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
    public partial class DeclareWinner : Form
    {
        MySqlCommand command;
        MySqlDataReader reader;
        String query;
        String connectionString;
        MySqlConnection conn;

        public DeclareWinner()
        {
            InitializeComponent();

            connectionString = "server=localhost;database=e_poller;userid = root;password = MSaa_1996";

            String electionName;
            conn = new MySqlConnection(connectionString);
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

        private void DeclareWinner_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboename.SelectedItem == null)
            {
                MessageBox.Show("Please select an election to delete");
                return;
            }

            String ename = comboename.Text;
            String winnerName = "";
            int count = 0;
            int e_id = 0;

            //Get election ID
            query = "select e_id as ID from election where name = '" + ename + "';";
            command = new MySqlCommand(query, conn);
            conn.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            { e_id = reader.GetInt32("ID"); }
            conn.Close();

            //To check for a tie
            query = "select count(c_id) as tot from count where e_id = " + e_id + " and no_of_votes >= all (select no_of_votes from count where e_id = " + e_id + ");";
            command = new MySqlCommand(query, conn);
            conn.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            { count = reader.GetInt32("tot"); }
            conn.Close();

            if (count != 1)
            {
                MessageBox.Show("It is not possible to declare a winner right now since there is a tie");
                return;
            }


            //To get winner
            query = "select name from candidate where c_id in (select c_id from count where e_id = " + e_id + " and no_of_votes >= all(select no_of_votes from count where e_id = " + e_id + "));";  
            command = new MySqlCommand(query, conn);
            conn.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            { winnerName = reader.GetString("name"); }
            conn.Close();


            //To add winner to Winner relation
            query = "insert into winner values ('" + ename + "', '" + winnerName + "');";
            command = new MySqlCommand(query, conn);
            conn.Open();
            reader = command.ExecuteReader();
            while (reader.Read()) { }
            conn.Close();

            //To delete relation completely from the database
            query = "delete from election where name = '" + ename + "';";
            command = new MySqlCommand(query, conn);
            conn.Open();
            reader = command.ExecuteReader();
            while (reader.Read()) { }
            conn.Close();

            MessageBox.Show("The winner has been declared!");
            return;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
