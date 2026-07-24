using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fitness.Models
{
    public class PackagePurchase
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }

        public int TrainerPackageId { get; set; }

        [ForeignKey("TrainerPackageId")]
        public TrainerPackage? TrainerPackage { get; set; }

        public DateTime PurchaseDate { get; set; } = DateTime.UtcNow;

        public DateTime ExpiryDate { get; set; }

        [MaxLength(50)]
        public string Status { get; set; } = "Active";

        public decimal AmountPaid { get; set; }
    }
}
