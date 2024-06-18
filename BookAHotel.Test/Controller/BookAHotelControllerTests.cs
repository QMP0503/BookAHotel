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
using BookAHotel.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;


namespace BookAHotel.Test.Controller
{
    public class BookAHotelControllerTests
    {
        private readonly IClientService _clientService;
        private readonly IRoomService _roomService;
        private readonly IBookingService _bookingService;
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        public BookAHotelControllerTests()
        {
            _logger = A.Fake<ILog>();
            _mapper = A.Fake<IMapper>();    
            _bookingService = A.Fake<IBookingService>();
            _clientService = A.Fake<IClientService>();
            _roomService = A.Fake<IRoomService>();
        }
        [Fact]//test case
        public void BookAHotelController_GetAllBooking_ReturnJsonResult()
        {
            //Arrange
            var bookingList = A.Fake<List<Booking>>();
           // bookingList.Add(A.Fake<Booking>());
            A.CallTo(() => _bookingService.ListBooking()).Returns(bookingList);
            var controller = new BookAHotelController(_bookingService, _clientService, _roomService, _logger, _mapper);
            //Act
            var result = controller.GetAllBooking();
            //Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(new JsonResult(bookingList));

        }
        [Fact]
        public void BookAHotelController_GetAllBooking_ReturnCatchedError() //testing try-catch 
        {
            //arrange
            A.CallTo(() => _bookingService.ListBooking()).Returns(null); //returns nothing to envoke error
            var controller = new BookAHotelController(_bookingService, _clientService, _roomService, _logger, _mapper);
            //Act
            var result = controller.GetAllBooking();
            //Assert
            result.Should().BeEquivalentTo(new JsonResult("Error. Try Again"));
        }
        [Fact]
        public void BookAHotelController_GetBooking_ReturnJsonResult()
        {
            //Arrange
            var client = A.Fake<Client>();
            client.Name = "Client 1";
            client.Id = 1;
            var booking = A.Fake<Booking>();
            booking.Client = client;
            booking.Room = A.Fake<Room>();
            A.CallTo(() => _bookingService.FindBooking(client.Name)).Returns(booking);
            var controller = new BookAHotelController(_bookingService, _clientService, _roomService, _logger, _mapper);
            //Act
            var result = controller.GetBooking(client.Name);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(new JsonResult($"{booking.Client.Name} booked {booking.Room.Name} from {booking.startDate} to {booking.endDate}"));
        }
        [Fact]
        public void BookAHotelController_GetBooking_ReturnCatchedError()
        {
            //Arrange
            var controller = new BookAHotelController(_bookingService, _clientService, _roomService, _logger, _mapper);
            //Act
            var result = controller.GetBooking(null);
            //Assert
            result.Should().BeEquivalentTo(new JsonResult("Error. Booking not found. Try Again"));
        }
        [Fact]
        public void BookAHotelController_GetClient_ReturnJsonResult()
        {
            //Arrange
            var client = A.Fake<Client>();
            client.Name = "Client 1";
            client.Id = 1;
            A.CallTo(() => _clientService.FindClient(client.Name)).Returns(client);
            var controller = new BookAHotelController(_bookingService, _clientService, _roomService, _logger, _mapper);
            //Act
            var result = controller.GetClient(client.Name);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(new JsonResult(client));
        }
        [Fact]
        public void BookAHotelController_GetClient_ReturnCatchedError()
        {
            //Arrange
            A.CallTo(() => _clientService.FindClient(null)).Returns(null);
            var controller = new BookAHotelController(_bookingService, _clientService, _roomService, _logger, _mapper);
            //Act
            var result = controller.GetClient(null);
            //Assert
            result.Should().BeEquivalentTo(new JsonResult("Error. Client not found. Try Again"));
        }
        [Fact]
        public void BookAHotelController_GetRoomList_ReturnJsonResult()
        {
            //Arrange
            var roomList = new List<Room>();
            RoomType roomType = A.Fake<RoomType>();
            roomType.RoomTypeName = RoomTypeNameEnum.Normal;
            for (int i = 0; i < 3; i++)
            {
                var room = A.Fake<Room>();
                room.Name = "Room 1";
                room.RoomType = roomType;
                roomList.Add(room);
            }
            A.CallTo(() => _roomService.RoomList("Room 1", "Normal")).Returns(roomList);
            var controller = new BookAHotelController(_bookingService, _clientService, _roomService, _logger, _mapper);
            //Act
            var result = controller.GetRoomList("Room 1", "Normal");
            //Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(new JsonResult(roomList));
        }
        [Fact]
        public void BookAHotelController_GetRoomList_ReturnCatchedError()
        {
            //Arrange
            A.CallTo(() => _roomService.RoomList(null, null)).Returns(null);
            var controller = new BookAHotelController(_bookingService, _clientService, _roomService, _logger, _mapper);
            //Act
            var result = controller.GetRoomList(null, null);
            //Assert
            result.Should().BeEquivalentTo(new JsonResult("Error. Try Again"));
        }
        [Fact]
        public void BookAHotelController_AddClient_ReturnJsonResult()
        {
            //Arrange
            var client = A.Fake<Client>();
            client.Name = "Client 1";
            A.CallTo(() => _clientService.AddClient(client.Name));
            var controller = new BookAHotelController(_bookingService, _clientService, _roomService, _logger, _mapper);
            //Act
            var result = controller.AddClient(client.Name);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(new JsonResult("client added"));
        }
        [Fact]
        public void BookAHotelController_AddBooking_ReturnJsonResult()
        {
            //Arrange
            var booking = A.Fake<Booking>();
            booking.Client = A.Fake<Client>();
            booking.Room = A.Fake<Room>();
            A.CallTo(() => _bookingService.AddBooking(booking.Client.Name, booking.Room.Name, booking.startDate.ToString(), booking.endDate.ToString(),0));
            var controller = new BookAHotelController(_bookingService, _clientService, _roomService, _logger, _mapper);
            //Act
            var result = controller.AddBooking(booking.Client.Name, booking.Room.Name, booking.startDate.ToString(), booking.endDate.ToString(), 0);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(new JsonResult("booking added"));
        }
        [Fact]
        public void BookAHotelController_UpdateBooking_ReturnJsonResult()
        {
            var booking = A.Fake<Booking>();
            booking.Client = A.Fake<Client>();
            booking.Room = A.Fake<Room>();
            A.CallTo(() => _bookingService.UpdateBooking(booking.Client.Name, booking.Room.Name, booking.startDate.ToString(), booking.endDate.ToString()));
            var controller = new BookAHotelController(_bookingService, _clientService, _roomService, _logger, _mapper);
            //Act
            var result = controller.UpdateBooking(booking.Client.Name, booking.Room.Name, booking.startDate.ToString(), booking.endDate.ToString());
            //Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(new JsonResult("booking updated"));
        }
        [Fact]
        public void BookAHotelController_CancelBooking_ReturnJsonResult()
        {
            string name = "Client 1";
            A.CallTo(() => _bookingService.CancelBooking(name));
            var controller = new BookAHotelController(_bookingService, _clientService, _roomService, _logger, _mapper);
            //Act
            var result = controller.CancelBooking(name);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(new JsonResult("booking deleted"));
        }
        [Fact]
        public void BookAHotelController_UpdateClient_ReturnJsonResult()
        {
            var client = A.Fake<Client>();
            client.Name = "Client 1";
            string newName = "Client 2";
            A.CallTo(() => _clientService.UpdateClient(client.Name, client.Status, newName));
            var controller = new BookAHotelController(_bookingService, _clientService, _roomService, _logger, _mapper);
            //Act
            var result = controller.UpdateClient(client.Name, client.Status, newName);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(new JsonResult("client updated"));
        }
        [Fact]
        public void BookAHotelController_DeleteClient_ReturnJsonResult()
        {
            string name = "Client 1";
            A.CallTo(() => _clientService.DeleteClient(name));
            var controller = new BookAHotelController(_bookingService, _clientService, _roomService, _logger, _mapper);
            //Act
            var result = controller.DeleteClient(name);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(new JsonResult("client deleted"));
        }
        
    }
}
