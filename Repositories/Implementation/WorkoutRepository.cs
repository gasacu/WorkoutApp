using Microsoft.EntityFrameworkCore;
using System.Xml;
using WorkoutApp.Context;
using WorkoutApp.Entities;
using WorkoutApp.Repositories.Interfaces;

namespace WorkoutApp.Repositories.Implementation
{
    public class WorkoutRepository : IWorkoutRepository
    {
        private readonly WorkoutAppContext _context;

        public WorkoutRepository(WorkoutAppContext context)
        {
            _context = context;
        }

        public ICollection<Workout> GetWorkouts()
        {

            return _context.Workouts.Include(x => x.User).ToList();

        }

        public Workout GetWorkoutById(int id)
        {
            return _context.Workouts.FirstOrDefault(w => w.Id == id);
        }

        public void AddWorkout(Workout workout)
        {
            _context.Workouts.Add(workout);
            _context.SaveChanges();
        }


        public void UpdateWorkout(Workout workout, int id)
        {
            var dbWorkout = _context.Workouts.FirstOrDefault(w => w.Id == id);
            if (dbWorkout != null)
            {
                dbWorkout.Name = workout.Name;
                dbWorkout.Date = workout.Date;
                dbWorkout.UserId = workout.UserId;

                _context.SaveChanges();
            }
        }
    }
}
