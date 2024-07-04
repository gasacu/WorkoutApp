using WorkoutApp.Context;
using WorkoutApp.DTOs;
using WorkoutApp.Entities;
using WorkoutApp.Repositories.Interfaces;

namespace WorkoutApp.Repositories.Implementation
{
    public class ExerciseRepository : IExerciseRepository
    {
        private readonly WorkoutAppContext _context;

        public ExerciseRepository(WorkoutAppContext context) { 
            _context = context;
        }

        public async Task AddExercise(ExerciseDto exerciseDto)
        {
            Exercise exercise = new Exercise()
            {
                Description = exerciseDto.Description,
                Type = exerciseDto.Type,
                ExerciseLogs = exerciseDto.ExerciseLogs,
            };

            _context.Exercises.Add(exercise);
            _context.SaveChanges();
        }

        public void DeleteExercise(int id)
        {
            var existingExercise = _context.Exercises.Find(id);
            if (existingExercise != null)
            {
                _context.Exercises.Remove(existingExercise);
                _context.SaveChanges();
            }
        }

        public void EditExercise(ExerciseDto exerciseDto)
        {
            var existingExercise = _context.Exercises.FirstOrDefault(x => x.Id == exerciseDto.Id);
            if (existingExercise != null)
            {
                existingExercise.Id = exerciseDto.Id;
                existingExercise.Description = exerciseDto.Description;
                existingExercise.Type = exerciseDto.Type;
                existingExercise.ExerciseLogs = exerciseDto.ExerciseLogs;



                _context.Exercises.Update(existingExercise);
                _context.SaveChanges();
            }

        }

        public IList<ExerciseDto> GetAllExercises()
        {
            var exercises = new List<ExerciseDto>();
            var allExercises = _context.Exercises.ToList();
            if (allExercises?.Any() == true)
            {
                foreach (var exercise in allExercises)
                {
                    exercises.Add(new ExerciseDto()
                    {
                        Description = exercise.Description,
                        Type = exercise.Type,
                        Id = exercise.Id
                    });
                }
            }

            return exercises;
        }

        public ExerciseDto GetExerciseById(int id)
        {
            var exercise = _context.Exercises.FirstOrDefault(x => x.Id == id);
            ExerciseDto exerciseDto = new ExerciseDto()
            {
                Id = id,
                Description = exercise.Description,
                Type = exercise.Type,
                ExerciseLogs = exercise.ExerciseLogs
            };

            return exerciseDto;
        }

       
    }
}
