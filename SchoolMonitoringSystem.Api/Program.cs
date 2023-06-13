using SchoolMonitoringSystem.Application;
using SchoolMonitoringSystem.Infrastructure;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.TelegramBot;

namespace SchoolMonitoringSystem.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            IConfiguration configuration = builder.Configuration;
            //Log.Logger = new LoggerConfiguration()
            //                .ReadFrom.Configuration(configuration)
            //                .WriteTo.TelegramBot(
            //                    token: configuration["TelegramBot:Token"],
            //                    chatId: configuration["TelegramBot:ChatId"],
            //                    restrictedToMinimumLevel: LogEventLevel.Information
            //                )
            //                .Enrich.FromLogContext()
            //                .CreateLogger();
            //// Add services to the container.
            //builder.Host.UseSerilog();
            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddInfrastructureService(builder.Configuration);
            builder.Services.AddApplicationService();
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

            app.UseAuthorization();


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}