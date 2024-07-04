namespace WorkoutApp.Entities
{
    public class Exercise
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public ICollection<ExerciseLog> ExerciseLogs { get; set; }
    }
}
