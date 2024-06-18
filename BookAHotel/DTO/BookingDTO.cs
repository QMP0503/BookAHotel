using System.ComponentModel.DataAnnotations;

namespace BookAHotel.DTO
{
    public class BookingDTO
    {
        public int ClientID { get; set; }
        public string ClientName { get; set; }
        [AllowedValues(typeof(string), new string[] { "Booked", "Cancelled", "CheckedIn", "CheckedOut", "N/A" })]
        public string Status { get; set; }
    }
}