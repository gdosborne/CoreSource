namespace Greg.Osborne.Installer.Support {
	using System;
	using System.Linq;
	using System.Windows.Input;

	public class ControlIcon {
		public ControlIcon(ICommand command) => this.ItemCommand = command;
		public ControlIcon(string hyperlinkUrl) => this.HyperlinkUri = new Uri(hyperlinkUrl);
		public ControlIcon(Uri hyperlinkUri) => this.HyperlinkUri = hyperlinkUri;
		public string Tooltip { get; set; }
		public Uri HyperlinkUri { get; set; }
		public ICommand ItemCommand { get; private set; }
		public int SegoeMDL2AssetsCharacterIndex { get; private set; }
		public char SegoeMDL2AssetsCharacter { get; private set; }
		private string iconHexValue = default;
		public string IconHexValue {
			get => this.iconHexValue;
			set {
				this.iconHexValue = value;
				var disallowdCharacters = new char[] {
					';', '&', '#', 'x'
				};
				var remaining = new string(value.ToCharArray().Except(disallowdCharacters).ToArray());
				this.SegoeMDL2AssetsCharacterIndex = Convert.ToInt32(remaining, 16);
				this.SegoeMDL2AssetsCharacter = (char)this.SegoeMDL2AssetsCharacterIndex;
			}
		}
	}
}
