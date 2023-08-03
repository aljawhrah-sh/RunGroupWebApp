using Microsoft.EntityFrameworkCore;
using RunGroupWebApp1.Data;
using RunGroupWebApp1.Interface;
using RunGroupWebApp1.Repsitoery;
using Microsoft.AspNetCore.Identity;
using RunGroupWebApp1.Models;
using RunGroupWebApp1.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using RunGroupWebApp1.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IClubReposiroty, ClubRepository>();
builder.Services.AddScoped<IRaceRepository, RaceRepository>();
builder.Services.AddScoped<IDashboradRepository, DashboardRepository>();
builder.Services.AddScoped<IPhotoServices, PhotoService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddMemoryCache();
//cookie authentication
builder.Services.AddSession();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme);



var app = builder.Build();
if (args.Length == 1 && args[0].ToLower() == "seeddata")
{
    await Seed.SeedUsersAndRolesAsync(app);
    //Seed.SeedData(app);
}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
//CHECK IF ENTERED INFO IS CORRECT
app.UseAuthentication();
//CHECK IF USER IS AUTHARIZED OR NOT
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

