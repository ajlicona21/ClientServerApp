using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServerApp
{
    public class SynchronousSocketListener
    {
        const int PORT = 11000;
        const string IP_ADDRESS = "127.0.0.1";
        const string JOKE = "JOKE";
        const string CONSPIRACY = "CONSPIRACY";
        string[] jokes;
        string[] conspiracies;
        const string JOKE_FILE = "jokes.txt";
        const string CONSP_FILE = "conspiracies.txt";
        TcpListener tcpListener;
        public SynchronousSocketListener()
        {
            try
            {
                jokes = File.ReadAllLines(JOKE_FILE);
                conspiracies = File.ReadAllLines(CONSP_FILE);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void StartListening()
        {
            IPAddress iPAddress = IPAddress.Parse(IP_ADDRESS);
            tcpListener = new TcpListener(iPAddress, PORT);
            tcpListener.Start();
            Thread thread = new Thread(new ThreadStart(ProcessSocket));

        }

        private void ProcessSocket()
        {
            while (true)
            {
                try
                {
                    Socket socket = tcpListener.AcceptSocket();
                    NetworkStream ns = new NetworkStream(socket);
                    StreamReader reader = new StreamReader(ns);
                    StreamWriter writer = new StreamWriter(ns);

                    writer.AutoFlush = true;

                    string clientinput = reader.ReadLine();
                    Console.WriteLine($"Received from client: {clientinput}");

                    Random rand = new Random();
                    if (clientinput.ToUpper() == JOKE)
                    { 
                        string joke = jokes[rand.Next(jokes.Length)];
                        Console.WriteLine(joke);
                        writer.WriteLine(joke);
                    }
                    else if (clientinput.ToUpper() == CONSPIRACY)
                    {
                        string consp = conspiracies[rand.Next(conspiracies.Length)];
                        Console.WriteLine(consp);
                        writer.WriteLine(consp);
                    }
                    else
                    {
                        writer.WriteLine($"Could not do anything with: {clientinput}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

        }
    }
}
