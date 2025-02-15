using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Web;
using System.Windows.Forms;

namespace Sueca
{
    public partial class Waiting : Form
    {
        private bool ready_flag;
        public Waiting()
        {
            InitializeComponent();
            label4.Text = "Waiting for all players...";
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Choose_games form2 = new Choose_games();

            form2.Show();

            this.Hide();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click_1(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Waiting_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string invitation_link = "www.youtube.com";

            Clipboard.SetText(invitation_link);
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }
        private void all_four_connected()
        {
            Main_menu.client_i.SendData("R1");
            byte[] buffer = new byte[1024];
            Main_menu.client_i.ReceiveData(buffer, buffer.Length);
            Console.WriteLine(Encoding.ASCII.GetString(buffer, 0, 5));
            if (string.Equals(Encoding.ASCII.GetString(buffer, 0, 5), "ready"))
            {
                ready_flag = true;
            }
            if (string.Equals(Encoding.ASCII.GetString(buffer, 0, 5), "block"))
            {
                ready_flag = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Console.WriteLine(ready_flag);
            all_four_connected();
            if (ready_flag == true)
            {
                Choose_teams form1 = new Choose_teams();

                form1.Show();

                this.Hide();
            }
        }
    }
}
        
