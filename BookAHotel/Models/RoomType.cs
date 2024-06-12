namespace BookAHotel.Models
{
    public class RoomType
    {
        public int Id { get; set; } 
        public RoomTypeNameEnum RoomTypeName { get; set; } = RoomTypeNameEnum.Normal;
        public int Price { get; set; } //only for the room
        public ICollection<Room> Rooms { get; set; } 

    }
    public enum RoomTypeNameEnum
    {
        Queen,
        Normal,
        King,
        President,
    }

}
