using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationModule.DTO;
using VacationModule.Services.Constants;
using VacationModule.Services.Interfaces;
using VacationModule.WebAPI.Controllers;

namespace ApiTest
{
    [TestClass]
    public class HolidayControllerTest
    {

        private Mock<IQueryService> _queryServiceMock;
        private Mock<IVacationRequestService> _requestServiceMock;
        private Fixture _fixture;

        private HolidayController _controller;

        public HolidayControllerTest()
        {
            _fixture = new Fixture();
            _queryServiceMock = new Mock<IQueryService>();
            _requestServiceMock = new Mock<IVacationRequestService>(); 
        }

        [TestMethod]
        public async Task Get_Holidays_OK()
        {
            
            var holidayList = _fixture.CreateMany<NationalHolidayDTO>(15).ToList();
            _queryServiceMock.Setup(service => service.nationalHolidays(2023)).ReturnsAsync(holidayList);

            _controller = new HolidayController(_queryServiceMock.Object, _requestServiceMock.Object);

            var result = await _controller.getHolidays();

            var obj = result as ObjectResult;

            Assert.AreEqual(200, obj?.StatusCode);
        }

        [TestMethod]
        public async Task Get_Holidays_Error()
        {
            _queryServiceMock.Setup(service => service.nationalHolidays(2023)).ThrowsAsync(new ArgumentException());
            _controller = new HolidayController(_queryServiceMock.Object, _requestServiceMock.Object);

            var result = await _controller.getHolidays();

            var obj = result as ObjectResult;

            Assert.AreEqual(500, obj?.StatusCode);
        }



        [TestMethod]
        public async Task Get_HolidayList_OK()
        {
            var holidayList = _fixture.CreateMany<DateTime>(15).ToList();
            _queryServiceMock.Setup(service => service.holidayList(2023)).ReturnsAsync(holidayList);
            _controller = new HolidayController(_queryServiceMock.Object, _requestServiceMock.Object);

            var result = await _controller.getHolidayList();
            var obj = result as ObjectResult;
            Assert.AreEqual(200, obj?.StatusCode);
        }

        [TestMethod]
        public async Task Get_HolidayList_Error()
        {
            _queryServiceMock.Setup(service => service.holidayList(2023)).ThrowsAsync(new ArgumentException());
            _controller = new HolidayController(_queryServiceMock.Object, _requestServiceMock.Object);

            var result = await _controller.getHolidayList();
            var obj = result as ObjectResult;
            Assert.AreEqual(500, obj?.StatusCode);
        }


        [TestMethod]
        public void Get_MyRequests_Ok()
        {
            var myRequests = _fixture.CreateMany<VacationRequestDTO>(10).ToList();
            _requestServiceMock.Setup(service => service.getMyRequests()).Returns(myRequests);
            _controller = new HolidayController(_queryServiceMock.Object, _requestServiceMock.Object);

            var result = _controller.myVacationRequests();
            var obj = result as ObjectResult;
            Assert.AreEqual(200, obj?.StatusCode);
        }

        [TestMethod]
        public void Get_MyRequests_Error()
        {
            _requestServiceMock.Setup(service => service.getMyRequests()).Throws(new ArgumentException());
            _controller = new HolidayController(_queryServiceMock.Object, _requestServiceMock.Object);

            var result = _controller.myVacationRequests();
            var obj = result as ObjectResult;
            Assert.AreEqual(500, obj?.StatusCode);
        }

        [TestMethod]
        public void Get_AllRequests_Ok()
        {
            var allRequsts = _fixture.CreateMany<AdminRequestsDTO>(100).ToList();
            _requestServiceMock.Setup(service => service.getAllRequests()).Returns(allRequsts);
            _controller = new HolidayController(_queryServiceMock.Object, _requestServiceMock.Object);

            var result = _controller.allRequests();
            var obj = result as ObjectResult;
            Assert.AreEqual(200, obj?.StatusCode);
        }


        [TestMethod]
        public async Task Post_Request_Ok()
        {
            FormVacationRequestDTO form = new FormVacationRequestDTO()
            {
                startDay = 1,
                startMonth = 1,
                startYear = 2023,
                endDay = 5,
                endMonth = 1,
                endYear = 2023
            };
            VacationRequestDTO response = new VacationRequestDTO()
            {
                Id = 1,
                requestedDays = new List<DateTime>(){ new DateTime(2023, 1, 3), new DateTime(2023, 1, 4), new DateTime(2023, 1, 5) }
            };
            _requestServiceMock.Setup(service => service.makeVacationRequest(form)).ReturnsAsync(response);
            _controller = new HolidayController(_queryServiceMock.Object, _requestServiceMock.Object);
            var result = await _controller.requestHoliday(form);
            var obj =  result as ObjectResult;
            Assert.AreEqual(200, obj?.StatusCode);
        }

        [TestMethod]
        public async Task Post_Request_Error()
        {
            FormVacationRequestDTO form = new FormVacationRequestDTO()
            {
                startDay = 1,
                startMonth = 1,
                startYear = 2023,
                endDay = 5,
                endMonth = 1,
                endYear = 2023
            };
            _requestServiceMock.Setup(service => service.makeVacationRequest(form)).ThrowsAsync(new ArgumentException());
            _controller = new HolidayController(_queryServiceMock.Object, _requestServiceMock.Object);
            var result = await _controller.requestHoliday(form);
            var obj = result as ObjectResult;
            Assert.AreEqual(500, obj?.StatusCode);
        }

        [TestMethod]
        public async Task Put_ModifyRequest_Ok()
        {
            ModifyRequestDTO form = new ModifyRequestDTO()
            {
                Id = 1,
                startDay = 1,
                startMonth = 1,
                startYear = 2023,
                endDay = 5,
                endMonth = 1,
                endYear = 2023
            };
            VacationRequestDTO response = new VacationRequestDTO()
            {
                Id = 1,
                requestedDays = new List<DateTime>() { new DateTime(2023, 1, 3), new DateTime(2023, 1, 4), new DateTime(2023, 1, 5) }
            };
            _requestServiceMock.Setup(service => service.modifyRequest(form)).ReturnsAsync(response);
            _controller = new HolidayController(_queryServiceMock.Object, _requestServiceMock.Object);
            var result = await _controller.modifyRequest(form);
            var obj = result as ObjectResult;
            Assert.AreEqual(200, obj?.StatusCode);
        }

        [TestMethod]
        public async Task Put_ModifyRequest_Error()
        {
            ModifyRequestDTO form = new ModifyRequestDTO()
            {
                Id = 1,
                startDay = 1,
                startMonth = 1,
                startYear = 2023,
                endDay = 5,
                endMonth = 1,
                endYear = 2023
            };
            _requestServiceMock.Setup(service => service.modifyRequest(form)).ThrowsAsync(new ArgumentException());
            _controller = new HolidayController(_queryServiceMock.Object, _requestServiceMock.Object);
            var result = await _controller.modifyRequest(form);
            var obj = result as ObjectResult;
            Assert.AreEqual(500, obj?.StatusCode);
        }

        [TestMethod]
        public void Get_AvailableDays_OK()
        {
            int availableDays = _fixture.Create<int>();
            _requestServiceMock.Setup(service => service.getAvailableDays(Year.CurrentYear)).Returns(availableDays);
            _controller = new HolidayController(_queryServiceMock.Object, _requestServiceMock.Object);

            var result = _controller.getAvailableDays(Year.CurrentYear);
            var obj = result as ObjectResult;
            Assert.AreEqual(200, obj?.StatusCode);
        }

        [TestMethod]
        public void Get_AvailableDays_Error()
        {
            _requestServiceMock.Setup(service => service.getAvailableDays(Year.CurrentYear)).Throws(new ArgumentException());
            _controller = new HolidayController(_queryServiceMock.Object, _requestServiceMock.Object);
            var result = _controller.getAvailableDays(Year.CurrentYear);
            var obj = result as ObjectResult;
            Assert.AreEqual(500, obj?.StatusCode);
        }

    }
}
