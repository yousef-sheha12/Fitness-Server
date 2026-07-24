using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fitness.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }

        public int? BookingId { get; set; }

        [ForeignKey("BookingId")]
        public Booking? Booking { get; set; }

        public decimal Amount { get; set; }

        [MaxLength(50)]
        public string PaymentMethod { get; set; } = "Card";

        [MaxLength(50)]
        public string Status { get; set; } = "Completed";

        [MaxLength(200)]
        public string? StripePaymentId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
