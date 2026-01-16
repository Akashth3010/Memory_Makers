using System;
using System.Collections.Generic;
using System.Text;
using TravelPackageManagementSystem.Repository.Models;

namespace TravelPackageManagementSystem.Services.Interfaces
{
    public interface IPackageService
    {
        void CreatePackage(TravelPackage package);
        List<TravelPackage> GetPackages();
        TravelPackage GetPackage(int id);
        void UpdatePackage(TravelPackage package);
        void DeletePackage(int id);
    }
}
