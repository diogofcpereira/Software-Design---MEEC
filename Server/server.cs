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
        private int num_con = 0;
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
            bool repeted_nick_flag = false;
            bool flag_nick = false;
            bool flag_ready = false;
            bool flag_team = false;
            bool flag_all_players_on_table = false;
            int partner = 0;
            int right = 0;
            int left = 0;
            try
            {
                NetworkStream stream = clientSocket.GetStream();
                byte[] buffer = new byte[1024];
                int bytesRead;
                byte[] responseBytes;
                string receivedData;
                int nick_count = 0;
                int team_count = 0;
                int team_int;

                while (true)
                {
                    while(flag_nick == false)
                    {
                        bytesRead = stream.Read(buffer, 0, buffer.Length);
                        if (bytesRead == 0) {
                            break;
                        }

                        receivedData = Encoding.ASCII.GetString(buffer, 0, bytesRead);               
                    
                        if(receivedData[0] == 'R' && receivedData[1] == '0')
                        {
                            receivedData = receivedData.Substring(2);
                            for(int i=0; i<count-1; i++) {
                                if(receivedData == game_player[i].nick) {
                                    Console.WriteLine("Repeted nickname, try a different one.");
                                    repeted_nick_flag = true;
                                }
                            }
                            
                            if(repeted_nick_flag == true) {
                                responseBytes = Encoding.ASCII.GetBytes("repeted_nick");
                                stream.Write(responseBytes, 0, responseBytes.Length);
                                repeted_nick_flag = false;
                            }
                            else {
                                game_player[count-1] = new Player();
                                game_player[count-1].nick = receivedData;
                                Console.WriteLine($"{game_player[count-1].nick}");
                                num_con++;
                                flag_nick = true;

                                responseBytes = Encoding.ASCII.GetBytes("allowed_nick");
                                stream.Write(responseBytes, 0, responseBytes.Length);
                            }
                        }
                    }

                    while(flag_ready == false)
                    {
                        bytesRead = stream.Read(buffer, 0, buffer.Length);
                        if (bytesRead == 0) {
                            break;
                        }

                        receivedData = Encoding.ASCII.GetString(buffer, 0, bytesRead);

                        if(receivedData[0] == 'R' && receivedData[1] == '1')
                        {
                            
                            if(num_con == 4)
                            {
                                responseBytes = Encoding.ASCII.GetBytes("ready");
                                stream.Write(responseBytes, 0, responseBytes.Length);
                                flag_ready = true;
                            }

                            else
                            {
                                responseBytes = Encoding.ASCII.GetBytes("block");
                                stream.Write(responseBytes, 0, responseBytes.Length);
                            }
                        }
                    }

                    num_con = 0;

                    while(flag_team == false)
                    {
                        bytesRead = stream.Read(buffer, 0, buffer.Length);
                        if (bytesRead == 0) {
                            break;
                        }

                        receivedData = Encoding.ASCII.GetString(buffer, 0, bytesRead);

                        if(receivedData[0] == 'R' && receivedData[1] == '2')
                        {
                            receivedData = receivedData.Substring(2);
                            team_int = int.Parse(receivedData);
                            for(int i = 0; i<4 ; i++)
                            {
                                if(team_int == game_player[i].Team)
                                {
                                    team_count++;
                                }
                            }

                            if(team_count == 2)
                            {
                                responseBytes = Encoding.ASCII.GetBytes("full_team");
                                stream.Write(responseBytes, 0, responseBytes.Length);
                                team_count = 0;
                            }

                            if(team_count < 2)
                            {
                                game_player[count-1].Team = team_int;
                                responseBytes = Encoding.ASCII.GetBytes("lock_team");
                                stream.Write(responseBytes, 0, responseBytes.Length);
                                num_con++;
                            }
                        }
                    }

                    while(flag_all_players_on_table == false)
                    {
                        bytesRead = stream.Read(buffer, 0, buffer.Length);
                        if (bytesRead == 0) {
                            break;
                        }

                        receivedData = Encoding.ASCII.GetString(buffer, 0, bytesRead);

                        if(receivedData[0] == 'R' && receivedData[1] == '3')
                        {
                            if(num_con == 4)
                            {
                                responseBytes = Encoding.ASCII.GetBytes("all_in");
                                stream.Write(responseBytes, 0, responseBytes.Length);
                                flag_all_players_on_table = true;
                            }

                            else
                            {
                                responseBytes = Encoding.ASCII.GetBytes("not_in");
                                stream.Write(responseBytes, 0, responseBytes.Length);
                            }
                        }
                    }

                    while(nick_count<4)
                    {
                        bytesRead = stream.Read(buffer, 0, buffer.Length);
                        if (bytesRead == 0) {
                            break;
                        } 

                        receivedData = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                        if(receivedData[0] == 'R' && receivedData[1] == '4')
                        {
                            receivedData = receivedData.Substring(2);
                            if(receivedData == "me_size")
                            {
                                responseBytes = BitConverter.GetBytes(game_player[count-1].nick.Length);
                                stream.Write(responseBytes, 0, responseBytes.Length);
                            }
                            if(receivedData == "me_nick")
                            {
                                responseBytes = Encoding.ASCII.GetBytes(game_player[count-1].nick);
                                stream.Write(responseBytes, 0, responseBytes.Length);
                                nick_count++;
                            }

                            if(receivedData == "partner_size")
                            {
                                for(int i = 0; i < 4; i++)
                                {
                                    if(i == count-1)
                                    {
                                        continue;
                                    }
                                    else if(game_player[i].Team == game_player[count-1].Team)
                                    {
                                        partner = i;
                                    }
                                }
                                responseBytes = BitConverter.GetBytes(game_player[partner].nick.Length);
                                stream.Write(responseBytes, 0, responseBytes.Length);
                            }
                            if(receivedData == "partner_nick");
                            {
                                responseBytes = Encoding.ASCII.GetBytes(game_player[partner].nick);
                                stream.Write(responseBytes, 0, responseBytes.Length);
                                nick_count++;
                            }

                            if(receivedData == "right_size")
                            {
                                for(int i = 0; i < 4; i++)
                                {
                                    if(i == count-1)
                                    {
                                        continue;
                                    }
                                    else if(game_player[i].Team != game_player[count-1].Team)
                                    {
                                        right = i;
                                    }
                                }
                                responseBytes = BitConverter.GetBytes(game_player[right].nick.Length);
                                stream.Write(responseBytes, 0, responseBytes.Length);
                            }
                            if(receivedData == "right_nick");
                            {
                                responseBytes = Encoding.ASCII.GetBytes(game_player[right].nick);
                                stream.Write(responseBytes, 0, responseBytes.Length);
                                nick_count++;
                            }

                            if(receivedData == "left_size")
                            {
                                for(int i = 0; i < 4; i++)
                                {
                                    if(i == count-1 || i == right)
                                    {
                                        continue;
                                    }
                                    else if(game_player[i].Team != game_player[count-1].Team)
                                    {
                                        left = i;
                                    }
                                }
                                responseBytes = BitConverter.GetBytes(game_player[left].nick.Length);
                                stream.Write(responseBytes, 0, responseBytes.Length);
                            }
                            if(receivedData == "left_nick");
                            {
                                responseBytes = Encoding.ASCII.GetBytes(game_player[left].nick);
                                stream.Write(responseBytes, 0, responseBytes.Length);
                                nick_count++;
                            }
                        }
                    }
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
