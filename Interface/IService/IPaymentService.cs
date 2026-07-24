using Fitness.Models;

namespace Fitness.Interface.IService
{
    public interface IPaymentService
    {
        Task<IEnumerable<Payment>> GetByUserIdAsync(int userId);
        Task<Payment?> GetByIdAsync(int id);
        Task<Payment> CreateAsync(Payment payment);
    }
}
