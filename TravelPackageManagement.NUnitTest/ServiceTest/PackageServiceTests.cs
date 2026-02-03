using NUnit.Framework;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using TravelPackageManagementSystem.Repository.Models;
using TravelPackageManagementSystem.Repository.Interfaces;
using TravelPackageManagementSystem.Services.Implementations;

namespace TravelPackageManagement.NUnitTest.Service
{
    [TestFixture]
    public class PackageServiceTests
    {
        private Mock<IPackageRepository> _packageRepoMock;
        private PackageService _service;

        [SetUp]
        public void Setup()
        {
            _packageRepoMock = new Mock<IPackageRepository>();
            _service = new PackageService(_packageRepoMock.Object);
        }

        [Test]
        public async Task GetPackages_ReturnsAll()
        {
            var packages = new List<TravelPackage> { new TravelPackage() };
            _packageRepoMock.Setup(r => r.GetAllPackages()).ReturnsAsync(packages);
            var result = await _service.GetPackages();
            Assert.That(result, Is.EqualTo(packages));
        }

        [Test]
        public async Task CreatePackage_CallsAdd()
        {
            var package = new TravelPackage();
            await _service.CreatePackage(package);
            _packageRepoMock.Verify(r => r.AddPackage(package), Times.Once);
        }
    }
}
