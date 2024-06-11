using BookAHotel.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace BookAHotel.Models
{
    public class SeedData //Seed data into room and room type
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new HotelBookingContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<HotelBookingContext>>()))
            {
                return;
            }
            
        }
    }
}
