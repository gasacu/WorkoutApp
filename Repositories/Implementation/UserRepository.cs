using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WorkoutApp.Context;
using WorkoutApp.DTOs;
using WorkoutApp.Entities;
using WorkoutApp.Mappers;
using WorkoutApp.Repositories.Interfaces;

namespace WorkoutApp.Repositories.Implementation
{

    public class UserRepository : IUserRepository
    {
        private readonly WorkoutAppContext _context;

        public UserRepository(WorkoutAppContext context)
        { 
            _context = context;
        }

        public IList<UserDto> GetAllUsers()
        {
            var users = new List<UserDto>();
            var allUsers =  _context.Users.Where(u => u.Birthday > new DateTime(0001, 1, 2)).ToList();
            if(allUsers?.Any() == true) 
            {
                foreach (var user in allUsers) 
                {
                    var userDto = UserMapper.ToUserDto(user);
                    users.Add(userDto);
                }
            }

            return users;
        }

        public UserDto GetUserById(int id)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);

            var userDto = UserMapper.ToUserDto(user);

            return userDto;
        }

        public async Task AddUser(UserDto userDto) 
        {
            var user = UserMapper.ToUser(userDto);

            _context.Users.Add(user);
            await _context.SaveChangesAsync(); 
        }

       

        public void EditUser(UserDto userDto)
        {
            var existingUser = _context.Users.FirstOrDefault(x => x.Id == userDto.Id);

            if (existingUser != null)
            {
                existingUser.Id = userDto.Id;
                existingUser.Birthday = userDto.Birthday;
                existingUser.Gender = userDto.Gender;
                existingUser.FirstName = userDto.FirstName;
                existingUser.LastName = userDto.LastName;
                existingUser.Workouts = userDto.Workouts;
                existingUser.Email = userDto.Email;
                existingUser.IsTrainer = userDto.IsTrainer;
                
                _context.Users.Update(existingUser);
                _context.SaveChanges();
            }
        }

        public void DeleteUser(int id)
        {
            var existingUser = _context.Users.Find(id);
            if (existingUser != null) 
            {
                _context.Users.Remove(existingUser);
                _context.SaveChanges();
            }
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(x => x.Email == email);

        }

    }
}
