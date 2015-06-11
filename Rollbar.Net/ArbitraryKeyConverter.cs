using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Rollbar {
    public class ArbitraryKeyConverter : JsonConverter<HasArbitraryKeys> {
        public override void WriteJson(JsonWriter writer, HasArbitraryKeys value, JsonSerializer serializer) {
            writer.WriteValue(value.Denormalize());
        }

        public override HasArbitraryKeys ReadJson(JsonReader reader, HasArbitraryKeys existingValue, JsonSerializer serializer) {
            IDictionary<string, JToken> obj = JObject.Load(reader);

            existingValue.WithKeys(obj.ToDictionary(x => x.Key, x => x.Value as object));
            existingValue.Normalize();

            return existingValue;
        }
    }
}