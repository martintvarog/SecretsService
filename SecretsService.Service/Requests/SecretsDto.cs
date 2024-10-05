using System.ComponentModel.DataAnnotations;

namespace SecretsService.Service.Requests;

/// <summary>
/// Data transfer object for a secret.
/// </summary>
public record class SecretDto
{
    /// <summary>
    /// Name of the secret.
    /// </summary>
    [Required]
    public string Name { get; init; } = null!;

    /// <summary>
    /// Value of the secret.
    /// </summary>
    [Required]
    public string Value { get; init; } = null!;
}