using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelPackageManagementSystem.Repository.Data;

public class PackageController : Controller
{
    private readonly AppDbContext _context;

    public PackageController(AppDbContext context) => _context = context;

    // 1. GET ALL PACKAGES (Screenshot 1: The Listing Page)
    public async Task<IActionResult> Index(string destination = "Meghalaya")
    {
        var packages = await _context.TravelPackages
            .Where(p => p.Destination == destination)
            .ToListAsync();
        return View(packages);
    }

    // 2. GET PACKAGE DETAILS + ITINERARY (Screenshot 2: Detail Page)
    public async Task<IActionResult> Details(int id)
    {
        // "Include" fetches the Itinerary table data linked to this Package
        var package = await _context.TravelPackages
            .Include(p => p.Itineraries)
            .FirstOrDefaultAsync(p => p.PackageId == id);

        if (package == null) return NotFound();
        return View(package);
    }
}