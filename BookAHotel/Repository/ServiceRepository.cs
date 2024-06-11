using BookAHotel.Data;
using BookAHotel.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using BookAHotel.Models;
using log4net;

namespace BookAHotel.Repository
{
    public class ServiceRepository : IServiceRepository
    {
        protected readonly HotelBookingContext _context;
        private readonly ILog _logger;
        public ServiceRepository(HotelBookingContext context, ILog log)
        {
            _context = context;
            _logger = log;
        }
        public async Task<JsonResult> GetAsync(string name, string type)
        {
            if (type.ToLower() == "room")
            {
                var result = _context.Rooms.Where(x => x.Name.Contains(name)).ToList();
                if (result == null) { return null; }
                return new JsonResult(result);
            }
            else if (type.ToLower() == "client")
            {
                var result = await _context.Clients.Include(x => x.Booking).ThenInclude(x => x.Room).FirstOrDefaultAsync(x => x.Name == name);
                if (result == null) { return null; }
                if(result.Booking.Count == 0) { return new JsonResult("name: " + result.Name); }
                var clientBooking = new ClientBookingVM()
                {
                    Name = result.Name,
                    RoomName = result.Booking.ToList().Select(x => x.Room).FirstOrDefault().Name,
                    CheckInDate = result.Booking.ToList().Select(x => x.startDate).FirstOrDefault(),
                    CheckOutDate = result.Booking.ToList().Select(x => x.endDate).FirstOrDefault(),
                    Price = result.Booking.ToList().Select(x => x.TotalPrice).FirstOrDefault()
                };
                return new JsonResult(clientBooking);
            }
            else { return null; }
        }

        public async Task<JsonResult> CreateBooking(string ClientName, string RoomName, string checkInDate, string checkOutDate)
        {
            var room = _context.Rooms.Include(x => x.Booking).Include(x=>x.RoomType).FirstOrDefault(x => x.Name == RoomName && x.IsAvailable == true);
            if (room == null) { return new JsonResult("Room Not Available"); }
            var client = _context.Clients.FirstOrDefault(x => x.Name == ClientName);
            if (client == null) { return new JsonResult("Client Not Found"); }
            var checkIn = DateTime.Parse(checkInDate);
            var checkOut = DateTime.Parse(checkOutDate);
            if (checkIn > checkOut) { return new JsonResult(" Invalid CheckInDate"); }
            _logger.Debug("CreateBooking: " + client.Name + " " + room.Name + " " + checkInDate + " " + checkOutDate);
            var booking = new Booking()
            {
                ClientId = client.Id,
                RoomId = room.Id,
                startDate = checkIn,
                endDate = checkOut,
                TotalPrice = ((checkOut - checkIn).Days * room.RoomType.Price)*1.20 //plus 20% tax and service charge
            };
            _logger.Error("booking creation failed");
            _context.Bookings.Add(booking);
            room.IsAvailable = false;
            client.Status = "Booked";
            _context.Clients.Update(client);
            _context.Rooms.Update(room);
            _context.SaveChanges();
            return new JsonResult("Booking Confirmed:" + booking.ToString()); //add automapper when method is successfull to hide id
        }
        public async Task<JsonResult> EditBooking(string ClientName, string RoomName, string checkInDate, string checkOutDate)
        {
        //    var booking = await _context.Bookings.Include(x => x.Room).Include(x => x.Client).FirstOrDefaultAsync(x => x.Room.Name == RoomName && x.Client.Name == ClientName);
        //    if(booking == null) { return new JsonResult("Booking Not Found"); }
        //    var room = _context.Rooms.Include(x => x.Booking).FirstOrDefault(x => x.Name == RoomName && x.IsAvailable == true);
        //    if (room == null) { return new JsonResult("Room Not Available"); }
        //    var client = _context.Clients.FirstOrDefault(x => x.Name == ClientName);
        //    if (client == null) { return new JsonResult("Client Not Found"); }
        //    var checkIn = DateTime.Parse(checkInDate);
        //    var checkOut = DateTime.Parse(checkOutDate);
        //    if (checkIn > checkOut) { return new JsonResult(" Invalid CheckInDate"); }

        //    var booking = new Booking()
        //    {
        //        ClientId = client.Id,
        //        RoomId = room.Id,
        //        startDate = checkIn,
        //        endDate = checkOut,
        //        TotalPrice = ((checkOut - checkIn).Days * room.RoomType.Price) * 1.20 //plus 20% tax and service charge
        //    };
        //    _context.Bookings.Update(booking);
        //    room.IsAvailable = false;
        //    client.Status = "Booked";
        //    _context.Clients.Update(client);
        //    _context.Rooms.Update(room);
        //    _context.SaveChanges();
        //    return new JsonResult("Booking Confirmed:" + booking.ToString()); //add automapper when method is successfull to hide id
        return null;
        }
        public async Task<JsonResult> DeleteAsync(string ClientName, string RoomName)
        {
            var result = await _context.Bookings.Include(x => x.Room).Include(x => x.Client).FirstOrDefaultAsync(x => x.Room.Name == RoomName && x.Client.Name == ClientName);
            if (result == null) { return new JsonResult("Booking Not Found!"); }
            _context.Bookings.Remove(result);
            _context.Rooms.Update(result.Room);
            _context.Clients.Update(result.Client);
            _context.SaveChanges();
            return new JsonResult($"{result.Client.Name}'s Booking Has Deleted");
        }
    }
}
