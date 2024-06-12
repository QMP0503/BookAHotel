using BookAHotel.Data;
using BookAHotel.Models;
using BookAHotel.Repository.IRepository;
using log4net;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BookAHotel.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly HotelBookingContext _context;
        private readonly DbSet<T> _dbSet;
        private readonly ILog _logger;
        public Repository(HotelBookingContext context, ILog log)
        {
            _context = context;
            _dbSet = context.Set<T>();
            _logger = log;
        }
        public void Add(T entity)
        {
            try
            {
                _dbSet.Add(entity);
                _context.SaveChanges();
            }
            catch (Exception ex) {_logger.Error(ex); }
        }
        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified; //update status to be changed when savechanges is called
        }
        public void Delete(T entity)
        {
            if(_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
        }
        public void Save()
        {
            _context.SaveChanges();
        }

    }
}
