using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sueca
{
    public partial class Join_game : Form
    {
        public Join_game()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Choose_games form2 = new Choose_games();

            form2.Show();

            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            setup form8 = new setup();

            form8.Show();

            this.Hide();
        }
    }
}
