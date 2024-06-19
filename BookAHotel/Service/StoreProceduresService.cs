using BookAHotel.Data;
using BookAHotel.Models;
using BookAHotel.Service.IService;
using Dapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BookAHotel.Service
{
    public class StoreProceduresService: IStoreProceduresService
    {
        private readonly DapperContext _context;
        public StoreProceduresService(DapperContext context)
        {
            _context = context;
        }
        public List<Room> DbRoomPaging(int pageSize, int pageNumber)
        {
            var query = $"CALL TablePaging({ pageSize},{ pageNumber});";
            using (var connection = _context.CreateConnection())
            {
                var room = connection.Query<Room>(query);
                return room.ToList();
            }
        }
        public List<Booking> DbBookingSorting(string sortBy, string columnName)
        {
            string query;
            if(sortBy == "ascending")
            {
                query = $"SELECT * FROM Bookings ORDER BY {columnName} ASC;";
            }
            else
            {
                query = $"SELECT * FROM Bookings ORDER BY {columnName} DESC;";
            }
            using (var connection = _context.CreateConnection())
            {
                var booking = connection.Query<Booking>(query);
                return booking.ToList();
            }
        }
        
        public List<Room> DbRoomSearching(string searchValue)
        {
            var query = "CALL RoomSearch(\'" + searchValue + "\');";
            using (var connection = _context.CreateConnection())
            {
                var room = connection.Query<Room>(query);
                return room.ToList();
            }
        }
    }
}
