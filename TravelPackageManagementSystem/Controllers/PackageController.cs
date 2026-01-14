using Microsoft.AspNetCore.Mvc;
using TravelPackageManagementSystem.Services.Interfaces;

public class PackageController : Controller
{
    private readonly IPackageService _packageService;

    // The constructor "injects" the service so this controller can fetch data
    public PackageController(IPackageService packageService)
    {
        _packageService = packageService;
    }

    // This handles Page 2: Showing the packages for a destination
    public async Task<IActionResult> List(string destination = "Meghalaya")
    {
        var packages = await _packageService.GetPackagesByDestinationAsync(destination);
        return View(packages);
    }

    // This handles Page 3: Showing details of one package
    public async Task<IActionResult> Details(int id)
    {
        var package = await _packageService.GetPackageByIdAsync(id);
        if (package == null) return NotFound();

        return View(package);
    }
}
