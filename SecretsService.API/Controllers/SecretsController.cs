using Microsoft.AspNetCore.Mvc;
using SecretsService.Service.Contracts;

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
        public async Task<IActionResult> GetSecret(string name)
        {
            var secret = await _secretsService.GetSecretAsync(name);
            if (secret == null)
            {
                return NotFound();
            }
            return Ok(secret);
        }

        [HttpPost]
        public async Task<IActionResult> StoreSecret([FromBody] SecretDto secretDto)
        {
            await _secretsService.StoreSecretAsync(secretDto);
            return CreatedAtAction(nameof(GetSecret), new { name = secretDto.Name }, secretDto);
        }
    }
}