using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BookAHotel.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Client")]
        public int ClientId { get; set; }
        public Client Client { get; set; }
        [ForeignKey("Room")]
        public int RoomId { get; set; }
        public Room Room { get; set; }
        [DataType(DataType.Date)]
        public DateTime startDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime endDate { get; set; }
        public double TotalPrice { get; set; }
        [AllowedValues(typeof(string), new string[] { "Booked", "Cancelled", "CheckedIn", "CheckedOut", "N/A" })]
        public string Status { get; set; }
        public bool PaymentStatus { get; set; }
    }
}