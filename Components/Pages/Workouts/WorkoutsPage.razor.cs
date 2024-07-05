using Blazorise.DataGrid;
using Blazorise;
using Microsoft.AspNetCore.Components;
using WorkoutApp.DTOs;
using WorkoutApp.Entities;
using WorkoutApp.Repositories.Implementation;
using WorkoutApp.Repositories.Interfaces;

namespace WorkoutApp.Components.Pages.Workouts
{
    public partial class WorkoutsPage : ComponentBase
    {
        [SupplyParameterFromForm]
        public List<WorkoutDto> WorkoutsData { get; set; }

        private WorkoutDto SelectedWorkout;

        [Inject]
        public IWorkoutRepository WorkoutRepository { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        private Modal modalRef;

        private bool cancelClose;

        protected override void OnInitialized()
        {
            WorkoutsData = WorkoutRepository.GetAllWorkouts().ToList();
        }

        private void EditWorkout(EditCommandContext<WorkoutDto> context)
        {
            if (context != null && context.Item != null)
            {
                NavigationManager.NavigateTo($"/workout/edit/{context.Item.Id}");
            }
        }


        private void OnDeleteButtonClicked(DeleteCommandContext<WorkoutDto> context)
        {

            SelectedWorkout = context.Item;

            if (modalRef is null || SelectedWorkout is null)
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
            if (SelectedWorkout != null)
            {
                WorkoutRepository.DeleteWorkout(SelectedWorkout.Id);
                OnInitialized();
            }

            cancelClose = false;
            return modalRef.Hide();
        }
    }
}
