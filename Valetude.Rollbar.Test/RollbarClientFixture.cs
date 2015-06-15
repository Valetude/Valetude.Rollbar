using Newtonsoft.Json;
using Valetude.Rollbar;
using Xunit;

namespace Rollbar.Test {
    public class RollbarClientFixture {
        private readonly RollbarClient _rollbarClient;

        public RollbarClientFixture() {
            this._rollbarClient= new RollbarClient();
        }

        [Fact]
        public void Client_rendered_as_dict_when_empty() {
            Assert.Equal("{}", JsonConvert.SerializeObject(_rollbarClient));
        }

        [Fact]
        public void Client_renders_arbitrary_keys_correctly() {
            _rollbarClient["test-key"] = "test-value";
            Assert.Equal("{\"test-key\":\"test-value\"}", JsonConvert.SerializeObject(_rollbarClient));
        }

        [Fact]
        public void Client_renders_javascript_entry_correctly() {
            _rollbarClient.Javascript = new RollbarJavascriptClient();
            Assert.Equal("{\"javascript\":{}}", JsonConvert.SerializeObject(_rollbarClient));
        }
    }
}
