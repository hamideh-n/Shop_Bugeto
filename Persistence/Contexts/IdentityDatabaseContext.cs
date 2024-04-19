using Domain.Users.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Contexts
{
    public class IdentityDatabaseContext: IdentityDbContext<User>
    {
        public IdentityDatabaseContext(DbContextOptions<IdentityDatabaseContext> options):base(options)
        {
                
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //تغییر نام جدول هابا totable()
            builder.Entity<IdentityUser<string>>().ToTable("Users", "identity");
            builder.Entity<IdentityRole<string>>().ToTable("Role", "identity");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaim", "identity");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogin", "identity");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaim", "identity");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRole", "identity");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserToken", "identity");
            //این سه تا جدول کلید هاشون رو از دست می دهند و دوباره کانفیگ میکنیم 
            builder.Entity<IdentityUserLogin<string>>().HasKey(p=>new {p.LoginProvider,p.ProviderKey});
            builder.Entity<IdentityUserRole<string>>().HasKey(p => new { p.UserId, p.RoleId });
            builder.Entity<IdentityUserToken<string>>().HasKey(p => new { p.UserId, p.LoginProvider,p.Name });
        }

        //migration برای جایی که چند دیتابیس داریم 
        //add-migration -context IdentityDatabaseContext addIdentity
        ////Update-database -Context IdentityDatabaseContext

    }
}
