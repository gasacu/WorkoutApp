using WorkoutApp.DTOs;
using WorkoutApp.Entities;

namespace WorkoutApp.Repositories.Interfaces
{
    public interface IExerciseRepository
    {
        IList<ExerciseDto> GetAllExercises();
        ExerciseDto GetExerciseById(int id);
        Task AddExercise(ExerciseDto exerciseDto);
        void EditExercise(ExerciseDto exerciseDto);
        void DeleteExercise(int id);

    }
}
