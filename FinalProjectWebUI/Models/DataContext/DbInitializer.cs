using FinalProjectWebUI.Models.Entity;
using FinalProjectWebUI.Models.Entity.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectWebUI.Models.DataContext
{
    public static class DbInitializer
    {


        public static IApplicationBuilder Seed(this IApplicationBuilder app)
        {
            const string adminEmail = "rhuseynli010@gmail.com";
            const string adminPassword = "Mirziya1948!";
            const string superAdminRoleName = "SuperAdmin";

            using (var scope = app.ApplicationServices.CreateScope())
            {

                var db = scope.ServiceProvider.GetRequiredService<FinalDbContext>();

                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();
                db.Database.Migrate();

                var role = roleManager.FindByNameAsync(superAdminRoleName).Result;
                if (role == null)
                {
                    role = new AppRole
                    {
                        Name = superAdminRoleName
                    };

                    roleManager.CreateAsync(role).Wait();
                }


                var userManeger = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

                var adminUser = userManeger.FindByEmailAsync(adminEmail).Result;

                if (adminUser == null)
                {
                    adminUser = new AppUser
                    {
                        Email = adminEmail,
                        UserName = adminEmail,
                        EmailConfirmed = true
                    };

                    var userResult = userManeger.CreateAsync(adminUser, adminPassword).Result;

                    if (userResult.Succeeded)
                    {
                        userManeger.AddToRoleAsync(adminUser, superAdminRoleName).Wait();
                    }

                }
                if (!db.Stores.Any())
                {
                    db.Stores.Add(new Store
                    {
                        Name = "Paris",
                    });
                    db.SaveChanges();

                }

                if (!db.SiteInfos.Any())
                {
                    db.SiteInfos.Add(new SiteInfo
                    {
                        Name = "AktiviKid",
                        Address = "baki",
                        Number = "000-00-000-00-00",
                        Email = "muradaliyev20233202@gmail.com",
                        Logo = "example.png",
                        FavIcon = "exampl.png",
                        StoreId = 1
                    });
                    db.SaveChanges();

                }
            }
            return app;
        }
    }
}
