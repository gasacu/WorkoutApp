using Microsoft.AspNetCore.Components;
using WorkoutApp.DTOs;
using WorkoutApp.Entities;
using WorkoutApp.Repositories.Implementation;
using WorkoutApp.Repositories.Interfaces;

namespace WorkoutApp.Components.Pages.ExerciseLogs
{
    public partial class AddOrEditExerciseLog : ComponentBase
    {
        [Parameter]
        public int? ExerciseLogId { get; set; }

        [Parameter]
        public int? WorkoutId { get; set; }

        [Parameter]
        public int? ExerciseId { get; set; }
        public List<ExerciseDto> exercises { get; set; }

        [SupplyParameterFromForm]
        public ExerciseLogDto exerciseLog { get; set; } = new ExerciseLogDto();

        [SupplyParameterFromForm]
        public WorkoutDto workout { get; set; }

        [SupplyParameterFromForm]
        public ExerciseDto exercise { get; set; }


        [Inject]
        public IExerciseLogRepository ExerciseLogRepository { get; set; }

        [Inject]
        public IWorkoutRepository WorkoutRepository { get; set; }

        [Inject]
        public IExerciseRepository ExerciseRepository { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override void OnParametersSet()
        {
            if (ExerciseLogId != null)
            {
                exerciseLog = ExerciseLogRepository.GetExerciseLogById(ExerciseLogId.Value);
            }

            if (WorkoutId != null)
            {
                workout = WorkoutRepository.GetWorkoutById(WorkoutId.Value);
                exerciseLog.WorkoutId = WorkoutId.Value;
            }

            if (ExerciseId != null)
            {
                exercise = ExerciseRepository.GetExerciseById(ExerciseId.Value);
                exerciseLog.ExerciseId = ExerciseId.Value;
            }

            exercises = ExerciseRepository.GetAllExercises().ToList();

        }


        public async Task Save()
        {
            if (ExerciseLogId == null)
            {
                exerciseLog.WorkoutId = workout.Id;
                await ExerciseLogRepository.AddExerciseLog(exerciseLog);
            }
            else
            {
                ExerciseLogRepository.EditExerciseLog(exerciseLog);
            }

            await InvokeAsync(() => NavigationManager.NavigateTo("/exerciselogs"));
        }
    }
}
