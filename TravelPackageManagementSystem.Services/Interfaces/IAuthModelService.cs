using System;
using System.Collections.Generic;
using System.Text;
using TravelPackageManagementSystem.Repository.Models;

namespace TravelPackageManagementSystem.Services.Interfaces
{
    public interface IAuthModelService
    {
        Task<(bool Success, string Message, User? User)> RegisterUserAsync(RegisterViewModel model);
        Task<User?> AuthenticateUserAsync(string username, string password);
    }
}
