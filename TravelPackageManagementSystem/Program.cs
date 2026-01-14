using Microsoft.EntityFrameworkCore;
//using TravelPackageManagementSystem.Application.Data;
using TravelPackageManagementSystem.Repository.Data;
using TravelPackageManagementSystem.Repository.Models;
//using TravelPackageManagementSystem.Application.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // session expires after 30mins
    options.Cookie.HttpOnly = true; // security: prevents JS Access to session cookie
    options.Cookie.IsEssential = true; // necessary for the app to function
});

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<TravelPackageManagementSystem.Repository.Data.AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<TravelPackageManagementSystem.Repository.Data.AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection")));

// Register the Database Context
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
// Note: In .NET 9, MapStaticAssets replaces UseStaticFiles for better performance
app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();