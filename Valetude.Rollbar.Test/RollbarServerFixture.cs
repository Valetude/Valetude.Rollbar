using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Valetude.Rollbar;
using Xunit;

namespace Rollbar.Test {
    public class RollbarServerFixture {
        private readonly RollbarServer _rollbarServer;

        public RollbarServerFixture() {
            this._rollbarServer = new RollbarServer();
        }

        [Fact]
        public void Request_host_rendered_when_present() {
            const string host = "www.valetude.com";
            _rollbarServer.Host = host;
            var json = JsonConvert.SerializeObject(_rollbarServer);
            Assert.Contains(host, json);
            var jObject = JObject.Parse(json);
            Assert.Equal(host, jObject["host"]);
        }

        [Fact]
        public void Request_root_rendered_when_present() {
            const string root = @"C:/inetpub/www/root";
            _rollbarServer.Root = root;
            var json = JsonConvert.SerializeObject(_rollbarServer);
            Assert.Contains(root, json);
            var jObject = JObject.Parse(json);
            Assert.Equal(root, jObject["root"]);
        }

        [Fact]
        public void Request_branch_rendered_when_present() {
            const string branch = "master";
            _rollbarServer.Branch = branch;
            var json = JsonConvert.SerializeObject(_rollbarServer);
            Assert.Contains(branch, json);
            var jObject = JObject.Parse(json);
            Assert.Equal(branch, jObject["branch"]);
        }

        [Fact]
        public void Request_code_version_rendered_when_present() {
            const string codeVersion = "b6437f45b7bbbb15f5eddc2eace4c71a8625da8c";
            _rollbarServer.CodeVersion = codeVersion;
            var json = JsonConvert.SerializeObject(_rollbarServer);
            Assert.Contains(codeVersion, json);
            var jObject = JObject.Parse(json);
            Assert.Equal(codeVersion, jObject["code_version"]);
        }
    }
}
