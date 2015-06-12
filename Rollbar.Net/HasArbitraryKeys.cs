using System.Collections;
using System.Collections.Generic;

namespace Rollbar {
    public abstract class HasArbitraryKeys : IEnumerable<KeyValuePair<string, object>> {
        protected HasArbitraryKeys(Dictionary<string, object> additionalKeys) {
            AdditionalKeys = additionalKeys ?? new Dictionary<string, object>();
        }

        public abstract void Normalize();

        public abstract Dictionary<string, object> Denormalize();

        public Dictionary<string, object> AdditionalKeys { get; private set; }

        public void Add(string key, object value) {
            AdditionalKeys.Add(key, value);
            Normalize();
        }

        public object this[string key] {
            get { return Denormalize()[key]; }
            set { Add(key, value); }
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator() {
            return Denormalize().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
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