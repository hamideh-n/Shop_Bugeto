using Domain.Users.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;

namespace Infrastructure.IdentityConfig
{
    public static class IdentityConfig
    {
//کانفیک های  خروجی رو ازش جدا میکنیم میتونیم در چند اپ خروجی از اون استفاده کنیم  
        public static IServiceCollection AddIdentityService(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<IdentityDatabaseContext>(options => options.UseSqlServer(connectionString));
            //معرفی سرویس ایدنتیتی 
            //اگر انرا کاستوم کرده باشی قرار می دهی در غیر این صورت از دیفالت استفاده میکنیم 
            
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<IdentityDatabaseContext>()
                .AddDefaultTokenProviders()
                .AddRoles<IdentityRole>()
                .AddErrorDescriber<CustomIdentityError>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 8;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredUniqueChars = 1;
                options.Password.RequireNonAlphanumeric = false;
                options.User.RequireUniqueEmail = true;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);

            });
            return services;
        }
    }
}
