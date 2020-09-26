using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatRoom.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ChatRoom.Controllers.Api
{
    public class RabbitMQController : Controller
    {
        [HttpGet]
        public ActionResult GetMessages()
        {
            var receive = BasicReceive();
            if (receive != null)
            {               
                return Ok(JsonConvert.DeserializeObject<Stock>(receive));
            }
            return Ok(null);
        }

        private string ReceiveRabbitMQ()
        {
            var ret = "";
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "stock",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);                  
                };

                

                channel.BasicConsume(queue: "stock",
                                     autoAck: true,
                                     consumer: consumer);
                
            }

            return ret;
        }

        private string BasicReceive()
        {
            try
            {
                BasicGetResult data;
                var factory = new ConnectionFactory() { HostName = "localhost" };
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    data = channel.BasicGet("stock", true);
                    channel.BasicAck(data.DeliveryTag, false);
                }
                return data != null ? System.Text.Encoding.UTF8.GetString(data.Body.ToArray()) : null;
            }
            catch
            {
                return null;
            }
        }

    
    }
}
