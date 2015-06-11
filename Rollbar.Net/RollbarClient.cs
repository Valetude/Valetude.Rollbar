using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Rollbar {
    [JsonConverter(typeof(ArbitraryKeyConverter))]
    public class RollbarClient : HasArbitraryKeys {
        public RollbarClient() : base(null) {
        }

        public RollbarClient(Dictionary<string, object> keys) : base(keys) {
        }

        public RollbarJavascriptClient Javascript { get; set; }

        public override void Normalize() {
            Javascript = (RollbarJavascriptClient) (AdditionalKeys.ContainsKey("javascript") ? AdditionalKeys["javascript"] : null);
            AdditionalKeys.Remove("javascript");
        }

        public override Dictionary<string, object> Denormalize() {
            var dict = AdditionalKeys.ToDictionary(k => k.Key, k => k.Value);
            if (Javascript != null) {
                dict["javascript"] = Javascript;
            }
            return dict;
        }
    }
}
