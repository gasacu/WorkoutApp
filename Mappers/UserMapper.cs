using WorkoutApp.DTOs;
using WorkoutApp.Entities;

namespace WorkoutApp.Mappers
{
    public static class UserMapper
    {
        public static User ToUser(UserDto userDto)
        {
            return new User
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Birthday = userDto.Birthday,
                Gender = userDto.Gender,
                Email = userDto.Email,
                IsTrainer = userDto.IsTrainer,
                Id = userDto.Id
            };
        }

        public static UserDto ToUserDto(User user)
        {
            return new UserDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Birthday = user.Birthday,
                Gender = user.Gender,
                Email= user.Email,
                IsTrainer = user.IsTrainer,
                Id = user.Id
            };
        }
    }
}
