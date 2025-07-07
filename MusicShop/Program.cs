using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using MusicShop.Models;
using MusicShop.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MusicShopContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("MusicShopConnection")));

builder.Services.AddScoped<IMusicShopService, MusicShopService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.MaxAge = TimeSpan.FromMinutes(20);
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        options.SlidingExpiration = true;
        options.AccessDeniedPath = "/Forbidden/";
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.SameSite = SameSiteMode.Strict;
        options.Cookie.HttpOnly = true;
    });

var app = builder.Build();

app.UseDeveloperExceptionPage();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Shop/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Shop}/{action=Index}/{id?}");

app.Run();
