using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fitness.Models
{
    public class TrainerSpecialization
    {
        [Key]
        public int Id { get; set; }

        public int TrainerId { get; set; }

        [ForeignKey("TrainerId")]
        public Trainer? Trainer { get; set; }

        public int SpecializationId { get; set; }

        [ForeignKey("SpecializationId")]
        public Specialization? Specialization { get; set; }
    }
}
