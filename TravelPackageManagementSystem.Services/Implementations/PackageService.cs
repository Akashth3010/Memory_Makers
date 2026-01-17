using TravelPackageManagementSystem.Repository.Models;
using TravelPackageManagementSystem.Repository.Interfaces;
using TravelPackageManagementSystem.Services.Interfaces;

namespace TravelPackageManagementSystem.Services.Implementations
{
    public class PackageService : IPackageService
    {
        private readonly IPackageRepository _packageRepository;

        public PackageService(IPackageRepository packageRepository)
        {
            _packageRepository = packageRepository;
        }

        public async Task<IEnumerable<TravelPackage>> GetPackages()
        {
            // Ensure this matches the method name in IPackageRepository
            return await _packageRepository.GetAllPackages();
        }

        public async Task<TravelPackage?> GetPackage(int id)
        {
            return await _packageRepository.GetPackageById(id);
        }

        public async Task CreatePackage(TravelPackage package)
        {
            await _packageRepository.AddPackage(package);
        }

        public async Task UpdatePackage(TravelPackage package)
        {
            await _packageRepository.UpdatePackage(package);
        }

        public async Task DeletePackage(int id)
        {
            await _packageRepository.DeletePackage(id);
        }
    }
}