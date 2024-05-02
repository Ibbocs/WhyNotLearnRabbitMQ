using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublisherAPP
{
    public class MessageDurability
    {
        public async void MessageDurabilityPublisher()
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.Uri = new Uri("amqps://qjcekrph:ZfNZD8HRmxSIngiZxu96wu_1jBtNoOSa@prawn.rmq.cloudamqp.com/qjcekrph");

            using IConnection connection = factory.CreateConnection();

            using IModel channel = connection.CreateModel();
                                                                    //durable olmasin dedim queue ucun
            channel.QueueDeclare("example-queue", exclusive: false, durable:true);

            byte[] message = Encoding.UTF8.GetBytes("Ayy Hello");
            channel.BasicPublish(exchange: "", routingKey: "example-queue", body: message);

            IBasicProperties properties = channel.CreateBasicProperties();
            properties.Persistent = true;//persist xususiyyetin acdim message ucun

            for (int i = 0; i < 100; i++)
            {
                await Task.Delay(300);
                byte[] message1 = Encoding.UTF8.GetBytes($"Ayy Hello {i}");
                channel.BasicPublish(exchange: "", routingKey: "example-queue", body: message1, basicProperties:properties);
            }
        }
    }
}
