namespace Fitness.Models.DTOs.Booking
{
    public class RescheduleBookingDto
    {
        public DateTime BookingDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
