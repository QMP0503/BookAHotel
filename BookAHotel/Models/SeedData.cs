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
                //context.RoomTypes.AddRange(
                //    new RoomType
                //    {
                //        RoomTypeName = RoomTypeNameEnum.Normal,
                //        Price = 50
                //    },
                //    new RoomType
                //    {
                //        RoomTypeName = RoomTypeNameEnum.Queen,
                //        Price = 80
                //    },
                //    new RoomType
                //    {
                //        RoomTypeName = RoomTypeNameEnum.King,
                //        Price = 120
                //    },
                //    new RoomType
                //    {
                //        RoomTypeName = RoomTypeNameEnum.President,
                //        Price = 200
                //    });
                //context.SaveChanges();
                return;
            }
            
        }
    }
}
