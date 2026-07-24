using Fitness.Data;
using Fitness.Interface.IRepository;
using Fitness.Models;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly AppDbContext _context;

        public BookingRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<Booking>> GetAllAsync() =>
            await _context.Bookings
                .Include(b => b.User)
                .Include(b => b.Trainer)
                .ToListAsync();

        public async Task<Booking?> GetByIdAsync(int id) =>
            await _context.Bookings
                .Include(b => b.User)
                .Include(b => b.Trainer)
                .FirstOrDefaultAsync(b => b.Id == id);

        public async Task<IEnumerable<Booking>> GetByUserIdAsync(int userId) =>
            await _context.Bookings
                .Include(b => b.Trainer)
                .Where(b => b.UserId == userId)
                .ToListAsync();

        public async Task<IEnumerable<Booking>> GetByTrainerIdAsync(int trainerId) =>
            await _context.Bookings
                .Include(b => b.User)
                .Where(b => b.TrainerId == trainerId)
                .ToListAsync();

        public async Task<Booking> CreateAsync(Booking booking)
        {
            await _context.Bookings.AddAsync(booking);
            await SaveChangesAsync();
            return booking;
        }

        public async Task<Booking?> UpdateAsync(Booking booking)
        {
            var existing = await _context.Bookings.FindAsync(booking.Id);
            if (existing == null) return null;
            _context.Entry(existing).CurrentValues.SetValues(booking);
            await SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null) return false;
            _context.Bookings.Remove(booking);
            return await SaveChangesAsync();
        }

        public async Task<bool> SaveChangesAsync() =>
            await _context.SaveChangesAsync() > 0;
    }
}
