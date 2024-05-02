// See https://aka.ms/new-console-template for more information
using ConsumerAPP;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

//Console.WriteLine("Hello, World!");
Console.Title = "Consumer";

//BasicRabbitMQ basicRabbitMQ = new BasicRabbitMQ();
//basicRabbitMQ.BasicConsumer();

#region Acknowledgement
ConnectionFactory factory = new ConnectionFactory();
factory.Uri = new Uri("amqps://qjcekrph:ZfNZD8HRmxSIngiZxu96wu_1jBtNoOSa@prawn.rmq.cloudamqp.com/qjcekrph");
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();
channel.QueueDeclare("example-queue", exclusive: false, durable: true);
EventingBasicConsumer consumer = new(channel);
                                             //burda silinmesin deye messageleri nizamladim
channel.BasicConsume(queue: "example-queue", autoAck: false, consumer);
consumer.Received += (sender, e) =>
{
    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
                                  //messagenin bir nov id'si deleveriTag sayilir.
    channel.BasicAck(deliveryTag: e.DeliveryTag, multiple: false);
};
#endregion

#region Temel Bir Consumer App
//ConnectionFactory factory = new ConnectionFactory();
////amqp url
//factory.Uri = new Uri("amqps://qjcekrph:ZfNZD8HRmxSIngiZxu96wu_1jBtNoOSa@prawn.rmq.cloudamqp.com/qjcekrph");

////2/Connection aktivlesdirme ve kanal acmaq
//using IConnection connection = factory.CreateConnection();

//using IModel channel = connection.CreateModel();

////3/Queue - burda eslinde queue yaratmiriq, olan queue tanidiriq sisteme, publisherde olan ile eyni olmalidi her bir parametirlerine qeder.
//channel.QueueDeclare("example-queue", exclusive: false);
////exclusive, autoDelete falan nedi Publisherde yazmisam.

////4/Queue message oxumaq.
////RabbitMq messageleri byte tipinde alir deye, normal olaraq bize her sey byte tipinde gelir ve onu biz stringe cevirmeliyik.
////channaldan melumat oxumaq ucun event isledirik.
//EventingBasicConsumer consumer = new(channel);
//channel.BasicConsume(queue: "example-queue", autoAck: false, consumer);
////autoAck- message islendikden sora silinsin ya yox.

////eventle isleyirik deye, delegete ile messageni oxuyuruq.
//consumer.Received += (sender, e) =>
//{
//    //e.Body queue de olan butunsel datani verir, e.body.ToArray ya da .Span ile ise sadece messageni goture bilirik byte formasinda.
//    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
//};
#endregion
Console.ReadLine();
