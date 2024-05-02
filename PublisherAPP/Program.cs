// See https://aka.ms/new-console-template for more information
using PublisherAPP;

//Console.WriteLine("Hello, World!");
Console.Title = "Publisher";

//BasicRabbitMQ basicRabbitMQ = new BasicRabbitMQ();
//basicRabbitMQ.BasicPublisher();
MessageDurability messageDurability = new MessageDurability();
messageDurability.MessageDurabilityPublisher();
Console.ReadLine();
