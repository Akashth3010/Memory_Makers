using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using TravelPackageManagementSystem.Repository.Interface;
using TravelPackageManagementSystem.Repository.Models;
using TravelPackageManagementSystem.Services.Interfaces;

namespace TravelPackageManagementSystem.Services.Implementations
{
    public class AuthModelService : IAuthModelService
    {
        private readonly IUserRepository _userRepository;
        private readonly PasswordHasher<User> _hasher;

        public AuthModelService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _hasher = new PasswordHasher<User>();
        }

        public async Task<(bool Success, string Message, User? User)> RegisterUserAsync(RegisterViewModel model)
        {
            var existingUser = await _userRepository.GetUserByUsernameOrEmailAsync(model.Username);
            if (existingUser != null)
            {
                return (false, "Username or email already exists.", null);
            }

            var user = new User
            {
                Username = model.Username,
                Email = model.Email,
                Role = UserRole.CUSTOMER
            };

            user.Password = _hasher.HashPassword(user, model.Password);

            var result = await _userRepository.AddUserAsync(user);
            return result ? (true, "Success", user) : (false, "Database error", null);
        }

        public async Task<User?> AuthenticateUserAsync(string username, string password)
        {
            var user = await _userRepository.GetUserByUsernameOrEmailAsync(username);
            if (user == null) return null;

            var verification = _hasher.VerifyHashedPassword(user, user.Password, password);
            return verification == PasswordVerificationResult.Failed ? null : user;
        }
    }
}
