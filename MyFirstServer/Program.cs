using System.Net;
using System.Net.Sockets;

namespace MyFirstServer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            int FirstServerSocket = 15;

            var ipAddress = (await Dns.GetHostEntryAsync("localhost")).AddressList[0];

            var EndPoint = new IPEndPoint(ipAddress, FirstServerSocket);

            var socket = new Socket(EndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            {
                using var server = new myFirstServer(ipAddress, EndPoint, socket);
                await server.GetAsync();
            }

            //socket = new Socket(EndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            //{
            //    using var server1 = new myFirstServer(ipAddress, EndPoint, socket);
            //    await server1.GetAsync();
            //}
        }
    }
}