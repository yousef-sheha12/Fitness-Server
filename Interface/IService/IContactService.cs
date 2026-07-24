using Fitness.Models;

namespace Fitness.Interface.IService
{
    public interface IContactService
    {
        Task<IEnumerable<Contact>> GetAllAsync();
        Task<Contact> CreateAsync(Contact contact);
    }
}
