using Microsoft.Extensions.Primitives;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookAHotel.Models
{
    public class Room
    {
        [Key]
        [DisplayName("Room Number")]
        public int Id { get; set; } //room number
        public string Name { get; set; }
        public bool IsAvailable { get; set; } //should be changed to status 
        public int RoomTypeId { get; set; } //foreign key
        public RoomType RoomType { get; set; }//one to many with roomtype
        public ICollection<Booking> Booking { get; set; } //many many to many

    }


}
