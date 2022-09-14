using client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //start server
            TcpServer server = new TcpServer();
            
            //start server using a specified port
            server.RunServer(30000);
        }
    }
}
