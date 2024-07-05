using Blazorise;
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

        [Inject]
        public IExerciseRepository ExerciseRepository { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        private Modal modalRef;

        private bool cancelClose;

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


        private void OnDeleteButtonClicked(DeleteCommandContext<ExerciseDto> context)
        {

            SelectedExercise = context.Item;

            if (modalRef is null || SelectedExercise is null)
            {
                return;
            }

            modalRef.Show();
        }

        private Task OnModalClosing(ModalClosingEventArgs e)
        {
            // just set Cancel to prevent modal from closing
            e.Cancel = cancelClose
                || e.CloseReason != CloseReason.UserClosing;

            return Task.CompletedTask;
        }

        private Task CloseModal()
        {
            cancelClose = false;

            return modalRef.Hide();
        }


        private Task TryCloseModal()
        {
            if (SelectedExercise != null)
            {
                ExerciseRepository.DeleteExercise(SelectedExercise.Id);
                OnInitialized();
            }

            cancelClose = false;
            return modalRef.Hide();
        }

    }
}
