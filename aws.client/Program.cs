using System;
using System.Net.Sockets;
using System.Text;

namespace aws.client
{
    class Program
    {
        NetworkStream myNetworkStream;
        TcpClient myTcpClient;

        static void Main(string[] args)
        {
            Program myNetworkClient = new Program();

            Console.WriteLine("Input server IP:");
            string hostName = Console.ReadLine();
            Console.WriteLine("Input server Port:");
            int connectPort = 8080; //int.Parse(Console.ReadLine());
            myNetworkClient.myTcpClient = new TcpClient();
            try
            {
                myNetworkClient.myTcpClient.Connect(hostName, connectPort);
                Console.WriteLine("Connect Success...\n");
            }
            catch
            {
                Console.WriteLine("Server {0} port {1} cant connect...", hostName, connectPort);
                return;
            }

            while (true)
            {
                myNetworkClient.WriteData();
                myNetworkClient.ReadData();
            }
        }
        
        void WriteData()
        {
            Console.WriteLine("Input Msg:");
            string strTest = Console.ReadLine();
            byte[] myBytes = Encoding.ASCII.GetBytes(strTest);
            
            myNetworkStream = myTcpClient.GetStream();
            myNetworkStream.Write(myBytes, 0, myBytes.Length);
        }
        
        void ReadData()
        {
            Console.WriteLine("Output Msg:");
            int bufferSize = myTcpClient.ReceiveBufferSize;
            byte[] myBufferBytes = new byte[bufferSize];
            myNetworkStream.Read(myBufferBytes, 0, bufferSize);
            string message = Encoding.ASCII.GetString(myBufferBytes, 0, bufferSize).Replace(" ", "").Replace("\0", "");
            Console.WriteLine(message);
            Console.WriteLine("---------------------------------");
        }
    }
}
