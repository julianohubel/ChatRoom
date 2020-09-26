using ChatRoom.Controllers.Api;
using ChatRoom.Handlers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatRoom.Test.Controllers
{
    public class RabbitMQControllerTests
    {

        private Mock<RabbitHandler> _rabbit;        

        [SetUp]
        public void Setup()
        {
            _rabbit = new Mock<RabbitHandler>();            
        }

        [Test]
        public void Get_ReturnsOk()
        {
            var rabbitMQController = new RabbitMQController(_rabbit.Object);

            var result = rabbitMQController.GetMessages();

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }
    }
}
