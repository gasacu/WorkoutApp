using Blazorise.DataGrid;
using Microsoft.AspNetCore.Components;
using System.Collections;
using WorkoutApp.DTOs;
using WorkoutApp.Entities;
using WorkoutApp.Repositories.Implementation;
using WorkoutApp.Repositories.Interfaces;

namespace WorkoutApp.Components.Pages.Users
{
    public partial class UsersPage : ComponentBase
    {
        [SupplyParameterFromForm]
        private List<UserDto> UsersData { get; set; }

        private UserDto SelectedUser;

        public DeleteConfirmationDialog DeleteConfirmation;

        [Inject]
        public IUserRepository UserRepository { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        

        protected override void OnInitialized()
        {
            UsersData = UserRepository.GetAllUsers().OrderByDescending(p => p.Id).ToList();
        }

        private void EditUser(EditCommandContext<UserDto> context)
        {
            if (context != null && context.Item != null)
            {
                NavigationManager.NavigateTo($"/user/edit/{context.Item.Id}");
            }
        }

        private void DeleteUser(DeleteCommandContext<UserDto?> context)
        {
            SelectedUser = context.Item;
            if (DeleteConfirmation is null || SelectedUser is null)
            {
                return;
            }

            DeleteConfirmation.Show();
        }

        private async Task HandleDeleteConfirmed(bool confirmed)
        {
            if (SelectedUser != null)
            {
                UserRepository.DeleteUser(SelectedUser.Id);
                OnInitializedAsync();
            }
        }
    }
}

