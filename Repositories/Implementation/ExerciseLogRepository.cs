using WorkoutApp.Context;
using WorkoutApp.Entities;
using WorkoutApp.Repositories.Interfaces;

namespace WorkoutApp.Repositories.Implementation
{
    public class ExerciseLogRepository : IExerciseLogRepository
    {
        private readonly WorkoutAppContext _context;

        public ExerciseLogRepository(WorkoutAppContext context) {
            _context = context;
        }

        public void AddExerciseLog(ExerciseLog exerciseLog)
        {
            _context.ExerciseLogs.Add(exerciseLog);
            _context.SaveChanges();
        }

        public ExerciseLog GetExerciseLogById(int id)
        {
            return _context.ExerciseLogs.FirstOrDefault(el => el.Id == id);
        }

        public ICollection<ExerciseLog> GetExerciseLogs() {
            return _context.ExerciseLogs.ToList();
        }

        public void UpdateExerciseLog(ExerciseLog exerciseLog, int id)
        {
            var dbExerciseLog = _context.ExerciseLogs.FirstOrDefault(el => el.Id == id);
            if (dbExerciseLog != null)
            {
                dbExerciseLog.Reps = exerciseLog.Reps;
                dbExerciseLog.Duration = exerciseLog.Duration;
                dbExerciseLog.ExerciseId = exerciseLog.ExerciseId;
                dbExerciseLog.WorkoutId = exerciseLog.WorkoutId;

                _context.SaveChanges();
            }
        }
    }
}
