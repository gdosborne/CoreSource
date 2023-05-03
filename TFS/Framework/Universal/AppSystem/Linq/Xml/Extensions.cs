namespace AppSystem.Linq.Xml {
    using System.Xml.Linq;
    using AppSystem.Primitives;

    public static class Extensions {
        public static T GetValue<T>(this XElement element, T defaultValue = default) {
            var result = defaultValue;
            if (!element.HasValue())
                return result;
            result = element.Value.CastTo<T>(defaultValue);
            return result;
        }

        public static bool HasValue(this XElement element) {
            return element != null && !string.IsNullOrEmpty(element.Value);
        }
    }
}
