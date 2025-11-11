using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iteration1
{
    public partial class LeaderboardForm : Form
    {
        public string Username { get; private set; }
        public LeaderboardForm()
        {
            InitializeComponent();
        }

        private void LeaderboardForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Please Enter a username First");
                return;
            }

            Username = textBox1.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
