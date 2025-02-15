using System;
using System.Timers;
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
    public partial class Game_Table : Form
    {
        private bool flag_choose_team;
        public Game_Table()
        {
            InitializeComponent();
            System.Timers.Timer timer = new System.Timers.Timer(1500);
            timer.AutoReset = true;
            timer.Elapsed += send;
            timer.Enabled = true;
            if(flag_choose_team)
            {
                timer.Stop();
                timer.Dispose(); 

            }
            get_nicknames();
            
        }

        private void Game_Table_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            /*Main_menu form1 = new Main_menu();

            form1.Show();

            this.Hide();*/
        }

        private void pictureBox17_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void send(object sender, EventArgs e)
        {
            Main_menu.client_i.SendData("R3");
            byte[] buffer = new byte[1024];
            Main_menu.client_i.ReceiveData(buffer, buffer.Length);
            Console.WriteLine(Encoding.ASCII.GetString(buffer, 0, 6));
            if (string.Equals(Encoding.ASCII.GetString(buffer, 0, 6), "all_in"))
            {
                flag_choose_team = true;
                
                
            }
            if (string.Equals(Encoding.ASCII.GetString(buffer, 0, 6), "not_in"))
            {
                flag_choose_team = false;
            }
        }

        private void get_nicknames()
        {
            byte[] buffer = new byte[1024];
            int size_nick;
            Main_menu.client_i.SendData("R4me_size");
            Main_menu.client_i.ReceiveData(buffer, buffer.Length);
            size_nick = BitConverter.ToInt32(buffer, 0);
            Main_menu.client_i.SendData("R4me_nick");
            Main_menu.client_i.ReceiveData(buffer, buffer.Length);
            label4.Text = Encoding.ASCII.GetString(buffer, 0, size_nick);

            Main_menu.client_i.SendData("R4partner_size");
            Main_menu.client_i.ReceiveData(buffer, buffer.Length);
            size_nick = BitConverter.ToInt32(buffer, 0);
            Main_menu.client_i.SendData("R4partner_nick");
            Main_menu.client_i.ReceiveData(buffer, buffer.Length);
            label3.Text = Encoding.ASCII.GetString(buffer, 0, size_nick);


            Main_menu.client_i.SendData("R4right_size");
            Main_menu.client_i.ReceiveData(buffer, buffer.Length);
            size_nick = BitConverter.ToInt32(buffer, 0);
            Main_menu.client_i.SendData("R4right_nick");
            Main_menu.client_i.ReceiveData(buffer, buffer.Length);
            label1.Text = Encoding.ASCII.GetString(buffer, 0, size_nick);

            Main_menu.client_i.SendData("R4left_size");
            Main_menu.client_i.ReceiveData(buffer, buffer.Length);
            size_nick = BitConverter.ToInt32(buffer, 0);
            Main_menu.client_i.SendData("R4left_nick");
            Main_menu.client_i.ReceiveData(buffer, buffer.Length);
            label2.Text = Encoding.ASCII.GetString(buffer, 0, size_nick);

        }

        private void label4_Click(object sender, EventArgs e)
        {
            
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
