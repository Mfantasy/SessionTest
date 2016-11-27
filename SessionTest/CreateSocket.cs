using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SessionTest
{
    public static class CreateSocket
    {
        static void CreateServer(object pt)
        {
            int port = (int)pt;
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            var ipe = new IPEndPoint(IPAddress.Any, port);
            server.Bind(ipe);
            server.Listen(10);

            //不断监听端口
            while (true)
            {
                Socket socket = server.Accept();
                //获取
                var x = socket.RemoteEndPoint as IPEndPoint;
#if DEBUG
                socket.Send(Encoding.Default.GetBytes("Server:发送数据测试。"));
#endif
                ThreadPool.QueueUserWorkItem(ServerReceive, socket);
            }
        }
        static void ServerReceive(object o)
        {
            byte[] result = new byte[64];
            Socket socket = o as Socket;
            while (true)
            {
                try
                {
                    socket.Receive(result);
                    //处理数据
                    string str = Encoding.Default.GetString(result);
                    Console.WriteLine(str);
                }
                catch (Exception ex)
                {
#if DEBUG
                    MessageBox.Show(ex.Message);
#endif
                    break;
                }
            }

        }

        public static void CreateServer(int pt)
        {
            ThreadPool.QueueUserWorkItem(CreateServer, pt);
        }

        public static void CreateTestClient(string ip, int port)
        {
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ipa = IPAddress.Parse(ip);
            IPEndPoint ipe = new IPEndPoint(ipa, port);
            client.Connect(ipe);
            byte[] bts = new byte[64];
            //client.Receive(bts);
            //Console.WriteLine("接收测试:");
            //string xx = Encoding.Default.GetString(bts);
            //Console.WriteLine(xx);
            //Console.WriteLine("客户端接收数据成功!");
            //Console.WriteLine("发送测试:");
            client.Send(Encoding.Default.GetBytes("客户端发送数据成功!"));
            Thread.Sleep(1000);
            client.Shutdown(SocketShutdown.Both);
            client.Close();
        }
    }
}
