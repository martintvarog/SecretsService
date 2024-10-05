namespace SecretsService.DataAccess;

public class Secret
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Value { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}