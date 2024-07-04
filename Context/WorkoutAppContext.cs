using Microsoft.EntityFrameworkCore;
using WorkoutApp.Entities;

namespace WorkoutApp.Context
{
    public class WorkoutAppContext : DbContext
    {
        // Constructor 
        public WorkoutAppContext(DbContextOptions<WorkoutAppContext> options) : base(options)
        {
                
        }

        // Representations of the Entities in the Database
        public DbSet<User> Users { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<ExerciseLog> ExerciseLogs { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure relationship and constraints
            modelBuilder.Entity<Workout>()
                .HasOne(w => w.User)
                .WithMany(u => u.Workouts)
                .HasForeignKey(w => w.UserId)
                .HasConstraintName("FK_Workouts_Users");

            modelBuilder.Entity<ExerciseLog>()
                .HasOne(el => el.Exercise)
                .WithMany(e => e.ExerciseLogs)
                .HasForeignKey(el => el.ExerciseId)
                .HasConstraintName("FK_ExerciseLog_Exercises");

            modelBuilder.Entity<ExerciseLog>()
                .HasOne(el => el.Workout)
                .WithMany(w => w.ExerciseLogs)
                .HasForeignKey(el => el.WorkoutId)
                .HasConstraintName("FK_ExerciseLog_Workouts");

            base.OnModelCreating(modelBuilder);
        }


    }
}
