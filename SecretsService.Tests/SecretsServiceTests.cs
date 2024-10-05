
using Microsoft.AspNetCore.DataProtection;
using NSubstitute;
using SecretsService.Model.Context;
using SecretsService.Service.Contracts;
using Xunit;
using Secret = SecretsService.Model.Secret;

namespace SecretsService.Tests;

public class SecretsServiceTests
{
    // private readonly DateTime
    //
    [Fact]
    public async Task GetsSecretsSuccessfully()
    {
        // Arrange
        // var persistedSecret = new Secret
        // {
        //     SecretId = 1,
        //     Name = "TestSecret",
        //     Value = "TestValue",
        //     UpdatedAt = 
        // }
        
        var context = Substitute.For<SecretsDbContext>();
        
        
    }


    private ISecretsService InitializeSecretsService(SecretsDbContext? context = null,
        IDataProtectionProvider? dataProtectionProvider = null)
    {
        return new Service.Services.SecretsService(context ?? Substitute.For<SecretsDbContext>(),
            dataProtectionProvider ?? Substitute.For<IDataProtectionProvider>());
    }
}