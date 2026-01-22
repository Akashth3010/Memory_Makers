using Microsoft.EntityFrameworkCore;
using TravelPackageManagementSystem.Repository.Implementation;
using TravelPackageManagementSystem.Repository.Interface;
using TravelPackageManagementSystem.Services.Implementation;
using TravelPackageManagementSystem.Services.Interface;

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

// 4. Enable CORS (So your browser JS can talk to this backend)
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

// 5. Build the App
var app = builder.Build(); // <--- The error was happening here!

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