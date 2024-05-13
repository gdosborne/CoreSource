namespace OzDB.Application.Symbol {
	using System;

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
						result = $"\\u{character.ToString("X")}";
						break;
					case ValueTypes.Xaml:
						result = $"&#x{character.ToString("X")};";
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
