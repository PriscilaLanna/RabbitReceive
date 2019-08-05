using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Receive
{
    class Receive
    {
        static void Main(string[] args)
        {
           var factory = new ConnectionFactory(){HostName="localhost"};

           using(var ConnectionFactory = factory.CreateConnection())
            using(var channel = ConnectionFactory.CreateModel()){

                channel.QueueDeclare(queue:"FilaTeste");

                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += (model , ea)=>{
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(" [x] Received {0}", message);
                };

                channel.BasicConsume(queue:"FilaTeste",
                                     autoAck:true,
                                     consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}
