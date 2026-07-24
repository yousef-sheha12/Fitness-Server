using Fitness.Data;
using Fitness.Interface.IRepository;
using Fitness.Models;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly AppDbContext _context;

        public PaymentRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<Payment>> GetAllAsync() =>
            await _context.Payments.ToListAsync();

        public async Task<Payment?> GetByIdAsync(int id) =>
            await _context.Payments.FindAsync(id);

        public async Task<IEnumerable<Payment>> GetByUserIdAsync(int userId) =>
            await _context.Payments.Where(p => p.UserId == userId).ToListAsync();

        public async Task<Payment> CreateAsync(Payment payment)
        {
            await _context.Payments.AddAsync(payment);
            await SaveChangesAsync();
            return payment;
        }

        public async Task<Payment?> UpdateAsync(Payment payment)
        {
            var existing = await _context.Payments.FindAsync(payment.Id);
            if (existing == null) return null;
            _context.Entry(existing).CurrentValues.SetValues(payment);
            await SaveChangesAsync();
            return existing;
        }

        public async Task<bool> SaveChangesAsync() =>
            await _context.SaveChangesAsync() > 0;
    }
}
