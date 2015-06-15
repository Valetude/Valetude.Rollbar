using Newtonsoft.Json;

namespace Valetude.Rollbar {
    [JsonConverter(typeof (ErrorLevelConverter))]
    public enum ErrorLevel {
        Critical,
        Error,
        Warning,
        Info,
        Debug
    }
}