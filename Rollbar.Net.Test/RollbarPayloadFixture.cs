using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Rollbar.Test {
    public class RollbarPayloadFixture {
        [Fact]
        public void Payload_works() {
            try {
                throw new Exception("Test");
            }
            catch (Exception e) {
                var payload = new RollbarPayload("access-token", new RollbarData("the-environment", new RollbarBody(e)));
                JObject asJson = JObject.Parse(payload.ToJson());

                Assert.Equal("access-token", asJson["access_token"].Value<string>());
                Assert.NotNull(asJson["data"]);
                Assert.Equal("the-environment", asJson["data"]["environment"].Value<string>());
                Assert.NotNull(asJson["data"]["body"]);
                Assert.NotNull(asJson["data"]["body"]["trace"]);
                Assert.Null(asJson["data"]["body"]["trace_chain"]);
                Assert.Null(asJson["data"]["body"]["message"]);
                Assert.Null(asJson["data"]["body"]["crash_report"]);

                var frames = Assert.IsType<JArray>(asJson["data"]["body"]["trace"]["frames"]);
                Assert.All(frames, token => Assert.NotNull(token["filename"]));
                Assert.Equal("Payload_works", frames[0]["method"].Value<string>());
                var exception = Assert.IsType<JObject>(asJson["data"]["body"]["trace"]["exception"]);
                Assert.Equal("Test", exception["message"].Value<string>());
                Assert.Equal("System.Exception", exception["class"].Value<string>());

                Assert.Equal("windows", asJson["data"]["platform"].Value<string>());
                Assert.Equal("c#", asJson["data"]["language"].Value<string>());
                Assert.Equal(payload.RollbarData.Notifier.ToArray(), asJson["data"]["notifier"].ToObject<Dictionary<string, string>>());

                IEnumerable<string> keys = ((JObject) asJson["data"]).Properties().Select(p => p.Name).ToArray();

                Assert.False(keys.Contains("level"), "level should not be present");
                Assert.False(keys.Contains("code_version"), "code_version should not be present");
                Assert.False(keys.Contains("framework"), "framework should not be present");
                Assert.False(keys.Contains("context"), "context should not be present");
                Assert.False(keys.Contains("request"), "request should not be present");
                Assert.False(keys.Contains("person"), "person should not be present");
                Assert.False(keys.Contains("server"), "server should not be present");
                Assert.False(keys.Contains("client"), "client should not be present");
                Assert.False(keys.Contains("custom"), "custom should not be present");
                Assert.False(keys.Contains("fingerprint"), "fingerprint should not be present");
                Assert.False(keys.Contains("title"), "title should not be present");
                Assert.False(keys.Contains("uuid"), "uuid should not be present");
            }
        }
    }
}
