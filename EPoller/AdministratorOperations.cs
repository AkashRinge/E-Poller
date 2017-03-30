using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EPoller
{
    public partial class AdministratorOperations : Form
    {
        String admin_id;
        public AdministratorOperations(String id)
        {
            admin_id = id;
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Validation(admin_id).ShowDialog();
            this.Show();
        }

        private void AdministratorOperations_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            new NewElection(admin_id).ShowDialog();
            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            new DeclareWinner().ShowDialog();
            this.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new ChangePassword(admin_id).ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
