using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatRoom.Models;
using ChatRoom.Handlers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ChatRoom.Controllers.Api
{
    public class RabbitMQController : Controller
    {
        private RabbitHandler _rabbit;
        public RabbitMQController(RabbitHandler rabbit)
        {
            _rabbit = rabbit;
        }
        [HttpGet]
        public ActionResult GetMessages()
        {
            var receive = _rabbit.BasicReceive();
            if (receive != null)
            {               
                return Ok(JsonConvert.DeserializeObject<Stock>(receive));
            }
            return Ok(null);
        }           

    
    }
}
