using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Infrastructure.IdentityConfig;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(connectionString));
//builder.Services.AddDbContext<IdentityDatabaseContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddIdentityService(builder.Configuration);
builder.Services.AddAuthentication();
//
builder.Services.ConfigureApplicationCookie(option =>
{
    //زمان اعتبار کوکی
    option.ExpireTimeSpan = TimeSpan.FromMinutes(10);
    //اگر مجوز نداشته باشد 
    option.LoginPath = "/Account/login";
    //
    option.AccessDeniedPath = "/Account/AccessDenied";
    //اگر در مدت زمانیکه مشخص کردیم کاربر فعالیتی داشت تمدید میشود 
    option.SlidingExpiration = true;
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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
