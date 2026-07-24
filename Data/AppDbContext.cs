using Fitness.Models;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<TrainerSpecialization> TrainerSpecializations { get; set; }
        public DbSet<TrainerPackage> TrainerPackages { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<FitnessProfile> FitnessProfiles { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<WorkoutHistory> WorkoutHistories { get; set; }
        public DbSet<ProgressActivity> ProgressActivities { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<PackagePurchase> PackagePurchases { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Trainer>()
                .HasIndex(t => t.UserId)
                .IsUnique();

            modelBuilder.Entity<TrainerSpecialization>()
                .HasIndex(ts => new { ts.TrainerId, ts.SpecializationId })
                .IsUnique();

            modelBuilder.Entity<Trainer>()
                .Property(t => t.Rating)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<TrainerPackage>()
                .Property(tp => tp.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Booking>()
                .Property(b => b.Amount)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Payment>()
                .Property(p => p.Amount)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<PackagePurchase>()
                .Property(pp => pp.AmountPaid)
                .HasColumnType("decimal(18,2)");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
