using Microsoft.EntityFrameworkCore;
using TravelPackageManagementSystem.Repository.Implementation;
using TravelPackageManagementSystem.Repository.Interface;
using TravelPackageManagementSystem.Services.Implementation;
using TravelPackageManagementSystem.Services.Interface;
//using TravelPackageManagementSystem.Repository.Interface;
using TravelPackageManagementSystem.Repository.Implementations;
using TravelPackageManagementSystem.Services.Interfaces;
//using TravelPackageManagementSystem.Application.Data;
using TravelPackageManagementSystem.Repository.Data;
//using TravelPackageManagementSystem.Repository.Implementations;
using TravelPackageManagementSystem.Repository.Interfaces;
using TravelPackageManagementSystem.Repository.Models;
using TravelPackageManagementSystem.Services.Implementations;
using TravelPackageManagementSystem.Repository.Interface;
//using TravelPackageManagementSystem.Services.Interfaces;
//using TravelPackageManagementSystem.Application.Models;

var builder = WebApplication.CreateBuilder(args);

// 1. Add Controllers (API capability)
builder.Services.AddControllersWithViews();

// 2. Database Configuration (CRITICAL STEP)
// This must happen BEFORE adding the Repository
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// 3. Register your Custom Services
// The app needs the Database (Step 2) to build these:
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
// 1. Tell the app to use Cookies for login
builder.Services.AddAuthentication("MyCookieAuth")
    .AddCookie("MyCookieAuth", options =>
    {
        options.Cookie.Name = "MyUserCookie";
        options.LoginPath = "/Account/Login"; // Where to go if not logged in
    });

// 2. Make sure Authorization is also enabled
builder.Services.AddAuthorization();
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
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthModelService, AuthModelService>();

// 4. Enable CORS (So your browser JS can talk to this backend)
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

// 3. Register Services
builder.Services.AddScoped<IPackageService, PackageService>();
builder.Services.AddScoped<IBookingService, BookingService>();
var app = builder.Build();

// --- PIPELINE SETUP ---
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication(); // Who are you?
app.UseAuthorization();  // Are you allowed to be here?
app.UseCors(); // Enable CORS
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();