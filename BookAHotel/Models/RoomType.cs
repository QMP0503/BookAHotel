using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace BookAHotel.Models
{
    public class RoomType
    {
        public int Id { get; set; }
        [DisplayName("Room Type Name")]
        public RoomTypeNameEnum RoomTypeName { get; set; }
        public int Price { get; set; } //only for the room
        public ICollection<Room> Rooms { get; set; } 

    }
    public enum RoomTypeNameEnum
    {
        Queen,
        Normal,
        King,
        President
    }
}
