namespace OSInstallerExtensibility.Classes.Managers
{
	using GregOsborne.Application.Media;
    using OSInstallerCommands;
	using OSInstallerExtensibility.Classes;
	using OSInstallerExtensibility.Classes.Data;
	using OSInstallerExtensibility.Classes.Install;
	using OSInstallerExtensibility.Events;
	using OSInstallerExtensibility.Interfaces;
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.IO;
	using System.Linq;
	using System.Reflection;
	using System.Threading.Tasks;
	using System.Windows.Media;
	using System.Xml.Linq;

	public class Manager : IInstallerManager
	{
		#region Public Constructors
		public Manager()
		{
			Properties = new List<IInstallerProperty>();
			Datum = new List<IInstallerData>();
			Steps = new List<IInstallerWizardStep>();
			Commands = new List<BaseCommand>();
			Extensions = new List<IInstallerExtension>();
			Items = new List<IInstallerItem>();
			PreprocessingItems = new List<IInstallerItem>();
			GeneratedTempFiles = new List<string>();
			RequiredDataNames = new List<string>
			{
				"CompanyName",
				"ApplicationName",
				"InstallationTitle",
				"InstallationDir",
				"CancelInstallTitle",
				"CancelInstallMessage",
				"CancelInstallAdditionalMessage",
				"ProgramFiles",
				"ProgramFilesX86",
				"RoamingAppDataFolder",
				"LocalAppDataFolder",
				"CommonAppDataFolder",
				"CommonDesktopFolder",
				"PublicDocumentsFolder",
				"DesktopFolder",
				"FontsFolder",
				"PersonalDocumentsFolder",
				"StartMenuFolder",
				"SystemFolder",
				"SystemX86Folder",
				"WindowsFolder",
				"MachineName",
			};
			VariableTrigger = '$';
			AllowSilentInstall = true;
			InstallerFileNameVersion = new Version(1, 0);
			Status = CommandStatuses.Success;
		}
		public Manager(IList<IInstallerProperty> properties, IList<IInstallerData> datum, IList<IInstallerWizardStep> steps, IList<BaseCommand> commands, IList<IInstallerExtension> extensions, IList<IInstallerItem> files)
			: this()
		{
			Properties = properties;
			Datum = datum;
			Steps = steps;
			Commands = commands;
			Extensions = extensions;
			Items = files;
		}
		#endregion Public Constructors

		#region Public Methods
		public void AddData(IInstallerData data)
		{
			data.PropertyChanged += property_PropertyChanged;
			Datum.Add(data);
		}
		public void AddProperty(IInstallerProperty property)
		{
			property.PropertyChanged += property_PropertyChanged;
			Properties.Add(property);
		}
		public void AddStep(IInstallerWizardStep step)
		{
			Steps.Add(step);
		}
		public void CreateNew(string fileName)
		{
			FileName = fileName;
			Datum.Add(new InstallerData("FileName") { Value = FileName, IsEditable = true, IsRequired = true, IsStepData = false, MustValidate = false });
			LoadExtensions();
			InitializeSystemData();
			var logFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ExpandVariable("CompanyName"));
			if (!Directory.Exists(logFolder))
				Directory.CreateDirectory(logFolder);
			logFolder = Path.Combine(logFolder, ExpandVariable("ApplicationName"));
			if (!Directory.Exists(logFolder))
				Directory.CreateDirectory(logFolder);

			LogFileName = Path.Combine(logFolder, DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss") + ".log");

			DefaultProperties();
			ValidateVisualProperties();
		}

		public void DisplayStep(IInstallerWizardStep step)
		{
			WriteToLog(string.Format("Displaying step: {0}", step.Id));
			StepNumber = step.Sequence;
			StepChanged(this, new StepChangedEventArgs(step));
			step.Initialize();
		}
		public string ExpandString(string source)
		{
			return source.ExpandString(Datum, VariableTrigger);
		}
		public string ExpandVariable(string stepId, string name)
		{
			if (string.IsNullOrEmpty(stepId) || string.IsNullOrEmpty(name) || !Datum.Any(x => x.Name.Equals(stepId + "." + name)))
				return string.Empty;
			return ExpandString(Datum.First(x => x.Name.Equals(stepId + "." + name)).Value);
		}
		public string ExpandVariable(string name)
		{
			if (string.IsNullOrEmpty(name) || !Datum.Any(x => x.Name.Equals(name)))
				return string.Empty;
			return ExpandString(Datum.First(x => x.Name.Equals(name)).Value);
		}
		public IInstallerComplete GetCompleteStep()
		{
			return (IInstallerComplete)Steps.FirstOrDefault(x => x.GetType().GetInterfaces().Contains(typeof(IInstallerComplete)));
		}
		public ImageSource GetImageSourceFromResource(string assemblyName, string resourceName)
		{
			return Helpers.GetImageSourceFromResource(assemblyName, resourceName);
		}
		public IInstallerWizardStep GetRevertStep()
		{
			return Steps.FirstOrDefault(x => x.GetType().GetInterfaces().Contains(typeof(IInstallerRevertStep)));
		}
		public void GoToNextStep()
		{
			var nextStep = Steps
				.OrderBy(x => x.Sequence)
				.Where(x => x.Sequence > StepNumber && !x.GetType().GetInterfaces().Contains(typeof(IInstallerRevertStep)))
				.FirstOrDefault();
			if (nextStep != null && StepChanged != null)
			{
				DisplayStep(nextStep);
				if (nextStep.IsInstallationStep)
				{
					StartInstallation(nextStep);
				}
			}
		}
		public void GoToPreviousStep()
		{
			var previousStep = Steps
				.OrderBy(x => x.Sequence)
				.Where(x => x.Sequence < StepNumber && !x.GetType().GetInterfaces().Contains(typeof(IInstallerRevertStep)))
				.LastOrDefault();
			if (previousStep != null && StepChanged != null)
				DisplayStep(previousStep);
		}
		public void Load(string fileName)
		{
			FileName = fileName;
			if (!File.Exists(FileName))
				throw new FileNotFoundException("Could not find installation definition file", FileName);
			LoadExtensions();
			var installerDoc = XDocument.Load(fileName);
			var root = installerDoc.Root;
			var dataRoot = root.Element("data");
			InitializeSystemData();
			var preItemsRoot = root.Element("preprocessingitems");
			if (preItemsRoot != null)
			{
				var items = preItemsRoot.Elements("add");
				foreach (var item in items)
				{
					var f = new InstallerItem(item.Attribute("id").Value)
					{
						Path = item.Attribute("originalpath").Value,
						ItemType = (ItemTypes)Enum.Parse(typeof(ItemTypes), item.Attribute("type").Value, true)
					};
					if (f.ItemType == ItemTypes.Folder && item.Attribute("includesubfolders") != null)
						f.IncludeSubFolders = bool.Parse(item.Attribute("includesubfolders").Value);
					PreprocessingItems.Add(f);
				}
			}
			if (dataRoot != null)
			{
				if (dataRoot.Attribute("variabletrigger") != null)
					VariableTrigger = dataRoot.Attribute("variabletrigger").Value.ToCharArray()[0];
				if (dataRoot.Attribute("allowsilentinstall") != null)
					AllowSilentInstall = bool.Parse(dataRoot.Attribute("allowsilentinstall").Value);
				var items = dataRoot.Elements("add");
				foreach (var item in items)
				{
					AddData(new InstallerData(item.Attribute("name").Value)
					{
						Value = item.Attribute("value").Value,
						IsEditable = true,
						MustValidate = bool.Parse(item.Attribute("mustvalidate").Value),
						IsRequired = RequiredDataNames.Contains(item.Attribute("name").Value)
					});
				}
			}
			var logFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ExpandVariable("CompanyName"));
			if (!Directory.Exists(logFolder))
				Directory.CreateDirectory(logFolder);
			logFolder = Path.Combine(logFolder, ExpandVariable("ApplicationName"));
			if (!Directory.Exists(logFolder))
				Directory.CreateDirectory(logFolder);

			LogFileName = Path.Combine(logFolder, DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss") + ".log");
			var propertiesRoot = root.Element("properties");
			if (propertiesRoot != null)
			{
				var items = propertiesRoot.Elements("add");
				foreach (var item in items)
				{
					var type = item.Attribute("type").Value.ToLower();
					switch (type)
					{
						case "color":
							AddProperty(new InstallerProperty(item.Attribute("name").Value) { Value = item.Attribute("value").Value.ToColor() });
							break;
						case "string":
							if (item.Attribute("name").Value.Equals("ImagePath"))
							{
								if (PreprocessingItems.Any(x => x.Name.Equals(item.Attribute("value").Value)))
									AddProperty(new InstallerProperty(item.Attribute("name").Value) { Value = PreprocessingItems.First(x => x.Name.Equals(item.Attribute("value").Value)).Path });
							}
							else
								AddProperty(new InstallerProperty(item.Attribute("name").Value) { Value = item.Attribute("value").Value });
							break;
					}
				}
			}
			ValidateVisualProperties();
			var stepsRoot = root.Element("steps");
			if (stepsRoot != null)
			{
				var items = stepsRoot.Elements("add");
				foreach (var item in items)
				{
					IInstallerWizardStep step = GetStep(item);
					if (step == null)
						continue;
					AddStep(step);
				}
			}
			var commandsRoot = root.Element("commands");
			if (commandsRoot != null)
			{
				var items = commandsRoot.Elements("add");
				foreach (var item in items)
				{
					var cmd = GetCommand(item);
					if (cmd == null)
						continue;
					Commands.Add(cmd);
				}
			}
			var itemsRoot = root.Element("items");
			if (itemsRoot != null)
			{
				var items = itemsRoot.Elements("add");
				foreach (var item in items)
				{
					var f = new InstallerItem(item.Attribute("id").Value)
					{
						Path = item.Attribute("originalpath").Value,
						ItemType = (ItemTypes)Enum.Parse(typeof(ItemTypes), item.Attribute("type").Value, true)
					};
					if (f.ItemType == ItemTypes.Folder && item.Attribute("includesubfolders") != null)
						f.IncludeSubFolders = bool.Parse(item.Attribute("includesubfolders").Value);
					Items.Add(f);
				}
			}
			IsDirty = false;
			if (LoadComplete != null)
				LoadComplete(this, EventArgs.Empty);
		}
		public void Save()
		{
			if (VariableTrigger == 0)
				return;
			var doc = new XDocument(
				new XElement("installation",
					new XAttribute("fileversion", InstallerFileNameVersion.ToString(2))));
			var dataElement = new XElement("data",
				new XAttribute("variabletrigger", VariableTrigger.ToString()),
				new XAttribute("allowsilentinstall", AllowSilentInstall.ToString()));
			//remove dups
			var names = Datum.Select(x => x.Name).Distinct().ToList();
			var temp = new List<IInstallerData>();
			names.ForEach(x => temp.Add(Datum.First(y => y.Name.Equals(x))));
			Datum = temp;
			//----
			Datum.Where(x => x.IsEditable && !x.IsStepData).ToList().ForEach(x =>
			{
				dataElement.Add(new XElement("add",
					new XAttribute("name", x.Name),
					new XAttribute("value", x.Value),
					new XAttribute("mustvalidate", x.MustValidate)));
			});
			doc.Root.Add(dataElement);
			var propertiesElement = new XElement("properties");
			Properties.ToList().ForEach(x =>
			{
				string value = string.Empty;
				if (x.Value == null)
					return;
				if (x.Value.GetType() == typeof(Color))
					value = ((Color)x.Value).ToHexValue();
				else if (x.Value.GetType() == typeof(string))
					value = (string)x.Value;

				propertiesElement.Add(new XElement("add",
					new XAttribute("name", x.Name),
					new XAttribute("type", x.Value.GetType().Name),
					new XAttribute("value", value)));
			});
			doc.Root.Add(propertiesElement);
			var stepsElement = new XElement("steps");
			Steps.ToList().ForEach(x =>
			{
				var stepElement = new XElement("add",
					new XAttribute("sequence", x.Sequence.ToString()),
					new XAttribute("id", x.Id),
					new XAttribute("type", x.GetType().FullName),
					new XAttribute("installstep", x.IsInstallationStep.ToString()));
				var stepDataElement = new XElement("data");
				Datum.Where(y => y.IsStepData && y.Name.StartsWith(string.Format("{0}.", x.Id))).ToList().ForEach(y =>
				{
					stepDataElement.Add(new XElement("add",
						new XAttribute("name", y.Name.Replace(string.Format("{0}.", x.Id), string.Empty)),
						new XAttribute("value", y.Value)));
				});
				stepElement.Add(stepDataElement);
				stepsElement.Add(stepElement);
			});
			doc.Root.Add(stepsElement);
			var preItemsElement = new XElement("preprocessingitems");
			PreprocessingItems.ToList().ForEach(x =>
			{
				preItemsElement.Add(new XElement("add",
					new XAttribute("id", x.Name),
					new XAttribute("originalpath", x.Path),
					new XAttribute("type", x.ItemType.ToString()),
					new XAttribute("includesubfolders", x.IncludeSubFolders.ToString())));
			});
			doc.Root.Add(preItemsElement);
			var itemsElement = new XElement("items");
			Items.ToList().ForEach(x =>
			{
				itemsElement.Add(new XElement("add",
					new XAttribute("id", x.Name),
					new XAttribute("originalpath", x.Path),
					new XAttribute("type", x.ItemType.ToString()),
					new XAttribute("includesubfolders", x.IncludeSubFolders.ToString())));
			});
			doc.Root.Add(itemsElement);
			SaveCommands(doc.Root, Commands);
			doc.Save(FileName);
			IsDirty = false;
		}
		public void Save(string fileName)
		{
			FileName = fileName;
			Save();
		}
		public void StartInstallation(IInstallerWizardStep installStep)
		{
			try
			{
				var worker = new InstallWorker(installStep);
				worker.InstallationStarted += worker_InstallationStarted;
				worker.InstallComplete += worker_InstallComplete;
				worker.InstallerCommandExecuting += worker_InstallerCommandExecuting;
				var t = Task.Factory.StartNew(new Action(worker.Start));
			}
			catch (Exception ex)
			{
				throw;
			}
		}
		public void StartWizard()
		{
			StepNumber = Steps.Min(x => x.Sequence);
			if (StepChanged != null)
				StepChanged(this, new StepChangedEventArgs(Steps.FirstOrDefault(x => x.Sequence == StepNumber)));
		}
		public void WriteToLog(string message)
		{
			using (var fs = new FileStream(LogFileName, FileMode.Append, FileAccess.Write))
			using (var sw = new StreamWriter(fs))
			{
				sw.WriteLine(message);
			}
		}
		#endregion Public Methods

		#region Private Methods
		private void DefaultProperties()
		{
			Properties.Add(new InstallerProperty("TitleBackground") { Value = Colors.Navy });
			Properties.Add(new InstallerProperty("TitleForeground") { Value = Colors.LightGray });
			Properties.Add(new InstallerProperty("WindowBackground") { Value = Colors.LightGray });
			Properties.Add(new InstallerProperty("WindowText") { Value = Colors.Black });
			Properties.Add(new InstallerProperty("AreaSeparator") { Value = Colors.Black });
		}
		private BaseCommand GetCommand(XElement element)
		{
			if (string.IsNullOrEmpty(element.Attribute("type").Value))
				return null;
			Type t = GetCommandType(element.Attribute("type").Value);
			if (t == null)
				return null;
			var sequence = int.Parse(element.Attribute("sequence").Value);
			var message = element.Attribute("message").Value;
			var cmd = (BaseCommand)Activator.CreateInstance(t);
			cmd.Parameters = new Dictionary<string, object>();
			cmd.Sequence = sequence;
			cmd.Message = message;
			if (element.Element("parameters") != null)
			{
				var items = element.Element("parameters").Elements("add");
				foreach (var item in items)
				{
					cmd.Parameters.Add(item.Attribute("name").Value, item.Attribute("value").Value);
				}
			}
			if (element.Element("commands") != null)
			{
				var items1 = element.Element("commands").Elements("add");
				foreach (var item in items1)
				{
					var cmd1 = GetCommand(item);
					if (cmd1 == null)
						continue;
					cmd.Commands.Add(cmd1);
				}
			}
			return cmd;
		}
		private Type GetCommandType(string name)
		{
			foreach (var assy in AppDomain.CurrentDomain.GetAssemblies())
			{
				foreach (var type in assy.GetTypes())
				{
					if (type.FullName.Equals(name))
						return type;
				}
			}
			return null;
		}
		private IInstallerWizardStep GetStep(XElement item)
		{
			Type t = GetStepType(item.Attribute("type").Value);
			if (t == null)
				return null;
			var id = item.Attribute("id").Value;
			var stepDataRoot = item.Element("data");
			if (stepDataRoot != null)
			{
				var dataItems = stepDataRoot.Elements("add");
				foreach (var item1 in dataItems)
				{
					AddData(new InstallerData(id + "." + item1.Attribute("name").Value) { Value = item1.Attribute("value").Value, IsEditable = true, IsStepData = true });
				}
			}
			IInstallerWizardStep step = (IInstallerWizardStep)Activator.CreateInstance(t);
			step.Sequence = int.Parse(item.Attribute("sequence").Value);
			step.Id = id;
			step.IsInstallationStep = bool.Parse(item.Attribute("installstep").Value);
			return step;
		}
		private Type GetStepType(string name)
		{
			foreach (var item in Extensions)
			{
				foreach (var item1 in item.Assembly.GetTypes())
				{
					var thisName = item1.FullName;
					if (thisName.Equals(name))
						return item1;
				}
			}
			return null;
		}
		private void InitializeSystemData()
		{
			AddData(new InstallerData("ProgramFiles") { Value = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), IsEditable = false, IsRequired = false, IsStepData = false, MustValidate = false });
			AddData(new InstallerData("ProgramFilesX86") { Value = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), IsEditable = false, IsRequired = false, IsStepData = false, MustValidate = false });
			AddData(new InstallerData("RoamingAppDataFolder") { Value = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), IsEditable = false, IsRequired = false, IsStepData = false, MustValidate = false });
			AddData(new InstallerData("LocalAppDataFolder") { Value = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), IsEditable = false, IsRequired = false, IsStepData = false, MustValidate = false });
			AddData(new InstallerData("CommonAppDataFolder") { Value = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), IsEditable = false, IsRequired = false, IsStepData = false, MustValidate = false });
			AddData(new InstallerData("CommonDesktopFolder") { Value = Environment.GetFolderPath(Environment.SpecialFolder.CommonDesktopDirectory), IsEditable = false, IsRequired = false, IsStepData = false, MustValidate = false });
			AddData(new InstallerData("PublicDocumentsFolder") { Value = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments), IsEditable = false, IsRequired = false, IsStepData = false, MustValidate = false });
			AddData(new InstallerData("DesktopFolder") { Value = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), IsEditable = false, IsRequired = false, IsStepData = false, MustValidate = false });
			AddData(new InstallerData("FontsFolder") { Value = Environment.GetFolderPath(Environment.SpecialFolder.Fonts), IsEditable = false, IsRequired = false, IsStepData = false, MustValidate = false });
			AddData(new InstallerData("PersonalDocumentsFolder") { Value = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), IsEditable = false, IsRequired = false, IsStepData = false, MustValidate = false });
			AddData(new InstallerData("StartMenuFolder") { Value = Environment.GetFolderPath(Environment.SpecialFolder.StartMenu), IsEditable = false, IsRequired = false, IsStepData = false, MustValidate = false });
			AddData(new InstallerData("SystemFolder") { Value = Environment.GetFolderPath(Environment.SpecialFolder.System), IsEditable = false, IsRequired = false, IsStepData = false, MustValidate = false });
			AddData(new InstallerData("SystemX86Folder") { Value = Environment.GetFolderPath(Environment.SpecialFolder.SystemX86), IsEditable = false, IsRequired = false, IsStepData = false, MustValidate = false });
			AddData(new InstallerData("WindowsFolder") { Value = Environment.GetFolderPath(Environment.SpecialFolder.Windows), IsEditable = false, IsRequired = false, IsStepData = false, MustValidate = false });
			AddData(new InstallerData("MachineName") { Value = Environment.MachineName });
		}
		private void LoadExtensions()
		{
			var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			if (Directory.Exists(path))
			{
				LoadExtensionsFromDirectory(path);
			}
		}
		private void LoadExtensionsFromDirectory(string path)
		{
			LoadExtensionsFromDirectory(new DirectoryInfo(path));
		}
		private void LoadExtensionsFromDirectory(DirectoryInfo dInfo)
		{
			var dllFiles = dInfo.GetFiles("*.dll").ToList();
			var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
			if (dllFiles.Any())
			{
				dllFiles.ForEach(x =>
				{
					Assembly assy = null;
					if (loadedAssemblies.Any(y => y.Location.Equals(x.FullName, StringComparison.OrdinalIgnoreCase)))
						assy = loadedAssemblies.First(y => y.Location.Equals(x.FullName, StringComparison.OrdinalIgnoreCase));
					else
						assy = Assembly.LoadFile(x.FullName);
					var types = assy.GetTypes().ToList();
					types.ForEach(y =>
					{
						if (y.GetInterfaces().Contains(typeof(IInstallerWizardStep)) && y != typeof(InstallerStep))
						{
							if (!Extensions.Any(z => z.Path.Equals(x.FullName)))
							{
								Extensions.Add(new InstallerExtension(x.FullName, assy));
							}
							return;
						}
					});
				});
			}
			dInfo.GetDirectories().ToList().ForEach(x => LoadExtensionsFromDirectory(x));
		}
		private void property_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			IsDirty = true;
		}
		private void SaveCommands(XElement root, IList<BaseCommand> commands)
		{
			if (!commands.Any())
				return;
			var commandsElement = new XElement("commands");
			commands.ToList().ForEach(x =>
			{
				var commandElement = new XElement("add",
					new XAttribute("sequence", x.Sequence.ToString()),
					new XAttribute("type", x.GetType().FullName),
					new XAttribute("message", x.Message));
				var paramsElement = new XElement("parameters");
				x.Parameters.ToList().ForEach(y =>
				{
					paramsElement.Add(new XElement("add",
						new XAttribute("name", y.Key),
						new XAttribute("value", y.Value)));
				});
				SaveCommands(commandElement, x.Commands);
				commandElement.Add(paramsElement);
				commandsElement.Add(commandElement);
			});
			root.Add(commandsElement);
		}
		private void ValidateVisualProperties()
		{
			if (!Properties.Any(x => x.Name.Equals("TitleBackground")))
				Properties.Add(new InstallerProperty("TitleBackground") { Value = Colors.LightBlue });
			if (!Properties.Any(x => x.Name.Equals("TitleForeground")))
				Properties.Add(new InstallerProperty("TitleForeground") { Value = Colors.DarkBlue });
			if (!Properties.Any(x => x.Name.Equals("WindowBackground")))
				Properties.Add(new InstallerProperty("WindowBackground") { Value = Colors.White });
			if (!Properties.Any(x => x.Name.Equals("WindowText")))
				Properties.Add(new InstallerProperty("WindowText") { Value = Colors.Black });
			if (!Properties.Any(x => x.Name.Equals("AreaSeparator")))
				Properties.Add(new InstallerProperty("AreaSeparator") { Value = Colors.DarkGray });
			if (!Properties.Any(x => x.Name.Equals("ImagePath")))
				Properties.Add(new InstallerProperty("ImagePath") { Value = null });
		}
		private void worker_InstallationStarted(object sender, EventArgs e)
		{
			IsInstallationInProgress = true;
			if (InstallationStarted != null)
				InstallationStarted(this, e);
		}
		private void worker_InstallComplete(object sender, InstallCompleteEventArgs e)
		{
			IsInstallationInProgress = false;
			if (InstallComplete != null)
				InstallComplete(this, e);
		}
		private void worker_InstallerCommandExecuting(object sender, InstallerCommandExecutingEventArgs e)
		{
			WriteToLog(string.Format("Executing command: {0}", e.Command.GetType().FullName));
			if (InstallerCommandExecuting != null)
				InstallerCommandExecuting(this, e);
		}
		#endregion Private Methods

		#region Public Events
		public event EventHandler InstallationStarted;
		public event InstallCompleteHandler InstallComplete;
		public event InstallerCommandExecutingHandler InstallerCommandExecuting;
		public event EventHandler LoadComplete;
		public event StepChangedHandler StepChanged;
		#endregion Public Events

		#region Public Fields
		public static readonly string PREPROCESS_IMAGE = "62A73D9B-DC28-4E81-8FE4-C09B0F3EBD3E";
		#endregion Public Fields

		#region Public Properties
		public bool AllowSilentInstall { get; set; }
		public IList<BaseCommand> Commands { get; private set; }
		public IList<IInstallerData> Datum { get; private set; }
		public IList<IInstallerExtension> Extensions { get; private set; }
		public string FileName { get; private set; }
		public IList<string> GeneratedTempFiles { get; private set; }
		public Version InstallerFileNameVersion { get; set; }
		public bool IsDirty { get; set; }
		public bool IsInstallationInProgress { get; private set; }
		public IList<IInstallerItem> Items { get; private set; }
		public string LogFileName { get; private set; }
		public IList<IInstallerItem> PreprocessingItems { get; private set; }
		public IList<IInstallerProperty> Properties { get; private set; }
		public IList<string> RequiredDataNames { get; private set; }
		public CommandStatuses Status { get; set; }
		public int StepNumber { get; private set; }
		public IList<IInstallerWizardStep> Steps { get; private set; }
		public char VariableTrigger { get; set; }
		#endregion Public Properties
	}
}
