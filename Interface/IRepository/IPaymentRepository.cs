using Fitness.Models;

namespace Fitness.Interface.IRepository
{
    public interface IPaymentRepository
    {
        Task<IEnumerable<Payment>> GetAllAsync();
        Task<Payment?> GetByIdAsync(int id);
        Task<IEnumerable<Payment>> GetByUserIdAsync(int userId);
        Task<Payment> CreateAsync(Payment payment);
        Task<Payment?> UpdateAsync(Payment payment);
        Task<bool> SaveChangesAsync();
    }
}
