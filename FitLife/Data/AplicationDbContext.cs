using FitLife.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace FitLife.Data
{
    public class AplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationUserRole, string>, IAplicationDbContext
    { 
        public DbSet<Product> Products { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Diet> Diets { get; set; }
        public DbSet<Training> Trainings { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<PartOfBody> PartsOfBody { get; set; }
        public DbSet<TrainingExercise> TrainingExercises { get; set; }
        public DbSet<Dimensions> Dimensions { get; set; }
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .ToTable("Product");
            modelBuilder.Entity<Product>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Meal>()
                .ToTable("Meal");
            modelBuilder.Entity<Meal>()
                .HasKey(m => m.Id);
            modelBuilder.Entity<Meal>()
                .HasOne(m => m.Diet)
                .WithMany(d => d.Meals)
                .HasForeignKey(m => m.DietId);


            modelBuilder.Entity<Diet>()
                .ToTable("Diet");
            modelBuilder.Entity<Diet>()
               .HasKey(d => d.Id);
            modelBuilder.Entity<Diet>()
                .HasOne(d => d.ApplicationUser)
                .WithMany(au => au.Diets)
                .HasForeignKey(d => d.ApplicationUserId);

            modelBuilder.Entity<TrainingExercise>()
                .ToTable("TrainingExercise");
            modelBuilder.Entity<TrainingExercise>()
       .HasKey(te => new { te.TrainingId, te.ExerciseId });
            modelBuilder.Entity<TrainingExercise>()
                .HasOne(te => te.Training)
                .WithMany(t => t.TrainingExercises)
                .HasForeignKey(te => te.TrainingId);
            modelBuilder.Entity<TrainingExercise>()
                .HasOne(te => te.Exercise)
                .WithMany(e => e.TrainingExercises)
                .HasForeignKey(te => te.ExerciseId);

            modelBuilder.Entity<Training>()
                .ToTable("Training");
            modelBuilder.Entity<Training>()
                .HasKey(t => t.Id);
            modelBuilder.Entity<Training>()
                .HasOne(t => t.ApplicationUser)
                .WithMany(au => au.trainings)
                .HasForeignKey(t => t.ApplicationUserId);

            modelBuilder.Entity<Exercise>()
                .ToTable("Exercise");
            modelBuilder.Entity<Exercise>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<PartOfBody>()
                .ToTable("PartOfBody");
            modelBuilder.Entity<PartOfBody>()
                 .HasKey(p => p.Id);
            modelBuilder.Entity<PartOfBody>()
                 .HasMany(p => p.Exercises)
                 .WithOne(e => e.PartOfBody);

            modelBuilder.Entity<Dimensions>()
                .ToTable("Dimensions");
            modelBuilder.Entity<Dimensions>()
                .HasKey(d => d.Id);
            modelBuilder.Entity<Dimensions>()
                .HasOne(d => d.ApplicationUser)
                .WithMany(au => au.Dimensions)
                .HasForeignKey(d => d.ApplicationUserId);

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        //{
        //    optionBuilder.UseLazyLoadingProxies();
        //}
    }
}
