using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
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
                StreamWriter sWriter = new StreamWriter(tcpClient.GetStream(), Encoding.ASCII);

                tcpClient.Connect(ip, port);

                Stopwatch timer = new Stopwatch();

                int exit = 0;


                //check if client is connected
                while (tcpClient.Connected)
                {
                    if(exit == 1)
                    {
                        MessageBox.Show("Closing connection for violating message count per second rule.");
                        tcpClient.Close();

                    }

                    timer.Start();
                    Console.WriteLine("Enter message to send:");
                    string message =  Console.ReadLine();

                    if(timer.ElapsedMilliseconds <= 1000)
                    {
                        MessageBox.Show("You can not send more than 1 message per second.");
                        exit++;
                    }

                    sWriter.WriteLine(message);
                    sWriter.Flush();
                    timer.Stop();
                }


            }
            catch (Exception ex)
            {

            }

        }


    }
}
