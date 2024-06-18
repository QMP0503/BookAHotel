using BookAHotel.Repository.IRepository;
using BookAHotel.Repository;
using BookAHotel.Models;
using BookAHotel.Service.IService;

namespace BookAHotel.Service
{
    public class BookingService: IBookingService
    {
        private readonly IRepository<Booking> _Repository;
        private readonly IRepository<Client> _RepositoryClient;
        private readonly IRepository<Room> _RepositoryRoom;
        private readonly IFindRepository<Booking> _BookingRepository;
        private readonly IFindRepository<Room> _RoomRepository;
        private readonly IFindRepository<Client> _ClientRepository;
        public BookingService(IRepository<Client> repositoryClient,IRepository<Booking> Repository, IFindRepository<Booking> bookingRepository, IFindRepository<Room> roomRepository, IFindRepository<Client> clientRepository, IRepository<Room> repositoryRoom)
        {
            _Repository = Repository;
            _BookingRepository = bookingRepository;
            _RoomRepository = roomRepository;
            _ClientRepository = clientRepository;
            _RepositoryClient = repositoryClient;
            _RepositoryRoom = repositoryRoom;
        }
        public Booking FindBooking(string ClientName)
        {
            if (ClientName == null) { throw new NullReferenceException("Client Not Found"); }
            return _BookingRepository.FindBy(x => x.Client.Name == ClientName && x.Status == "Booked");
        }
        public List<Booking> ListBooking() 
        {
            return _BookingRepository.ListBy(x => true); //change to get alll

        }
        public void AddBooking(string ClientName, string RoomName, string checkInDate, string checkOutDate, double discount) //store param in object (put in object if more than 3)
        {
            if(ClientName == null || RoomName == null || checkInDate == null || checkOutDate == null) { throw new Exception("Not All Fields are filled"); }
            var checkIn = DateTime.Parse(checkInDate);
            var checkOut = DateTime.Parse(checkOutDate);
            if (checkIn > checkOut) { throw new Exception("CheckOutDate must be greater than CheckInDate"); }
            var client = _ClientRepository.FindBy(x=> x.Name == ClientName);
            var room = _RoomRepository.FindBy(x => x.Name == RoomName && x.IsAvailable == true);
            if (room == null) { throw new Exception("Room Not Found or Not Available"); }
            room.IsAvailable = false;
            _RepositoryRoom.Update(room);
            if (client == null) 
            {
                var newClient = new Client
                {
                    Name = ClientName,
                    Status = "Booked"
                };
                _RepositoryClient.Add(newClient);
                client = _ClientRepository.FindBy(x => x.Name == ClientName);
            }
            client.Status = "Booked";
            _RepositoryClient.Update(client);
            var booking = new Booking
            {
                ClientId = client.Id,
                RoomId = room.Id,
                startDate = checkIn,
                endDate = checkOut,
                TotalPrice = room.RoomType.Price *(1-(discount/100))*1.20, //discount plus VAT
                Status = "Booked"
            };
            _Repository.Add(booking);


        }
        public void UpdateBooking(string ClientName, string? RoomName, string? checkInDate, string? checkOutDate)
        {
            if(ClientName == null || RoomName == null || checkInDate == null || checkOutDate == null) { throw new Exception("Not All Fields are filled"); }
            var checkIn = DateTime.Parse(checkInDate);
            var checkOut = DateTime.Parse(checkOutDate);
            if (checkIn > checkOut) { throw new Exception("CheckOutDate must be greater than CheckInDate"); }
            var client = _ClientRepository.FindBy(x=> x.Name == ClientName);
            var room = _RoomRepository.FindBy(x => x.Name == RoomName);
            if (client == null || room == null) { throw new Exception("Client or Room Not Found"); }
            var booking = FindBooking(ClientName);
            if(booking == null) { throw new Exception("Booking Not Found"); }
            booking.ClientId = client.Id;
            booking.RoomId = room.Id;
            booking.startDate = checkIn;
            booking.endDate = checkOut;
            booking.TotalPrice = room.RoomType.Price * 1.20; //vat and no discount for room change...
            _Repository.Update(booking);
        }
        public void CancelBooking(string ClientName)
        {
            var booking = FindBooking(ClientName);
            if(booking == null) { throw new Exception("Booking Not Found"); }
            if(booking.Status == "Cancelled") { throw new Exception("Booking Already Cancelled"); }
            booking.Status = "Cancelled";
            var client = _ClientRepository.FindBy(x => x.Name == ClientName);
            client.Status = "Not Booked";
            var room = _RoomRepository.FindBy(x => x.Id == booking.RoomId);
            room.IsAvailable=true;
            _Repository.Update(booking);
            _RepositoryClient.Update(client);
            _RepositoryRoom.Update(room);
        }
        public void Save()
        {
            _Repository.Save();
        }
    }
}
