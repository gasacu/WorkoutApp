using Microsoft.EntityFrameworkCore;
using System.Xml;
using WorkoutApp.Context;
using WorkoutApp.DTOs;
using WorkoutApp.Entities;
using WorkoutApp.Mappers;
using WorkoutApp.Repositories.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WorkoutApp.Repositories.Implementation
{
    public class WorkoutRepository : IWorkoutRepository
    {
        private readonly WorkoutAppContext _context;

        public WorkoutRepository(WorkoutAppContext context)
        {
            _context = context;
        }

        public IList<WorkoutDto> GetAllWorkouts()
        {

            var workouts = new List<WorkoutDto>();
            var allWorkouts = _context.Workouts.Include(x => x.User).ToList();
            if (allWorkouts?.Any() == true)
            {
                foreach (var workout in allWorkouts)
                {
                    var workoutDto = WorkoutMapper.ToWorkoutDto(workout);
                    workouts.Add(workoutDto);
                }
            }

            return workouts;

        }

        public WorkoutDto GetWorkoutById(int id)
        {
            var workout = _context.Workouts.FirstOrDefault(x => x.Id == id);
            var workoutDto = WorkoutMapper.ToWorkoutDto(workout);

            return workoutDto;
        }

        public IList<WorkoutDto> GetWorkoutsByUserId(int id)
        {

            var workouts = new List<WorkoutDto>();
            var allWorkouts = _context.Workouts.Where(w => w.UserId == id).Include(x => x.User).ToList();
            if (allWorkouts?.Any() == true)
            {
                foreach (var workout in allWorkouts)
                {
                    var workoutDto = WorkoutMapper.ToWorkoutDto(workout);
                    workouts.Add(workoutDto);
                }
            }

            return workouts;

        }

        public async Task AddWorkout(WorkoutDto workoutDto)
        {
            var workout = WorkoutMapper.ToWorkout(workoutDto);

            _context.Workouts.Add(workout);
            await _context.SaveChangesAsync();
        }


        public void EditWorkout(WorkoutDto workoutDto)
        {
            var existingWorkout = _context.Workouts.FirstOrDefault(x => x.Id == workoutDto.Id);

            if (existingWorkout != null)
            {
                existingWorkout.Name = workoutDto.Name;
                existingWorkout.Date = workoutDto.Date;
                existingWorkout.UserId = workoutDto.UserId;
                existingWorkout.Id = workoutDto.Id;

                _context.Workouts.Update(existingWorkout);
                _context.SaveChanges();
            }
        }

        public void DeleteWorkout(int id)
        {
            var existingWorkout = _context.Workouts.Find(id);
            if (existingWorkout != null)
            {
                _context.Workouts.Remove(existingWorkout);
                _context.SaveChanges();
            }
        }
    }
}
