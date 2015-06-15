using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Valetude.Rollbar;
using Xunit;

namespace Rollbar.Test {
    public class RollbarRequestFixture {
        private readonly RollbarRequest _rollbarRequest;

        public RollbarRequestFixture() {
            this._rollbarRequest = new RollbarRequest();
        }

        [Fact]
        public void Empty_request_rendered_as_empty_dict() {
            Assert.Equal("{}", JsonConvert.SerializeObject(_rollbarRequest));
        }

        [Fact]
        public void Request_url_rendered_when_present() {
            const string url = "/my/url?search=string";
            _rollbarRequest.Url = url;
            var json = JsonConvert.SerializeObject(_rollbarRequest);
            Assert.Contains(url, json);
            var jObject = JObject.Parse(json);
            Assert.Equal(url, jObject["url"]);
        }

        [Fact]
        public void Request_method_rendered_when_present() {
            const string method = "whatever";
            _rollbarRequest.Method = method;
            var json = JsonConvert.SerializeObject(_rollbarRequest);
            Assert.Contains(method, json);
            var jObject = JObject.Parse(json);
            Assert.Equal(method, jObject["method"]);
        }

        [Fact]
        public void Request_headers_rendered_when_present() {
            var headers = new Dictionary<string,string> {
                { "Header", "header-value" },
                { "Accept", "json" },
            };
            _rollbarRequest.Headers = headers;
            var json = JsonConvert.SerializeObject(_rollbarRequest);
            Assert.Contains("\"Header\":\"header-value\"", json);
            Assert.Contains("\"Accept\":\"json\"", json);
            JObject jObject = JObject.Parse(json)["headers"] as JObject;
            Assert.Equal(headers.Values.OrderBy(x => x), jObject.Properties().Select( x=> x.Value).Values<string>().OrderBy(x => x));
        }

        [Fact]
        public void Request_params_rendered_when_present() {
            var @params = new Dictionary<string, object> {
                { "One", (long)1 },
                { "Name", "Chris" },
            };
            _rollbarRequest.Params = @params;
            var json = JsonConvert.SerializeObject(_rollbarRequest);
            Assert.Contains("\"One\":1", json);
            Assert.Contains("\"Name\":\"Chris\"", json);
            var jObject = JObject.Parse(json)["params"] as JObject;
            Assert.NotNull(jObject);
            Assert.Equal(@params.Keys.OrderBy(x => x), jObject.Properties().Select(x => x.Name).OrderBy(x => x));
            foreach(var kvp in @params) {
                Assert.Equal(kvp.Value, jObject[kvp.Key].ToObject<object>());
            }
        }

        [Fact]
        public void Request_get_params_rendered_when_present() {
            var getParams = new Dictionary<string, object> {
                { "One", (long)1 },
                { "Name", "Chris" },
            };
            _rollbarRequest.GetParams = getParams;
            var json = JsonConvert.SerializeObject(_rollbarRequest);
            Assert.Contains("\"One\":1", json);
            Assert.Contains("\"Name\":\"Chris\"", json);
            var jObject = JObject.Parse(json)["get_params"] as JObject;
            Assert.NotNull(jObject);
            Assert.Equal(getParams.Keys.OrderBy(x => x), jObject.Properties().Select(x => x.Name).OrderBy(x => x));
            foreach (var kvp in getParams) {
                Assert.Equal(kvp.Value, jObject[kvp.Key].ToObject<object>());
            }
        }

        [Fact]
        public void Request_query_string_rendered_when_present() {
            const string queryString = "whatever";
            _rollbarRequest.QueryString = queryString;
            var json = JsonConvert.SerializeObject(_rollbarRequest);
            Assert.Contains(queryString, json);
            var jObject = JObject.Parse(json);
            Assert.Equal(queryString, jObject["query_string"]);
        }

        [Fact]
        public void Request_post_params_rendered_when_present() {
            var postParams = new Dictionary<string, object> {
                { "One", (long)1 },
                { "Name", "Chris" },
            };
            _rollbarRequest.PostParams = postParams;
            var json = JsonConvert.SerializeObject(_rollbarRequest);
            Assert.Contains("\"One\":1", json);
            Assert.Contains("\"Name\":\"Chris\"", json);
            var jObject = JObject.Parse(json)["post_params"] as JObject;
            Assert.NotNull(jObject);
            Assert.Equal(postParams.Keys.OrderBy(x => x), jObject.Properties().Select(x => x.Name).OrderBy(x => x));
            foreach(var kvp in postParams) {
                Assert.Equal(kvp.Value, jObject[kvp.Key].ToObject<object>());
            }
        }

        [Fact]
        public void Request_post_body_rendered_when_present() {
            const string postBody = "whatever";
            _rollbarRequest.PostBody = postBody;
            var json = JsonConvert.SerializeObject(_rollbarRequest);
            Assert.Contains(postBody, json);
            var jObject = JObject.Parse(json);
            Assert.Equal(postBody, jObject["post_body"]);
        }

        [Fact]
        public void Request_user_ip_rendered_when_present() {
            const string userIp = "whatever";
            _rollbarRequest.UserIp = userIp;
            var json = JsonConvert.SerializeObject(_rollbarRequest);
            Assert.Contains(userIp, json);
            var jObject = JObject.Parse(json);
            Assert.Equal(userIp, jObject["user_ip"]);
        }

        [Fact]
        public void Request_arbitrary_key_gets_rendered() {
            _rollbarRequest["whatever"] = "value";
            Assert.Contains("whatever", _rollbarRequest.Select(x => x.Key).ToArray());
            var json = JsonConvert.SerializeObject(_rollbarRequest);
            Assert.Contains("\"whatever\":\"value\"", json);
        }
    }
}
