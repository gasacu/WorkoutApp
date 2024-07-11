using WorkoutApp.DTOs;
using WorkoutApp.Entities;

namespace WorkoutApp.Repositories.Interfaces
{
    public interface IExerciseLogRepository
    {
        IList<ExerciseLogDto> GetAllExerciseLogs();
        ExerciseLogDto GetExerciseLogById(int id);
        IList<ExerciseLogDto> GetExerciseLogsByUserId(int userId);
        Task AddExerciseLog(ExerciseLogDto exerciseLogDto);
        void EditExerciseLog(ExerciseLogDto exerciseLogDto);
        void DeleteExerciseLog(int id);
    }
}
