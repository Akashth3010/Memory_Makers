using System;
using System.Collections.Generic;
using System.Text;
using TravelPackageManagementSystem.Repository.Models;

namespace TravelPackageManagementSystem.Services.Interfaces
{
    public interface IPackageService
    {
        // Change return types to Task or Task<T> to support async
        Task<IEnumerable<TravelPackage>> GetPackages();
        Task<TravelPackage?> GetPackage(int id);
        Task CreatePackage(TravelPackage package);
        Task UpdatePackage(TravelPackage package);
        Task DeletePackage(int id);
    }
}
