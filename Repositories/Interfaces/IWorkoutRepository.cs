using WorkoutApp.Entities;

namespace WorkoutApp.Repositories.Interfaces
{
    public interface IWorkoutRepository
    {
        ICollection<Workout> GetWorkouts();
        Workout GetWorkoutById(int id);
        void AddWorkout(Workout workout);
        void UpdateWorkout(Workout workout, int id);
    }
}
