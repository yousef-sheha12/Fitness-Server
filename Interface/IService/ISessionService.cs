using Fitness.Models;

namespace Fitness.Interface.IService
{
    public interface ISessionService
    {
        Task<IEnumerable<Session>> GetAllAsync();
        Task<Session?> GetByIdAsync(int id);
        Task<IEnumerable<Session>> GetByTrainerIdAsync(int trainerId);
        Task<IEnumerable<Session>> GetByBookingIdAsync(int bookingId);
        Task<Session> CreateAsync(Session session);
        Task<Session?> UpdateAsync(int id, Session session);
    }
}
