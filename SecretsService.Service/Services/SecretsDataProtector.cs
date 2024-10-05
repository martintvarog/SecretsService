using Microsoft.AspNetCore.DataProtection;
using SecretsService.Service.Contracts;

namespace SecretsService.Service.Services;

public class SecretsDataProtector : ISecretsDataProtector
{
    private const string DataProtectionPurpose = "SecretsService.SecretsService";

    private readonly IDataProtector _dataProtector;

    public SecretsDataProtector(IDataProtectionProvider dataProtectionProvider)
    {
        _dataProtector = dataProtectionProvider.CreateProtector(DataProtectionPurpose);
    }

    public string Protect(string value)
    {
        return _dataProtector.Protect(value);
    }

    public string Unprotect(string protectedValue)
    {
        return _dataProtector.Unprotect(protectedValue);
    }
}