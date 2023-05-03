namespace GregOsborne.Developers.Suite.Configuration {
	using System;
	using System.Collections.ObjectModel;
	using System.IO;
	using System.Xml.Linq;
	using GregOsborne.Application.Primitives;

	public class ConfigFile : Changeable {
		private ConfigFile(string fileName) {
			this.Programs = new ObservableCollection<Program>();
			this.FileName = fileName;
		}

		public static ConfigFile CreateNew(string fileName = "default.devsuite") {
			var directory = Path.GetDirectoryName(fileName);
			var file = Path.GetFileName(fileName);
			if (string.IsNullOrEmpty(directory)) {
				directory = App.Current.As<App>().ApplicationDirectory;
			}
			var dt = DateTime.Now;
			var result = new ConfigFile(Path.Combine(directory, file)) {
				CreatedDate = dt,
				LastDateSaved = dt
			};
			result.Save();
			return result;
		}

		private string fileName = default;
		public string FileName {
			get => this.fileName;
			set {
				this.fileName = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}


		private DateTime createdDate = default;
		public DateTime CreatedDate {
			get => this.createdDate;
			set {
				this.createdDate = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		private DateTime lastDateSaved = default;
		public DateTime LastDateSaved {
			get => this.lastDateSaved;
			set {
				this.lastDateSaved = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		private Version version = default;
		public Version Version {
			get => this.version;
			set {
				this.version = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		private ObservableCollection<Program> programs = default;
		public ObservableCollection<Program> Programs {
			get => this.programs;
			set {
				this.programs = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public override XElement ToXElement() {
			var result = new XElement("ConfigFile",
				new XAttribute("created", this.CreatedDate),
				new XAttribute("lastdatesaved", DateTime.Now),
				new XAttribute("version", this.Version));
			var programsXElement = new XElement("programs");

			foreach (var program in this.Programs) {
				programsXElement.Add(program.ToXElement());
			}

			result.Add(programsXElement);
			return result;
		}

		public void Save() {
			var doc = new XDocument();
			doc.Add(this.ToXElement());
			doc.Save(this.FileName);
			this.IsSaveRequired = false;
		}

		public void Save(string fileName) {
			this.FileName = fileName;
			this.Save();
		}

		private bool isSaveRequired = default;
		public bool IsSaveRequired {
			get => this.isSaveRequired;
			set {
				this.isSaveRequired = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public static ConfigFile Open(string filename) {
			var doc = XDocument.Load(filename);
			var result = new ConfigFile(filename) {
				Version = Version.Parse(doc.Root.Attribute("version").Value),
				CreatedDate = DateTime.Parse(doc.Root.Attribute("created").Value),
				LastDateSaved = DateTime.Parse(doc.Root.Attribute("lastdatesaved").Value)
			};
			foreach (var element in doc.Root.Elements()) {
				if (element.Name.LocalName == "programs") {
					foreach (var e in element.Elements()) {
						result.Programs.Add(Program.FromXElement(e));
					}
				}
			}
			return result;
		}
	}
}
