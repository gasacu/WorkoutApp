using System.ComponentModel.DataAnnotations;
using WorkoutApp.Entities;

namespace WorkoutApp.DTOs
{
    public class WorkoutDto
    {
        public int Id { get; set; }

        [StringLength(100, ErrorMessage = "Name cannot be longer that 100 characters")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Date is required")]
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<ExerciseLog> ExerciseLogs { get; set; }
    }
}
