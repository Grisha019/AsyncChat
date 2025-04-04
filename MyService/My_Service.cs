using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel;

namespace WCFMyServiceLibrary
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class ChatService : IChatService
    {
        // Список подключённых клиентов
        private List<IChatCallback> clients = new List<IChatCallback>();

        public void Connect()
        {
            IChatCallback callback = OperationContext.Current.GetCallbackChannel<IChatCallback>();
            if (!clients.Contains(callback))
            {
                clients.Add(callback);
                Console.WriteLine("Клиент подключен.");
            }
        }

        public void SendMessage(string message)
        {
            Console.WriteLine("Получено сообщение: " + message);
            // Рассылка сообщения всем клиентам
            foreach (var client in clients)
            {
                try
                {
                    client.ReceiveMessage(message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ошибка при отправке сообщения клиенту: " + ex.Message);
                    // Здесь можно реализовать удаление клиента из списка при разрыве соединения
                }
            }
        }
    }
}