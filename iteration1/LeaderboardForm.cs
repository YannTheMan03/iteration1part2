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
        public LeaderboardForm()
        {
            InitializeComponent();
        }

        private void LeaderboardForm_Load(object sender, EventArgs e)
        {
            this.Height = 60;
            this.Width = 40;
        }
    }
}
