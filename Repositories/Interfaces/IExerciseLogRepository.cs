using WorkoutApp.Entities;

namespace WorkoutApp.Repositories.Interfaces
{
    public interface IExerciseLogRepository
    {
        ICollection<ExerciseLog> GetExerciseLogs();
        ExerciseLog GetExerciseLogById(int id);
        void AddExerciseLog(ExerciseLog exerciseLog);
        void UpdateExerciseLog(ExerciseLog exerciseLog, int id);
    }
}
