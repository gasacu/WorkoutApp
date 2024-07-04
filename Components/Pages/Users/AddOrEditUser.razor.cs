using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WorkoutApp.DTOs;
using WorkoutApp.Entities;
using WorkoutApp.Repositories.Implementation;
using WorkoutApp.Repositories.Interfaces;


namespace WorkoutApp.Components.Pages.Users
{
    public partial class AddOrEditUser : ComponentBase
    {

        [Parameter]
        public int? UserId { get; set; }

        [SupplyParameterFromForm]
        public UserDto user { get; set; } = new UserDto();

        [Inject]
        public IUserRepository UserRepository { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public async Task AddUser()
        {
            await UserRepository.AddUser(user);


            //NavigationManager.NavigateTo("/users");
            await InvokeAsync(() => NavigationManager.NavigateTo("/users"));

        }

        protected override void OnParametersSet()
        {
            if (UserId != null)
            {
                user = UserRepository.GetUserById(UserId.Value);
            }
        }

        public async Task EditUser()
        {
            UserRepository.EditUser(user);


            //NavigationManager.NavigateTo("/users");
            await InvokeAsync(() => NavigationManager.NavigateTo("/users"));

        }

        
    }
}
