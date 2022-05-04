using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HTTP_Server
{
    public class HTTPServer
    {
        private int port;
        private bool running = false;

        private TcpListener listener;

        public HTTPServer(int port)
        {
            listener = new TcpListener(IPAddress.Any, port);
        }

        public void Start()
        {
            Thread serverThread = new Thread(new ThreadStart(Run));
            serverThread.Start();
        }

        private void Run()
        {
            running = true;
            listener.Start();

            while (running)
            {
                Console.WriteLine("Waiting for connection...");

                TcpClient client = listener.AcceptTcpClient();

                Console.WriteLine("Client connected!");

                HandleClient(client);

                client.Close();
            }
            running = false;

            listener.Stop();
        }

        private void HandleClient(TcpClient client)
        {
            StreamReader reader = new StreamReader(client.GetStream());

            String msg = "";
            while(reader.Peek()!= -1)
            {
                msg += $"{reader.ReadLine()}\n";
            }

            Console.WriteLine($"Request: \n{msg}");

        }
    }
}
