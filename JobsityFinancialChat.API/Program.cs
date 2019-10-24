using JobsityFinancialChat.Domain.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace JobsityFinancialChat.API
{
    public static class Program
    {
        private static IConfiguration Configuration { get; set; }

        public static void Main(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var builder = new ConfigurationBuilder()
                .AddJsonFile($"appSettings.{environment}.json", optional: false, reloadOnChange: true)
                .AddCommandLine(args);
            Configuration = builder.Build();
            InitWebHost(args, Configuration)
                .Run();
        }

        public static IWebHost InitWebHost(string[] args, IConfiguration configuration) =>
          WebHost.CreateDefaultBuilder(args)
                 .UseStartup<Startup>()
                 .UseConfiguration(configuration)
                 .Build();

        public static IWebHost MigrateDatabase(this IWebHost webHost)
        {
            var serviceScopeFactory = (IServiceScopeFactory)webHost.Services.GetService(typeof(IServiceScopeFactory));

            using (var scope = serviceScopeFactory.CreateScope())
            {
                var services = scope.ServiceProvider;
                var dbContext = services.GetRequiredService<ApplicationDbContext>();

                dbContext.Database.Migrate();
            }

            return webHost;
        }
    }
}
