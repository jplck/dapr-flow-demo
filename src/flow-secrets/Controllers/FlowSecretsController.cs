using System.Threading.Tasks;
using Dapr;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace flow_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FlowSecretsController : ControllerBase
    {
        private readonly ILogger<FlowSecretsController> _logger;
        private const string SecretStoreName = "secretstore";

        public FlowSecretsController(ILogger<FlowSecretsController> logger)
        {
            _logger = logger;
        }

        [HttpGet("/secrets/{key}")]
        public async Task<ActionResult<string>> FlowEventSecretGet(string key, [FromServices] DaprClient daprClient )
        {
            var secret = await daprClient.GetSecretAsync(SecretStoreName, key);
            return secret.Count == 0 ? NoContent() : Ok(secret[key]);
        }
    }
}

