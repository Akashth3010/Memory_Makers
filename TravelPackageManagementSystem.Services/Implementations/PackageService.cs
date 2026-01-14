using TravelPackageManagementSystem.Repository.Interfaces;
using TravelPackageManagementSystem.Repository.Models;
using TravelPackageManagementSystem.Services.Interfaces;

namespace TravelPackageManagementSystem.Services.Implementations
{
    public class PackageService : IPackageService
    {
        private readonly IPackageRepository _packageRepo;

        public PackageService(IPackageRepository packageRepo)
        {
            _packageRepo = packageRepo;
        }

        public async Task<IEnumerable<TravelPackage>> GetPackagesForFrontendAsync()
        {
            // Here you can filter or sort the packages before sending to UI
            return await _packageRepo.GetAllPackagesAsync();
        }
    }
}