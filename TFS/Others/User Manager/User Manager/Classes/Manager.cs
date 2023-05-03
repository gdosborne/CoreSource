// ***********************************************************************
// Assembly         : User Manager
// Author           : Greg
// Created          : 06-15-2015
//
// Last Modified By : Greg
// Last Modified On : 06-26-2015
// ***********************************************************************
// <copyright file="Manager.cs" company="Statistics and Controls, Inc.">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Xml.Linq;
using SNC.Authorization;
using SNC.Authorization.Management;
using System.Linq;
using User_Manager.Classes.Exceptions;

namespace User_Manager.Classes
{
	public class Manager : PermissionManager
	{
		#region Private Fields
		private string _Contents = null;
		private string _FileName = null;
		#endregion

		#region Public Constructors

		public Manager()
		{
			base.ItemAdded += Manager_ItemAdded;
		}

		public Manager(string xmlData)
			: base(xmlData) { }

		public Manager(XElement dataElement)
			: base(dataElement) { }

		#endregion

		#region Public Methods

		public static XElement GetNewFileElement(Manager manager)
		{
			manager.CustomNames = ApplicationSettings.GetCustomNames();
			var result = new XElement(manager.RootName,
				new XElement(manager.GroupName,
					new XElement(manager.ItemName,
						new XAttribute(manager.ItemTypeAttributeName, "User"),
						new XAttribute(manager.ItemNameAttributeName, "admin"),
						new XAttribute(manager.ItemFirstNameAttributeName, "System"),
						new XAttribute(manager.ItemLastNameAttributeName, "Administrator"),
						new XAttribute(manager.ItemPasswordAttributeName, "Password1234"),
						new XElement(manager.UserPermissionsName,
							new XElement(manager.UserPermissionName,
								new XAttribute(manager.UserValueAttributeName, Convert.ToInt64(AuthorizationDefaults.AllRights)))))));
			return result;
		}

		public override Task LoadFileAsync(string fileName)
		{
			_FileName = fileName;
			return new Task(GetFileData);
		}

		public override Task SaveFileAsync(string fileName, string contents)
		{
			return SaveFileAsync(fileName, contents, false);
		}

		#endregion

		#region Private Methods

		public XDocument OriginalDocument { get; set; }
		public XElement OriginalXElement { get; set; }
		private void GetFileData()
		{
			App.WriteEventMessage(string.Format("Reading security data from {0}", _FileName));
			ValidateUsersFile(_FileName);
			var fInfo = new FileInfo(_FileName);
			if (fInfo.Extension.Equals(".xml"))
			{
				OriginalDocument = XDocument.Load(_FileName);
				var metaElement = OriginalDocument.Descendants("permission_metadata");
				var rootName = RootName;
				if (metaElement != null)
					rootName = metaElement.Elements().FirstOrDefault(x => x.Attribute("key").Value == "Root").Attribute("value").Value;

				OriginalXElement = OriginalDocument.Descendants(rootName).FirstOrDefault();
				if (OriginalXElement != null)
					base.Load(OriginalXElement);
				else
				{
					OriginalXElement = null;
					OriginalDocument = null;
					throw new PermissionRootElementMissingException(string.Format("Root element {0} is missing", rootName));
				}
				return;
			}
			var tempPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "temp");
			if (!Directory.Exists(tempPath))
				Directory.CreateDirectory(tempPath);
			var cmd = new SNC.OptiRamp.Services.fSncZip.GetFileFromZipCommand();
			var parameters = new Dictionary<string, object>
				{
					{"ZipFile", _FileName},
					{"FileName", "users.xml"},
					{"LeftShift", "SAPPY_pappy"},
					{"TempPath", tempPath}
				};
			cmd.Execute(parameters);
			var result = XDocument.Load((string)cmd.Value);
			File.Delete((string)cmd.Value);
			var data = result.Root.ToString();
			base.Parse(data);
		}

		private void Manager_ItemAdded(object sender, ItemAddedEventArgs e)
		{
		}

		private XElement MetaElement()
		{
			var result = new XElement("permission_metadata");
			foreach (var key in CustomNames.Keys)
			{
				result.Add(new XElement("customname",
					new XAttribute("key", key),
					new XAttribute("value", CustomNames[key])));
			}
			return result;
		}

		private void SaveUsersDocument(XDocument doc, string fileName)
		{
			App.WriteEventMessage(string.Format("Saving security data to {0}", fileName));
			var fInfo = new FileInfo(fileName);
			if (fInfo.Extension.Equals(".xml"))
			{
				doc.Save(fileName);
				return;
			}
			var tempPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "temp");
			if (!Directory.Exists(tempPath))
				Directory.CreateDirectory(tempPath);
			var tempFile = Path.Combine(tempPath, "users.xml");
			doc.Save(tempFile);
			var cmd = new SNC.OptiRamp.Services.fSncZip.ZipFileCommand();
			var parameters = new Dictionary<string, object>
				{
					{"FileName", tempFile},
					{"ZipFileName", fileName},
					{"IgnoreController", true},
					{"HideFileName", false},
					{"LeftShift", "SAPPY_pappy"}
				};
			cmd.Execute(parameters);
			File.Delete(tempFile);
		}

		private void SetFileData()
		{
			if (_Contents == null)
				_Contents = GetNewFileElement(this).ToString();
			if (!_IsWebFile)
			{
				var element = XElement.Parse(_Contents);
				element.Add(MetaElement());
				OriginalXElement.ReplaceWith(element);
				SaveUsersDocument(OriginalDocument, _FileName);
			}
			else
			{
				var doc = XDocument.Parse(_Contents);
				doc.Root.Add(MetaElement());
				App.WriteEventMessage(string.Format("Saving security data to {0}", _FileName));
				EndpointAddress endpointAdress = new EndpointAddress(_FileName);
				BasicHttpBinding binding1 = new BasicHttpBinding();
				var client = new SecurityService.SecurityServiceClient(ApplicationSettings.ServiceName, endpointAdress);
				client.UpdateSecurityData(doc.ToString());
			}
		}

		private void ValidateUsersFile(string fileName)
		{
			if (!File.Exists(fileName))
			{
				var doc = new XDocument(GetNewFileElement(this));
				doc.Root.Add(MetaElement());
				SaveUsersDocument(doc, fileName);
			}
		}

		#endregion
		private bool _IsWebFile;
		public override Task SaveFileAsync(string fileName, string contents, bool isWebFile)
		{
			_FileName = fileName;
			_Contents = contents;
			_IsWebFile = isWebFile;
			return new Task(SetFileData);
		}
	}
}
