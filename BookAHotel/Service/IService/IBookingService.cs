using BookAHotel.Repository;
using BookAHotel.Models;

namespace BookAHotel.Service.IService
{
    public interface IBookingService
    {
        public Booking FindBooking(string ClientName);
        public List<Booking> ListBooking(string? RoomName);
        public void AddBooking(string ClientName, string RoomName, string checkInDate, string checkOutDate, double discount);
        public void UpdateBooking(string ClientName, string RoomName, string checkInDate, string checkOutDate);
        public void CancelBooking(string ClientName);
        public void Save();
    }
}
