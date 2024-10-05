using System.ComponentModel.DataAnnotations;
using SecretsService.Service.Contracts;
using Microsoft.EntityFrameworkCore;
using SecretsService.Model.Context;
using SecretsService.Service.Requests;
using SecretsService.Service.Results;
using Secret = SecretsService.Model.Secret;


namespace SecretsService.Service.Services;

public class SecretsService : ISecretsService
{
    private readonly SecretsDbContext _context;
    private readonly ISecretsDataProtector _dataProtector;

    public SecretsService(SecretsDbContext context, ISecretsDataProtector dataProtectionProvider)
    {
        _context = context;
        _dataProtector = dataProtectionProvider;
    }

    public async Task<SecretsResult?> GetSecretAsync(string name, CancellationToken cancellationToken)
    {
        var secret = await _context.Secrets.FirstOrDefaultAsync(s => s.Name == name, cancellationToken);

        if (secret == null) return null;

        var decryptedValue = _dataProtector.Unprotect(secret.Value);

        return new SecretsResult()
        {
            SecretId = secret.SecretId,
            Name = secret.Name,
            Value = decryptedValue,
            CreatedAt = secret.CreatedAt,
            UpdatedAt = secret.UpdatedAt
        };
    }

    public async Task StoreSecretAsync(SecretsRequest secretsRequest, CancellationToken cancellationToken)
    {
        await ValidateUniqueNameAsync(secretsRequest.Name, cancellationToken);

        var encryptedValue = _dataProtector.Protect(secretsRequest.Value);

        var secret = new Secret
        {
            Name = secretsRequest.Name,
            Value = encryptedValue,
            CreatedAt = DateTimeOffset.Now,
            UpdatedAt = DateTimeOffset.Now
        };

        _context.Secrets.Add(secret);

        await _context.SaveChangesAsync(cancellationToken);
    }

    private async Task ValidateUniqueNameAsync(string name, CancellationToken cancellationToken)
    {
        if (await _context.Secrets.AnyAsync(s => s.Name == name, cancellationToken))
        {
            throw new ValidationException($"Secret with name '{name}' already exists.");
        }
    }
}