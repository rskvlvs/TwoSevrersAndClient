
using System.Net;

namespace Client
{
    class Program
    {
        static async Task Main()
        {
            int FirstSocket = 15;
            int SecondSocket = 30;
            string delenie = "----------------------------------"; 
            var ipAddress = (await Dns.GetHostEntryAsync("localhost")).AddressList[0];

            var client = new client(ipAddress, FirstSocket);
            await client.Connect(FirstSocket);
            await client.SendData("Fcheck_number_one");
            await client.SendData("Fcheck_number_two");
            await client.SendData("Fcheck_number_three");
            client.Dispose();
            Console.WriteLine(delenie);

            ipAddress = (await Dns.GetHostEntryAsync("localhost")).AddressList[0];
            client = new client(ipAddress, FirstSocket);
            await client.Connect(FirstSocket);
            await client.SendData("Scheck_number_one");
            await client.SendData("Scheck_number_two");
            client.Dispose();
            Console.WriteLine(delenie);

            ipAddress = (await Dns.GetHostEntryAsync("localhost")).AddressList[0];
            client = new client(ipAddress, SecondSocket);
            await client.Connect(SecondSocket);
            await client.SendData("Fcheck_number_one");
            await client.SendData("Fcheck_number_two");
            await client.SendData("Fcheck_number_three");
            client.Dispose();
            Console.WriteLine(delenie);

            ipAddress = (await Dns.GetHostEntryAsync("localhost")).AddressList[0];
            client = new client(ipAddress, SecondSocket);
            await client.Connect(SecondSocket);
            await client.SendData("Scheck_number_one");
            await client.SendData("Scheck_number_two");
            client.Dispose();
            Console.WriteLine(delenie);
        }
    }
} 

