using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TravelPackageManagementSystem.Repository.Data;
using TravelPackageManagementSystem.Repository.Interface;
using TravelPackageManagementSystem.Repository.Models;

namespace TravelPackageManagementSystem.Repository.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByUsernameOrEmailAsync(string identifier)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == identifier || u.Email == identifier);
        }

        public async Task<bool> AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
