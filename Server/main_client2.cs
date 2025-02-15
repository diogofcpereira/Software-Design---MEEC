using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace SERVER_TEST
{
    class Program
    {
        static void Main() {
            int serverPort = 2048; // Replace with your server port
            string serverIp = "127.0.0.1"; // Replace with your server IP

            Client client1 = new Client(serverIp, serverPort);
            
            try
            {
                bool flag_ready = false;
                bool flag_all_in = false;
                client1.ConnectToServer();

                // Example of sending and receiving data
                string nickname = "R0rui";
                client1.SendData(nickname);

                byte[] buffer = new byte[1024];
                client1.ReceiveData(buffer, buffer.Length);

                while(flag_ready == false)
                {
                    client1.SendData("R1");

                    byte[] buffer2 = new byte[1024];
                    client1.ReceiveData(buffer2, buffer2.Length);
                    if(Encoding.ASCII.GetString(buffer2, 0, 5) == "ready")
                        flag_ready = true;
                }

                client1.SendData("R21");
                byte[] buffer3 = new byte[1024];
                client1.ReceiveData(buffer3, buffer3.Length);

                while(flag_all_in == false)
                {
                    client1.SendData("R3");

                    byte[] buffer4 = new byte[1024];
                    client1.ReceiveData(buffer4, buffer4.Length);
                    if(Encoding.ASCII.GetString(buffer4, 0, 6) == "all_in")
                        flag_ready = true;
                }

                byte[] buffer5 = new byte[1024];
                client1.SendData("R4me_size");
                client1.ReceiveData(buffer5, buffer5.Length);
                client1.SendData("R4me_nick");
                client1.ReceiveData(buffer5, buffer5.Length);
                client1.SendData("R4partner_size");
                client1.ReceiveData(buffer5, buffer5.Length);
                client1.SendData("R4partner_nick");
                client1.ReceiveData(buffer5, buffer5.Length);
                client1.SendData("R4right_size");
                client1.ReceiveData(buffer5, buffer5.Length);
                client1.SendData("R4right_nick");
                client1.ReceiveData(buffer5, buffer5.Length);
                client1.SendData("R4left_size");
                client1.ReceiveData(buffer5, buffer5.Length);
                client1.SendData("R4left_nick");
                client1.ReceiveData(buffer5, buffer5.Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}