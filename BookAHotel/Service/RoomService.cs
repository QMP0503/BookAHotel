using BookAHotel.Repository.IRepository;
using BookAHotel.Models;
using BookAHotel.Service.IService;
using BookAHotel.Repository;

namespace BookAHotel.Service
{
    public class RoomService: IRoomService
    {
        private readonly IFindRepository<Room> _RoomRepository;
        public RoomService(IFindRepository<Room> roomRepository)
        {
            _RoomRepository = roomRepository;
        }
        public Room FindRoom(string RoomName)
        {
            return _RoomRepository.FindBy(x => x.Name == RoomName);
        }
        public List<Room> RoomList(string? RoomName, string? RoomType) 
        {
            if(RoomName == null && RoomType == null) { return null; }
            else if(RoomName == null) { return _RoomRepository.ListBy(x => x.RoomType.RoomTypeName.Equals(RoomType)); }
            else if(RoomType == null) { return _RoomRepository.ListBy(x => x.Name.Contains(RoomName)); }
            return _RoomRepository.ListBy(x => x.Name.Contains(RoomName) && x.RoomType.Equals(RoomType));
        }
    }
}
