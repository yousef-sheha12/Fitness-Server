using Fitness.Data;
using Fitness.Interface.IRepository;
using Fitness.Models;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<User>> GetAllAsync() =>
            await _context.Users.ToListAsync();

        public async Task<User?> GetByIdAsync(int id) =>
            await _context.Users.FindAsync(id);

        public async Task<User?> GetByEmailAsync(string email) =>
            await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

        public async Task<User> CreateAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await SaveChangesAsync();
            return user;
        }

        public async Task<User?> UpdateAsync(User user)
        {
            var existing = await _context.Users.FindAsync(user.Id);
            if (existing == null) return null;
            _context.Entry(existing).CurrentValues.SetValues(user);
            await SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;
            _context.Users.Remove(user);
            return await SaveChangesAsync();
        }

        public async Task<bool> SaveChangesAsync() =>
            await _context.SaveChangesAsync() > 0;
    }
}
