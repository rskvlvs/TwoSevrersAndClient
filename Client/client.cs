using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class client : IDisposable
    {
        public IPAddress ipAdress{get; private set;}

        public IPEndPoint ipEndPoint { get; private set;}

        public Socket Sender { get; private set;}

        public client(IPAddress ipAdress, int port)
        {
            this.ipAdress = ipAdress;
            ipEndPoint = new IPEndPoint(ipAdress, port);
            Sender = new Socket(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        }

        public async Task Connect(int port)
        {
            ipAdress = Dns.GetHostEntry("localhost").AddressList[0];
            ipEndPoint = new IPEndPoint(ipAdress, port);
            Sender = new Socket(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                await Sender.ConnectAsync(ipEndPoint);
                Console.WriteLine("Соединение произошло");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public async Task SendData(string data)
        {
            try
            {
                byte[] message = Encoding.ASCII.GetBytes(data);
                await Sender.SendAsync(message, SocketFlags.None);
                Console.WriteLine($"Данные отправлены на {Sender.RemoteEndPoint}");
                byte[] buffer = new byte[1024];
                int bytesRead = await Sender.ReceiveAsync(buffer, SocketFlags.None);
                StringBuilder response = new StringBuilder(Encoding.ASCII.GetString(buffer, 0, bytesRead));
                Console.WriteLine($"Получен ответ от сервера: {response}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void Dispose()
        {
            Sender.Dispose();
            Console.WriteLine("Соединение с сервером прервано");
        }
    }
}
