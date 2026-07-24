using Fitness.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fitness.Data.Configuration
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.HasOne(b => b.User)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(b => b.Trainer)
                .WithMany(t => t.Bookings)
                .HasForeignKey(b => b.TrainerId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
