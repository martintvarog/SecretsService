using Microsoft.AspNetCore.DataProtection;
using NSubstitute;
using SecretsService.DataAccess.Context;
using SecretsService.Service.Contracts;

namespace SecretsService.Tests;

public class SecretsServiceTests
{
    private ISecretsService InitializeSecretsService()
    {
        return new Service.Services.SecretsService(Substitute.For<SecretsDbContext>(),
            Substitute.For<IDataProtectionProvider>());
    }
}