using EventManagement.Models;
using EventManagement.Repositories;
using EventManagement.Services.FestivalService;
using EventManagement.Services.MemberService.MemberService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connection = builder.Configuration.GetConnectionString("ConnStr") ?? throw new InvalidOperationException("Connection string 'ConnStr' not found."); 

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<EventManagementContext>(options => options.UseSqlServer(connection));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<EventManagementContext>();

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitOfWork , UnitOfWork>();
builder.Services.AddScoped<IMemberService , MemberService>();
builder.Services.AddScoped<IFestivalService, FestivalService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
