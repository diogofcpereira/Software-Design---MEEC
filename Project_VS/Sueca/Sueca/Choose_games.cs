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
    public partial class Choose_games : Form
    {
        
        public Choose_games()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Rules form4 = new Rules();
            form4.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            About_us form5 = new About_us();
            form5.Show();
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            New_game form3 = new New_game();

            form3.Show();

            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Join_game form6 = new Join_game();

            form6.Show();

            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Main_menu form1 = new Main_menu();

            form1.Show();

            this.Hide();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            New_game form3 = new New_game();

            form3.Show();

            this.Hide();
        }
    }
}
