namespace SecretsService.Service.Contracts;

/// <summary>
/// Abstraction for protecting and unprotecting data over <see cref="Microsoft.AspNetCore.DataProtection"/>.
/// </summary>
public interface ISecretsDataProtector
{
    /// <summary>
    /// Encrypts the specified value.
    /// </summary>
    /// <param name="value">The value to protect.</param>
    /// <returns>The protected value.</returns>
    string Protect(string value);
    
    /// <summary>
    /// Decrypts the specified protected value.
    /// </summary>
    /// <param name="protectedValue">The protected value to unprotect.</param>
    /// <returns>The original value.</returns>
    string Unprotect(string protectedValue);
}