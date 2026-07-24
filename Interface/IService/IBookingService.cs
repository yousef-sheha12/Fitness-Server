using Fitness.Models;
using Fitness.Models.DTOs.Trainer;

namespace Fitness.Interface.IService
{
    public interface IBookingService
    {
        Task<IEnumerable<BookingDto>> GetByUserIdAsync(int userId);
        Task<IEnumerable<BookingDto>> GetByTrainerIdAsync(int trainerId);
        Task<Booking?> GetByIdAsync(int id);
        Task<Booking> CreateAsync(Booking booking);
        Task<Booking?> UpdateAsync(int id, Booking booking);
        Task<bool> DeleteAsync(int id);
    }
}
