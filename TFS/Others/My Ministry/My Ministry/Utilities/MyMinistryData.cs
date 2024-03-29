namespace MyMinistry.Utilities
{
	using Microsoft.Live;
	using System;
	using System.Collections.ObjectModel;
	using System.IO;
	using System.Linq;
	using System.Threading.Tasks;
	using System.Xml.Linq;
	using Windows.Storage;

	public class MyMinistryData
	{
		#region Public Constructors

		public MyMinistryData(string firstName, string lastName, MyMinistry.Utilities.Enumerations.CongregationAssignments assignment)
			: this(new MyMinistryUser { FirstName = firstName, LastName = lastName, Assignment = assignment })
		{
		}

		public MyMinistryData(MyMinistryUser user)
			: this()
		{
			User = user;
		}

		public MyMinistryData()
		{
			Entries = new ObservableCollection<MyMinistryEntry>();
			Contacts = new ObservableCollection<MyMinistryContact>();
		}

		#endregion Public Constructors

		#region Public Properties

		public ObservableCollection<MyMinistryContact> Contacts { get; private set; }
		public DateTime CreationDate { get; set; }

		public ObservableCollection<MyMinistryEntry> Entries { get; private set; }
		public MyMinistryUser User { get; set; }

		#endregion Public Properties

		#region Public Methods

		public static async Task<MyMinistryData> ReadInfo(string data)
		{
			return await MyMinistryData.Create(data);
		}

		public static async Task WriteInfo<T>(T data, string folderId, string fileName) where T : MyMinistryData
		{
			try
			{
				await ApplicationData.Current.LocalFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
			}
			catch (Exception ex)
			{
			}
		}

		public static async Task WriteInfo<T>(T data, string folderId, string fileName, LiveConnectClient client) where T : MyMinistryData
		{
			try
			{
				var tmpFile = await ApplicationData.Current.LocalFolder.CreateFileAsync("tmp.txt", CreationCollisionOption.ReplaceExisting);
				using (var writer = new StreamWriter(await tmpFile.OpenStreamForWriteAsync()))
				{
					await writer.WriteAsync(data.ToString());
				}
				var operationResult = await client.BackgroundUploadAsync(folderId, fileName, tmpFile, OverwriteOption.Overwrite);
			}
			catch (Exception ex)
			{
			}
		}

		public static async Task WriteInfo<T>(T data, string folderId, StorageFile file) where T : MyMinistryData
		{
			try
			{
				using (var writer = new StreamWriter(await file.OpenStreamForWriteAsync()))
				{
					await writer.WriteAsync(data.ToString());
				}
			}
			catch (Exception ex)
			{
			}
		}

		public void AddEntry(MyMinistryEntry entry)
		{
			Entries.Add(entry);
		}

		public void AddEntry(DateTime entryDateTime, TimeSpan? lengthOfTime, int? numberOfVideos, int? totalPlacements)
		{
			AddEntry(new MyMinistryEntry
			{
				EntryDateTime = entryDateTime,
				LengthOfTime = lengthOfTime,
				NumberOfVideos = numberOfVideos,
				TotalPlacements = totalPlacements
			});
		}

		public override string ToString()
		{
			XDocument doc = new XDocument();
			var root = new XElement("MyMinistry");
			var userElement = new XElement("UserInfo",
				new XAttribute("FirstName", User == null ? string.Empty : User.FirstName),
				new XAttribute("LastName", User == null ? string.Empty : User.LastName),
				new XAttribute("Assignment", User == null ? Enumerations.CongregationAssignments.Publisher : User.Assignment));
			var entriesRootElement = new XElement("Entries");
			var contactsRootElement = new XElement("Contacts");
			if (Contacts.Any())
			{
				foreach (var item in Contacts)
				{
					contactsRootElement.Add(item.ToXElement());
				}
			}
			root.Add(userElement);
			root.Add(entriesRootElement);
			root.Add(contactsRootElement);
			doc.Add(root);
			return doc.ToString();
		}

		#endregion Public Methods

		#region Private Methods

		private static async Task<MyMinistryData> Create(string data)
		{
			XDocument doc = XDocument.Parse(data);
			var result = new MyMinistryData();
			var u = new MyMinistryUser();
			if (doc.Root.Element("UserInfo") != null)
			{
				if (doc.Root.Element("UserInfo").Attribute("FirstName") != null)
					u.FirstName = doc.Root.Element("UserInfo").Attribute("FirstName").Value;
				if (doc.Root.Element("UserInfo").Attribute("LastName") != null)
					u.LastName = doc.Root.Element("UserInfo").Attribute("LastName").Value;
				if (doc.Root.Element("UserInfo").Attribute("Assignment") != null)
					u.Assignment = (MyMinistry.Utilities.Enumerations.CongregationAssignments)Enum.Parse(typeof(MyMinistry.Utilities.Enumerations.CongregationAssignments), doc.Root.Element("UserInfo").Attribute("Assignment").Value, true);
			}
			if (doc.Root.Element("Entries") != null)
			{
				foreach (var item in doc.Root.Element("Entries").Elements())
				{
				}
			}
			if (doc.Root.Element("Contacts") != null)
			{
				foreach (var item in doc.Root.Element("Contacts").Elements())
				{
					result.Contacts.Add(MyMinistryContact.FromXElement(item));
				}
			}
			result.User = u;
			return result;
		}

		#endregion Private Methods
	}
}
