using System.ComponentModel.DataAnnotations;

namespace SecretsService.Service.Requests;

public record class SecretDto
{
    [Required]
    public string Name { get; init; } = null!;

    [Required]
    public string Value { get; init; } = null!;
}