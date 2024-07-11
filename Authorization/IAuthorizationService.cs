using WorkoutApp.DTOs;

namespace WorkoutApp.Authorization
{
        public interface IAuthorizationService
        {
            void Login(string Password, string Email);
            Task<UserDto?> GetCurrentUserAsync();
            Task LogOutAsync();
        }
}

