using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.IO;

namespace JobsityFinancialChat.Domain.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                 .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "..", "JobsityFinancialChat.API"))
                 .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                 .Build();

            var connectionString = configuration.GetSection("ConnectionStrings:DefaultConnection").Value;

            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseLoggerFactory(GetLoggerFactory()).UseNpgsql(connectionString);

            return new ApplicationDbContext(builder.Options);
        }

        private ILoggerFactory GetLoggerFactory()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(builder =>
                   builder.AddFilter(DbLoggerCategory.Database.Command.Name,
                                     LogLevel.Information));
            return serviceCollection.BuildServiceProvider()
                    .GetService<ILoggerFactory>();
        }
    }
}
