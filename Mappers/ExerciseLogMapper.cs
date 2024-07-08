using WorkoutApp.DTOs;
using WorkoutApp.Entities;

namespace WorkoutApp.Mappers
{
    public static class ExerciseLogMapper
    {
        public static ExerciseLog ToExerciseLog(ExerciseLogDto exerciseLogDto)
        {
            return new ExerciseLog
            {
                Reps = exerciseLogDto.Reps,
                Duration = exerciseLogDto.Duration,
                ExerciseId = exerciseLogDto.ExerciseId,
                WorkoutId = exerciseLogDto.WorkoutId,
                Workout = exerciseLogDto.Workout,
                Exercise = exerciseLogDto.Exercise,
                Id = exerciseLogDto.Id
            };
        }

        public static ExerciseLogDto ToExerciseLogDto(ExerciseLog exerciseLog)
        {
            return new ExerciseLogDto
            {
                Reps = exerciseLog.Reps,
                Duration = exerciseLog.Duration,
                ExerciseId = exerciseLog.ExerciseId,
                WorkoutId = exerciseLog.WorkoutId,
                Workout = exerciseLog.Workout,
                Exercise = exerciseLog.Exercise,
                Id = exerciseLog.Id
            };
        }
    }
}
