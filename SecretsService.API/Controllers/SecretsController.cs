using Microsoft.AspNetCore.Mvc;
using SecretsService.Service.Contracts;
using SecretsService.Service.Requests;

namespace SecretsService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SecretsController : ControllerBase
    {
        private readonly ISecretsService _secretsService;

        public SecretsController(ISecretsService secretsService)
        {
            _secretsService = secretsService;
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetSecret(string name, CancellationToken cancellationToken)
        {
            var secret = await _secretsService.GetSecretAsync(name, cancellationToken);

            if (secret == null) return NotFound();

            return Ok(secret);
        }

        [HttpPost]
        public async Task<IActionResult> StoreSecret([FromBody] SecretDto secretDto,
            CancellationToken cancellationToken)
        {
            await _secretsService.StoreSecretAsync(secretDto, cancellationToken);

            return CreatedAtAction(nameof(GetSecret), new { name = secretDto.Name }, secretDto);
        }
    }
}