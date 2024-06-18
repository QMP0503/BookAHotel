using BookAHotel.Data;
using BookAHotel.Models;
using BookAHotel.Repository.IRepository;
using BookAHotel.Service;
using Microsoft.EntityFrameworkCore;

namespace BookAHotel.Repository
{
    public class RoomRepository : IFindRepository<Room>
    {
        private readonly HotelBookingContext _context;
        public RoomRepository(HotelBookingContext context) 
        {
            _context = context;
        }
        public Room FindBy(Func<Room, bool> predicate)
        {
            return FindRoom().FirstOrDefault(predicate);
        }
        public List<Room> ListBy(Func<Room, bool> predicate) 
        {
            return FindRoom().Where(predicate).ToList();
        }

        public IQueryable<Room> FindRoom()
        {
            var rooms = _context.Rooms.Include(r => r.Booking).ThenInclude(b => b.Client);
            var RoomTypes = _context.RoomTypes;
            var bookings = _context.Bookings;

            var result = from room in rooms
                         join roomType in RoomTypes on room.RoomType.Id equals roomType.Id
                         select new Room
                         {
                             Id = room.Id,
                             Name = room.Name,
                             RoomType = roomType,
                             RoomTypeId = roomType.Id,
                             Booking = room.Booking.ToList(),
                             IsAvailable = room.IsAvailable,
               
                         };

            return result;
        }
    }
}
