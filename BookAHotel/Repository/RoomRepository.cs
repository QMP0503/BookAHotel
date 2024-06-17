using BookAHotel.Data;
using BookAHotel.Models;
using BookAHotel.Repository.IRepository;
using BookAHotel.Service;

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
            var rooms = _context.Rooms;
            var RoomTypes = _context.RoomTypes;

            var result = from room in rooms
                         join roomType in RoomTypes on room.RoomType.Id equals roomType.Id
                         select new Room
                         {
                             Id = room.Id,
                             Name = room.Name,
                             RoomType = roomType
                         };
            return result;
        }
    }
}
