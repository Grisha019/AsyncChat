using System;
using System.ServiceModel;
using System.Threading;
using WCFMyServiceLibrary;

namespace my_client
{
    // Реализация callback интерфейса, который обрабатывает входящие сообщения от сервера
    public class ChatCallback : IChatCallback
    {
        public void ReceiveMessage(string message)
        {
            Console.WriteLine("Сообщение от сервера: " + message);
        }
    }

    class Programs
    {
        static void Main(string[] args)
        {
            // Создаем экземпляр callback для обработки сообщений
            ChatCallback callback = new ChatCallback();
            InstanceContext context = new InstanceContext(callback);
            NetTcpBinding binding = new NetTcpBinding();
            EndpointAddress endpoint = new EndpointAddress("net.tcp://localhost:9000/ChatService");

            // Создаем duplex канал для связи с сервером
            DuplexChannelFactory<IChatService> factory = new DuplexChannelFactory<IChatService>(context, binding, endpoint);
            IChatService proxy = factory.CreateChannel();

            // Подключаемся к серверу
            proxy.Connect();

            // Поток для обработки ввода пользователя
            Thread inputThread = new Thread(() =>
            {
                while (true)
                {
                    string message = Console.ReadLine();
                    if (!string.IsNullOrEmpty(message))
                    {
                        proxy.SendMessage(message);
                    }
                }
            });

            inputThread.IsBackground = true;
            inputThread.Start();

            // Основной поток ожидает завершения (например, нажатия Enter)
            Console.WriteLine("Клиент запущен. Для завершения нажмите Enter.");
            Console.ReadLine();

            // Закрытие канала связи
            factory.Close();
        }
    }
}