using System.Collections.Generic;
using Newtonsoft.Json;

namespace Rollbar {
    [JsonConverter(typeof (ArbitraryKeyConverter))]
    public class RollbarJavascriptClient : HasArbitraryKeys {
        public RollbarJavascriptClient() : base(null) {
        }

        public RollbarJavascriptClient(Dictionary<string, object> additionalKeys) : base(additionalKeys) {
        }

        public string Browser { get; set; }

        public string CodeVersion { get; set; }

        public string SourceMapEnabled { get; set; }

        public string GuessUncaughtFrames { get; set; }

        public override void Normalize() {
            Browser = (string) (AdditionalKeys.ContainsKey("browser") ? AdditionalKeys["browser"] : null);
            AdditionalKeys.Remove("browser");
            CodeVersion = (string) (AdditionalKeys.ContainsKey("code_version") ? AdditionalKeys["code_version"] : null);
            AdditionalKeys.Remove("code_version");
            SourceMapEnabled = (string) (AdditionalKeys.ContainsKey("source_map_enabled") ? AdditionalKeys["source_map_enabled"] : null);
            AdditionalKeys.Remove("source_map_enabled");
            GuessUncaughtFrames = (string) (AdditionalKeys.ContainsKey("guess_uncaught_frames") ? AdditionalKeys["guess_uncaught_frames"] : null);
            AdditionalKeys.Remove("guess_uncaught_frames");
        }

        public override Dictionary<string, object> Denormalize() {
            var dict = new Dictionary<string, object>();
            if (Browser != null) {
                dict["browser"] = Browser;
            }
            if (CodeVersion != null) {
                dict["code_version"] = CodeVersion;
            }
            if (SourceMapEnabled != null) {
                dict["source_map_enabled"] = SourceMapEnabled;
            }
            if (GuessUncaughtFrames != null) {
                dict["guess_uncaught_frames"] = GuessUncaughtFrames;
            }
            return dict;
        }
    }
}
