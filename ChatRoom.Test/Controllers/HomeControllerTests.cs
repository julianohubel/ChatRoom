using Moq;
using ChatRoom.Controllers;
using NUnit.Framework;
using ChatRoom.Data;
using Microsoft.EntityFrameworkCore;
using ChatRoom.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ChatRoom.Test.Controllers
{
    public class HomeControllerTests
    {

        //private Mock<ApplicationDbContext> _context;
        //private Mock<UserManager<User>> _userManager;
        //private Mock<ControllerBase> _controllerBase;

        //[SetUp]
        //public void Setup()
        //{
        //    _controllerBase = new Mock<ControllerBase>();


        //    var mockSet = new Mock<DbSet<Message>>();
        //    _context = new Mock<ApplicationDbContext>();
        //    _context.Setup(m => m.Messages).Returns(mockSet.Object);

        //    _userManager = new Mock<UserManager<User>>();
        //    _userManager.Setup(u => u.GetUserAsync(_controllerBase.Object.User))
        //        .ReturnsAsync(new User()
        //        {
        //            UserName = "a"
        //        });
        //}

        //[Test]
        //public void Index_RetursView()
        //{
        //    var home = new HomeController(_context.Object, _userManager.Object);

        //    var result = home.Index();

        //    Assert.That(result, Is.TypeOf<ViewResult>());
        //}
    }
}
