using BookAHotel.Repository.IRepository;
using BookAHotel.Models;
using Org.BouncyCastle.Asn1.Mozilla;


namespace BookAHotel.Service.IService
{
    public interface IClientService
    {

        public void AddClient(string Name);
        public Client FindClient(string Name);
        public List<Client> ListClientByStatus(string Status);
        public void UpdateClient(string Name, string Status, string? newName);
        public void DeleteClient(string Name);
        public void AddPayment(Client client, int CardNumber);
        public void Save();
    

    }
}
