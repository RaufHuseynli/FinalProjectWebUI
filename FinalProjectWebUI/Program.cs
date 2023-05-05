using FinalProjectWebUI.Helper;
using FinalProjectWebUI.Models.DataContext;
using FinalProjectWebUI.Models.Entity.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddDbContext<FinalDbContext>(cfg =>
{

    cfg.UseSqlServer(builder.Configuration.GetConnectionString("cString"));
});

builder.Services.AddControllersWithViews(cfg =>
{
    cfg.ModelBinderProviders.Insert(0, new BooleanBinderProvider());
});

builder.Services.AddIdentity<AppUser, AppRole>(opts =>
{

    opts.User.RequireUniqueEmail = true;
    opts.User.AllowedUserNameCharacters = "abcçdefgğhıijklmnoöpqrsştuüvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@";

    opts.Password.RequiredLength = 4;
    opts.Password.RequireNonAlphanumeric = false;
    opts.Password.RequireLowercase = false;
    opts.Password.RequireUppercase = false;
    opts.Password.RequireDigit = false;

}).AddPasswordValidator<CustomPasswordValidator>()
  .AddUserValidator<CustomUserValidator>()
  .AddErrorDescriber<CustomIdentityErrorDescriber>()
  .AddEntityFrameworkStores<FinalDbContext>()
  .AddDefaultTokenProviders();

CookieBuilder cookieBuilder = new CookieBuilder();
cookieBuilder.Name = "FinalProject";
cookieBuilder.HttpOnly = false;
cookieBuilder.SameSite = SameSiteMode.Lax;
cookieBuilder.SecurePolicy = CookieSecurePolicy.SameAsRequest;

builder.Services.ConfigureApplicationCookie(opts =>
{

    opts.Cookie = cookieBuilder;
    opts.SlidingExpiration = true;
    opts.ExpireTimeSpan = System.TimeSpan.FromDays(60);

});





var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.Seed();
app.UseAuthentication();
app.UseAuthorization();

app.MapAreaControllerRoute("adminArea",
areaName: "Admin",
pattern: "admin/{controller=SiteInfo}/{action=index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
