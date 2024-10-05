using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using SecretsService.DataAccess.Context;

namespace SecretsService.DataAccess
{
    public class SecretsDbContextFactory : IDesignTimeDbContextFactory<SecretsDbContext>
    {
        public SecretsDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SecretsDbContext>();

            // Configure the connection string
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Development.json")
                .AddUserSecrets<SecretsDbContextFactory>()
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);

            return new SecretsDbContext(optionsBuilder.Options);
        }
    }
}