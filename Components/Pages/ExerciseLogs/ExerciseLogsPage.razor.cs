using Blazorise;
using Blazorise.DataGrid;
using Microsoft.AspNetCore.Components;
using WorkoutApp.DTOs;
using WorkoutApp.Entities;
using WorkoutApp.Repositories.Implementation;
using WorkoutApp.Repositories.Interfaces;

namespace WorkoutApp.Components.Pages.ExerciseLogs
{
    public partial class ExerciseLogsPage : ComponentBase
    {
        [SupplyParameterFromForm]
        public List<ExerciseLogDto> ExerciseLogsData { get; set; }

        private ExerciseLogDto SelectedExerciseLog;

        [Inject]
        public IExerciseLogRepository ExerciseLogRepository { get; set; }

        [Inject]
        public IWorkoutRepository WorkoutRepository { get; set; }

        [Inject]
        public IExerciseRepository ExerciseRepository { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        private Modal modalRef;

        private bool cancelClose;

        protected override void OnInitialized()
        {
            ExerciseLogsData = ExerciseLogRepository.GetAllExerciseLogs().ToList();
        }


        private void OnDeleteButtonClicked(DeleteCommandContext<ExerciseLogDto> context)
        {

            SelectedExerciseLog = context.Item;

            if (modalRef is null || SelectedExerciseLog is null)
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
            if (SelectedExerciseLog != null)
            {
                ExerciseLogRepository.DeleteExerciseLog(SelectedExerciseLog.Id);
                OnInitialized();
            }

            cancelClose = false;
            return modalRef.Hide();
        }


       


    }
}
