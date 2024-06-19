using BookAHotel.Models;
using Org.BouncyCastle.Security;

namespace BookAHotel.Service.IService
{
    public interface IStoreProceduresService
    {
        public List<Room> DbRoomPaging(int pageSize, int pageNumber);
        public List<Booking> DbBookingSorting(string sortBy, string columnName); //asc/desc/none &
        public List<Room> DbRoomSearching(string searchValue);


    }
}
