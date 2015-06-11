using System.Collections.Generic;

namespace Rollbar {
    public abstract class HasArbitraryKeys {
        protected HasArbitraryKeys(Dictionary<string, object> additionalKeys) {
            AdditionalKeys = additionalKeys ?? new Dictionary<string, object>();
        }

        public abstract void Normalize();

        public abstract Dictionary<string, object> Denormalize();

        public Dictionary<string, object> AdditionalKeys { get; private set; }
    }

    public static class HasArbitraryKeysExtension {
        public static T WithKeys<T>(this T has, Dictionary<string, object> otherKeys) where T : HasArbitraryKeys {
            foreach (var kvp in otherKeys) {
                has.AdditionalKeys.Add(kvp.Key, kvp.Value);
            }
            return has;
        }
    }
}