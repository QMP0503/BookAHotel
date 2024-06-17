using Castle.Core.Logging;
using FakeItEasy;
using BookAHotel.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookAHotel.Models;
using BookAHotel.Controllers;
using log4net;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;


namespace BookAHotel.Test.Controller
{
    public class BookAHotelControllerTests
    {
        private readonly IClientService _clientService;
        private readonly IRoomService _roomService;
        private readonly IBookingService _bookingService;
        private readonly ILog _logger;
        public BookAHotelControllerTests()
        {
            _logger = A.Fake<ILog>();
            _bookingService = A.Fake<IBookingService>();
            _clientService = A.Fake<IClientService>();
            _roomService = A.Fake<IRoomService>();
        }
        [Fact]//test case
        public void BookAHotelController_GetAllBooking_ReturnJsonResult()
        {
            //Arrange
            var bookings = A.Fake<ICollection<Booking>>();
            var bookingList = A.Fake<List<Booking>>();
            A.CallTo(() => _bookingService.ListBooking(null)).Returns(bookingList);
            var controller = new BookAHotelController(_bookingService, _clientService, _roomService, _logger);   
            //Act
            var result = controller.GetAllBooking();
            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(JsonResult));
        }
        [Fact]
        public void BookAHotelController_GetBooking_ReturnJsonResult()
        {
            //Arrange
            var client = A.Fake<Client>();
            var booking = A.Fake<Booking>();
            A.CallTo(() => _bookingService.FindBooking(client.Name)).Returns(booking);
            var controller = new BookAHotelController(_bookingService, _clientService, _roomService, _logger);
            //Act
            var result = controller.GetBooking(client.Name);
            var catchResponce = controller.GetBooking(null);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(JsonResult));
            catchResponce.Should().BeEquivalentTo(new JsonResult("Error. Booking not found. Try Again"));
        }

    }
}
