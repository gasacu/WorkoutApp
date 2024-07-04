using Blazorise.DataGrid;
using Microsoft.AspNetCore.Components;
using WorkoutApp.DTOs;
using WorkoutApp.Entities;
using WorkoutApp.Repositories.Implementation;
using WorkoutApp.Repositories.Interfaces;

namespace WorkoutApp.Components.Pages.Exercises
{
    public partial class ExercisesPage : ComponentBase
    {
        [SupplyParameterFromForm]
        public List<ExerciseDto> ExercisesData { get; set; }

        private ExerciseDto SelectedExercise;

        public DeleteConfirmationDialog DeleteConfirmation;

        [Inject]
        public IExerciseRepository ExerciseRepository { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override void OnInitialized()
        {
            ExercisesData = ExerciseRepository.GetAllExercises().OrderByDescending(p => p.Id).ToList();
        }

        private void EditExercise(EditCommandContext<ExerciseDto> context)
        {
            if (context != null && context.Item != null)
            {
                NavigationManager.NavigateTo($"/exercise/edit/{context.Item.Id}");
            }
        }

        private void DeleteExercise(CommandContext<ExerciseDto?> context)
        {
            SelectedExercise = context.Item;
            if (DeleteConfirmation is null || SelectedExercise is null)
            {
                return;
            }

            DeleteConfirmation.Show();
        }

        private async Task HandleDeleteConfirmed(bool confirmed)
        {
            if (SelectedExercise != null)
            {
                ExerciseRepository.DeleteExercise(SelectedExercise.Id);
                OnInitializedAsync();
            }
        }
    }
}
