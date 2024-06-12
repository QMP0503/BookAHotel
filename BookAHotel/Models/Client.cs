using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookAHotel.Models
{
    public class Client
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Booking>? Booking { get; set; } = null;
        
        [AllowedValues(typeof(string), new string[] { "Booked", "Not Booked", "CheckedIn", "CheckedOut", "N/A"})]
        public string Status { get; set; } = "N/A"; //default value
    }
}
