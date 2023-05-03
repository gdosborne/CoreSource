namespace AppSystem.ApplicationHelpers {
    using System;

    public class SettingsKey {
        private SettingsKey(Type valueType, string key, object value) {
            this.ValueType = valueType;
            this.Key = key;
            this.Value = value;
        }

        public string PersistValue(char bracketLeft, char bracketRight, char otherBracketLeft, char otherBracketRight) => 
            $"{bracketLeft}{Key}{bracketRight}{Value}{otherBracketLeft}{ValueType.FullName}{otherBracketRight}";

        public string Key { get; private set; } = null;

        public object Value { get; set; } = null;

        public Type ValueType { get; private set; } = null;
        public override string ToString() => $"{this.Key}={this.Value}";

        public static SettingsKey GetKey(Type valueType, string key, object value) => new SettingsKey(valueType, key, value);
    }
}
