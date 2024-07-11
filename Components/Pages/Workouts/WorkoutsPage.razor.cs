using Blazorise.DataGrid;
using Blazorise;
using Microsoft.AspNetCore.Components;
using WorkoutApp.DTOs;
using WorkoutApp.Entities;
using WorkoutApp.Repositories.Implementation;
using WorkoutApp.Repositories.Interfaces;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using WorkoutApp.Authorization;

namespace WorkoutApp.Components.Pages.Workouts
{
    public partial class WorkoutsPage : ComponentBase
    {
        [SupplyParameterFromForm]
        public List<WorkoutDto> WorkoutsData { get; set; }

        private WorkoutDto SelectedWorkout;

        public UserDto User { get; set; }

        [Parameter]
        public int? UserId { get; set; }

        [Inject]
        public IWorkoutRepository WorkoutRepository { get; set; }

        [Inject]
        public IUserRepository UserRepository { get; set; }

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

            if(userSession == null)
            {
                // Handle the case when the user session is not available
                NavigationManager.NavigateTo("/login", true);
                return;
            }

            // Use your custom AuthorizationService to get the current user
            var isAdmin = userSession.IsTrainer;    // Assuming IsTrainer indicates admin rights

            if(isAdmin)
            {
                // Admin can view all workouts or specific user's workouts if UserId is provided
                WorkoutsData = UserId.HasValue
                    ? WorkoutRepository.GetWorkoutsByUserId(UserId.Value).ToList()
                    : WorkoutRepository.GetAllWorkouts().ToList();
            }
            else
            {
                // Regular users can only view their own workouts
                WorkoutsData = WorkoutRepository.GetWorkoutsByUserId(userSession.Id).ToList();
            }

            User = userSession;

        }

        // add Exercise Log for Workout
        private void OnAddExerciseLogButtonClicked(EditCommandContext<WorkoutDto> context)
        {
            if (context != null && context.Item != null)
            {
                NavigationManager.NavigateTo($"/exerciselog/add/{context.Item.Id}");
            }
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
