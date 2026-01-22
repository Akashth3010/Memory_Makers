using Microsoft.EntityFrameworkCore;
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

        // Fix: Actually fetch users instead of throwing an error
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        // Fix: Actually find user by ID
        public async Task<User?> GetUserByIdAsync(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        // Fix: Actually save changes when a user logs in/out
        public async Task UpdateUserAsync(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}