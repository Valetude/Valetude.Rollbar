using Newtonsoft.Json;
using Xunit;

namespace Rollbar.Test {
    public class RollbarMessageFixture {
        private readonly RollbarMessage _rollbarMessage;

        public RollbarMessageFixture() {
            this._rollbarMessage = new RollbarMessage("Body of the message");
        }

        [Fact]
        public void Message_has_body() {
            Assert.Equal("{\"body\":\"Body of the message\"}", JsonConvert.SerializeObject(_rollbarMessage));
        }

        [Fact]
        public void Message_has_arbitrary_keys() {
            _rollbarMessage["whatever"] = "fun";
            Assert.Equal("{\"whatever\":\"fun\",\"body\":\"Body of the message\"}", JsonConvert.SerializeObject(_rollbarMessage));
        }

        [Fact]
        public void Message_body_cant_be_overwritten_by_body_indexer() {
            _rollbarMessage["body"] = null;
            Assert.Equal("{\"body\":\"Body of the message\"}", JsonConvert.SerializeObject(_rollbarMessage));
        }

        [Fact]
        public void Message_body_cant_be_set_to_incorrect_type() {
            _rollbarMessage["body"] = 10;
            Assert.Equal("{\"body\":\"Body of the message\"}", JsonConvert.SerializeObject(_rollbarMessage));
        }
    }
}
