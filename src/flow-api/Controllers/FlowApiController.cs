using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudNative.CloudEvents;
using Dapr;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using flow_api.Model;

namespace flow_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FlowApiController : ControllerBase
    {
        private readonly ILogger<FlowApiController> _logger;
        private const string PubSubName = "servicebus-pubsub";
        private const string PubSubTopic = "deviceevents";

        private const string StateStoreName = "statestore";

        public FlowApiController(ILogger<FlowApiController> logger)
        {
            _logger = logger;
        }

        [HttpPost("/start")]
        public async Task<ActionResult<string>> FlowEventStart([FromBody] FlowEvent message, [FromServices] DaprClient daprClient )
        {
            _logger.LogInformation(message.Id);
            await daprClient.PublishEventAsync(PubSubName, PubSubTopic, message);
            return Ok("Thanks");
        }

        [HttpGet("/state/{key}")]
        public async Task<ActionResult<string>> FlowEventStateGet(string key, [FromServices] DaprClient daprClient )
        {
            var content = await daprClient.GetStateAsync<string>(StateStoreName, key);
            return string.IsNullOrEmpty(content) ? NoContent() : Ok(content);
        }
    }
}

