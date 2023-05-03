using System;

namespace Common.AppFramework.Symbol {
    public enum ValueTypes {
        Symbol,
        Code,
        Xaml
    }
    public static class Extensions {
        public static string GetGlyphCharacter(this int character, ValueTypes type) {
            var result = string.Empty;
            try {
                switch (type) {
                    case ValueTypes.Symbol:
                        result = char.ConvertFromUtf32(character);
                        break;
                    case ValueTypes.Code:
                        result = $"\\u{character:X}";
                        break;
                    case ValueTypes.Xaml:
                        result = $"&#x{character:X};";
                        break;
                }
            }
            catch (System.Exception ex) {
                throw new ApplicationException($"Cannot convert the value {character}", ex);
            }
            return result;
        }
    }
}
