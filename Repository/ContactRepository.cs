using Fitness.Data;
using Fitness.Interface.IRepository;
using Fitness.Models;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Repository
{
    public class ContactRepository : IContactRepository
    {
        private readonly AppDbContext _context;

        public ContactRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<Contact>> GetAllAsync() =>
            await _context.Contacts.ToListAsync();

        public async Task<Contact?> GetByIdAsync(int id) =>
            await _context.Contacts.FindAsync(id);

        public async Task<Contact> CreateAsync(Contact contact)
        {
            await _context.Contacts.AddAsync(contact);
            await SaveChangesAsync();
            return contact;
        }

        public async Task<bool> SaveChangesAsync() =>
            await _context.SaveChangesAsync() > 0;
    }
}
