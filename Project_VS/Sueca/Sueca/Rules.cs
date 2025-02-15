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
    public partial class Rules : Form
    {
       

        public Rules()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Choose_games form2 = new Choose_games();

            form2.Show();

            this.Hide();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://cantinhodojogo.blogspot.com/2008/05/regras-da-sueca.html");
        }

        private void Rules_Load(object sender, EventArgs e)
        {

        }
    }
}
