using System;
using Newtonsoft.Json;

namespace Rollbar {
    public class RollbarPayload {
        public RollbarPayload(string accessToken, RollbarData data) {
            if (string.IsNullOrWhiteSpace(accessToken)) {
                throw new ArgumentNullException("accessToken");
            }
            if (data == null) {
                throw new ArgumentNullException("data");
            }
            AccessToken = accessToken;
            RollbarData = data;
        }

        public string ToJson() {
            return JsonConvert.SerializeObject(this);
        }

        [JsonProperty("access_token", Required = Required.Always)]
        public string AccessToken { get; private set; }

        [JsonProperty("data", Required = Required.Always)]
        public RollbarData RollbarData { get; private set; }
    }
}
