using Microsoft.AspNetCore.Components;
using WorkoutApp.Authorization;
using WorkoutApp.DTOs;
using WorkoutApp.Entities;
using WorkoutApp.Repositories.Implementation;
using WorkoutApp.Repositories.Interfaces;

namespace WorkoutApp.Components.Pages
{
    public partial class Home : ComponentBase
    {
        private UserDto? User { get; set; }


        [Inject]
        public IAuthorizationService AuthorizationService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            User = await AuthorizationService.GetCurrentUserAsync(); ;
        }
            
        
    }
}
