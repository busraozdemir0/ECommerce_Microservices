using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ECommerce.MessageBus
{
    // Bu sinif Microsoft Azure Service Bus kullanilarak mesaj yayinlamak icin kullanilmaktadir.
    public class MessageBus : IMessageBus
    {
        // Microsoft Azure'da olusturulan Service Bus ile Shared access policies ayarı altında yer alan Primary Connection String 
        private string connectionString = "Endpoint=sb://mangoweb.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=HjoslS58pPHtAULb0tay/jx4Ys0+MO5/R+ASbCcFTG0=";
        public async Task PublishMessage(object message, string topic_queue_Name)
        {
            await using var client = new ServiceBusClient(connectionString);

            ServiceBusSender sender = client.CreateSender(topic_queue_Name);

            var jsonMessage = JsonConvert.SerializeObject(message);
            ServiceBusMessage finalMessage = new ServiceBusMessage(Encoding.UTF8.GetBytes(jsonMessage))
            {
                CorrelationId = Guid.NewGuid().ToString(), // Mesaja benzersiz bir id atanir
            };

            await sender.SendMessageAsync(finalMessage); // Mesaj belirtilen kuyruga gonderilir
            await client.DisposeAsync(); // En son nesne kapatilir
        }
    }
}
