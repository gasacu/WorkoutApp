using WorkoutApp.Context;
using WorkoutApp.DTOs;
using WorkoutApp.Entities;
using WorkoutApp.Mappers;
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
            var exercise = ExerciseMapper.ToExercise(exerciseDto);

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
                    var exerciseDto = ExerciseMapper.ToExerciseDto(exercise);
                    exercises.Add(exerciseDto);
                }
            }

            return exercises;
        }

        public ExerciseDto GetExerciseById(int id)
        {
            var exercise = _context.Exercises.FirstOrDefault(x => x.Id == id);

            var exerciseDto = ExerciseMapper.ToExerciseDto(exercise);

            return exerciseDto;
        }

       
    }
}
