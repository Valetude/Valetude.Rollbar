using Newtonsoft.Json;

namespace Valetude.Rollbar {
    public class RollbarCodeContext {
        [JsonProperty("pre", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string[] Pre { get; set; }

        [JsonProperty("post", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string[] Post { get; set; }
    }
}
