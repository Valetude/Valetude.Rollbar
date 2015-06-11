using System;
using Newtonsoft.Json;
using RestSharp;

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

        public void Send() {
            var http = new RestClient("https://api.rollbar.com");
            var request = new RestRequest("/api/1/item/", Method.POST);
            request.AddParameter("application/json", JsonConvert.SerializeObject(this), ParameterType.RequestBody);
            http.Execute(request);
        }

        [JsonProperty("access_token", Required = Required.Always)]
        public string AccessToken { get; private set; }

        [JsonProperty("data", Required = Required.Always)]
        public RollbarData RollbarData { get; private set; }
    }
}
