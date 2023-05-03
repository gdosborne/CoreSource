namespace AppSystem.ApplicationHelpers {
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using System.Xml.Linq;
	using AppSystem.Security;
	using Windows.Storage;

	public class PermissionManager {
		public enum PermissionTypes {
			NoAccess,
			Administrator,
			User
		}

		private static Dictionary<string, PermissionTypes> permissionsData = default;
		public PermissionTypes GetPermission(string username) {
			this.InitializeData().GetAwaiter();
			var lowerUsername = username.ToLower();
			if (permissionsData != null && permissionsData.ContainsKey(lowerUsername)) {
				return permissionsData[lowerUsername];
			} else if (permissionsData != null) {
				var permission = permissionsData.Any() ? PermissionTypes.User : PermissionTypes.Administrator;
				this.AddUser(username, permission);
				return permission;
			}
			return this.GetPermission(username);
		}

		private void AddUser(string username, PermissionTypes type) {
			var data = this.GetData();
			var doc = XDocument.Parse(data);
			var newUserXElement = new XElement("user",
				new XAttribute("name", username),
				new XAttribute("created", DateTime.Now),
				new XAttribute("permission", type));
			doc.Root.Add(newUserXElement);
			var roamingFolder = ApplicationData.Current.RoamingFolder;
			var file = default(StorageFile);
			try {
				file = roamingFolder.GetFileAsync(this.PermissionManagerFilename).AsTask().Result;
			}
			catch { }
			var crypto = new Crypto();
			var encData = crypto.Encrypt(doc.ToString(), "catfish1");
			FileIO.WriteTextAsync(file, encData).AsTask();
			permissionsData.Add(username, type);
		}

		private string GetData() {
			var roamingFolder = ApplicationData.Current.RoamingFolder;
			this.PermissionManagerFilename = $".permissions";

			var file = default(StorageFile);
			var isNewFile = false;
			try {
				file = roamingFolder.GetFileAsync(this.PermissionManagerFilename).AsTask().Result;
			}
			catch {
				if (file == null) {
					file = roamingFolder.CreateFileAsync(this.PermissionManagerFilename).AsTask().Result;
					isNewFile = true;
				}
			}
			var data = default(string);
			var crypto = new Crypto();
			if (!isNewFile) {
				data = FileIO.ReadTextAsync(file).AsTask().Result;
			} else {
				if (string.IsNullOrEmpty(data)) {
					var doc = new XDocument(
						new XElement("permissions"));
					data = crypto.Encrypt(doc.ToString(), "catfish1");
					FileIO.WriteTextAsync(file, data).AsTask();
				}

			}
			var realData = crypto.Decrypt(data, "catfish1");
			return realData;
		}

		private async Task InitializeData() {
			if (permissionsData != null) {
				return;
			}
			var realData = this.GetData();
			var perDoc = XDocument.Parse(realData);
			permissionsData = new Dictionary<string, PermissionTypes>();

			perDoc.Root.Elements().ToList().ForEach(x => {
				var uName = x.Attribute("name").Value;
				var permission = (PermissionTypes)Enum.Parse(typeof(PermissionTypes), x.Attribute("permission").Value, true);
				permissionsData.Add(uName, permission);
			});
		}

		public async void SetPermissionAsync(string username) {
			if (!this.IsSecurityEnabled) {
				this.Permission = PermissionTypes.Administrator;
			}
			await this.InitializeData();
			var lowerUsername = username.ToLower();
			if (permissionsData != null && permissionsData.ContainsKey(lowerUsername)) {
				this.Permission = permissionsData[lowerUsername];
				return;
			}
			this.Permission = PermissionTypes.NoAccess;
		}

		public PermissionManager(string username, bool isSecurityEnabled) {
			this.IsSecurityEnabled = isSecurityEnabled;
			if (!this.IsSecurityEnabled) {
				this.Permission = PermissionTypes.Administrator;
				return;
			}
			this.Username = username;
			this.SetPermissionAsync(username);
		}
		private string firstName = default;
		public string FirstName {
			get => this.firstName;
			set {
				this.firstName = value;
				this.FullName = $"{(!string.IsNullOrEmpty(this.FirstName) ? $"{this.FirstName} " : string.Empty)} {(!string.IsNullOrEmpty(this.LastName) ? this.LastName : string.Empty)}";
			}
		}
		private string lastName = default;
		public string LastName {
			get => this.lastName;
			set {
				this.lastName = value;
				this.FullName = $"{(!string.IsNullOrEmpty(this.FirstName) ? $"{this.FirstName} " : string.Empty)} {(!string.IsNullOrEmpty(this.LastName) ? this.LastName : string.Empty)}";
			}
		}
		public string FullName { get; set; }
		public string PermissionManagerFilename { get; private set; } = default;
		public string Username { get; private set; }
		public PermissionTypes Permission { get; set; }
		public bool IsSecurityEnabled { get; private set; }
	}
}


