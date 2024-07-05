using WorkoutApp.DTOs;
using WorkoutApp.Entities;

namespace WorkoutApp.Mappers
{
    public static class WorkoutMapper
    {
        public static Workout ToWorkout(WorkoutDto workoutDto)
        {
            return new Workout
            {
                Name = workoutDto.Name,
                Date = workoutDto.Date,
                UserId = workoutDto.UserId,
                Id = workoutDto.Id
            };
        }

        public static WorkoutDto ToWorkoutDto(Workout workout)
        {
            return new WorkoutDto
            {
                Name = workout.Name,
                Date = workout.Date,
                UserId = workout.UserId,
                Id = workout.Id
            };
        }
    }
}
