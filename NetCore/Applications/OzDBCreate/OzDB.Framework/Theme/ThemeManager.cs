namespace OzDB.Application.Theme {
	using System;
	using System.Collections.Generic;
	using System.IO;

	public class ThemeManager {
		public ThemeManager(string themesFileName)
			: this() {
			this.ThemesFileName = themesFileName;
			this.Load();
		}

		public ThemeManager() => this.Themes = new List<ApplicationTheme>();

		private void Load() {
			if (!File.Exists(this.ThemesFileName)) {
				throw new FileNotFoundException("Theme file missing", this.ThemesFileName);
			}
			try {
				this.Themes = new List<ApplicationTheme> {
					new ApplicationTheme(null) { Name = "Default" }
				};
				this.Themes.AddRange(ApplicationTheme.Create(this.ThemesFileName));
			}
			catch (Exception ex) {
				//log error
				throw ex;
			}
		}

		public void Load(string themesFileName) {
			this.ThemesFileName = themesFileName;
			this.Load();
		}


		public List<ApplicationTheme> Themes { get; private set; } = default;

		public string ThemesFileName { get; private set; } = default;
	}
}
