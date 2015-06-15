using System;
using Newtonsoft.Json;
using Xunit;

namespace Rollbar.Test {
    public class RollbarExceptionFixture {
        [Fact]
        public void Exception_from_exception_has_class() {
            var rollbarException = new RollbarException(GetException());
            Assert.Equal("System.NotFiniteNumberException", rollbarException.Class);
        }

        [Fact]
        public void Exception_from_exception_can_have_message() {
            var rollbarException = new RollbarException(GetException()) {
                Message = "Hello World!",
            };
            Assert.Equal("Hello World!", rollbarException.Message);
        }

        [Fact]
        public void Exception_from_exception_can_have_description() {
            var rollbarException = new RollbarException(GetException()) {
                Description = "Hello World!",
            };
            Assert.Equal("Hello World!", rollbarException.Description);
        }

        [Fact]
        public void Exception_from_class_name_has_class() {
            var rollbarException = new RollbarException("NotFiniteNumberException");
            Assert.Equal("NotFiniteNumberException", rollbarException.Class);
        }

        [Fact]
        public void Exception_from_class_name_can_have_mesasge() {
            var rollbarException = new RollbarException("NotFiniteNumberException") {
                Message = "Hello World!",
            };
            Assert.Equal("Hello World!", rollbarException.Message);
        }

        [Fact]
        public void Exception_from_class_name_can_have_description() {
            var rollbarException = new RollbarException("NotFiniteNumberException") {
                Description = "Hello World!",
            };
            Assert.Equal("Hello World!", rollbarException.Description);
        }

        [Fact]
        public void Exception_serializes_correctly() {
            var rollbarException = new RollbarException("Test");
            Assert.Equal("{\"class\":\"Test\"}", JsonConvert.SerializeObject(rollbarException));
        }

        [Fact]
        public void Exceptoin_serializes_message_correctly() {
            var rollbarException = new RollbarException("Test") {Message = "Oops!"};
            Assert.Contains("\"message\":\"Oops!\"", JsonConvert.SerializeObject(rollbarException));
            Assert.Contains("\"class\":\"Test\"", JsonConvert.SerializeObject(rollbarException));
        }

        [Fact]
        public void Exceptoin_serializes_description_correctly() {
            var rollbarException = new RollbarException("Test") { Description = "Oops!" };
            Assert.Contains("\"description\":\"Oops!\"", JsonConvert.SerializeObject(rollbarException));
            Assert.Contains("\"class\":\"Test\"", JsonConvert.SerializeObject(rollbarException));
        }

        private static Exception GetException() {
            try {
                throw new NotFiniteNumberException("Not a Finite Number!");
            }
            catch (Exception e) {
                return e;
            }
        }
    }
}
