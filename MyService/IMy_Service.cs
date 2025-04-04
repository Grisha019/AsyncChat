using System;
using System.ServiceModel;

using System.ServiceModel;


namespace WCFMyServiceLibrary
{

    // Интерфейс для обратного вызова клиенту
    public interface IChatCallback
    {
        [OperationContract(IsOneWay = true)]
        void ReceiveMessage(string message);
    }

    // Двунаправленный контракт сервиса
    [ServiceContract(CallbackContract = typeof(IChatCallback))]
    public interface IChatService
    {
        // Метод для подключения клиента к серверу
        [OperationContract(IsOneWay = true)]
        void Connect();

        // Метод для отправки сообщения серверу
        [OperationContract(IsOneWay = true)]
        void SendMessage(string message);
    }
}