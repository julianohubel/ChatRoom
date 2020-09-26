using ChatRoom.Controllers.Api;
using ChatRoom.Handlers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;

namespace ChatRoom.Test.Controllers
{
    public class StockControllerTests
    {

        private Mock<RabbitHandler> _rabbit;
        private Mock<StockApiHandler> _stock;

        [SetUp]        
        public void Setup()
        {
            _rabbit = new Mock<RabbitHandler>();
            _stock = new Mock<StockApiHandler>();    
        }

        [Test]
        public void Get_ReturnsOk()
        {
            var stock = new StockController(_rabbit.Object, _stock.Object );

            var result = stock.Get("a");

            Assert.That(result, Is.TypeOf<OkResult>());
        }
    }
}
