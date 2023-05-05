
using FinalProjectWebUI.Models.Entity;
using FinalProjectWebUI.Models.Entity.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Reflection.Metadata;

namespace FinalProjectWebUI.Models.DataContext
{
    public class FinalDbContext : IdentityDbContext<AppUser, AppRole, int, AppUserClaim, AppUserRole, AppUserLogin, AppRoleClaim, AppUserToken>
    {



         public FinalDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<SiteInfo> SiteInfos { get; set; }

        public DbSet<Basket> Baskets { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<FinalProjectWebUI.Models.Entity.Color> Color { get; set; }

        public DbSet<Store> Stores { get; set; }
        public DbSet<SocialLink> SocialLinks { get; set; }
        public DbSet<SiteSocial> SiteSocial { get; set; }
        public DbSet<Genders> Genders { get; set; }



        public DbSet<Product> Products { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<ProductCollection> ProductCollections { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderProduct> OrderProduct { get; set; }
        public DbSet<ProductColorSizeCount> ProductColorSizeCount { get; set; }
        public DbSet<ProductImages> ProductImages { get; set; }
        public DbSet<ProductStore> ProductStore { get; set; }
        public DbSet<Contact> Contact { get; set; }
        #region Membership
        public DbSet<AppUser> Users { get; set; }
        public DbSet<AppRole> Roles { get; set; }
        public DbSet<AppUserRole> UserRoles { get; set; }
        public DbSet<AppUserClaim> UserClaims { get; set; }
        public DbSet<AppRoleClaim> RoleClaims { get; set; }
        public DbSet<AppUserToken> UserTokes { get; set; }
        public DbSet<AppUserLogin> UserLogins { get; set; }
        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Membership
            modelBuilder.Entity<AppUser>(e =>
            {
                e.ToTable("Users", "Membership");
            });

            modelBuilder.Entity<AppUserRole>(e =>
            {
                e.ToTable("UserRoles", "Membership");
            });

            modelBuilder.Entity<AppUserClaim>(e =>
            {
                e.ToTable("UserClaims", "Membership");

            });

            modelBuilder.Entity<AppRole>(e =>
            {
                e.ToTable("Roles", "Membership");
            });

            modelBuilder.Entity<AppRoleClaim>(e =>
            {
                e.ToTable("RoleClaims", "Membership");
            });

            modelBuilder.Entity<AppUserLogin>(e =>
            {
                e.ToTable("UserLogins", "Membership");
            });

            modelBuilder.Entity<AppUserToken>(e =>
            {
                e.ToTable("UserTokens", "Membership");
            });
            #endregion





            modelBuilder.Entity<ProductCollection>()
.HasKey(bc => new { bc.ProductId, bc.CollectionId});

            modelBuilder.Entity<ProductCollection>()
                .HasOne(bc => bc.Product)
                .WithMany(b => b.Collections)
                .HasForeignKey(bc => bc.ProductId);

            modelBuilder.Entity<ProductCollection>()
                .HasOne(bc => bc.Collection)
                .WithMany(c => c.Collections)
                .HasForeignKey(bc => bc.CollectionId);

            modelBuilder.Entity<SiteSocial>()
.HasKey(bc => new { bc.SocialId, bc.SiteInfoId});
            modelBuilder.Entity<SiteSocial>()
    .HasOne(bc => bc.SiteInfo)
    .WithMany(c => c.SiteSocial)
    .HasForeignKey(bc => bc.SiteInfoId);


            modelBuilder.Entity<SiteSocial>()
                .HasOne(bc => bc.Social)
                .WithMany(b => b.SiteSocial)
                .HasForeignKey(bc => bc.SocialId);




        }
    }
}
