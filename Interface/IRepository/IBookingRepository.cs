using Fitness.Models;

namespace Fitness.Interface.IRepository
{
    public interface IBookingRepository
    {
        Task<IEnumerable<Booking>> GetAllAsync();
        Task<Booking?> GetByIdAsync(int id);
        Task<IEnumerable<Booking>> GetByUserIdAsync(int userId);
        Task<IEnumerable<Booking>> GetByTrainerIdAsync(int trainerId);
        Task<Booking> CreateAsync(Booking booking);
        Task<Booking?> UpdateAsync(Booking booking);
        Task<bool> DeleteAsync(int id);
        Task<bool> SaveChangesAsync();
    }
}
