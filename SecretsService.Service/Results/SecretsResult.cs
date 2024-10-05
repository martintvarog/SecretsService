namespace SecretsService.Service.Results;

/// <summary>
/// Secrets result.
/// </summary>
public record class SecretsResult
{
    /// <summary>
    /// SecretId.
    /// </summary>
    public int SecretId { get; init; }

    /// <summary>
    /// Name of Secret.
    /// </summary>
    public string Name { get; init; } = null!;

    /// <summary>
    /// Encrypted secret's value
    /// </summary>
    public string Value { get; init; } = null!;

    /// <summary>
    /// Creation date.
    /// </summary>
    public DateTimeOffset CreatedAt { get; init; }

    /// <summary>
    /// Date of last update.
    /// </summary>
    public DateTimeOffset UpdatedAt { get; init; }
}