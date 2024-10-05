using Microsoft.EntityFrameworkCore;
using SecretsService.Model.Context;

namespace SecretsService.Tests.InMemoryDb;

public class SecretsInMemoryDb
{
    public SecretsDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<SecretsDbContext>()
            .UseInMemoryDatabase(databaseName: "SecretsTestDb")
            .Options;

        return new SecretsDbContext(options);
    }
}