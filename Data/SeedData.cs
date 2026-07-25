using Fitness.Models;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Data
{
    public static class SeedData
    {
        public static void Initialize(AppDbContext context)
        {
            if (context.Users.Any()) return;

            var passwordHash = BCrypt.Net.BCrypt.HashPassword("Password123!");

            var users = new List<User>
            {
                new User { Id = 1, Name = "Admin User", Email = "admin@fitness.com", PasswordHash = passwordHash, Phone = "+1000000000", IsAdmin = true, IsActive = true, CreatedAt = DateTime.UtcNow },
                new User { Id = 2, Name = "Ahmed Hassan", Email = "ahmed@trainer.com", PasswordHash = passwordHash, Phone = "+1011111111", IsActive = true, CreatedAt = DateTime.UtcNow },
                new User { Id = 3, Name = "Sara Mohamed", Email = "sara@trainer.com", PasswordHash = passwordHash, Phone = "+1022222222", IsActive = true, CreatedAt = DateTime.UtcNow },
                new User { Id = 4, Name = "Omar Khalid", Email = "omar@trainer.com", PasswordHash = passwordHash, Phone = "+1033333333", IsActive = true, CreatedAt = DateTime.UtcNow },
                new User { Id = 5, Name = "Fatma Ali", Email = "fatma@trainer.com", PasswordHash = passwordHash, Phone = "+1044444444", IsActive = true, CreatedAt = DateTime.UtcNow },
                new User { Id = 6, Name = "Youssef Ibrahim", Email = "youssef@trainer.com", PasswordHash = passwordHash, Phone = "+1055555555", IsActive = true, CreatedAt = DateTime.UtcNow },
                new User { Id = 7, Name = "Mariam Saad", Email = "mariam@trainer.com", PasswordHash = passwordHash, Phone = "+1066666666", IsActive = true, CreatedAt = DateTime.UtcNow },
                new User { Id = 8, Name = "User One", Email = "user1@test.com", PasswordHash = passwordHash, Phone = "+2011111111", IsActive = true, CreatedAt = DateTime.UtcNow },
                new User { Id = 9, Name = "User Two", Email = "user2@test.com", PasswordHash = passwordHash, Phone = "+2022222222", IsActive = true, CreatedAt = DateTime.UtcNow },
                new User { Id = 10, Name = "User Three", Email = "user3@test.com", PasswordHash = passwordHash, Phone = "+2033333333", IsActive = true, CreatedAt = DateTime.UtcNow },
                new User { Id = 11, Name = "User Four", Email = "user4@test.com", PasswordHash = passwordHash, Phone = "+2044444444", IsActive = true, CreatedAt = DateTime.UtcNow },
                new User { Id = 12, Name = "User Five", Email = "user5@test.com", PasswordHash = passwordHash, Phone = "+2055555555", IsActive = true, CreatedAt = DateTime.UtcNow },
                new User { Id = 13, Name = "Khaled Nabil", Email = "khaled@trainer.com", PasswordHash = passwordHash, Phone = "+1077777777", IsActive = true, CreatedAt = DateTime.UtcNow },
                new User { Id = 14, Name = "Nour El-Din", Email = "nour@trainer.com", PasswordHash = passwordHash, Phone = "+1088888888", IsActive = true, CreatedAt = DateTime.UtcNow },
                new User { Id = 15, Name = "Hana Mansour", Email = "hana@trainer.com", PasswordHash = passwordHash, Phone = "+1099999999", IsActive = true, CreatedAt = DateTime.UtcNow },
            };
            context.Users.AddRange(users);

            var specializations = new List<Specialization>
            {
                new Specialization { Id = 1, Name = "Weight Loss", Description = "Fat burning and calorie deficit programs" },
                new Specialization { Id = 2, Name = "Muscle Building", Description = "Hypertrophy and strength training" },
                new Specialization { Id = 3, Name = "Cardio", Description = "Heart health and endurance training" },
                new Specialization { Id = 4, Name = "Yoga", Description = "Flexibility, balance, and mindfulness" },
                new Specialization { Id = 5, Name = "CrossFit", Description = "High-intensity functional fitness" },
                new Specialization { Id = 6, Name = "Pilates", Description = "Core strength and body alignment" },
                new Specialization { Id = 7, Name = "Boxing", Description = "Combat fitness and self-defense" },
                new Specialization { Id = 8, Name = "Rehabilitation", Description = "Post-injury recovery and mobility" },
                new Specialization { Id = 9, Name = "Nutrition Coaching", Description = "Meal planning and dietary guidance" },
                new Specialization { Id = 10, Name = "HIIT", Description = "High-intensity interval training" },
                new Specialization { Id = 11, Name = "Calisthenics", Description = "Bodyweight strength training" },
                new Specialization { Id = 12, Name = "Powerlifting", Description = "Squat, bench press, and deadlift programs" },
            };
            context.Specializations.AddRange(specializations);

            var trainers = new List<Trainer>
            {
                new Trainer { Id = 1, Name = "Ahmed Hassan", Bio = "Certified personal trainer with 10+ years experience in weight loss and muscle building. Former national bodybuilding champion.", Location = "Cairo, Egypt", Rating = 4.8m, ExperienceYears = 10, UserId = 2, IsApproved = true, ProfileImage = "/images/trainers/ahmed.jpg" },
                new Trainer { Id = 2, Name = "Sara Mohamed", Bio = "Yoga and Pilates instructor with international certifications. Passionate about holistic wellness and mind-body connection.", Location = "Alexandria, Egypt", Rating = 4.9m, ExperienceYears = 8, UserId = 3, IsApproved = true, ProfileImage = "/images/trainers/sara.jpg" },
                new Trainer { Id = 3, Name = "Omar Khalid", Bio = "CrossFit Level 3 trainer and competitive athlete. Specializes in functional fitness and athletic performance.", Location = "Giza, Egypt", Rating = 4.7m, ExperienceYears = 6, UserId = 4, IsApproved = true, ProfileImage = "/images/trainers/omar.jpg" },
                new Trainer { Id = 4, Name = "Fatma Ali", Bio = "Certified nutrition coach and fitness trainer. Helps clients achieve sustainable weight management through balanced approach.", Location = "Cairo, Egypt", Rating = 4.6m, ExperienceYears = 5, UserId = 5, IsApproved = true, ProfileImage = "/images/trainers/fatma.jpg" },
                new Trainer { Id = 5, Name = "Youssef Ibrahim", Bio = "Professional boxing coach and fitness trainer. Trained multiple national team athletes.", Location = "Luxor, Egypt", Rating = 4.5m, ExperienceYears = 12, UserId = 6, IsApproved = true, ProfileImage = "/images/trainers/youssef.jpg" },
                new Trainer { Id = 6, Name = "Mariam Saad", Bio = "HIIT and cardio specialist. Known for creating fun and effective workout programs that keep clients motivated.", Location = "Aswan, Egypt", Rating = 4.8m, ExperienceYears = 7, UserId = 7, IsApproved = true, ProfileImage = "/images/trainers/mariam.jpg" },
                new Trainer { Id = 7, Name = "Khaled Nabil", Bio = "Powerlifting champion and strength coach. Expert in progressive overload and periodization programming.", Location = "Mansoura, Egypt", Rating = 4.4m, ExperienceYears = 9, UserId = 13, IsApproved = true, ProfileImage = "/images/trainers/khaled.jpg" },
                new Trainer { Id = 8, Name = "Nour El-Din", Bio = "Rehabilitation and mobility specialist. Physiotherapist turned fitness trainer with focus on injury prevention.", Location = "Tanta, Egypt", Rating = 4.7m, ExperienceYears = 11, UserId = 14, IsApproved = true, ProfileImage = "/images/trainers/nour.jpg" },
                new Trainer { Id = 9, Name = "Hana Mansour", Bio = "Calisthenics expert and gymnastics coach. Specializes in bodyweight training and progressive skill development.", Location = "Port Said, Egypt", Rating = 4.6m, ExperienceYears = 4, UserId = 15, IsApproved = true, ProfileImage = "/images/trainers/hana.jpg" },
            };
            context.Trainers.AddRange(trainers);

            var trainerSpecializations = new List<TrainerSpecialization>
            {
                new TrainerSpecialization { Id = 1, TrainerId = 1, SpecializationId = 1 },
                new TrainerSpecialization { Id = 2, TrainerId = 1, SpecializationId = 2 },
                new TrainerSpecialization { Id = 3, TrainerId = 1, SpecializationId = 12 },
                new TrainerSpecialization { Id = 4, TrainerId = 2, SpecializationId = 4 },
                new TrainerSpecialization { Id = 5, TrainerId = 2, SpecializationId = 6 },
                new TrainerSpecialization { Id = 6, TrainerId = 3, SpecializationId = 5 },
                new TrainerSpecialization { Id = 7, TrainerId = 3, SpecializationId = 10 },
                new TrainerSpecialization { Id = 8, TrainerId = 4, SpecializationId = 1 },
                new TrainerSpecialization { Id = 9, TrainerId = 4, SpecializationId = 9 },
                new TrainerSpecialization { Id = 10, TrainerId = 5, SpecializationId = 7 },
                new TrainerSpecialization { Id = 11, TrainerId = 5, SpecializationId = 3 },
                new TrainerSpecialization { Id = 12, TrainerId = 6, SpecializationId = 10 },
                new TrainerSpecialization { Id = 13, TrainerId = 6, SpecializationId = 3 },
                new TrainerSpecialization { Id = 14, TrainerId = 7, SpecializationId = 12 },
                new TrainerSpecialization { Id = 15, TrainerId = 7, SpecializationId = 2 },
                new TrainerSpecialization { Id = 16, TrainerId = 8, SpecializationId = 8 },
                new TrainerSpecialization { Id = 17, TrainerId = 8, SpecializationId = 4 },
                new TrainerSpecialization { Id = 18, TrainerId = 9, SpecializationId = 11 },
                new TrainerSpecialization { Id = 19, TrainerId = 9, SpecializationId = 5 },
            };
            context.TrainerSpecializations.AddRange(trainerSpecializations);

            var trainerPackages = new List<TrainerPackage>
            {
                new TrainerPackage { Id = 1, Name = "Starter Plan", Description = "4 sessions per month with basic nutrition guidance. Perfect for beginners.", Price = 200, DurationDays = 30, TrainerId = 1, IsActive = true },
                new TrainerPackage { Id = 2, Name = "Pro Plan", Description = "12 sessions per month with personalized meal plan and weekly check-ins.", Price = 500, DurationDays = 30, TrainerId = 1, IsActive = true },
                new TrainerPackage { Id = 3, Name = "Elite Plan", Description = "Unlimited sessions, full nutrition coaching, supplement guidance, and 24/7 support.", Price = 1000, DurationDays = 30, TrainerId = 1, IsActive = true },
                new TrainerPackage { Id = 4, Name = "Yoga Basics", Description = "8 yoga sessions focusing on fundamentals, flexibility, and breathing techniques.", Price = 150, DurationDays = 30, TrainerId = 2, IsActive = true },
                new TrainerPackage { Id = 5, Name = "Yoga Advanced", Description = "16 advanced yoga sessions with meditation and advanced poses.", Price = 350, DurationDays = 30, TrainerId = 2, IsActive = true },
                new TrainerPackage { Id = 6, Name = "Pilates Fusion", Description = "12 Pilates sessions combined with core strengthening routines.", Price = 300, DurationDays = 30, TrainerId = 2, IsActive = true },
                new TrainerPackage { Id = 7, Name = "CrossFit Intro", Description = "8 beginner-friendly CrossFit sessions to build functional strength.", Price = 250, DurationDays = 30, TrainerId = 3, IsActive = true },
                new TrainerPackage { Id = 8, Name = "CrossFit Pro", Description = "16 high-intensity CrossFit sessions with competition prep.", Price = 600, DurationDays = 30, TrainerId = 3, IsActive = true },
                new TrainerPackage { Id = 9, Name = "Weight Loss Bundle", Description = "12 training sessions + customized meal plan + weekly body measurements.", Price = 400, DurationDays = 30, TrainerId = 4, IsActive = true },
                new TrainerPackage { Id = 10, Name = "Nutrition Only", Description = "Personalized meal plan with bi-weekly consultations and adjustments.", Price = 180, DurationDays = 30, TrainerId = 4, IsActive = true },
                new TrainerPackage { Id = 11, Name = "Boxing Bootcamp", Description = "8 boxing sessions including technique, bag work, and sparring basics.", Price = 280, DurationDays = 30, TrainerId = 5, IsActive = true },
                new TrainerPackage { Id = 12, Name = "Fighter Fit", Description = "16 sessions combining boxing technique with cardio conditioning.", Price = 550, DurationDays = 30, TrainerId = 5, IsActive = true },
                new TrainerPackage { Id = 13, Name = "HIIT Burn", Description = "12 high-intensity interval training sessions to maximize calorie burn.", Price = 220, DurationDays = 30, TrainerId = 6, IsActive = true },
                new TrainerPackage { Id = 14, Name = "Cardio Blast", Description = "16 cardio-focused sessions with variety of equipment and styles.", Price = 380, DurationDays = 30, TrainerId = 6, IsActive = true },
                new TrainerPackage { Id = 15, Name = "Strength Foundation", Description = "12 powerlifting sessions focusing on squat, bench, and deadlift form.", Price = 350, DurationDays = 30, TrainerId = 7, IsActive = true },
                new TrainerPackage { Id = 16, Name = "Power Program", Description = "16 advanced powerlifting sessions with periodization and peaking.", Price = 700, DurationDays = 30, TrainerId = 7, IsActive = true },
                new TrainerPackage { Id = 17, Name = "Recovery Plan", Description = "8 rehabilitation sessions with mobility work and corrective exercises.", Price = 300, DurationDays = 30, TrainerId = 8, IsActive = true },
                new TrainerPackage { Id = 18, Name = "Mobility Mastery", Description = "12 sessions focused on full body mobility and movement quality.", Price = 280, DurationDays = 30, TrainerId = 8, IsActive = true },
                new TrainerPackage { Id = 19, Name = "Street Workout", Description = "8 calisthenics sessions from basics to advanced bodyweight skills.", Price = 200, DurationDays = 30, TrainerId = 9, IsActive = true },
                new TrainerPackage { Id = 20, Name = "Gymnastics Flow", Description = "16 advanced calisthenics sessions including muscle-ups and handstands.", Price = 500, DurationDays = 30, TrainerId = 9, IsActive = true },
            };
            context.TrainerPackages.AddRange(trainerPackages);

            var bookings = new List<Booking>
            {
                new Booking { Id = 1, UserId = 8, TrainerId = 1, BookingDate = DateTime.UtcNow.AddDays(1), StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(10, 0, 0), Status = "Confirmed", Amount = 50, IsPaid = true, CreatedAt = DateTime.UtcNow },
                new Booking { Id = 2, UserId = 9, TrainerId = 1, BookingDate = DateTime.UtcNow.AddDays(1), StartTime = new TimeSpan(11, 0, 0), EndTime = new TimeSpan(12, 0, 0), Status = "Confirmed", Amount = 50, IsPaid = true, CreatedAt = DateTime.UtcNow },
                new Booking { Id = 3, UserId = 10, TrainerId = 2, BookingDate = DateTime.UtcNow.AddDays(2), StartTime = new TimeSpan(8, 0, 0), EndTime = new TimeSpan(9, 0, 0), Status = "Pending", Amount = 45, IsPaid = false, CreatedAt = DateTime.UtcNow },
                new Booking { Id = 4, UserId = 8, TrainerId = 3, BookingDate = DateTime.UtcNow.AddDays(3), StartTime = new TimeSpan(17, 0, 0), EndTime = new TimeSpan(18, 0, 0), Status = "Confirmed", Amount = 60, IsPaid = true, CreatedAt = DateTime.UtcNow },
                new Booking { Id = 5, UserId = 11, TrainerId = 4, BookingDate = DateTime.UtcNow.AddDays(2), StartTime = new TimeSpan(10, 0, 0), EndTime = new TimeSpan(11, 0, 0), Status = "Confirmed", Amount = 40, IsPaid = true, CreatedAt = DateTime.UtcNow },
                new Booking { Id = 6, UserId = 12, TrainerId = 5, BookingDate = DateTime.UtcNow.AddDays(4), StartTime = new TimeSpan(16, 0, 0), EndTime = new TimeSpan(17, 0, 0), Status = "Pending", Amount = 55, IsPaid = false, CreatedAt = DateTime.UtcNow },
                new Booking { Id = 7, UserId = 9, TrainerId = 6, BookingDate = DateTime.UtcNow.AddDays(1), StartTime = new TimeSpan(7, 0, 0), EndTime = new TimeSpan(8, 0, 0), Status = "Confirmed", Amount = 35, IsPaid = true, CreatedAt = DateTime.UtcNow },
                new Booking { Id = 8, UserId = 10, TrainerId = 7, BookingDate = DateTime.UtcNow.AddDays(5), StartTime = new TimeSpan(14, 0, 0), EndTime = new TimeSpan(15, 30, 0), Status = "Pending", Amount = 65, IsPaid = false, CreatedAt = DateTime.UtcNow },
                new Booking { Id = 9, UserId = 11, TrainerId = 8, BookingDate = DateTime.UtcNow.AddDays(3), StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(10, 0, 0), Status = "Confirmed", Amount = 50, IsPaid = true, CreatedAt = DateTime.UtcNow },
                new Booking { Id = 10, UserId = 12, TrainerId = 9, BookingDate = DateTime.UtcNow.AddDays(2), StartTime = new TimeSpan(15, 0, 0), EndTime = new TimeSpan(16, 0, 0), Status = "Confirmed", Amount = 40, IsPaid = true, CreatedAt = DateTime.UtcNow },
                new Booking { Id = 11, UserId = 8, TrainerId = 2, BookingDate = DateTime.UtcNow.AddDays(6), StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(10, 0, 0), Status = "Pending", Amount = 45, IsPaid = false, CreatedAt = DateTime.UtcNow },
                new Booking { Id = 12, UserId = 9, TrainerId = 3, BookingDate = DateTime.UtcNow.AddDays(-2), StartTime = new TimeSpan(17, 0, 0), EndTime = new TimeSpan(18, 0, 0), Status = "Completed", Amount = 60, IsPaid = true, CreatedAt = DateTime.UtcNow.AddDays(-7) },
                new Booking { Id = 13, UserId = 10, TrainerId = 1, BookingDate = DateTime.UtcNow.AddDays(-3), StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(10, 0, 0), Status = "Completed", Amount = 50, IsPaid = true, CreatedAt = DateTime.UtcNow.AddDays(-10) },
                new Booking { Id = 14, UserId = 11, TrainerId = 5, BookingDate = DateTime.UtcNow.AddDays(-1), StartTime = new TimeSpan(16, 0, 0), EndTime = new TimeSpan(17, 0, 0), Status = "Cancelled", Amount = 55, IsPaid = false, CreatedAt = DateTime.UtcNow.AddDays(-5) },
                new Booking { Id = 15, UserId = 12, TrainerId = 6, BookingDate = DateTime.UtcNow.AddDays(-5), StartTime = new TimeSpan(7, 0, 0), EndTime = new TimeSpan(8, 0, 0), Status = "Completed", Amount = 35, IsPaid = true, CreatedAt = DateTime.UtcNow.AddDays(-12) },
            };
            context.Bookings.AddRange(bookings);

            var sessions = new List<Session>
            {
                new Session { Id = 1, TrainerId = 1, BookingId = 1, SessionDate = DateTime.UtcNow.AddDays(1), StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(10, 0, 0), Status = "Scheduled", Notes = "First session - assessment and goal setting" },
                new Session { Id = 2, TrainerId = 1, BookingId = 2, SessionDate = DateTime.UtcNow.AddDays(1), StartTime = new TimeSpan(11, 0, 0), EndTime = new TimeSpan(12, 0, 0), Status = "Scheduled", Notes = "Upper body focus" },
                new Session { Id = 3, TrainerId = 2, BookingId = 3, SessionDate = DateTime.UtcNow.AddDays(2), StartTime = new TimeSpan(8, 0, 0), EndTime = new TimeSpan(9, 0, 0), Status = "Scheduled", Notes = "Beginner yoga flow" },
                new Session { Id = 4, TrainerId = 3, BookingId = 4, SessionDate = DateTime.UtcNow.AddDays(3), StartTime = new TimeSpan(17, 0, 0), EndTime = new TimeSpan(18, 0, 0), Status = "Scheduled", Notes = "CrossFit fundamentals" },
                new Session { Id = 5, TrainerId = 4, BookingId = 5, SessionDate = DateTime.UtcNow.AddDays(2), StartTime = new TimeSpan(10, 0, 0), EndTime = new TimeSpan(11, 0, 0), Status = "Scheduled", Notes = "Nutrition consultation + initial assessment" },
                new Session { Id = 6, TrainerId = 5, BookingId = 6, SessionDate = DateTime.UtcNow.AddDays(4), StartTime = new TimeSpan(16, 0, 0), EndTime = new TimeSpan(17, 0, 0), Status = "Scheduled", Notes = "Boxing basics and footwork" },
                new Session { Id = 7, TrainerId = 6, BookingId = 7, SessionDate = DateTime.UtcNow.AddDays(1), StartTime = new TimeSpan(7, 0, 0), EndTime = new TimeSpan(8, 0, 0), Status = "Scheduled", Notes = "HIIT cardio blast" },
                new Session { Id = 8, TrainerId = 7, BookingId = 8, SessionDate = DateTime.UtcNow.AddDays(5), StartTime = new TimeSpan(14, 0, 0), EndTime = new TimeSpan(15, 30, 0), Status = "Scheduled", Notes = "Powerlifting form review" },
                new Session { Id = 9, TrainerId = 8, BookingId = 9, SessionDate = DateTime.UtcNow.AddDays(3), StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(10, 0, 0), Status = "Scheduled", Notes = "Mobility assessment" },
                new Session { Id = 10, TrainerId = 9, BookingId = 10, SessionDate = DateTime.UtcNow.AddDays(2), StartTime = new TimeSpan(15, 0, 0), EndTime = new TimeSpan(16, 0, 0), Status = "Scheduled", Notes = "Calisthenics basics" },
                new Session { Id = 11, TrainerId = 1, BookingId = 12, SessionDate = DateTime.UtcNow.AddDays(-2), StartTime = new TimeSpan(17, 0, 0), EndTime = new TimeSpan(18, 0, 0), Status = "Completed", Notes = "Great progress on deadlifts" },
                new Session { Id = 12, TrainerId = 1, BookingId = 13, SessionDate = DateTime.UtcNow.AddDays(-3), StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(10, 0, 0), Status = "Completed", Notes = "Completed full body workout" },
                new Session { Id = 13, TrainerId = 6, BookingId = 15, SessionDate = DateTime.UtcNow.AddDays(-5), StartTime = new TimeSpan(7, 0, 0), EndTime = new TimeSpan(8, 0, 0), Status = "Completed", Notes = "Intense HIIT session completed" },
            };
            context.Sessions.AddRange(sessions);

            var payments = new List<Payment>
            {
                new Payment { Id = 1, UserId = 8, BookingId = 1, Amount = 50, PaymentMethod = "Card", Status = "Completed", StripePaymentId = "pi_seed_001", CreatedAt = DateTime.UtcNow },
                new Payment { Id = 2, UserId = 9, BookingId = 2, Amount = 50, PaymentMethod = "Card", Status = "Completed", StripePaymentId = "pi_seed_002", CreatedAt = DateTime.UtcNow },
                new Payment { Id = 3, UserId = 8, BookingId = 4, Amount = 60, PaymentMethod = "Card", Status = "Completed", StripePaymentId = "pi_seed_003", CreatedAt = DateTime.UtcNow },
                new Payment { Id = 4, UserId = 11, BookingId = 5, Amount = 40, PaymentMethod = "Vodafone Cash", Status = "Completed", StripePaymentId = "vc_seed_004", CreatedAt = DateTime.UtcNow },
                new Payment { Id = 5, UserId = 9, BookingId = 7, Amount = 35, PaymentMethod = "Card", Status = "Completed", StripePaymentId = "pi_seed_005", CreatedAt = DateTime.UtcNow },
                new Payment { Id = 6, UserId = 11, BookingId = 9, Amount = 50, PaymentMethod = "Card", Status = "Completed", StripePaymentId = "pi_seed_006", CreatedAt = DateTime.UtcNow },
                new Payment { Id = 7, UserId = 12, BookingId = 10, Amount = 40, PaymentMethod = "PayPal", Status = "Completed", StripePaymentId = "pp_seed_007", CreatedAt = DateTime.UtcNow },
                new Payment { Id = 8, UserId = 8, BookingId = 12, Amount = 60, PaymentMethod = "Card", Status = "Completed", StripePaymentId = "pi_seed_008", CreatedAt = DateTime.UtcNow.AddDays(-7) },
                new Payment { Id = 9, UserId = 10, BookingId = 13, Amount = 50, PaymentMethod = "Card", Status = "Completed", StripePaymentId = "pi_seed_009", CreatedAt = DateTime.UtcNow.AddDays(-10) },
                new Payment { Id = 10, UserId = 12, BookingId = 15, Amount = 35, PaymentMethod = "Vodafone Cash", Status = "Completed", StripePaymentId = "vc_seed_010", CreatedAt = DateTime.UtcNow.AddDays(-12) },
                new Payment { Id = 11, UserId = 8, BookingId = null, Amount = 500, PaymentMethod = "Card", Status = "Completed", StripePaymentId = "pi_seed_011", CreatedAt = DateTime.UtcNow.AddDays(-15) },
                new Payment { Id = 12, UserId = 9, BookingId = null, Amount = 150, PaymentMethod = "Card", Status = "Completed", StripePaymentId = "pi_seed_012", CreatedAt = DateTime.UtcNow.AddDays(-20) },
            };
            context.Payments.AddRange(payments);

            var fitnessProfiles = new List<FitnessProfile>
            {
                new FitnessProfile { Id = 1, UserId = 8, Weight = 85, Height = 175, FitnessGoal = "Weight Loss", FitnessLevel = "Beginner", MedicalConditions = "None", DietaryPreferences = "No restrictions" },
                new FitnessProfile { Id = 2, UserId = 9, Weight = 70, Height = 168, FitnessGoal = "Muscle Building", FitnessLevel = "Intermediate", MedicalConditions = "None", DietaryPreferences = "High protein" },
                new FitnessProfile { Id = 3, UserId = 10, Weight = 95, Height = 180, FitnessGoal = "Weight Loss", FitnessLevel = "Beginner", MedicalConditions = "Lower back pain", DietaryPreferences = "Low carb" },
                new FitnessProfile { Id = 4, UserId = 11, Weight = 62, Height = 165, FitnessGoal = "Toning", FitnessLevel = "Intermediate", MedicalConditions = "None", DietaryPreferences = "Vegetarian" },
                new FitnessProfile { Id = 5, UserId = 12, Weight = 78, Height = 172, FitnessGoal = "Endurance", FitnessLevel = "Advanced", MedicalConditions = "None", DietaryPreferences = "Balanced diet" },
            };
            context.FitnessProfiles.AddRange(fitnessProfiles);

            var workoutHistory = new List<WorkoutHistory>
            {
                new WorkoutHistory { Id = 1, UserId = 8, WorkoutName = "Morning HIIT", Description = "30-min high intensity interval training", DurationMinutes = 30, CaloriesBurned = 350, WorkoutDate = DateTime.UtcNow.AddDays(-1) },
                new WorkoutHistory { Id = 2, UserId = 8, WorkoutName = "Upper Body Strength", Description = "Bench press, rows, shoulder press, bicep curls", DurationMinutes = 45, CaloriesBurned = 280, WorkoutDate = DateTime.UtcNow.AddDays(-3) },
                new WorkoutHistory { Id = 3, UserId = 8, WorkoutName = "Cardio Run", Description = "5km outdoor running", DurationMinutes = 35, CaloriesBurned = 400, WorkoutDate = DateTime.UtcNow.AddDays(-5) },
                new WorkoutHistory { Id = 4, UserId = 9, WorkoutName = "Chest Day", Description = "Incline bench, flat bench, cable flyes, dips", DurationMinutes = 60, CaloriesBurned = 420, WorkoutDate = DateTime.UtcNow.AddDays(-1) },
                new WorkoutHistory { Id = 5, UserId = 9, WorkoutName = "Leg Day", Description = "Squats, lunges, leg press, hamstring curls", DurationMinutes = 55, CaloriesBurned = 500, WorkoutDate = DateTime.UtcNow.AddDays(-4) },
                new WorkoutHistory { Id = 6, UserId = 9, WorkoutName = "Back & Biceps", Description = "Deadlifts, pull-ups, barbell rows, hammer curls", DurationMinutes = 50, CaloriesBurned = 380, WorkoutDate = DateTime.UtcNow.AddDays(-6) },
                new WorkoutHistory { Id = 7, UserId = 10, WorkoutName = "Walking", Description = "30 min brisk walking", DurationMinutes = 30, CaloriesBurned = 150, WorkoutDate = DateTime.UtcNow.AddDays(-1) },
                new WorkoutHistory { Id = 8, UserId = 10, WorkoutName = "Stretching", Description = "Full body stretching routine", DurationMinutes = 20, CaloriesBurned = 80, WorkoutDate = DateTime.UtcNow.AddDays(-2) },
                new WorkoutHistory { Id = 9, UserId = 11, WorkoutName = "Pilates Core", Description = "Mat pilates focusing on core engagement", DurationMinutes = 40, CaloriesBurned = 200, WorkoutDate = DateTime.UtcNow.AddDays(-1) },
                new WorkoutHistory { Id = 10, UserId = 11, WorkoutName = "Yoga Flow", Description = "Vinyasa yoga flow", DurationMinutes = 50, CaloriesBurned = 180, WorkoutDate = DateTime.UtcNow.AddDays(-3) },
                new WorkoutHistory { Id = 11, UserId = 12, WorkoutName = "Swimming", Description = "1km freestyle swimming", DurationMinutes = 30, CaloriesBurned = 450, WorkoutDate = DateTime.UtcNow.AddDays(-1) },
                new WorkoutHistory { Id = 12, UserId = 12, WorkoutName = "Cycling", Description = "20km outdoor cycling", DurationMinutes = 60, CaloriesBurned = 550, WorkoutDate = DateTime.UtcNow.AddDays(-2) },
                new WorkoutHistory { Id = 13, UserId = 12, WorkoutName = "Interval Sprint", Description = "10x100m sprints with rest", DurationMinutes = 25, CaloriesBurned = 300, WorkoutDate = DateTime.UtcNow.AddDays(-4) },
                new WorkoutHistory { Id = 14, UserId = 8, WorkoutName = "Leg Day", Description = "Squats, deadlifts, leg press", DurationMinutes = 50, CaloriesBurned = 400, WorkoutDate = DateTime.UtcNow.AddDays(-7) },
                new WorkoutHistory { Id = 15, UserId = 9, WorkoutName = "Shoulders & Arms", Description = "OHP, lateral raises, tricep pushdowns", DurationMinutes = 45, CaloriesBurned = 320, WorkoutDate = DateTime.UtcNow.AddDays(-8) },
            };
            context.WorkoutHistories.AddRange(workoutHistory);

            var progressActivities = new List<ProgressActivity>
            {
                new ProgressActivity { Id = 1, UserId = 8, ActivityType = "Weight", Value = 85, Unit = "kg", ActivityDate = DateTime.UtcNow.AddDays(-30) },
                new ProgressActivity { Id = 2, UserId = 8, ActivityType = "Weight", Value = 83.5, Unit = "kg", ActivityDate = DateTime.UtcNow.AddDays(-20) },
                new ProgressActivity { Id = 3, UserId = 8, ActivityType = "Weight", Value = 82, Unit = "kg", ActivityDate = DateTime.UtcNow.AddDays(-10) },
                new ProgressActivity { Id = 4, UserId = 8, ActivityType = "Weight", Value = 81, Unit = "kg", ActivityDate = DateTime.UtcNow },
                new ProgressActivity { Id = 5, UserId = 8, ActivityType = "Body Fat", Value = 25, Unit = "%", ActivityDate = DateTime.UtcNow.AddDays(-30) },
                new ProgressActivity { Id = 6, UserId = 8, ActivityType = "Body Fat", Value = 23.5, Unit = "%", ActivityDate = DateTime.UtcNow },
                new ProgressActivity { Id = 7, UserId = 9, ActivityType = "Weight", Value = 68, Unit = "kg", ActivityDate = DateTime.UtcNow.AddDays(-30) },
                new ProgressActivity { Id = 8, UserId = 9, ActivityType = "Weight", Value = 69.5, Unit = "kg", ActivityDate = DateTime.UtcNow.AddDays(-15) },
                new ProgressActivity { Id = 9, UserId = 9, ActivityType = "Weight", Value = 70, Unit = "kg", ActivityDate = DateTime.UtcNow },
                new ProgressActivity { Id = 10, UserId = 9, ActivityType = "Bench Press Max", Value = 80, Unit = "kg", ActivityDate = DateTime.UtcNow.AddDays(-30) },
                new ProgressActivity { Id = 11, UserId = 9, ActivityType = "Bench Press Max", Value = 87.5, Unit = "kg", ActivityDate = DateTime.UtcNow },
                new ProgressActivity { Id = 12, UserId = 10, ActivityType = "Weight", Value = 98, Unit = "kg", ActivityDate = DateTime.UtcNow.AddDays(-30) },
                new ProgressActivity { Id = 13, UserId = 10, ActivityType = "Weight", Value = 96, Unit = "kg", ActivityDate = DateTime.UtcNow.AddDays(-15) },
                new ProgressActivity { Id = 14, UserId = 10, ActivityType = "Weight", Value = 94, Unit = "kg", ActivityDate = DateTime.UtcNow },
                new ProgressActivity { Id = 15, UserId = 10, ActivityType = "Waist", Value = 102, Unit = "cm", ActivityDate = DateTime.UtcNow.AddDays(-30) },
                new ProgressActivity { Id = 16, UserId = 10, ActivityType = "Waist", Value = 99, Unit = "cm", ActivityDate = DateTime.UtcNow },
                new ProgressActivity { Id = 17, UserId = 11, ActivityType = "Weight", Value = 63, Unit = "kg", ActivityDate = DateTime.UtcNow.AddDays(-30) },
                new ProgressActivity { Id = 18, UserId = 11, ActivityType = "Weight", Value = 62.5, Unit = "kg", ActivityDate = DateTime.UtcNow },
                new ProgressActivity { Id = 19, UserId = 12, ActivityType = "Weight", Value = 79, Unit = "kg", ActivityDate = DateTime.UtcNow.AddDays(-30) },
                new ProgressActivity { Id = 20, UserId = 12, ActivityType = "Weight", Value = 78, Unit = "kg", ActivityDate = DateTime.UtcNow },
                new ProgressActivity { Id = 21, UserId = 12, ActivityType = "5K Time", Value = 25.5, Unit = "min", ActivityDate = DateTime.UtcNow.AddDays(-30) },
                new ProgressActivity { Id = 22, UserId = 12, ActivityType = "5K Time", Value = 23.2, Unit = "min", ActivityDate = DateTime.UtcNow },
            };
            context.ProgressActivities.AddRange(progressActivities);

            var contacts = new List<Contact>
            {
                new Contact { Id = 1, Name = "John Doe", Email = "john@example.com", Subject = "Membership Inquiry", Message = "I would like to know about your monthly membership plans and what they include.", IsRead = true, CreatedAt = DateTime.UtcNow.AddDays(-5) },
                new Contact { Id = 2, Name = "Jane Smith", Email = "jane@example.com", Subject = "Personal Training", Message = "Do you offer one-on-one personal training sessions? I am looking for a weight loss program.", IsRead = true, CreatedAt = DateTime.UtcNow.AddDays(-3) },
                new Contact { Id = 3, Name = "Mike Wilson", Email = "mike@example.com", Subject = "Trial Session", Message = "Can I schedule a free trial session before committing to a membership?", IsRead = false, CreatedAt = DateTime.UtcNow.AddDays(-1) },
                new Contact { Id = 4, Name = "Emily Brown", Email = "emily@example.com", Subject = "Nutrition Advice", Message = "I need help with meal planning. Do your trainers provide nutrition coaching?", IsRead = false, CreatedAt = DateTime.UtcNow },
                new Contact { Id = 5, Name = "Alex Turner", Email = "alex@example.com", Subject = "Group Classes", Message = "Are there group fitness classes available? I am interested in yoga and HIIT classes.", IsRead = false, CreatedAt = DateTime.UtcNow },
            };
            context.Contacts.AddRange(contacts);

            var packagePurchases = new List<PackagePurchase>
            {
                new PackagePurchase { Id = 1, UserId = 8, TrainerPackageId = 2, PurchaseDate = DateTime.UtcNow.AddDays(-15), ExpiryDate = DateTime.UtcNow.AddDays(15), Status = "Active", AmountPaid = 500 },
                new PackagePurchase { Id = 2, UserId = 9, TrainerPackageId = 4, PurchaseDate = DateTime.UtcNow.AddDays(-10), ExpiryDate = DateTime.UtcNow.AddDays(20), Status = "Active", AmountPaid = 150 },
                new PackagePurchase { Id = 3, UserId = 10, TrainerPackageId = 9, PurchaseDate = DateTime.UtcNow.AddDays(-5), ExpiryDate = DateTime.UtcNow.AddDays(25), Status = "Active", AmountPaid = 400 },
                new PackagePurchase { Id = 4, UserId = 11, TrainerPackageId = 6, PurchaseDate = DateTime.UtcNow.AddDays(-25), ExpiryDate = DateTime.UtcNow.AddDays(5), Status = "Active", AmountPaid = 300 },
                new PackagePurchase { Id = 5, UserId = 12, TrainerPackageId = 13, PurchaseDate = DateTime.UtcNow.AddDays(-20), ExpiryDate = DateTime.UtcNow.AddDays(10), Status = "Active", AmountPaid = 220 },
                new PackagePurchase { Id = 6, UserId = 8, TrainerPackageId = 7, PurchaseDate = DateTime.UtcNow.AddDays(-45), ExpiryDate = DateTime.UtcNow.AddDays(-15), Status = "Expired", AmountPaid = 250 },
                new PackagePurchase { Id = 7, UserId = 9, TrainerPackageId = 11, PurchaseDate = DateTime.UtcNow.AddDays(-40), ExpiryDate = DateTime.UtcNow.AddDays(-10), Status = "Expired", AmountPaid = 280 },
            };
            context.PackagePurchases.AddRange(packagePurchases);

            context.SaveChanges();
        }
    }
}
