using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace client
{
    public class ClientTcp
    {


        public ClientTcp()
        {


        }

        public void startClient(string ip , int port)
        {



            try
            {
                TcpClient tcpClient = new TcpClient();

                //client should be connected before streamwriter
                tcpClient.Connect(ip, port);

                StreamWriter sWriter = new StreamWriter(tcpClient.GetStream(), Encoding.ASCII);


                Stopwatch timer = new Stopwatch();

                int exit = 0;


                //check if client is connected
                while (tcpClient.Connected)
                {
                    if(exit == 2)
                    {
                        MessageBox.Show( "Closing connection for violating message count per second rule.");


                        Console.WriteLine("Connection Closed");

                        tcpClient.Close();

                        break;

                    }

                    timer.Restart();
                    Console.WriteLine("Enter message to send:");
                    string message =  Console.ReadLine();

                    //check time inbetween sent messages
                    if (timer.ElapsedMilliseconds <= 1000)
                    {


                        MessageBox.Show("You can not send more than 1 message per second. Click OK");
                        exit++;
                    }
                    else
                    {
                        sWriter.WriteLine(message);
                        sWriter.Flush();
                    }




                }


            }
            catch (Exception ex)
            {

            }

        }


    }
}
