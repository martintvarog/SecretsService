using Microsoft.EntityFrameworkCore;
using SecretsService.DataAccess;
using SecretsService.DataAccess.Context;
using SecretsService.Service.Contracts;

namespace SecretsService.Service.Services;

public class SecretsService : ISecretsService
{
    private readonly SecretsDbContext _context;

    public SecretsService(SecretsDbContext context)
    {
        _context = context;
    }

    public async Task<SecretDto> GetSecretAsync(string name)
    {
        var secret = await _context.Secrets.FirstOrDefaultAsync(s => s.Name == name);
        if (secret == null) return null;

        return new SecretDto { Name = secret.Name, Value = secret.Value };
    }

    public async Task StoreSecretAsync(SecretDto secretDto)
    {
        var secret = new Secret
        {
            Name = secretDto.Name,
            Value = secretDto.Value,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Secrets.Add(secret);
        await _context.SaveChangesAsync();
    }
}