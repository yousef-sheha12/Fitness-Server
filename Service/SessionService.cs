using Fitness.Interface.IRepository;
using Fitness.Interface.IService;
using Fitness.Models;

namespace Fitness.Service
{
    public class SessionService : ISessionService
    {
        private readonly ISessionRepository _repo;
        public SessionService(ISessionRepository repo) => _repo = repo;

        public async Task<IEnumerable<Session>> GetAllAsync() => await _repo.GetAllAsync();
        public async Task<Session?> GetByIdAsync(int id) => await _repo.GetByIdAsync(id);
        public async Task<IEnumerable<Session>> GetByTrainerIdAsync(int trainerId) => await _repo.GetByTrainerIdAsync(trainerId);
        public async Task<IEnumerable<Session>> GetByBookingIdAsync(int bookingId) => await _repo.GetByBookingIdAsync(bookingId);
        public async Task<Session> CreateAsync(Session session) => await _repo.CreateAsync(session);

        public async Task<Session?> UpdateAsync(int id, Session session)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return null;
            existing.Status = session.Status ?? existing.Status;
            existing.Notes = session.Notes ?? existing.Notes;
            return await _repo.UpdateAsync(existing);
        }
    }
}
