using SecretsService.DataAccess.Context;
using SecretsService.Service.Contracts;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using SecretsService.Service.Requests;
using Secret = SecretsService.DataAccess.Secret;

namespace SecretsService.Service.Services;

public class SecretsService : ISecretsService
{
    private readonly SecretsDbContext _context;
    private readonly IDataProtector _dataProtector;

    public SecretsService(SecretsDbContext context, IDataProtectionProvider dataProtectionProvider)
    {
        _context = context;
        _dataProtector = dataProtectionProvider.CreateProtector("SecretsService.SecretsService");
    }

    public async Task<SecretDto?> GetSecretAsync(string name, CancellationToken cancellationToken)
    {
        var secret = await _context.Secrets.FindAsync([name], cancellationToken);

        if (secret == null) return null;

        var decryptedValue = _dataProtector.Unprotect(secret.Value);

        return new SecretDto { Name = secret.Name, Value = decryptedValue };
    }

    public async Task StoreSecretAsync(SecretDto secretDto, CancellationToken cancellationToken)
    {
        await ValidateUniqueNameAsync(secretDto.Name, cancellationToken);

        var encryptedValue = _dataProtector.Protect(secretDto.Value);

        var secret = new Secret
        {
            Name = secretDto.Name,
            Value = encryptedValue,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Secrets.Add(secret);

        await _context.SaveChangesAsync(cancellationToken);
    }

    private async Task ValidateUniqueNameAsync(string name, CancellationToken cancellationToken)
    {
        if (await _context.Secrets.AnyAsync(s => s.Name == name, cancellationToken))
        {
            throw new InvalidOperationException($"Secret with name '{name}' already exists.");
        }
    }
}