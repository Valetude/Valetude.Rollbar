using System.Collections.Generic;
using Newtonsoft.Json;

namespace Rollbar {
    [JsonConverter(typeof(ArbitraryKeyConverter))]
    public class RollbarClient : HasArbitraryKeys {
        public RollbarJavascriptClient Javascript { get; set; }

        protected override void Normalize() {
            Javascript = (RollbarJavascriptClient) (AdditionalKeys.ContainsKey("javascript") ? AdditionalKeys["javascript"] : null);
            AdditionalKeys.Remove("javascript");
        }

        protected override Dictionary<string, object> Denormalize(Dictionary<string, object> dict) {
            if (Javascript != null) {
                dict["javascript"] = Javascript;
            }
            return dict;
        }
    }
}
