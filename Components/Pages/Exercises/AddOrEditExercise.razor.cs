using Microsoft.AspNetCore.Components;
using WorkoutApp.DTOs;
using WorkoutApp.Entities;
using WorkoutApp.Repositories.Implementation;
using WorkoutApp.Repositories.Interfaces;

namespace WorkoutApp.Components.Pages.Exercises
{
    public partial class AddOrEditExercise : ComponentBase
    {

        [Parameter]
        public int? ExerciseId { get; set; }

        [SupplyParameterFromForm]
        public ExerciseDto exercise { get; set; } = new ExerciseDto();

        [Inject]
        public IExerciseRepository ExerciseRepository { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public async Task AddExercise()
        {
            await ExerciseRepository.AddExercise(exercise);

            await InvokeAsync(() => NavigationManager.NavigateTo("/exercises"));
        }

        protected override void OnParametersSet()
        {
            if (ExerciseId != null)
            {
                exercise = ExerciseRepository.GetExerciseById(ExerciseId.Value);
            }
        }

        public async Task EditExercise()
        {
            ExerciseRepository.EditExercise(exercise);


            //NavigationManager.NavigateTo("/users");
            await InvokeAsync(() => NavigationManager.NavigateTo("/exercises"));

        }
    }
}
