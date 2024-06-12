using BookAHotel.Models;
using Microsoft.EntityFrameworkCore;

namespace BookAHotel.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Save();

    }
}
