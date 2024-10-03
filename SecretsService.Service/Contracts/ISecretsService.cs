namespace SecretsService.Service.Contracts;

public interface ISecretsService
{
    Task<SecretDto> GetSecretAsync(string name);
    Task StoreSecretAsync(SecretDto secretDto);
}