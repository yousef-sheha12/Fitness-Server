using Fitness.Data;
using Fitness.Interface.IRepository;
using Fitness.Models;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Repository
{
    public class SessionRepository : ISessionRepository
    {
        private readonly AppDbContext _context;

        public SessionRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<Session>> GetAllAsync() =>
            await _context.Sessions.ToListAsync();

        public async Task<Session?> GetByIdAsync(int id) =>
            await _context.Sessions.FindAsync(id);

        public async Task<IEnumerable<Session>> GetByTrainerIdAsync(int trainerId) =>
            await _context.Sessions.Where(s => s.TrainerId == trainerId).ToListAsync();

        public async Task<IEnumerable<Session>> GetByBookingIdAsync(int bookingId) =>
            await _context.Sessions.Where(s => s.BookingId == bookingId).ToListAsync();

        public async Task<Session> CreateAsync(Session session)
        {
            await _context.Sessions.AddAsync(session);
            await SaveChangesAsync();
            return session;
        }

        public async Task<Session?> UpdateAsync(Session session)
        {
            var existing = await _context.Sessions.FindAsync(session.Id);
            if (existing == null) return null;
            _context.Entry(existing).CurrentValues.SetValues(session);
            await SaveChangesAsync();
            return existing;
        }

        public async Task<bool> SaveChangesAsync() =>
            await _context.SaveChangesAsync() > 0;
    }
}
