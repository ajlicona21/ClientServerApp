using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp
{
     class Program
    {
        static void Main(string[] args)
        {
            TempServer tempServer = new TempServer();
            tempServer.LoadFiles();

            Console.WriteLine("Welcome to Kodeys Joke/Conspiracy Server ");
            Console.WriteLine("-----------------------------------------");

            SynchronousSocketListener ssl = new SynchronousSocketListener();
            ssl.StartListening();
          
        }
    }
}
