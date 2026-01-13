using Microsoft.EntityFrameworkCore;
using TravelPackageManagementSystem.Application.Models; // Make sure this namespace is correct

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // This line creates the "TravelPackages" table in SQL Server
    public DbSet<TravelPackage> TravelPackages { get; set; }
}