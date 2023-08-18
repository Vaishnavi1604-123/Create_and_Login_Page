using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using login_page.Data;
using login_page.Areas.Identity.Data;

namespace login_page
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("login_pageDbContextConnection") 
                ?? throw new InvalidOperationException("Connection string 'login_pageDbContextConnection' not found.");

            builder.Services.AddDbContext<login_pageDbContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddDefaultIdentity<login_pageUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<login_pageDbContext>();


            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

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
            app.MapRazorPages();

            app.Run();
        }
    }
}