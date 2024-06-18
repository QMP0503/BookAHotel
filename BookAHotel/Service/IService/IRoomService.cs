using BookAHotel.DTO;
using BookAHotel.Models;
using BookAHotel.Repository;

namespace BookAHotel.Service.IService
{
    public interface IRoomService
    {
        public Room FindRoom(string RoomName);
        public List<Room> RoomList(string? RoomName, string? RoomType);
        public RoomDTO RoomHistory(string RoomName);
    }
}
