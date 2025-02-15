using System;

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
                client1.ConnectToServer();

                // Example of sending and receiving data
                string nickname = "daniel";
                client1.SendData(nickname);

                byte[] buffer = new byte[1024];
                client1.ReceiveData(buffer, buffer.Length);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}