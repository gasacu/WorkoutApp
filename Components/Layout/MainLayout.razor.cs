using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using WorkoutApp.Authentication;
using WorkoutApp.Authorization;
using WorkoutApp.DTOs;

namespace WorkoutApp.Components.Layout
{
    public partial class MainLayout : LayoutComponentBase
    {
        [Inject]
        public IAuthorizationService AuthorizationService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public AuthenticationStateProvider CustomAuthenticationStateProvider { get; set; }

        private UserDto? User { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var authState = await CustomAuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if(user.Identity.IsAuthenticated)
            {
                User = await AuthorizationService.GetCurrentUserAsync();
            }
        }

        private async Task LogOut()
        {
            await AuthorizationService.LogOutAsync();
            NavigationManager.NavigateTo("login");
        }
    }
}
