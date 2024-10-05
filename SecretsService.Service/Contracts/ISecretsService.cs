using SecretsService.Service.Requests;
using SecretsService.Service.Results;

namespace SecretsService.Service.Contracts;

/// <summary>
/// Service for managing secrets.
/// </summary>
public interface ISecretsService
{
    /// <summary>
    /// Retrieves a secret by name.
    /// </summary>
    /// <param name="name">The name of the secret to retrieve.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The secret with the specified name, or null if no secret with the specified name exists.</returns>
    Task<SecretsResult?> GetSecretAsync(string name, CancellationToken cancellationToken);

    /// <summary>
    /// Stores a secret.
    /// </summary>
    /// <param name="secretsRequest">The secret to store.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    Task StoreSecretAsync(SecretsRequest secretsRequest, CancellationToken cancellationToken);
}