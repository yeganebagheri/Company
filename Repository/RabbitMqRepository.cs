using DapperASPNetCore.Contracts;
using DapperASPNetCore.Entities;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperASPNetCore.Repository
{
    public class RabbitMqRepository : IRabbitMqRepository
    {
        public void Producer(Company company)
        {

            var factory = new ConnectionFactory() { HostName = "localhost" /*, VirtualHost="/" , UserName= "guest ", Password= "guest "*//*, Port= 15672*/ };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel()) 
            {
                channel.QueueDeclare(queue: "hello",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var message = string.Format(" {0} created!", company.Name); 
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "hello",
                                     basicProperties: null,
                                     body: body);
                //Console.WriteLine(" [x] Sent {0}", message);
            }

           // Console.WriteLine(" Press [enter] to exit.");
            //Console.ReadLine();
        }
    }
}

