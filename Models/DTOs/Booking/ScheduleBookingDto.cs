using System.ComponentModel.DataAnnotations;

namespace Fitness.Models.DTOs.Booking
{
    public class ScheduleBookingDto
    {
        [Required]
        public int TrainerId { get; set; }

        [Required]
        public DateTime BookingDate { get; set; }

        [Required]
        public TimeSpan StartTime { get; set; }

        [Required]
        public TimeSpan EndTime { get; set; }

        public decimal Amount { get; set; }

        public string? Notes { get; set; }
    }
}
