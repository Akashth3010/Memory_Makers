using Microsoft.EntityFrameworkCore;
using TravelPackageManagementSystem.Repository.Interface;
using TravelPackageManagementSystem.Repository.Implementation;
using TravelPackageManagementSystem.Services.Interfaces;
using TravelPackageManagementSystem.Services.Implementations;

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

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthModelService, AuthModelService>();

builder.Services.AddDbContext<TravelPackageManagementSystem.Repository.Data.AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection")));

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

