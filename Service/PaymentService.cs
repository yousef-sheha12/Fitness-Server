using Fitness.Interface.IRepository;
using Fitness.Interface.IService;
using Fitness.Models;

namespace Fitness.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _repo;
        public PaymentService(IPaymentRepository repo) => _repo = repo;

        public async Task<IEnumerable<Payment>> GetByUserIdAsync(int userId) => await _repo.GetByUserIdAsync(userId);
        public async Task<Payment?> GetByIdAsync(int id) => await _repo.GetByIdAsync(id);
        public async Task<Payment> CreateAsync(Payment payment) => await _repo.CreateAsync(payment);
    }
}
