using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fitness.Models
{
    public class TrainerPackage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? Description { get; set; }

        public decimal Price { get; set; }

        public int DurationDays { get; set; }

        public int TrainerId { get; set; }

        [ForeignKey("TrainerId")]
        public Trainer? Trainer { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<PackagePurchase> PackagePurchases { get; set; } = new List<PackagePurchase>();
    }
}
