using Fitness.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fitness.Data.Configuration
{
    public class PackagePurchaseConfiguration : IEntityTypeConfiguration<PackagePurchase>
    {
        public void Configure(EntityTypeBuilder<PackagePurchase> builder)
        {
            builder.HasOne(pp => pp.User)
                .WithMany(u => u.PackagePurchases)
                .HasForeignKey(pp => pp.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(pp => pp.TrainerPackage)
                .WithMany(tp => tp.PackagePurchases)
                .HasForeignKey(pp => pp.TrainerPackageId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
