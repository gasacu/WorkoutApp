using Microsoft.AspNetCore.Identity;

namespace WorkoutApp.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string Gender { get; set; }
        public ICollection<Workout> Workouts { get; set; }
        public string? Email { get; set; }
        public bool IsTrainer { get; set; } = false;
    }
}
