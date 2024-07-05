using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WorkoutApp.DTOs;
using WorkoutApp.Entities;
using WorkoutApp.Repositories.Implementation;
using WorkoutApp.Repositories.Interfaces;

namespace WorkoutApp.Components.Pages.Workouts
{
    public partial class AddOrEditWorkout : ComponentBase
    {

        [Parameter]
        public int? WorkoutId { get; set; }

        [Parameter]
        public int? UserId { get; set; }

        [SupplyParameterFromForm]
        public UserDto user { get; set; } = new UserDto();

        [SupplyParameterFromForm]
        public WorkoutDto workout { get; set; } = new WorkoutDto();

        [Inject]
        public IWorkoutRepository WorkoutRepository { get; set; }

        [Inject]
        public IUserRepository UserRepository { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override void OnParametersSet()
        {
            if (WorkoutId != null)
            {
                workout = WorkoutRepository.GetWorkoutById(WorkoutId.Value);
            }

            if (UserId != null)
            {
                user = UserRepository.GetUserById(UserId.Value);
                workout.UserId = UserId.Value;
            }
        }


        public async Task Save()
        {
            if(WorkoutId == null)
            {
                workout.UserId = user.Id;
                await WorkoutRepository.AddWorkout(workout);
            }
            else
            {
                WorkoutRepository.EditWorkout(workout);
            }

            await InvokeAsync(() => NavigationManager.NavigateTo("/workouts"));
        }

       
       
    }
}
