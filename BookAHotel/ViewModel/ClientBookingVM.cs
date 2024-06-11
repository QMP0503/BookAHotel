using System.ComponentModel.DataAnnotations;

namespace BookAHotel.ViewModel
{
    public class ClientBookingVM
    {
        public string Name { get; set; }
        public string? RoomName { get; set; }
        [DataType(DataType.Date)]
        public DateTime? CheckInDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? CheckOutDate { get; set; }
        public double? Price { get; set; }
    }
}
