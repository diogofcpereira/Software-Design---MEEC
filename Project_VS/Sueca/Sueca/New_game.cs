using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sueca
{
    partial class New_game : Form
    {
        private string nick;
        private bool nick_flag;

        public New_game()
        {
            InitializeComponent();
            Main_menu.client_i = new Client("127.0.0.1", 2048);
            Main_menu.client_i.ConnectToServer();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
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

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            nick = textBox3.Text;
            Console.WriteLine(nick);

            if (nick == "")
            {
                MessageBox.Show("Nickname can't be null!");
            }
            else if (nick.Length > 8)
            {
                MessageBox.Show("Nickname can't have more than 8 characters!");
            }
            else if (ContainsSpecialCharacteres(nick))
            {
                MessageBox.Show("Nickname can't have special characters!");
            }
            else
            {
                Console.WriteLine(nick_flag);
                nick_assign();
                if(nick_flag == true)
                {
                    Waiting form7 = new Waiting();

                    form7.Show();

                    this.Hide();
                }
            }
        }

        private static bool ContainsSpecialCharacteres(string input)
        {
            string pattern = @"[^a-zA-Z0-9]";

            Regex regex = new Regex(pattern);

            return regex.IsMatch(input);
        }

        private void nick_assign()
        {
            Console.WriteLine("function_nick: "+nick);
            Main_menu.client_i.SendData("R0" + nick);
            byte[] buffer = new byte[1024];
            Main_menu.client_i.ReceiveData(buffer, buffer.Length);
            Console.WriteLine(Encoding.ASCII.GetString(buffer, 0, 12));
            if (string.Equals(Encoding.ASCII.GetString(buffer, 0, 12), "repeted_nick"))
            {
                MessageBox.Show("That nickname has already been choose!");
                nick_flag = false;

            }
            if (string.Equals(Encoding.ASCII.GetString(buffer, 0, 12), "allowed_nick"))
            {
                nick_flag = true;
            }
        }

        private void label1_Click(object sender, EventArgs e)
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
