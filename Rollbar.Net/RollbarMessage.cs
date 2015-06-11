using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Rollbar {
    [JsonConverter(typeof (ArbitraryKeyConverter))]
    public class RollbarMessage : HasArbitraryKeys {
        public RollbarMessage(string body) : this(body, null) {
        }

        public RollbarMessage(string body, Dictionary<string, object> keys) : base(keys) {
            if (string.IsNullOrWhiteSpace(body)) {
                throw new ArgumentNullException("body");
            }
            Body = body;
        }

        public string Body { get; private set; }

        public override void Normalize() {
            Body = (string) (AdditionalKeys.ContainsKey("body") ? AdditionalKeys["body"] : null);
            AdditionalKeys.Remove("body");
        }

        public override Dictionary<string, object> Denormalize() {
            var dict = AdditionalKeys.ToDictionary(k => k.Key, v => v.Value);
            dict["body"] = Body;
            return dict;
        }
    }
}
