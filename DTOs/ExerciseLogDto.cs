using System.ComponentModel.DataAnnotations;
using WorkoutApp.Entities;

namespace WorkoutApp.DTOs
{
    public class ExerciseLogDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Reps is required")]
        public int Reps { get; set; }

        [Required(ErrorMessage = "Duration is required")]
        public int Duration { get; set; }
        public int ExerciseId { get; set; }
        public Exercise Exercise { get; set; }
        public int WorkoutId { get; set; }
        public Workout Workout { get; set; }
    }
}
