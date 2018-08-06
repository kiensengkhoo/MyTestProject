using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace app.ClientMultiConnection
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket client;
            byte[] buf = new byte[1024];
            string input;
            Console.Write("Input a IPAddress:");
            string ip = Console.ReadLine();
            IPAddress local = IPAddress.Parse(ip);
            IPEndPoint iep = new IPEndPoint(local, 8080);
            try
            {
                client = new Socket(AddressFamily.InterNetwork, SocketType.Stream,
                ProtocolType.Tcp);
                client.Connect(iep);
            }
            catch (SocketException)
            {
                Console.WriteLine("Server connect is error...");
                return;
            }
            finally
            {

            }

            while (true)
            {
                //在控制台上输入一条消息  
                input = Console.ReadLine();
                //输入exit，可以断开与服务器的连接  
                if (input == "exit")
                {
                    break;
                }
                client.Send(Encoding.ASCII.GetBytes(input));
                //得到实际收到的字节总数  
                int rec = client.Receive(buf);
                Console.WriteLine(Encoding.ASCII.GetString(buf, 0, rec));

            }
            Console.WriteLine("Server connection is closed.");
            client.Close();
        }
    }
}
