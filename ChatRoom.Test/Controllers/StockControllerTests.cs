using ChatRoom.Controllers.Api;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;

namespace ChatRoom.Test.Controllers
{
    public class StockControllerTests
    {

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Get_ReturnsOk()
        {
            var home = new StockController();

            var result = home.Get("a");

            Assert.That(result, Is.TypeOf<OkResult>());
        }
    }
}
