using client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace client
{
    internal class Program
    {
        static void Main(string[] args)
        {

            ClientTcp toServer = new ClientTcp();

            //connect to server here
            toServer.startClient("192.168.1.34", 30000 );

        }
    }
}
