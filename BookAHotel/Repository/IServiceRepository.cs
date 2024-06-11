using Microsoft.AspNetCore.Mvc;

namespace BookAHotel.Repository
{
    public interface IServiceRepository
    {
        Task<JsonResult> GetAsync(string name, string type);

        Task<JsonResult> CreateBooking(string ClientName, string RoomName, string checkInDate, string checkOutDate);
        Task<JsonResult> EditBooking(string ClientName, string RoomName, string checkInDate, string checkOutDate);
        Task<JsonResult> DeleteAsync(string ClientName, string RoomName);
    }
}
