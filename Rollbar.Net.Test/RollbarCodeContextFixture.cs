using System.Linq;
using System.Runtime.Remoting.Contexts;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Rollbar.Test {
    public class RollbarCodeContextFixture {
        private RollbarCodeContext _rollbarCodeContext;

        public RollbarCodeContextFixture() {
            this._rollbarCodeContext = new RollbarCodeContext {
                Pre = new[] {
                    "public RollbarCodeContextFixture() {",
                    "this._rollbarCodeContext = new RollbarCodeContext {",
                },
                Post = new[] {
                    "};",
                    "}",
                }
            };
        }

        [Fact]
        public void Context_serializes_reasonably() {
            var json = JsonConvert.SerializeObject(_rollbarCodeContext);
            var jobj = JObject.Parse(json);
            var pre = Assert.IsType<JArray>(jobj["pre"]).Select(j => j.Value<string>()).ToArray();
            Assert.Equal(2, pre.Length);
            Assert.Equal(_rollbarCodeContext.Pre, pre);
            var post = Assert.IsType<JArray>(jobj["post"]).Select(j => j.Value<string>()).ToArray();
            Assert.Equal(2, post.Length);
            Assert.Equal(_rollbarCodeContext.Post, post);
        }

    }
}
