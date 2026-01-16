using System;
using System.Collections.Generic;
using System.Text;
using TravelPackageManagementSystem.Repository.Models;

namespace TravelPackageManagementSystem.Repository.Interfaces
{
    public interface IPackageRepository
    {
        void AddPackage(TravelPackage package);
        List<TravelPackage> GetAllPackages();
        TravelPackage GetPackageById(int id);
        void UpdatePackage(TravelPackage package);
        void DeletePackage(int id);
    }
}
