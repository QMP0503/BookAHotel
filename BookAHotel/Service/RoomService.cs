using BookAHotel.Repository.IRepository;
using BookAHotel.Models;
using BookAHotel.Service.IService;
using BookAHotel.Repository;
using BookAHotel.DTO;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace BookAHotel.Service
{
    public class RoomService: IRoomService
    {
        private readonly IFindRepository<Room> _RoomRepository;
        private readonly IMapper _mapper;
        public RoomService(IFindRepository<Room> roomRepository, IMapper mapper)
        {
            _RoomRepository = roomRepository;
            _mapper = mapper;
        }
        public Room FindRoom(string RoomName)
        {
            return _RoomRepository.FindBy(x => x.Name == RoomName);
        }
        public List<Room> RoomList(string? RoomName, string RoomType) 
        {
            if(RoomName == null && RoomType == null) { return null; }
            else if(RoomName == null) { return _RoomRepository.ListBy(x => x.RoomType.RoomTypeName.ToString().Equals(RoomType)); }
            else if(RoomType == null) { return _RoomRepository.ListBy(x => x.Name.Contains(RoomName)); }
            return _RoomRepository.ListBy(x => x.Name.Contains(RoomName) && x.RoomType.RoomTypeName.ToString().Equals(RoomType));
        }
        public RoomDTO RoomHistory(string RoomName)
        {
            var room = FindRoom(RoomName);
            if(room == null) { return null; }
            List<BookingDTO> bookingDTO = new List<BookingDTO>();
            foreach(var booking in room.Booking)
            {
                bookingDTO.Add(_mapper.Map<BookingDTO>(booking));
            }
            RoomDTO roomDTO = new RoomDTO()
            {
                RoomID=room.Id,
                RoomName = room.Name,
                RoomType = room.RoomType.RoomTypeName.ToString(),
                IsAvailable = room.IsAvailable,
                Bookings = bookingDTO
            };

            return roomDTO;

        }

    }
}
