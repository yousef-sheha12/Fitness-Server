using Fitness.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fitness.Data.Configuration
{
    public class SessionConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.HasOne(s => s.Trainer)
                .WithMany(t => t.Sessions)
                .HasForeignKey(s => s.TrainerId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(s => s.Booking)
                .WithMany(b => b.Sessions)
                .HasForeignKey(s => s.BookingId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
