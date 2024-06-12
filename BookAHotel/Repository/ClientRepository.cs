using BookAHotel.Data;
using BookAHotel.Models;
using Microsoft.EntityFrameworkCore;
using ZstdSharp.Unsafe;
using BookAHotel.Repository.IRepository;

namespace BookAHotel.Repository
{
    public class ClientRepository : IFindRepository<Client>
    
    {
        private readonly HotelBookingContext _context;
        private readonly DbSet<Client> _clientsDb;
        public ClientRepository(HotelBookingContext context) 
        {
            _context = context;
            _clientsDb = context.Clients;
        }
        public Client FindBy(Func<Client, bool> predicate)
        {
            return _clientsDb.Include(x => x.Booking).ThenInclude(x => x.Room).FirstOrDefault(predicate);
        }
        public List<Client> ListBy(Func<Client, bool> predicate)
        {
            return _clientsDb.Where(predicate).ToList();
        }
    }
}
