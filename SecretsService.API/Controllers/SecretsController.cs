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

        /// <summary>
        /// Get secret by name.
        /// </summary>
        /// <param name="name">The name of the secret.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <response code="200">Returns the secret if found.</response>
        /// <response code="404">If the secret is not found.</response>
        /// <returns>An <see cref="IActionResult"/> containing the secret if found, otherwise a 404 Not Found status.</returns>
        [ProducesResponseType(typeof(SecretDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{name}")]
        public async Task<IActionResult> GetSecret(string name, CancellationToken cancellationToken)
        {
            var secret = await _secretsService.GetSecretAsync(name, cancellationToken);

            if (secret == null) return NotFound();

            return Ok(secret);
        }

        /// <summary>
        /// Store a new secret.
        /// </summary>
        /// <param name="secretDto">The secret to store.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <response code="201">Returns the stored secret.</response>
        /// <response code="400">If the secret with the same name already exists.</response>
        /// <returns>An <see cref="IActionResult"/> containing the stored secret if successful, otherwise a 400 Bad Request status.</returns>
        [ProducesResponseType(typeof(SecretDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(InvalidOperationException), StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> StoreSecret([FromBody] SecretDto secretDto,
            CancellationToken cancellationToken)
        {
            await _secretsService.StoreSecretAsync(secretDto, cancellationToken);

            return CreatedAtAction(nameof(GetSecret), new { name = secretDto.Name }, secretDto);
        }
    }
}