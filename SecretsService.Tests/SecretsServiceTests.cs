using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using SecretsService.Model.Context;
using SecretsService.Service.Contracts;
using SecretsService.Tests.InMemoryDb;
using Xunit;
using Secret = SecretsService.Model.Secret;

namespace SecretsService.Tests;

public class SecretsServiceTests : SecretsInMemoryDb
{
    private readonly DateTimeOffset _today = new(2022, 1, 1, 0, 0, 0, TimeSpan.Zero);

    [Fact]
    public async Task GetsSecretsSuccessfully()
    {
        // Arrange
        var context = CreateDbContext();

        var persistedSecret = new Secret
        {
            SecretId = 1,
            Name = "TestSecret",
            Value = "EncryptedValue",
            UpdatedAt = _today,
            CreatedAt = _today
        };

        context.Secrets.Add(persistedSecret);
        await context.SaveChangesAsync();

        var dataProtector = Substitute.For<ISecretsDataProtector>();
        dataProtector.Unprotect(Arg.Any<string>()).Returns("DecryptedValue");

        var secretsService = InitializeSecretsService(context, dataProtector);

        // Act
        var secret = await secretsService.GetSecretAsync(persistedSecret.Name, default);

        // Assert
        Assert.NotNull(secret);
        Assert.Equal("DecryptedValue", secret.Value);
        Assert.Equal(persistedSecret.Name, secret.Name);
        Assert.Equal(persistedSecret.UpdatedAt, secret.UpdatedAt);
        Assert.Equal(persistedSecret.CreatedAt, secret.CreatedAt);
    }

    [Fact]
    public async Task GetSecretReturnsNullIfSecretNotFound()
    {
        // Arrange
        var secretsService = InitializeSecretsService();

        // Act
        var secret = await secretsService.GetSecretAsync("NonExistentSecret", default);

        // Assert
        Assert.Null(secret);
    }

    [Fact]
    public async Task StoresSecretSuccessfully()
    {
        // Arrange
        var context = CreateDbContext();

        var dataProtector = Substitute.For<ISecretsDataProtector>();
        dataProtector.Protect(Arg.Any<string>()).Returns("EncryptedValue");

        var secretsService = InitializeSecretsService(context, dataProtector);

        var request = new Service.Requests.SecretsRequest
        {
            Name = "TestSecret",
            Value = "DecryptedValue"
        };

        // Act
        await secretsService.StoreSecretAsync(request, default);

        // Assert
        var secret = await context.Secrets.FirstOrDefaultAsync(s => s.Name == request.Name);

        Assert.NotNull(secret);
        Assert.Equal(request.Name, secret.Name);
        Assert.Equal("EncryptedValue", secret.Value);
    }

    [Fact]
    public async Task ThrowsValidationExceptionIfSecretNameIsNotUnique()
    {
        // Arrange
        var context = CreateDbContext();

        var persistedSecret = new Secret
        {
            SecretId = 1,
            Name = "TestSecret",
            Value = "EncryptedValue",
            UpdatedAt = _today,
            CreatedAt = _today
        };

        context.Secrets.Add(persistedSecret);
        await context.SaveChangesAsync();

        var secretsService = InitializeSecretsService(context);

        var request = new Service.Requests.SecretsRequest
        {
            Name = "TestSecret",
            Value = "DecryptedValue"
        };

        // Act
        var exception =
            await Record.ExceptionAsync(async () => await secretsService.StoreSecretAsync(request, default));

        // Assert
        Assert.NotNull(exception);
        Assert.IsAssignableFrom<ValidationException>(exception);
    }


    private ISecretsService InitializeSecretsService(SecretsDbContext? context = null,
        ISecretsDataProtector? dataProtectionProvider = null)
    {
        return new Service.Services.SecretsService(context ?? CreateDbContext(),
            dataProtectionProvider ?? Substitute.For<ISecretsDataProtector>());
    }
}