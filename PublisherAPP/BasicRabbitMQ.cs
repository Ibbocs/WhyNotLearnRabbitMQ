using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublisherAPP
{
    public class BasicRabbitMQ
    {
        public async void BasicPublisher()
        {
            //1/Connection
            ConnectionFactory factory = new ConnectionFactory();
            //amqp url
            factory.Uri = new Uri("amqps://qjcekrph:ZfNZD8HRmxSIngiZxu96wu_1jBtNoOSa@prawn.rmq.cloudamqp.com/qjcekrph");

            //2/Connection aktivlesdirme ve kanal acmaq
            using IConnection connection = factory.CreateConnection();

            using IModel channel = connection.CreateModel();

            //3/Queue 
            channel.QueueDeclare("example-queue", exclusive: false);
            //exclusive- birden cox baglanti ile bu queue el catib catmayacagin bildiri, eger true olarsa demeli bu queue yalniz publisher baglanib message gondere bilecek amma consummer baglana bilmeyecek, cunki consumer ona baglanamadan publisher baglanib isini gorecek ve sora bu queue silinecek.
            //autoDelete - bu da queue silinib silinmemeyin bildiri true olsa eger butun messageler bitse icinde.

            //4/Queue message gonderme
            //RabbitMq messageleri byte tipinde alir deye, her seyi byte cevirmeliyik.
            byte[] message = Encoding.UTF8.GetBytes("Ayy Hello");
            channel.BasicPublish(exchange: "", routingKey: "example-queue", body:message);
            //exchange nese vermirem deye, default yeni direct exchangeni isledir, burda da route key olaraq queue adin verirdik de.

            for (int i = 0; i < 100; i++)
            {
                await Task.Delay(300);
                byte[] message1 = Encoding.UTF8.GetBytes($"Ayy Hello {i}");
                channel.BasicPublish(exchange: "", routingKey: "example-queue", body: message1);
            }
        }
    }
}
