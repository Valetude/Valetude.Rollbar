using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Valetude.Rollbar {
    public class RollbarBody {
        public RollbarBody(IEnumerable<Exception> exceptions) {
            if (exceptions == null) {
                throw new ArgumentNullException("exceptions");
            }
            var allExceptions = exceptions as Exception[] ?? exceptions.ToArray();
            if (!allExceptions.Any()) {
                throw new ArgumentException("Trace Chains must have at least one Trace", "exceptions");
            }
            TraceChain = allExceptions.Select(e => new RollbarTrace(e)).ToArray();
        }

        public RollbarBody(AggregateException exception) : this(exception.InnerExceptions) {
        }

        public RollbarBody(Exception exception) {
            if (exception == null) {
                throw new ArgumentNullException("exception");
            }
            Trace = new RollbarTrace(exception);
        }

        public RollbarBody(RollbarMessage rollbarMessage) {
            if (rollbarMessage == null) {
                throw new ArgumentNullException("rollbarMessage");
            }
            Message = rollbarMessage;
        }

        public RollbarBody(string crashReport) {
            if (string.IsNullOrWhiteSpace(crashReport)) {
                throw new ArgumentNullException("crashReport");
            }
            CrashReport = new Dictionary<string, string> {
                { "raw", crashReport },
            };
        }

        [JsonProperty("trace", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public RollbarTrace Trace { get; private set; }

        [JsonProperty("trace_chain", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public RollbarTrace[] TraceChain { get; private set; }

        [JsonProperty("message", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public RollbarMessage Message { get; private set; }

        [JsonProperty("crash_report", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Dictionary<string, string> CrashReport { get; private set; }
    }
}
