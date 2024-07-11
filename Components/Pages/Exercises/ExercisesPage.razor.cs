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

        private void EditExercise(ExerciseDto exercise)
        {
            NavigationManager.NavigateTo($"/exercise/edit/{exercise.Id}");
        }


        private void OnDeleteButtonClicked(ExerciseDto exercise)
        {

            SelectedExercise = exercise;

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
