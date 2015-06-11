using System;
using System.Diagnostics;
using System.Linq;
using Newtonsoft.Json;

namespace Rollbar {
    public class RollbarTrace {
        public RollbarTrace(RollbarFrame[] frames, RollbarException exception) {
            if (frames == null) {
                throw new ArgumentNullException("frames");
            }
            if (exception == null) {
                throw new ArgumentNullException("exception");
            }
            Frames = frames;
            Exception = exception;
        }

        public RollbarTrace(Exception exception) {
            if (exception == null) {
                throw new ArgumentNullException("exception");
            }

            var frames = new StackTrace(exception, true).GetFrames() ?? new StackFrame[0];
            
            Frames = frames.Select(frame => new RollbarFrame(frame)).ToArray();
            Exception = new RollbarException(exception);
        }

        [JsonProperty("frames", Required = Required.Always)]
        public RollbarFrame[] Frames { get; private set; }

        [JsonProperty("exception", Required = Required.Always)]
        public RollbarException Exception { get; private set; }
    }
}
