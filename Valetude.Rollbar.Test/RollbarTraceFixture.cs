using System;
using Xunit;

namespace Rollbar.Test {
    public class RollbarTraceFixture {
        [Fact]
        public void Trace_built_from_exception_has_correct_frames() {
            var trace = new RollbarTrace(GetException());
            Assert.NotNull(trace.Frames);
            Assert.NotEmpty(trace.Frames);
            Assert.Equal(2, trace.Frames.Length);
            Assert.Equal("Rollbar.Test.RollbarTraceFixture.ThrowException()", trace.Frames[0].Method);
            Assert.Equal("Rollbar.Test.RollbarTraceFixture.GetException()", trace.Frames[1].Method);
            Assert.All(trace.Frames, frame => Assert.EndsWith("RollbarTraceFixture.cs", frame.FileName));
        }

        [Fact]
        public void Trace_built_from_exception_has_frame_and_exception() {
            var trace = new RollbarTrace(GetException());
            Assert.NotNull(trace.Exception);
            Assert.NotNull(trace.Frames);
            Assert.NotEmpty(trace.Frames);
        }

        [Fact]
        public void Trace_built_manually_works_correctly() {
            var trace = new RollbarTrace(new RollbarFrame[0], new RollbarException("BadClass"));
            Assert.Equal("BadClass", trace.Exception.Class);
            Assert.Empty(trace.Frames);
        }

        [Fact]
        public void Null_frames_not_allowed() {
            Assert.Throws<ArgumentNullException>(() => {
                new RollbarTrace(null, new RollbarException("whatever"));
            });
        }

        [Fact]
        public void Null_rollbar_exception_not_allowed() {
            Assert.Throws<ArgumentNullException>(() => {
                new RollbarTrace(new RollbarFrame[0], null);
            });
        }

        [Fact]
        public void Null_exception_not_allowed() {
            Assert.Throws<ArgumentNullException>(() => {
                new RollbarTrace(null);
            });
        }

        private static Exception GetException() {
            try {
                ThrowException();
            }
            catch (Exception e) {
                return e;
            }
            throw new Exception("Unreachable");
        }

        private static void ThrowException() {
            throw new Exception("Bummer");
        }
    }
}
