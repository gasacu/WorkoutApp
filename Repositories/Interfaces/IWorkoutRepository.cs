using WorkoutApp.DTOs;
using WorkoutApp.Entities;

namespace WorkoutApp.Repositories.Interfaces
{
    public interface IWorkoutRepository
    {
        IList<WorkoutDto> GetAllWorkouts();
        WorkoutDto GetWorkoutById(int id);
        Task AddWorkout(WorkoutDto workoutDto);
        void EditWorkout(WorkoutDto workoutDto);
        void DeleteWorkout(int id);
    }
}
