using BookAHotel.DTO;
using BookAHotel.Models;

namespace BookAHotel.Profile
{
    public class BookingProfile : AutoMapper.Profile
    {
        public BookingProfile()
        {
            CreateMap<Booking, BookingDTO>();
        }
    }
}
