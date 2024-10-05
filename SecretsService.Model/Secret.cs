namespace SecretsService.DataAccess;

/// <summary>
/// Represents a secret.
/// </summary>
public class Secret
{
    /// <summary>
    /// The unique identifier of the secret.
    /// </summary>
    public int SecretId { get; set; }

    /// <summary>
    /// The name of the secret that is unique.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// The value of the secret that is encrypted.
    /// </summary>
    public string Value { get; set; } = null!;

    /// <summary>
    /// The date and time the secret was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// The date and time the secret was last updated.
    /// </summary>
    public DateTime UpdatedAt { get; set; }
}