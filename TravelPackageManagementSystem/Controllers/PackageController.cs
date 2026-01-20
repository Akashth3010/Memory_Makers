using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelPackageManagementSystem.Repository.Data;

public class PackageController : Controller
{
    private readonly AppDbContext _context;

    public PackageController(AppDbContext context) => _context = context;

    // 1. GET ALL PACKAGES FOR A SPECIFIC STATE
    public async Task<IActionResult> Index(string state = "Meghalaya")
    {
        // Fix: Use Include to join with the Destinations table and filter by StateName
        var packages = await _context.TravelPackages
            .Include(p => p.ParentDestination)
            .Where(p => p.ParentDestination != null && p.ParentDestination.StateName == state)
            .ToListAsync();

        ViewBag.StateName = state;
        return View(packages);
    }

    // 2. GET PACKAGE DETAILS + ITINERARY
    public async Task<IActionResult> Details(int id)
    {
        // Fix: Fetch the package along with its Itineraries AND its Parent Destination details
        var package = await _context.TravelPackages
            .Include(p => p.Itineraries)
            .Include(p => p.ParentDestination)
            .FirstOrDefaultAsync(p => p.PackageId == id);

        if (package == null) return NotFound();

        return View(package);
    }
}