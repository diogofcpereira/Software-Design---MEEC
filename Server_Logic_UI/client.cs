using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace SERVER_TEST
{
    class Client
    {
        private string serverIp;
        private int port;
        private TcpClient client;
        private NetworkStream stream;

        public Client(string serverIp, int serverPort)
        {
            this.serverIp = serverIp;
            this.port = serverPort;
            client = new TcpClient();
            Console.WriteLine("Client created; ");
        }

        public void ConnectToServer()
        {
            try
            {
                client.Connect(serverIp, port);
                stream = client.GetStream();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error connecting to server: " + ex.Message);
                client.Close();
                Environment.Exit(1);
            }
        }

        public void SendData(string data)
        {
            byte[] byteData = Encoding.ASCII.GetBytes(data);
            stream.Write(byteData, 0, byteData.Length);
        }

        public void ReceiveData(byte[] buffer, int bufferSize)
        {
            int bytesRead = stream.Read(buffer, 0, bufferSize);
            Console.WriteLine("Received: " + Encoding.ASCII.GetString(buffer, 0, bytesRead));
        }

        public void CloseConnection()
        {
            stream.Close();
            client.Close();
        }
    }
}