using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MySecondServer
{
    public class mySecondServer : IDisposable
    {
        public IPAddress ipAdress { get; private set; }

        public IPEndPoint ipEndPoint { get; private set; }

        public Socket getter { get; private set; }

        public mySecondServer(IPAddress ipAdress, IPEndPoint ipEndPoint, Socket getter)
        {
            this.ipAdress = ipAdress;
            this.ipEndPoint = ipEndPoint;
            this.getter = getter;
        }

        public async Task GetAsync()
        {
            try
            {
                getter.Bind(ipEndPoint);
                Console.WriteLine("Сервер 2 запущен");
                getter.Listen();
                Console.WriteLine("Ожидаю соединения");
                while (true)
                {
                    var handler = await getter.AcceptAsync();
                    Console.WriteLine("Клиент подключен");
                    while (true)
                    {
                        var buffer = new byte[1024];
                        var str = new StringBuilder();
                        string message;
                        int count;
                        if ((count = await handler.ReceiveAsync(buffer, SocketFlags.None)) > 0)
                        {
                            message = Encoding.UTF8.GetString(buffer, 0, count);
                            str.Append($"{message}\n ");
                            Console.Write("На сервер пришли следующие сообщения: ");
                            Console.WriteLine(str.ToString());

                            //Отправка ответа клиенту
                            byte[] response = Encoding.UTF8.GetBytes($"I got it! ({str.ToString()})");
                            await handler.SendAsync(response, SocketFlags.None);

                        }
                        else
                        {
                            //Больше нет сообщений от клиента
                            Console.WriteLine("Клиент оборвал соединение");
                            break;
                        }
                    }
                    //Закрытие соединеиня
                    handler.Shutdown(SocketShutdown.Both);
                    //break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void Dispose()
        {

            getter.Dispose();
            Console.WriteLine("Соединение прервано");
        }
    }
}
