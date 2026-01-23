using Microsoft.EntityFrameworkCore;
//using TravelPackageManagementSystem.Data;
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

        public async Task<IEnumerable<TravelPackage>> GetAllPackages()
        {
            return await _context.TravelPackages.ToListAsync();
        }

        // Fix: Change 'Task<TravelPackage>' to 'Task<TravelPackage?>'
        public async Task<TravelPackage?> GetPackageById(int id)
        {
            // The compiler warned about a "Possible null reference return" here.
            // This method correctly returns null if the ID isn't found in the DB.
            return await _context.TravelPackages.FirstOrDefaultAsync(p => p.PackageId == id);
        }

        public async Task AddPackage(TravelPackage package)
        {
            _context.TravelPackages.Add(package);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePackage(TravelPackage package)
        {
            _context.TravelPackages.Update(package);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePackage(int id)
        {
            var package = await GetPackageById(id);

            // Fix: Always check for null before using the object
            if (package != null)
            {
                _context.TravelPackages.Remove(package);
                await _context.SaveChangesAsync();
            }
        }
    }
}