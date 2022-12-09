using Azure.Messaging.ServiceBus;

namespace MongoExample.MessageBusObject
{
    public class Sender
    {
        private string _connectionString = "Endpoint=sb://quang-test-1-namespace.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=UZYj0TSyGHZQM+Fxjb7IMGaJhuzPaHWNx+BeCevmGbk=";
        private string _queueName = "queue-1";
        private ServiceBusSender _sender;

        public Sender(string connectionString, string queueName)
        {
            var clientOptions = new ServiceBusClientOptions()
            {
                TransportType = ServiceBusTransportType.AmqpWebSockets
            };
            ServiceBusClient client = new ServiceBusClient(connectionString, clientOptions);
            this._sender = client.CreateSender(queueName);
        }

        public async Task sendMessage(string content)
        {
            ServiceBusMessage message = new ServiceBusMessage(content);
            await this._sender.SendMessageAsync(message);
        }
    }
}
