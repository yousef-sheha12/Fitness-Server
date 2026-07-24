using Fitness.Interface.IRepository;
using Fitness.Interface.IService;
using Fitness.Models;
using Fitness.Models.DTOs.Trainer;

namespace Fitness.Service
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        public BookingService(IBookingRepository bookingRepository) => _bookingRepository = bookingRepository;

        public async Task<IEnumerable<BookingDto>> GetByUserIdAsync(int userId)
        {
            var bookings = await _bookingRepository.GetByUserIdAsync(userId);
            return bookings.Select(MapToDto);
        }

        public async Task<IEnumerable<BookingDto>> GetByTrainerIdAsync(int trainerId)
        {
            var bookings = await _bookingRepository.GetByTrainerIdAsync(trainerId);
            return bookings.Select(MapToDto);
        }

        public async Task<Booking?> GetByIdAsync(int id) => await _bookingRepository.GetByIdAsync(id);
        public async Task<Booking> CreateAsync(Booking booking) => await _bookingRepository.CreateAsync(booking);

        public async Task<Booking?> UpdateAsync(int id, Booking booking)
        {
            var existing = await _bookingRepository.GetByIdAsync(id);
            if (existing == null) return null;
            existing.Status = booking.Status ?? existing.Status;
            existing.Notes = booking.Notes ?? existing.Notes;
            return await _bookingRepository.UpdateAsync(existing);
        }

        public async Task<bool> DeleteAsync(int id) => await _bookingRepository.DeleteAsync(id);

        private static BookingDto MapToDto(Booking b) => new()
        {
            Id = b.Id, UserId = b.UserId, UserName = b.User?.Name,
            TrainerId = b.TrainerId, TrainerName = b.Trainer?.Name,
            BookingDate = b.BookingDate, StartTime = b.StartTime, EndTime = b.EndTime,
            Status = b.Status, Amount = b.Amount, IsPaid = b.IsPaid
        };
    }
}
