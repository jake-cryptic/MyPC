using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Absx2.MyPC
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create and run the webserver
            WebServer ws = new WebServer("http://localhost:9091/");
            ws.Run();

            // Start up a browser window (localhost/a/site/projects
            System.Diagnostics.Process.Start("http://projects.absolutedouble.co.uk/mypc/");

            // Wait 30 seconds and stop server
            Thread.Sleep(10000);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\nServer will automatically close in 20 seconds.\n");
            Console.ForegroundColor = ConsoleColor.White;

            Thread.Sleep(20000);
            ws.Stop();
        }
    }
}
