using WorkoutApp.DTOs;
using WorkoutApp.Entities;

namespace WorkoutApp.Repositories.Interfaces

{
    public interface IUserRepository
    {
        IList<UserDto> GetAllUsers();
        UserDto GetUserById(int id);
        Task AddUser(UserDto userDto);
        void EditUser(UserDto userDto);
        void DeleteUser(int id);
 
    }

}
