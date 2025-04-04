using System;
using System.ServiceModel;
using WCFMyServiceLibrary;

namespace my_host
{
    class Program
    {
        static void Main(string[] args)
        {
            // Адрес сервиса по протоколу net.tcp
            Uri baseAddress = new Uri("net.tcp://localhost:9000/ChatService");

            // Создаем и настраиваем ServiceHost
            using (ServiceHost host = new ServiceHost(typeof(ChatService), baseAddress))
            {
                NetTcpBinding binding = new NetTcpBinding();
                host.AddServiceEndpoint(typeof(IChatService), binding, "");

                host.Open();
                Console.WriteLine("Сервер запущен. Ожидание подключений...");
                Console.WriteLine("Для завершения работы сервера нажмите Enter.");
                Console.ReadLine();
                host.Close();
            }
        }
    }
}