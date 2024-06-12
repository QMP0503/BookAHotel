using BookAHotel.Models;
using BookAHotel.Repository.IRepository;
using BookAHotel.Service.IService;
using Org.BouncyCastle.Asn1.Mozilla;
namespace BookAHotel.Service
{
    public class ClientService: IClientService
    {
        private readonly IRepository<Client> _Repository;
        private readonly IFindRepository<Client> _ClientRepository;
        public ClientService(IRepository<Client> Repository, IFindRepository<Client> clientRepository)
        {
            _Repository = Repository;
            _ClientRepository = clientRepository;
        }
        public void AddClient(string name)
        {
            var client = new Client
            {
                Name = name,
                Status = "Booked"
            };
            _Repository.Add(client);
        }
        public Client FindClient(string Name)
        {
            if(Name == null) { throw new Exception("Input Empty"); }
            return _ClientRepository.FindBy(x => x.Name == Name);
        }
        public List<Client> ListClientByStatus(string Status)
        {
            if(Status == null) { throw new Exception("Input Empty"); }
            return _ClientRepository.ListBy(x => x.Status == Status);
        }
        public void UpdateClient(string Name, string Status, string? newName)
        {
            var client = FindClient(Name);
            client.Name = newName==null ? client.Name : newName;
            client.Status = Status;
            _Repository.Update(client);
        }
        public void DeleteClient(string Name)
        {
            var client = FindClient(Name);
            _Repository.Delete(client);
        }
        public void Save()
        {
            _Repository.Save();
        }
    }
}
