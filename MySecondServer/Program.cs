using System.Net;
using System.Net.Sockets;

namespace MySecondServer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            int SecondServerSocket = 30;

            var ipAddress = (await Dns.GetHostEntryAsync("localhost")).AddressList[0];

            var EndPoint = new IPEndPoint(ipAddress, SecondServerSocket);

            var socket = new Socket(EndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            {
                using var server = new mySecondServer(ipAddress, EndPoint, socket);
                await server.GetAsync();
            }
            //socket = new Socket(EndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            //{
            //    using var server1 = new mySecondServer(ipAddress, EndPoint, socket);
            //    await server1.GetAsync();
            //}
        }
    }
}