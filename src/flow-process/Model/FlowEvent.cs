using Newtonsoft.Json;

namespace flow_process.Model
{
    public class FlowEvent {
        [JsonProperty("id")]
        public string Id { get; set;}

        [JsonProperty("content")]
        public string Content { get; set;}
    }
}