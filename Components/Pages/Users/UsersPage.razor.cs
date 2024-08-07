﻿using Blazorise;
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

       
        private UserDto SelectedUser { get; set; }

        [Inject]
        public IUserRepository UserRepository { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }


        private Modal modalRef;

        private bool cancelClose;

        protected override void OnInitialized()
        {
            UsersData = UserRepository.GetAllUsers().OrderByDescending(p => p.Id).ToList();
        }

        private void OnAddButtonClicked()
        {
            NavigationManager.NavigateTo($"/user/add");
        }

        // add workout for user
        private void OnAddWorkoutButtonClicked(UserDto user)
        {
           
            NavigationManager.NavigateTo($"/workout/add/{user.Id}");
            
        }

        // see workouts for user
        private void SeeWorkoutsButtonClicked(UserDto user)
        {
            NavigationManager.NavigateTo($"/workouts/{user.Id}");
        }

        private void EditUser(UserDto user)
        {
            NavigationManager.NavigateTo($"/user/edit/{user.Id}");
        }

        private void OnDeleteButtonClicked(UserDto user)
        {

            SelectedUser = user;


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
            if (SelectedUser != null)
            {
                UserRepository.DeleteUser(SelectedUser.Id);
                OnInitialized();
            }

            cancelClose = false;
            return modalRef.Hide();
        }

        

        





    }
}

