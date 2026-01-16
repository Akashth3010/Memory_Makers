using Microsoft.EntityFrameworkCore;
using TravelPackageManagementSystem.Repository.Data;
using TravelPackageManagementSystem.Repository.Interfaces;
using TravelPackageManagementSystem.Repository.Models;

namespace TravelPackageManagementSystem.Repository.Implementations
{
    public class PackageRepository : IPackageRepository
    {
        private readonly AppDbContext _context;

        public PackageRepository(AppDbContext context)
        {
            _context = context;
        }

        // Fetching all travel packages from the database
        public async Task<IEnumerable<TravelPackage>> GetAllPackagesAsync()
        {
            return await _context.TravelPackages.ToListAsync();
        }

        // Fetching a single package by its ID (useful for Page 3 details)
        public async Task<TravelPackage?> GetPackageByIdAsync(int id)
        {
            return await _context.TravelPackages
                .Include(p => p.Itineraries) // This includes the days/activities
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}