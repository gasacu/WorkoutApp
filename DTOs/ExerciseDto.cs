using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WorkoutApp.Entities;

namespace WorkoutApp.DTOs
{
    public class ExerciseDto
    {
        public int Id { get; set; }

        [StringLength(100, ErrorMessage = "Description cannot be longer than 100 characters")]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [StringLength(100, ErrorMessage = "Type cannot be longer than 100 characters")]
        [Required(ErrorMessage = "Type is required")]
        public string Type { get; set; }
        public ICollection<ExerciseLog> ExerciseLogs { get; set; }
    }
}
