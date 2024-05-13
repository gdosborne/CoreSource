/* File="Extensions"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

using System;

namespace OzFramework.Symbol {
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
