using Fitness.Models;

namespace Fitness.Interface.IRepository
{
    public interface IContactRepository
    {
        Task<IEnumerable<Contact>> GetAllAsync();
        Task<Contact?> GetByIdAsync(int id);
        Task<Contact> CreateAsync(Contact contact);
        Task<bool> SaveChangesAsync();
    }
}
