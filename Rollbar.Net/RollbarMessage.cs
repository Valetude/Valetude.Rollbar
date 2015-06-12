using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Rollbar {
    [JsonConverter(typeof (ArbitraryKeyConverter))]
    public class RollbarMessage : HasArbitraryKeys {
        public RollbarMessage(string body) {
            if (string.IsNullOrWhiteSpace(body)) {
                throw new ArgumentNullException("body");
            }
            Body = body;
        }

        public string Body { get; private set; }

        protected override void Normalize() {
            Body = (string) (AdditionalKeys.ContainsKey("body") ? AdditionalKeys["body"] : null);
            AdditionalKeys.Remove("body");
        }

        protected override Dictionary<string, object> Denormalize(Dictionary<string, object> dict) {
            dict["body"] = Body;
            return dict;
        }
    }
}
