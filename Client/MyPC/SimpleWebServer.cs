using System;
using System.Net;
using System.Threading;
using System.Text;
using Newtonsoft.Json;

namespace Absx2.MyPC
{
    public class WebServer
    {
        private readonly HttpListener _listener = new HttpListener();

        public WebServer(string prefixes)
        {
            if (!HttpListener.IsSupported)
                throw new NotSupportedException("Needs Windows XP SP2, Server 2003 or later.");
            
            _listener.Prefixes.Add(prefixes);
            _listener.Start();
        }

        public void Run()
        {
            ThreadPool.QueueUserWorkItem((o) =>
            {
                try
                {
                    while (_listener.IsListening)
                    {
                        ThreadPool.QueueUserWorkItem((c) =>
                        {
                            var ctx = c as HttpListenerContext;
                            try
                            {
                                if (ctx.Request.RawUrl == "/close") Environment.Exit(0);

                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("\nWebRequest from {0}\nRequested: {1}\n\nHeaders:\n--------------------------\n{2}--------------------------\n", 
                                    ctx.Request.UserHostAddress, ctx.Request.Url, ctx.Request.Headers);
                                Console.ForegroundColor = ConsoleColor.White;

                                // Create a MySystem object
                                MySystem Sys = new MySystem();
                                Sys.Generated = DateTime.Now.ToString();
                                Sys.RAM = MyPC.InfoRAM();
                                Sys.CPU = MyPC.InfoCPU();
                                Sys.GPU = MyPC.InfoGPU();
                                Sys.BSE = MyPC.InfoBSE();
                                Sys.NIC = MyPC.InfoNIC();
                                Sys.HDD = MyPC.InfoHDD();
                                Sys.INF = MyPC.InfoINF();
                                Sys.USR = MyPC.InfoUSR();

                                // JSON encode the data
                                string rstr = JsonConvert.SerializeObject(Sys).ToString();

                                // Return data
                                byte[] buf = Encoding.UTF8.GetBytes(rstr);

                                ctx.Response.AddHeader("Access-Control-Allow-Origin", "*");
                                ctx.Response.AddHeader("Server", "MyPC");
                                ctx.Response.ContentType = "text/json";
                                ctx.Response.ContentLength64 = buf.Length;
                                ctx.Response.OutputStream.Write(buf, 0, buf.Length);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Error!\n\n{0}", e.ToString());
                            }
                            finally
                            {
                                ctx.Response.OutputStream.Close();
                            }
                        }, _listener.GetContext());
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error!\n\n{0}", e.ToString());
                }
            });
        }

        public void Stop()
        {
            _listener.Stop();
            _listener.Close();
        }
    }
}