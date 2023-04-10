using AutoFixture;
using AutoMapper.Internal;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationModule.DTO;
using VacationModule.Services.Interfaces;
using VacationModule.WebAPI.Controllers;

namespace ApiTest
{
    [TestClass]
    public class AuthControllerTest
    {
        private Mock<IUserService> _userServiceMock;
        private Fixture _fixture;

        private AuthController _controller;

        public AuthControllerTest()
        {
            _fixture = new Fixture();
            _userServiceMock = new Mock<IUserService>();
        }

        [TestMethod]
        public void Post_Register_Error()
        {
            var request = _fixture.Create<RegisterDTO>();
            _userServiceMock.Setup(service => service.Register(request)).Throws(new ArgumentException());
            _controller = new AuthController(_userServiceMock.Object);
            var result = _controller.Register(request);

            var obj = result as ObjectResult;
            Assert.AreEqual(500, obj?.StatusCode);
        }

        [TestMethod]
        public void Post_Login_Ok()
        {
            var request = _fixture.Create<LoginDTO>();
            var response = _fixture.Create<string>();
            _userServiceMock.Setup(service => service.Login(request)).Returns(response);
            _controller = new AuthController(_userServiceMock.Object);
            var result = _controller.Login(request);

            var obj = result as ObjectResult;
            Assert.AreEqual(200, obj?.StatusCode);
        }

        [TestMethod]
        public void Post_Login_Error()
        {
            var request = _fixture.Create<LoginDTO>();
            _userServiceMock.Setup(service => service.Login(request)).Throws(new ArgumentException());
            _controller = new AuthController(_userServiceMock.Object);
            var result = _controller.Login(request);

            var obj = result as ObjectResult;
            Assert.AreEqual(500, obj?.StatusCode);
        }
    }
}
