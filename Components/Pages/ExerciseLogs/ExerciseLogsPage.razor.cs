using Blazorise;
using Blazorise.DataGrid;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using WorkoutApp.Authorization;
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

        [Inject] public IAuthorizationService AuthorizationService { get; set; }

        [Inject] public ProtectedSessionStorage SessionStorage { get; set; }


        private Modal modalRef;

        private bool cancelClose;

        protected override async Task OnParametersSetAsync()
        {
            // Fetch the user session from the session storage
            var user = await SessionStorage.GetAsync<UserDto>("UserSession");
            var userSession = user.Success ? user.Value : null;

            if (userSession == null)
            {
                // Handle the case when the user session is not available
                NavigationManager.NavigateTo("/login", true);
                return;
            }

            // Use your custom AuthorizationService to get the current user
            var isAdmin = userSession.IsTrainer;    // Assuming IsTrainer indicates admin rights


            if (isAdmin)
            {
                // Admin can view all exercise logs
                ExerciseLogsData = ExerciseLogRepository.GetAllExerciseLogs().ToList();
            }
            else
            {
                // Regular users can only view their own exercise logs
                ExerciseLogsData = ExerciseLogRepository.GetExerciseLogsByUserId(userSession.Id).ToList();
            }


        
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
