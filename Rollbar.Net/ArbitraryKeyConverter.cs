﻿using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Rollbar {
    public class ArbitraryKeyConverter : JsonConverter<HasArbitraryKeys> {
        public override void WriteJson(JsonWriter writer, HasArbitraryKeys value, JsonSerializer serializer) {
            JObject.FromObject(value.Denormalize(), serializer).WriteTo(writer);
        }

        public override HasArbitraryKeys ReadJson(JsonReader reader, HasArbitraryKeys existingValue, JsonSerializer serializer) {
            IDictionary<string, JToken> obj = JObject.Load(reader);

            existingValue.Extend(obj.ToDictionary(x => x.Key, x => x.Value as object));

            return existingValue;
        }
    }
}