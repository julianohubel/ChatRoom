using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatRoom.Handlers
{
    public class RabbitHandler
    {
        public void SendRabbitMQ(string message)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "stock",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);
                
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "stock",
                                     basicProperties: null,
                                     body: body);
            }
        }

        public string BasicReceive()
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
