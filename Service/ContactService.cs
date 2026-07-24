using Fitness.Interface.IRepository;
using Fitness.Interface.IService;
using Fitness.Models;

namespace Fitness.Service
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _repo;
        public ContactService(IContactRepository repo) => _repo = repo;

        public async Task<IEnumerable<Contact>> GetAllAsync() => await _repo.GetAllAsync();
        public async Task<Contact> CreateAsync(Contact contact) => await _repo.CreateAsync(contact);
    }
}
