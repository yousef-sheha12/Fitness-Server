using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fitness.Models
{
    public class Conversation
    {
        [Key]
        public int Id { get; set; }

        public int User1Id { get; set; }

        [ForeignKey("User1Id")]
        public User? User1 { get; set; }

        public int User2Id { get; set; }

        [ForeignKey("User2Id")]
        public User? User2 { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? LastMessageAt { get; set; }

        public ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}
