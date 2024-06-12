using BookAHotel.Data;
using BookAHotel.Models;
using BookAHotel.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BookAHotel.Repository
{
    public class BookingRepository: IFindRepository<Booking>
    {
        private readonly HotelBookingContext _context;
        public BookingRepository(HotelBookingContext context)
        {
            _context = context;
        }
        public Booking FindBy(Func<Booking, bool> predicate)
        {
            return _context.Bookings.Include(x => x.Room).Include(x => x.Client).FirstOrDefault(predicate);
        }
        public List<Booking> ListBy(Func<Booking, bool> predicate)
        {
            return _context.Bookings.Include(x => x.Room).Include(x => x.Client).Where(predicate).ToList();
        }
 
    }
}
