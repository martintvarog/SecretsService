namespace SecretsService.Service.Contracts;

public interface ISecretsService
{
    Task<SecretDto?> GetSecretAsync(string name, CancellationToken cancellationToken);

    Task StoreSecretAsync(SecretDto secretDto, CancellationToken cancellationToken);
}