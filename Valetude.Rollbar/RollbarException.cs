using System;
using Newtonsoft.Json;

namespace Rollbar {
    public class RollbarException {
        public RollbarException(string @class) {
            Class = @class;
        }

        public RollbarException(Exception exception) {
            if (exception == null) {
                throw new ArgumentNullException("exception");
            }
            Class = exception.GetType().FullName;
            Message = exception.Message;
        }

        [JsonProperty("class", Required = Required.Always)]
        public string Class { get; private set; }

        [JsonProperty("message", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Message { get; set; }

        [JsonProperty("description", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Description { get; set; }
    }
}
