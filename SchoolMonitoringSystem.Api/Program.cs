using Microsoft.AspNetCore.Identity;
using SchoolMonitoringSystem.Api.Middleware;
using SchoolMonitoringSystem.Application;
using SchoolMonitoringSystem.Infrastructure.Persistence;
using Serilog;

namespace SchoolMonitoringSystem.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();
            IConfiguration configuration = builder.Configuration;

            LoggingConfigurations.UseLogging(configuration);
            builder.Host.UseSerilog();
            //Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddInfrastructureService(configuration);
            builder.Services.AddApplicationService();
            builder.Services.AddLazyCache();
            builder.Services.AddRazorPages();
            var app = builder.Build();
            app.UseCustomMiddleware();

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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}
