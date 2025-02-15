using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace SERVER_TEST
{
    class Server
    {
        private int port;
        private TcpListener serverListener;
        private TcpClient[] clientSockets = new TcpClient[4];

        public Player[] game_player = new Player[4];

        public Server(int port)
        {
            this.port = port;
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            serverListener = new TcpListener(ipAddress, port);
        }

        public void StartServer()
        {
            try
            {
                serverListener.Start();
                Console.WriteLine($"Server listening on port {port}");

                int count = 0;
                while (count < 4)
                {
                    //Console.WriteLine($"Count ss1 = {count}");
                    clientSockets[count] = serverListener.AcceptTcpClient(); 
                    //Console.WriteLine("New client connected");

                    count++;
                     
                    // Start a new thread for each client
                    //Console.WriteLine($"Count ss2= {count-1}");
                    Thread clientThread = new Thread(() => HandleClient(clientSockets[count-1], count));
                    //Console.WriteLine($"Count ss3= {count-1}");
                    clientThread.Start();

                }
                //Console.WriteLine($"Count ss4= {count}");


            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private void HandleClient(TcpClient clientSocket, int count)
        {
            try
            {
                NetworkStream stream = clientSocket.GetStream();
                byte[] buffer = new byte[1024];
                int bytesRead;

                while (true)
                {
                    bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0)
                        break;

                    string receivedData = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    //Console.WriteLine($"Received from client: {receivedData}");

                    // Add your logic to handle the received data

                    //Check if there is any previous nick with the same receivedData
                    for(int i=0; i<count-1; i++) {
                        if(receivedData == game_player[count-1].nick) {
                            Console.WriteLine("Repeted nickname, try a different one.");
                        }
                    }
                    //Console.WriteLine($"Count hc1 = {count_aux-1}");
                    //Console.WriteLine("Pre assemble");
                    game_player[count-1] = new Player();
                    game_player[count-1].CreatePlayer(count+1);
                    game_player[count-1].nick = receivedData;
                    Console.WriteLine($"{game_player[count-1].nick}");
                    //Console.WriteLine("Pos assemble");

                    // Example: Echo the data back to the client
                    //Console.WriteLine($"Count hc2 = {count_aux-1}");
                    byte[] responseBytes = Encoding.ASCII.GetBytes(game_player[count-1].nick);
                    stream.Write(responseBytes, 0, responseBytes.Length);
                    //Console.WriteLine("After echo");

                }

                stream.Close();
                clientSocket.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error handling client: " + ex.Message);
            }
        }
    }
}
