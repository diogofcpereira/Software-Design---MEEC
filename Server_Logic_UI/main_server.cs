using System;

namespace SERVER_TEST 
{
    class Program
    {
        static void Main()
        {
            int serverPort = 2048; // Replace with your server port

            Server server = new Server(serverPort); 

            try
            {
                server.StartServer();
            }
            catch (Exception ex)
            {
                Console.WriteLine("MAIN SERVER CATCH");
                Console.WriteLine("Error: " + ex.Message);
            }

            /*Console.WriteLine($"{server.game_player[0].nick}");
            Console.WriteLine($"{server.game_player[1].nick}");
            Console.WriteLine($"{server.game_player[2].nick}");
            Console.WriteLine($"{server.game_player[3].nick}");*/
        }
    }
}       
