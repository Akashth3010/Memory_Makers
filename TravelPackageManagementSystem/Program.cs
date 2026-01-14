using Microsoft.EntityFrameworkCore;
//using TravelPackageManagementSystem.Application.Data;
using TravelPackageManagementSystem.Repository.Data;
using TravelPackageManagementSystem.Repository.Implementations;
using TravelPackageManagementSystem.Repository.Interfaces;
using TravelPackageManagementSystem.Repository.Models;
using TravelPackageManagementSystem.Services.Implementations;
using TravelPackageManagementSystem.Services.Interfaces;
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
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));

builder.Services.AddDbContext<TravelPackageManagementSystem.Repository.Data.AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));

// 1. Database Connection
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));

// 2. Register Repositories
builder.Services.AddScoped<IPackageRepository, PackageRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();

// 3. Register Services
builder.Services.AddScoped<IPackageService, PackageService>();
builder.Services.AddScoped<IBookingService, BookingService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();

