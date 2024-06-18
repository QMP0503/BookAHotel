using BookAHotel.Models;
using BookAHotel.DTO;
namespace BookAHotel.Profile
{
    public class RoomProfile : AutoMapper.Profile
    {
        public RoomProfile()
        {
            CreateMap<Room, RoomDTO>();
        }
    }
}
