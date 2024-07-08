using WorkoutApp.DTOs;
using WorkoutApp.Entities;

namespace WorkoutApp.Repositories.Interfaces
{
    public interface IExerciseLogRepository
    {
        IList<ExerciseLogDto> GetAllExerciseLogs();
        ExerciseLogDto GetExerciseLogById(int id);
        Task AddExerciseLog(ExerciseLogDto exerciseLogDto);
        void EditExerciseLog(ExerciseLogDto exerciseLogDto);
        void DeleteExerciseLog(int id);
    }
}
