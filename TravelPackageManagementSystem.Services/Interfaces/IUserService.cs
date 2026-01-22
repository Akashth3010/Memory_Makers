using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TravelPackageManagementSystem.Repository.Models;

namespace TravelPackageManagementSystem.Services.Interfaces
{
    public interface IUserService
    {
        // Task to get all users for the admin dashboard
        Task<IEnumerable<User>> GetAllUsersAsync();

        // You can add more methods here later, like:
        // Task<User> GetUserByIdAsync(int id);
        Task UpdateLoginStatusAsync(int userId, bool status); // Add this line
    }
}