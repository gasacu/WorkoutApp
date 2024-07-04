using Microsoft.AspNetCore.Components;
using WorkoutApp.Entities;
using WorkoutApp.Repositories.Interfaces;

namespace WorkoutApp.Components.Pages
{
    public partial class ExerciseLogsPage : ComponentBase
    {
        [Inject]
        public IExerciseLogRepository ExerciseLogRepository { get; set; }
        public List<ExerciseLog> exerciseLogs { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            exerciseLogs = ExerciseLogRepository.GetExerciseLogs().ToList();
        }
    }
}
