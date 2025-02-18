using Microsoft.EntityFrameworkCore;
using SPA.Application.Services.Implementations;
using SPA.Application.Services.Interfaces;
using SPA.Infrastructure.Context;
using SPA.Infrastructure.Repositories.Implementations;
using SPA.Infrastructure.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<MyContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("SPA")));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
