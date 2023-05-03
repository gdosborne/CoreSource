using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace GregOsborne.Application.Symbol {
    public enum ValueTypes {
        Symbol,
        Code,
        Xaml
    }
    public static class Extensions {
        public static string GetGlyphCharacter(this int character, ValueTypes type) {
            var result = string.Empty;
            try
            {
                switch (type)
                {
                    case ValueTypes.Symbol:
                        result = Char.ConvertFromUtf32(character);
                        break;
                    case ValueTypes.Code:
                        result = $"\\u{character.ToString("X")}";
                        break;
                    case ValueTypes.Xaml:
                        result = $"&#x{character.ToString("X")};";
                        break;
                }
            }
            catch (System.Exception ex)
            {
                throw new ApplicationException($"Cannot convert the value {character}", ex);
            }
            return result;
        }
    }
}
