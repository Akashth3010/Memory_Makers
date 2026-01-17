using System;
using System.Collections.Generic;
using System.Text;
using TravelPackageManagementSystem.Repository.Models;

namespace TravelPackageManagementSystem.Repository.Interface
{
    public interface IUserRepository
    {
        Task<User?> GetUserByUsernameOrEmailAsync(string identifier);
        Task<bool> AddUserAsync(User user);
    }
}
