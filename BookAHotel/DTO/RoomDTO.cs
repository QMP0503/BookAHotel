using BookAHotel.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookAHotel.DTO
{
    public class RoomDTO
    {
        public int RoomID { get; set; }
        public string RoomName { get; set; }
        public string RoomType { get; set; }
        public bool IsAvailable { get; set; }
        [DisplayName("Client Bookings")]
        public List<BookingDTO> Bookings { get; set; }    //order so that first client is more recent client
    }
}
