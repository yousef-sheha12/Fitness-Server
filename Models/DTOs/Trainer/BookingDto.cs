namespace Fitness.Models.DTOs.Trainer
{
    public class BookingDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public int TrainerId { get; set; }
        public string? TrainerName { get; set; }
        public DateTime BookingDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Status { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public bool IsPaid { get; set; }
    }
}
