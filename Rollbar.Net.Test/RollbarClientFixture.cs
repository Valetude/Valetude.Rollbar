using Newtonsoft.Json;
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
    }
}
