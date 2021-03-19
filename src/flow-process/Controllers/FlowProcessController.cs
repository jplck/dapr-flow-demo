using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudNative.CloudEvents;
using Dapr;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using flow_process.Model;

namespace flow_process.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FlowProcessController : ControllerBase
    {
        private readonly ILogger<FlowProcessController> _logger;
        private const string PubSubName = "servicebus-pubsub";
        private const string PubSubTopic = "deviceevents";
        private const string StateStoreName = "statestore";

        public FlowProcessController(ILogger<FlowProcessController> logger)
        {
            _logger = logger;
        }

        [Topic(PubSubName, PubSubTopic)]
        [HttpPost("/floweventreceive")]
        public async Task<ActionResult<string>> FlowEventReceive([FromBody] FlowEvent message, [FromServices] DaprClient daprClient )
        {
            _logger.LogInformation($"Received message with Id: {message.Id}");

            await daprClient.SaveStateAsync(StateStoreName, message.Id, message.Content);

            return Ok("Thanks");
        }
    }
}

