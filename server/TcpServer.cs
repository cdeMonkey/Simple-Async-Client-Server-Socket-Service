using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Timers;
using System.Linq;
using System.Runtime.InteropServices;

namespace client
{
    public class TcpServer
    {
        private TcpListener _listener;

        private List<TcpClient> _clients = new List<TcpClient>();

        private Boolean _isServerRunning = false;
        private int indexOfClient = 0;

        public string StatusMessage;
        public bool IsNewStatusTriggered;
        public string sData = null;


        public TcpServer()
        {


        }



        public void RunServer(int port)
        {


            try
            {
                indexOfClient = 0;
                _listener = new TcpListener(IPAddress.Any, port);
                _listener.Start();
                _isServerRunning = true;


                StatusMessage = "Waiting for connection...";
                Console.WriteLine(StatusMessage);
                IsNewStatusTriggered = true;

                LoopClients();
            }
            catch (Exception ex)
            {
                _isServerRunning = false;
                StatusMessage = "Server is down.";
                Console.WriteLine(StatusMessage);
                IsNewStatusTriggered = true;
            }

        }

        public void LoopClients()
        {
            while (_isServerRunning)
            {



                // wait for client connection
                _clients.Add(_listener.AcceptTcpClient());


                // client found.
                StatusMessage = "[Client]" + _clients[indexOfClient].Client.RemoteEndPoint + " has Connected";
                Console.WriteLine(StatusMessage);
                IsNewStatusTriggered = true;

                // create a thread to handle communication
                Thread t = new Thread(new ParameterizedThreadStart(HandleClient));



                t.Start(_clients[indexOfClient]);



                ++indexOfClient;

            }
        }


        public void HandleClient(object obj)
        {
            // retrieve client from parameter passed to thread
            TcpClient client = (TcpClient)obj;
            StreamWriter sWriter = new StreamWriter(client.GetStream(), Encoding.ASCII);
            StreamReader sReader = new StreamReader(client.GetStream(), Encoding.ASCII);





            string ip = "" + client.Client.RemoteEndPoint;


            //timeout for readline 

            sReader.BaseStream.ReadTimeout = 2500;



            // you could use the NetworkStream to read and write, 

            // but there is no forcing flush, even when requested

            Boolean bClientConnected = true;

            //sReader.BaseStream.ReadTimeout = 2500;



            while (bClientConnected)
            {


                if (client.Connected)
                {

                    try
                    {

                       

                        sData = sReader.ReadLine();



                        if (string.IsNullOrEmpty(sData))
                        {
                            StatusMessage = "[Client]" + ip + " has disconnected";
                            Console.WriteLine(StatusMessage);
                            IsNewStatusTriggered = true;

                            sWriter.Close();
                            sReader.Close();
                            client.Close();
                            bClientConnected = false;

                        }

                        else
                        {
                            // shows content on the console.
                            StatusMessage = "[Client]" + client.Client.RemoteEndPoint + " " + sData;
                            Console.WriteLine(StatusMessage);
                            IsNewStatusTriggered = true;








                        }



                    }

                    catch (Exception ex)
                    {


                    }
                }
                else
                {
                    try
                    {


                        StatusMessage = "[Client]" + ip + " has disconnected";
                        Console.WriteLine(StatusMessage);
                        IsNewStatusTriggered = true;


                        sWriter.Close();
                        sReader.Close();
                        client.Close();
                        client.Dispose();


                        bClientConnected = false;

                    }

                    catch (Exception ex) { }
                }
            }

        }


    }
}
