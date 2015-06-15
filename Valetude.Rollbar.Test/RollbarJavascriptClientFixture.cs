using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Valetude.Rollbar;
using Xunit;

namespace Rollbar.Test {
    public class RollbarJavascriptClientFixture {
        private readonly RollbarJavascriptClient _rollbarJsClient;

        public RollbarJavascriptClientFixture() {
            this._rollbarJsClient = new RollbarJavascriptClient();
        }

        [Fact]
        public void Empty_request_rendered_as_empty_dict() {
            Assert.Equal("{}", JsonConvert.SerializeObject(_rollbarJsClient));
        }

        [Fact]
        public void JsClient_browser_rendered_when_present() {
            const string browser = "chromex64";
            _rollbarJsClient.Browser = browser;
            var json = JsonConvert.SerializeObject(_rollbarJsClient);
            Assert.Contains(browser, json);
            var jObject = JObject.Parse(json);
            Assert.Equal(browser, jObject["browser"]);
        }

        [Fact]
        public void JsClient_code_version_rendered_when_present() {
            const string codeVersion = "6846d84aecf68d46d8acease684cfe86a6es84cf";
            _rollbarJsClient.CodeVersion = codeVersion;
            var json = JsonConvert.SerializeObject(_rollbarJsClient);
            Assert.Contains(codeVersion, json);
            var jObject = JObject.Parse(json);
            Assert.Equal(codeVersion, jObject["code_version"]);
        }

        [Fact]
        public void JsClient_source_map_enabled_rendered_when_present() {
            const bool sourceMapEnabled = true;
            _rollbarJsClient.SourceMapEnabled = sourceMapEnabled;
            var json = JsonConvert.SerializeObject(_rollbarJsClient);
            Assert.Contains("true", json);
            var jObject = JObject.Parse(json);
            Assert.Equal(sourceMapEnabled, jObject["source_map_enabled"]);
        }

        [Fact]
        public void JsClient_guess_uncaught_frames_rendered_when_present() {
            const bool guessUncaughtFrames = false;
            _rollbarJsClient.GuessUncaughtFrames = guessUncaughtFrames;
            var json = JsonConvert.SerializeObject(_rollbarJsClient);
            Assert.Contains("false", json);
            var jObject = JObject.Parse(json);
            Assert.Equal(guessUncaughtFrames, jObject["guess_uncaught_frames"]);
        }

        [Fact]
        public void JsClient_can_have_arbitrary_keys() {
            const string browser = "chromex64";
            _rollbarJsClient.Browser = browser;
            _rollbarJsClient["whatever"] = "test";
            var json = JsonConvert.SerializeObject(_rollbarJsClient);
            var jObject = JObject.Parse(json);
            Assert.Equal(browser, jObject["browser"]);
            Assert.Equal("test", jObject["whatever"]);
        }
    }
}
