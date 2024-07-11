using Microsoft.EntityFrameworkCore;
using WorkoutApp.Context;
using WorkoutApp.DTOs;
using WorkoutApp.Entities;
using WorkoutApp.Mappers;
using WorkoutApp.Repositories.Interfaces;

namespace WorkoutApp.Repositories.Implementation
{
    public class ExerciseLogRepository : IExerciseLogRepository
    {
        private readonly WorkoutAppContext _context;

        public ExerciseLogRepository(WorkoutAppContext context) 
        {
            _context = context;
        }

        public IList<ExerciseLogDto> GetAllExerciseLogs()
        {
            var exerciseLogs = new List<ExerciseLogDto>();
            var allExerciseLogs = _context.ExerciseLogs.Include(w => w.Workout).Include(e => e.Exercise).ToList();

            if (allExerciseLogs?.Any() == true)
            {
                foreach (var exerciseLog in allExerciseLogs)
                {
                    var exerciseLogDto = ExerciseLogMapper.ToExerciseLogDto(exerciseLog);
                    exerciseLogs.Add(exerciseLogDto);
                }
            }

            return exerciseLogs;
        }


        public ExerciseLogDto GetExerciseLogById(int id)
        {
            var exerciseLog = _context.ExerciseLogs.FirstOrDefault(x => x.Id == id);
            var exerciseLogDto = ExerciseLogMapper.ToExerciseLogDto(exerciseLog);

            return exerciseLogDto;
        }


        public IList<ExerciseLogDto> GetExerciseLogsByUserId(int userId)
        {

            var exerciseLogs = new List<ExerciseLogDto>();
            var allExerciseLogs = _context.ExerciseLogs.Where(w => w.Workout.UserId == userId).Include(x => x.Workout).ToList();
            if (allExerciseLogs?.Any() == true)
            {
                foreach (var exerciseLog in allExerciseLogs)
                {
                    var exerciseLogDto = ExerciseLogMapper.ToExerciseLogDto(exerciseLog);
                    exerciseLogs.Add(exerciseLogDto);
                }
            }

            return exerciseLogs;

        }

        public async Task AddExerciseLog(ExerciseLogDto exerciseLogDto)
        {
            var exerciseLog = ExerciseLogMapper.ToExerciseLog(exerciseLogDto);

            _context.ExerciseLogs.Add(exerciseLog);
            await _context.SaveChangesAsync();
        }

        public void EditExerciseLog(ExerciseLogDto exerciseLogDto)
        {
            var dbExerciseLog = _context.ExerciseLogs.FirstOrDefault(el => el.Id == exerciseLogDto.Id);
            if (dbExerciseLog != null)
            {
                dbExerciseLog.Reps = exerciseLogDto.Reps;
                dbExerciseLog.Duration = exerciseLogDto.Duration;
                dbExerciseLog.ExerciseId = exerciseLogDto.ExerciseId;
                dbExerciseLog.WorkoutId = exerciseLogDto.WorkoutId;
                dbExerciseLog.Id = exerciseLogDto.Id;

                _context.ExerciseLogs.Update(dbExerciseLog);
                _context.SaveChanges();
            }
        }

        public void DeleteExerciseLog(int id)
        {
            var existingExerciseLog = _context.ExerciseLogs.Find(id);
            if (existingExerciseLog != null)
            {
                _context.ExerciseLogs.Remove(existingExerciseLog);
                _context.SaveChanges();
            }
        }

    }
}
