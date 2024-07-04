using Microsoft.AspNetCore.Components;
using WorkoutApp.Entities;
using WorkoutApp.Repositories.Implementation;
using WorkoutApp.Repositories.Interfaces;

namespace WorkoutApp.Components.Pages
{
    public partial class WorkoutsPage : ComponentBase
    {
        [Inject]
        public IWorkoutRepository WorkoutRepository { get; set; }

        List<Workout> workouts { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            workouts = WorkoutRepository.GetWorkouts().ToList();
        }
    }
}
