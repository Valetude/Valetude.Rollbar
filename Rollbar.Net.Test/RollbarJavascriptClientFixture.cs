using Newtonsoft.Json;
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

    }
}
