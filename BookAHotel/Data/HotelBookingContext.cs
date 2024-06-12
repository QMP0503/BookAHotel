using BookAHotel.Models;
using Microsoft.EntityFrameworkCore;

namespace BookAHotel.Data
{
    public class HotelBookingContext:DbContext
    {
        public HotelBookingContext(DbContextOptions<HotelBookingContext> options) : base(options) { }
        public DbSet<Room> Rooms { get; set; }
        //many to many with Client
        //One to many with RoomType
        public DbSet<Client> Clients { get; set; }
        //many to many with Room
        public DbSet<RoomType> RoomTypes { get; set; }
        //one to many with room
        public DbSet<Booking> Bookings { get; set; }
        //Connection for room and client
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

      => optionsBuilder.UseMySQL("Server=localhost;Database=hotelbooking;User=root;Password=Munmeo0503.;");


        protected override void OnModelCreating(ModelBuilder modelBuilder)  
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Client>().HasIndex(e => e.Name).IsUnique();

            modelBuilder.Entity<Room>()
                .HasMany(c => c.Booking)
                .WithOne(cr => cr.Room)
                .HasForeignKey(cr => cr.RoomId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Client>()
                .HasMany(c => c.Booking)
                .WithOne(cr => cr.Client)
                .HasForeignKey(c => c.ClientId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RoomType>()
                .HasMany(rt => rt.Rooms)
                .WithOne(r => r.RoomType)
                .HasForeignKey(r => r.RoomTypeId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Room>()
                .HasOne(r => r.RoomType)
                .WithMany(rt => rt.Rooms)
                .HasForeignKey(r => r.RoomTypeId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);
        }
                
        


    }
}
