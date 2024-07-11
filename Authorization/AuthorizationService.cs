using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Authentication;
using WorkoutApp.Authentication;
using WorkoutApp.DTOs;
using WorkoutApp.Entities;
using WorkoutApp.Repositories.Interfaces;
using WorkoutApp.Mappers;
using System.Security.Claims;

namespace WorkoutApp.Authorization
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IUserRepository _userRepository;
        private readonly CustomAuthenticationStateProvider _customAuthenticationStateProvider;


        public AuthorizationService(IUserRepository userRepository, AuthenticationStateProvider authenticationStateProvider)
        {
            _userRepository = userRepository;
            _customAuthenticationStateProvider = (CustomAuthenticationStateProvider)authenticationStateProvider;
        }

        public static UserDto? CurrentUser { get; set; }


        public void Login(string Password, string Email)
        {
            var userToLogin = _userRepository.GetUserByEmail(Email);
            CurrentUser = UserMapper.ToUserDto(userToLogin);  

            if (CurrentUser == null)
            {
                throw new Exception("User does not exist.");
            }
           
            if(Password != CurrentUser.Birthday.ToString("MMyyyy"))
            {
                throw new Exception("Incorrect password.");
            }
            _customAuthenticationStateProvider.UpdateAuthenticationState(userToLogin);
        }

        public async Task<UserDto?> GetCurrentUserAsync()
        {
            var authState = await _customAuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity.IsAuthenticated)
            {
                var email = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
                if (email != null)
                {
                    var userEntity = _userRepository.GetUserByEmail(email);
                    return UserMapper.ToUserDto(userEntity);
                }
            }
            return null;
        }

        public async Task LogOutAsync()
        {
            await _customAuthenticationStateProvider.UnsetUserAsync();
        }

       
    }
}
