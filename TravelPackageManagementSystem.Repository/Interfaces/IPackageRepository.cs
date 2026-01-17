using System;
using System.Collections.Generic;
using System.Text;
using TravelPackageManagementSystem.Repository.Models;

namespace TravelPackageManagementSystem.Repository.Interfaces
{
    public interface IPackageRepository
    {
        Task<IEnumerable<TravelPackage>> GetAllPackages(); // Changed from List to Task<IEnumerable>
        Task<TravelPackage?> GetPackageById(int id);      // Changed to Task<TravelPackage?>
        Task AddPackage(TravelPackage package);           // Changed from void to Task
        Task UpdatePackage(TravelPackage package);        // Changed from void to Task
        Task DeletePackage(int id);                       // Changed from void to Task
    }
}
