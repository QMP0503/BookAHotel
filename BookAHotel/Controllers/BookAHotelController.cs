using BookAHotel.Data;
using BookAHotel.Repository.IRepository;
using BookAHotel.Models;
using BookAHotel.Service.IService;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.AccessControl;

namespace BookAHotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookAHotelController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly IRoomService _roomService;
        private readonly IBookingService _bookingService;
        private readonly ILog _logger;

        public BookAHotelController(IBookingService bookingService, IClientService clientService, IRoomService roomService, ILog log)
        {
            _logger = log;
            _bookingService = bookingService;
            _clientService = clientService;
            _roomService = roomService;
        }
        [HttpGet("AllBooking")]
        public JsonResult GetAllBooking()
        {
            try
            {
                var bookingList = _bookingService.ListBooking(null);
                return new JsonResult(bookingList);
            }catch(Exception ex)
            {
                _logger.Error(ex.Message);
                return new JsonResult("Error. Try Again");
            }
        }

        [HttpGet("booking")]
        public JsonResult GetBooking(string ClientName) 
        {
            try
            {
                var booking = _bookingService.FindBooking(ClientName);
                if (booking == null)
                {
                    throw new NullReferenceException($"Booking not found. Value entered {ClientName}");
                }
                return new JsonResult($"{booking.Client.Name} booked {booking.Room.Name} from {booking.startDate} to {booking.endDate}");
            }
            catch (NullReferenceException ex)
            {
                _logger.Error(ex.Message);
                return new JsonResult("Error. Booking not found. Try Again");
            }
            
        }
        [HttpGet("client")]
        public JsonResult GetClient(string ClientName) 
        {
            try
            {
                var client = _clientService.FindClient(ClientName);
                if(client == null)
                {
                    throw new NullReferenceException(ClientName + "not found. Invalid value entered");
                }
                return new JsonResult(client);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return new JsonResult("Error. Client not found. Try Again");
            }
            
        }
        [HttpGet("room")]
        public JsonResult GetRoomList(string? RoomName, string? RoomType) //add function to filer room status
        {
            try
            {
                var roomList = _roomService.RoomList(RoomName, RoomType);
                if (roomList == null || roomList.Count == 0)
                {
                    throw new NullReferenceException(RoomName+ "or" + RoomType + "not found. Invalid value entered");
                }
                return new JsonResult(roomList);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return new JsonResult("Error. Try Again");
            }
            
        }
        [HttpPost("addClient")]
        public JsonResult AddClient(string Name)
        {
            try
            {
                _clientService.AddClient(Name);
                //_clientService.Save();
                return new JsonResult("client added");
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return new JsonResult("Error. Try Again");
            }
        }
        [HttpPost("addBooking")]
        public JsonResult AddBooking(string ClientName, string RoomName, string checkInDate, string checkOutDate, double discount)
        {
            try
            {
                _bookingService.AddBooking(ClientName, RoomName, checkInDate, checkOutDate, discount);
                _bookingService.Save();
                return new JsonResult("booking added");
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return new JsonResult("Error. Try Again");
            }
        }
        [HttpPost("updateBooking")]
        public JsonResult UpdateBooking(string ClientName, string RoomName, string checkInDate, string checkOutDate)
        {
            try
            {
                _bookingService.UpdateBooking(ClientName, RoomName, checkInDate, checkOutDate);
                _bookingService.Save();
                return new JsonResult("booking updated");
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return new JsonResult("Error. Try Again");
            }
        }
        [HttpPost("cancelBooking")]
        public JsonResult CancelBooking(string ClientName)
        {
            try
            {
                _bookingService.CancelBooking(ClientName);
                _bookingService.Save();
                return new JsonResult("booking deleted");
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return new JsonResult("Error. Try Again");
            }
        }
        [HttpPost("updateClient")]
        public JsonResult UpdateClient(string Name, string Status, string? NewName)
        {
            try
            {
                _clientService.UpdateClient(Name, Status, NewName);
                _clientService.Save();
                return new JsonResult("client updated");
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return new JsonResult("Error. Try Again");
            }
        }
        [HttpDelete("deleteClient")]
        public JsonResult DeleteClient(string Name)
        {
            try
            {
                _clientService.DeleteClient(Name);
                _clientService.Save();
                return new JsonResult("client deleted");
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return new JsonResult("Error. Try Again");
            }
        }
    }
}
