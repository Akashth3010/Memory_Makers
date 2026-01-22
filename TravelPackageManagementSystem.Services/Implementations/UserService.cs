using TravelPackageManagementSystem.Repository.Interface;
using TravelPackageManagementSystem.Repository.Models;
using TravelPackageManagementSystem.Services.Interfaces;

namespace TravelPackageManagementSystem.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task UpdateLoginStatusAsync(int userId, bool status)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user != null)
            {
                user.IsLoggedIn = status;
                user.LastLogin = System.DateTime.Now;
                await _userRepository.UpdateUserAsync(user);
            }
        }
    }
}