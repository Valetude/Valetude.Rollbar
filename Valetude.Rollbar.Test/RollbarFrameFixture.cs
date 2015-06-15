using System;
using System.Diagnostics;
using Newtonsoft.Json;
using Xunit;

namespace Rollbar.Test {
    public class RollbarFrameFixture {
        [Fact]
        public void Frame_from_filename_leaves_everything_else_null() {
            var frame = new RollbarFrame("ThisFile.cs");
            Assert.Equal("ThisFile.cs", frame.FileName);
            Assert.Null(frame.LineNo);
            Assert.Null(frame.ColNo);
            Assert.Null(frame.Method);
        }

        [Fact]
        public void Frame_from_stackframe_fills_out_everythign() {
            var frame = new RollbarFrame(GetFrame());
            Assert.EndsWith("RollbarFrameFixture.cs", frame.FileName);
            Assert.NotNull(frame.LineNo);
            Assert.NotNull(frame.ColNo);
            Assert.NotNull(frame.Method);
        }

        [Fact]
        public void Frame_from_stack_frame_serializes_correctly() {
            var frame = new RollbarFrame(GetFrame());
            var json = JsonConvert.SerializeObject(frame);
            Assert.Contains(string.Format("\"filename\":\"{0}\"", frame.FileName.Replace("\\", "\\\\")), json);
            Assert.Matches("\"lineno\":\\d+", json);
            Assert.Matches("\"colno\":\\d+", json);
            Assert.Contains("\"method\":\"Rollbar.Test.RollbarFrameFixture.GetFrame()\"", json);
        }

        private static StackFrame GetFrame() {
            try {
                throw new InvalidOperationException("I'm afraid I can't do that HAL");
            }
            catch (Exception e) {
                return new StackTrace(e, true).GetFrames()[0];
            }
        }
    }
}
