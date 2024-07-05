using System.Reflection.Metadata.Ecma335;
using WorkoutApp.DTOs;
using WorkoutApp.Entities;

namespace WorkoutApp.Mappers
{
    public static class ExerciseMapper
    {
        public static Exercise ToExercise(ExerciseDto exerciseDto)
        {
            return new Exercise
            {
                Description = exerciseDto.Description,
                ExerciseLogs = exerciseDto.ExerciseLogs,
                Type = exerciseDto.Type,
                Id = exerciseDto.Id  
            };
        }

        public static ExerciseDto ToExerciseDto(Exercise exercise)
        {
            return new ExerciseDto
            {
                Description = exercise.Description,
                Type = exercise.Type,
                ExerciseLogs = exercise.ExerciseLogs,
                Id = exercise.Id
            };
        }
    }
}
