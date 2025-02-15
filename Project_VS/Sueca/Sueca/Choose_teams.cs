using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sueca
{
    public partial class Choose_teams : Form
    {
        private int selected_team = 0;
        private bool team_flag;
        public Choose_teams()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Waiting form7 = new Waiting();

            form7.Show();

            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Main_menu form1 = new Main_menu();

            form1.Show();

            this.Hide();
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            button4.BackColor = Color.SeaGreen;
            button3.BackColor = SystemColors.Control;
            selected_team = 2;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.BackColor = Color.SeaGreen;
            button4.BackColor = SystemColors.Control;
            selected_team = 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(selected_team == 1 || selected_team == 2) 
            {
                assign_team();
                if (team_flag == true)
                {
                    Game_Table form2 = new Game_Table();

                    form2.Show();

                    this.Hide();
                }
                    
            }
            else
            {
                MessageBox.Show("You need to select a team!");
            }
        }

        private void assign_team()
        {
            Main_menu.client_i.SendData("R2" + selected_team);
            byte[] buffer = new byte[1024];
            Main_menu.client_i.ReceiveData(buffer, buffer.Length);
            Console.WriteLine(Encoding.ASCII.GetString(buffer, 0, 9));
            if (string.Equals(Encoding.ASCII.GetString(buffer, 0, 9), "lock_team"))
            {
                team_flag = true;
            }
            if (string.Equals(Encoding.ASCII.GetString(buffer, 0, 9), "full_team"))
            {
                MessageBox.Show("The team you choose is already full. Choose the other one!");
                team_flag = false;
            }
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }
    }
}
