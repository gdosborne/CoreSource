namespace GregOsborne.AppVersion
{
	using EnvDTE;
	using GregOsborne.Application.Primitives;
	using Microsoft.VisualStudio;
	using Microsoft.VisualStudio.Shell;
	using Microsoft.VisualStudio.Shell.Interop;
	using MVVMFramework;
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.ComponentModel;
	using System.IO;
	using System.Linq;
	using System.Runtime.InteropServices;
	using System.Windows;
	using System.Xml.Linq;
	using VersionEngine;
	using VersionEngine.Implementation;

	public class VersionWindowView : INotifyPropertyChanged
	{
		#region Public Constructors
		public VersionWindowView() {
			AssemblyMajorFixedVisibility = Visibility.Collapsed;
			AssemblyMinorFixedVisibility = Visibility.Collapsed;
			AssemblyBuildFixedVisibility = Visibility.Collapsed;
			AssemblyRevisionFixedVisibility = Visibility.Collapsed;
			FileMajorFixedVisibility = Visibility.Collapsed;
			FileMinorFixedVisibility = Visibility.Collapsed;
			FileBuildFixedVisibility = Visibility.Collapsed;
			FileRevisionFixedVisibility = Visibility.Collapsed;
			CPPSelectionVisibility = Visibility.Collapsed;
			CPPVisibility = Visibility.Collapsed;
			CPPUsesVariable = false;
			IsSameSchemaUsed = false;
			ErrorVisibility = Visibility.Collapsed;
		}
		#endregion Public Constructors
		private Visibility _CPPVisibility;
		public Visibility CPPVisibility {
			get { return _CPPVisibility; }
			set {
				_CPPVisibility = value;
				CPPSelectionVisibility = CPPVisibility == Visibility.Visible && CPPUsesVariable ? Visibility.Visible : Visibility.Collapsed;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("CPPVisibility"));
			}
		}
		private Visibility _CPPSelectionVisibility;
		public Visibility CPPSelectionVisibility {
			get { return _CPPSelectionVisibility; }
			set {
				_CPPSelectionVisibility = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("CPPSelectionVisibility"));
			}
		}
		#region Public Methods
		public void ClearAltFile() {
			AssemblyInfoFileName = GetAssemblyInfoFileName(true);
		}
		public void InitView() {
			VerMethods = new ObservableCollection<VersionMethods>
			{
				VersionMethods.Fixed,
				VersionMethods.Increment,
				VersionMethods.IncrementResetEachDay,
				VersionMethods.Year,
				VersionMethods.Year2Digit,
				VersionMethods.Month,
				VersionMethods.Day,
				VersionMethods.Second,
				VersionMethods.Ignore
			};
			IntPtr hierarchyPointer;
			IntPtr selectionContainerPointer;
			Object selectedObject = null;
			IVsMultiItemSelect multiItemSelect;
			uint projectItemId;
			IVsMonitorSelection monitorSelection = (IVsMonitorSelection)Package.GetGlobalService(typeof(SVsShellMonitorSelection));
			monitorSelection.GetCurrentSelection(out hierarchyPointer, out projectItemId, out multiItemSelect, out selectionContainerPointer);
			if (hierarchyPointer.ToInt32() == 0)
				throw new ApplicationException("You must be on a project file.");
			IVsHierarchy selectedHierarchy = Marshal.GetTypedObjectForIUnknown(hierarchyPointer, typeof(IVsHierarchy)) as IVsHierarchy;
			if (selectedHierarchy != null) {
				ErrorHandler.ThrowOnFailure(selectedHierarchy.GetProperty(projectItemId, (int)__VSHPROPID.VSHPROPID_ExtObject, out selectedObject));
			}
			CurrentProject = selectedObject.As<Project>();
			Marshal.Release(hierarchyPointer);
			Marshal.Release(selectionContainerPointer);
			if (CurrentProject == null)
				throw new ApplicationException("Project not selected.");
			switch (new FileInfo(CurrentProject.FullName).Extension) {
				case ".vcxproj":
					ProjectType = ProjectTypes.CPPProject;
					break;
				case ".csproj":
					ProjectType = ProjectTypes.CSProject;
					break;
				case ".vbproj":
					ProjectType = ProjectTypes.VBProject;
					break;
			}
			//CPPVisibility = ProjectType == ProjectTypes.CPPProject ? Visibility.Visible : Visibility.Collapsed;
			//CPPSelectionVisibility = CPPVisibility == Visibility.Visible && CPPUsesVariable ? Visibility.Visible : Visibility.Collapsed;
			AssemblyInfoFileName = GetAssemblyInfoFileName();
			VersionEngine = new Engine(ProjectType, CurrentProject.FullName, AssemblyInfoFileName);
			UpdateInterface();
		}
		public void UpdateInterface() {
			OKCommand.RaiseCanExecuteChanged();
			CancelCommand.RaiseCanExecuteChanged();
			SelectFileCommand.RaiseCanExecuteChanged();
			ClearAlternateFileCommand.RaiseCanExecuteChanged();
		}
		#endregion Public Methods
		private bool _CPPUsesVariable;
		public bool CPPUsesVariable {
			get { return _CPPUsesVariable; }
			set {
				_CPPUsesVariable = value;
				CPPSelectionVisibility = CPPVisibility == Visibility.Visible && CPPUsesVariable ? Visibility.Visible : Visibility.Collapsed;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("CPPUsesVariable"));
			}
		}
		private DelegateCommand _SelectCPPFileCommand = null;
		public DelegateCommand SelectCPPFileCommand {
			get {
				if (_SelectCPPFileCommand == null)
					_SelectCPPFileCommand = new DelegateCommand(SelectCPPFile, ValidateSelectCPPFileState);
				return _SelectCPPFileCommand as DelegateCommand;
			}
		}
		private void SelectCPPFile(object state) {
			if (ExecuteUIAction != null)
				ExecuteUIAction(this, new ExecuteUIActionEventArgs("SelectCPPVariableFileName"));
		}
		private bool ValidateSelectCPPFileState(object state) {
			return true;
		}
		private DelegateCommand _ClearCPPFileCommand = null;
		public DelegateCommand ClearCPPFileCommand {
			get {
				if (_ClearCPPFileCommand == null)
					_ClearCPPFileCommand = new DelegateCommand(ClearCPPFile, ValidateClearCPPFileState);
				return _ClearCPPFileCommand as DelegateCommand;
			}
		}
		private void ClearCPPFile(object state) {

		}
		private bool ValidateClearCPPFileState(object state) {
			return !string.IsNullOrEmpty(CPPVariableFileName);
		}
		private string _CPPVariableData;
		public string CPPVariableData {
			get { return _CPPVariableData; }
			set {
				_CPPVariableData = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("CPPVariableData"));
			}
		}
		private string _CPPVariableFileName;
		public string CPPVariableFileName {
			get { return _CPPVariableFileName; }
			set {
				_CPPVariableFileName = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("CPPVariableFileName"));
			}
		}
		#region Private Methods
		private void AssemblySchema_PropertyChanged(object sender, PropertyChangedEventArgs e) {
			AssemblySchema = new VersionSchema
			{
				Build = sender.As<VersionSchema>().Build,
				BuildFixed = sender.As<VersionSchema>().BuildFixed,
				Major = sender.As<VersionSchema>().Major,
				MajorFixed = sender.As<VersionSchema>().MajorFixed,
				Minor = sender.As<VersionSchema>().Minor,
				MinorFixed = sender.As<VersionSchema>().MinorFixed,
				Revision = sender.As<VersionSchema>().Revision,
				RevisionFixed = sender.As<VersionSchema>().RevisionFixed,
			};
			AssemblySchema.PropertyChanged += AssemblySchema_PropertyChanged;
			UpdateInterface();
		}
		private void Cancel(object state) {
			DialogResult = false;
		}
		private void ClearAlternateFile(object state) {
			if (ExecuteUIAction != null)
				ExecuteUIAction(this, new ExecuteUIActionEventArgs("AskClearAlternateFile"));
		}
		private void FileSchema_PropertyChanged(object sender, PropertyChangedEventArgs e) {
			FileSchema = new VersionSchema
			{
				Build = sender.As<VersionSchema>().Build,
				BuildFixed = sender.As<VersionSchema>().BuildFixed,
				Major = sender.As<VersionSchema>().Major,
				MajorFixed = sender.As<VersionSchema>().MajorFixed,
				Minor = sender.As<VersionSchema>().Minor,
				MinorFixed = sender.As<VersionSchema>().MinorFixed,
				Revision = sender.As<VersionSchema>().Revision,
				RevisionFixed = sender.As<VersionSchema>().RevisionFixed,
			};
			FileSchema.PropertyChanged += FileSchema_PropertyChanged;
			UpdateInterface();
		}
		private string GetAssemblyInfoFileName(bool skipAltCheck = false) {
			var projectsDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Version Updater", "temp");
			if (!Directory.Exists(projectsDir))
				Directory.CreateDirectory(projectsDir);
			var projectsFile = Path.Combine(projectsDir, "projects.xml");
			if (!skipAltCheck && File.Exists(projectsFile)) {
				var doc = GregOsborne.Application.Xml.Linq.Extension.GetXDocument(projectsFile);
				var element = doc.Root.Elements().FirstOrDefault(x => x.Attribute("projectpath").Value.Equals(CurrentProject.FullName, StringComparison.OrdinalIgnoreCase));
				if (element != null) {
					if (element.Attribute("alternatefilename") != null) {
						var fileName = element.Attribute("alternatefilename").Value;
						if (File.Exists(fileName))
							IsAlternateFile = true;
						return fileName;
					}
				}
			}
			var projectDir = Path.GetDirectoryName(CurrentProject.FullName);
			FileInfo f = null;
			string keyFileName = ProjectType == ProjectTypes.CSProject ? "assemblyinfo.cs" : ProjectType == ProjectTypes.CPPProject ? "assemblyinfo.cpp" : "assemblyinfo.vb";
			string keyDirName = ProjectType == ProjectTypes.CSProject ? "Properties" : ProjectType == ProjectTypes.CPPProject ? string.Empty : "My Project";
			var propertiesDir = Path.Combine(projectDir, keyDirName);
			if (!Directory.Exists(propertiesDir))
				return null;
			var dInfo = new DirectoryInfo(propertiesDir);
			f = dInfo.GetFiles().FirstOrDefault(x => x.Name.Equals(keyFileName, StringComparison.OrdinalIgnoreCase));
			IsAlternateFile = false;
			return f.FullName;
		}
		private VersionSchema GetSchema(bool isFile) {
			VersionSchema result = new VersionSchema();
			var projectsDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Version Updater", "temp");
			if (!Directory.Exists(projectsDir))
				Directory.CreateDirectory(projectsDir);
			var projectsFile = Path.Combine(projectsDir, "projects.xml");
			if (!File.Exists(projectsFile))
				return result;
			var doc = GregOsborne.Application.Xml.Linq.Extension.GetXDocument(projectsFile);
			var type = isFile ? "file" : "assembly";
			foreach (var element in doc.Root.Elements()) {
				if (element.Attribute("projectpath").Value.Equals(CurrentProject.FullName, StringComparison.OrdinalIgnoreCase)) {
					result = VersionSchema.FromXElement(element, isFile);
					IsSameSchemaUsed = bool.Parse(element.Attribute("issameschemaused").Value);
					break;
				}
			}
			return result;
		}
		private void OK(object state) {
			try {
				SetSchema(false, AssemblySchema);
				if (IsSameSchemaUsed)
					SetSchema(true, IsSameSchemaUsed ? AssemblySchema : FileSchema);
				VersionEngine.UpdateVersion(AssemblySchema, FileSchema, CurrentAssemblyVersion, CurrentFileVersion, LastUpdate);
				DialogResult = true;
			}
			catch (Exception ex) {
				ErrorVisibility = Visibility.Visible;
				ErrorText = ex.Message;
			}
		}
		private void SelectFile(object state) {
			if (ExecuteUIAction != null)
				ExecuteUIAction(this, new ExecuteUIActionEventArgs("SelectAlternateFile"));
		}
		private void SetSchema(bool isFile, VersionSchema schema) {
			var projectsDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Version Updater", "temp");
			if (!Directory.Exists(projectsDir))
				Directory.CreateDirectory(projectsDir);
			var projectsFile = Path.Combine(projectsDir, "projects.xml");
			XDocument doc = null;
			if (!File.Exists(projectsFile))
				doc = new XDocument(new XElement("projects"));
			else
				doc = doc = GregOsborne.Application.Xml.Linq.Extension.GetXDocument(projectsFile);
			XElement projectElement = null;
			foreach (var element in doc.Root.Elements()) {
				if (element.Attribute("projectpath").Value.Equals(CurrentProject.FullName, StringComparison.OrdinalIgnoreCase)) {
					projectElement = element;
					break;
				}
			}
			if (projectElement != null) {
				projectElement.Attribute("issameschemaused").Value = IsSameSchemaUsed.ToString();
				if (IsAlternateFile) {
					if (projectElement.Attribute("alternatefilename") == null)
						projectElement.Add(new XAttribute("alternatefilename", AssemblyInfoFileName));
					else
						projectElement.Attribute("alternatefilename").Value = AssemblyInfoFileName;
				}

				XElement elem = null;
				if (!isFile) {
					elem = projectElement.Element("assembly");
				}
				else
					elem = projectElement.Element("file");
				schema.SetXElement(elem);
			}
			else {
				projectElement = new XElement("project",
						new XAttribute("projectpath", CurrentProject.FullName),
						new XAttribute("issameschemaused", IsSameSchemaUsed));
				if (IsAlternateFile)
					projectElement.Add(new XAttribute("alternatefilename", AssemblyInfoFileName));
				if (!isFile) {
					projectElement.Add(schema.ToXElement(false));
					projectElement.Add(new VersionSchema().ToXElement(true));
				}
				else {
					projectElement.Add(schema.ToXElement(true));
					projectElement.Add(new VersionSchema().ToXElement(false));
				}
				doc.Root.Add(projectElement);
			}
			if (!IsAlternateFile && projectElement.Attribute("alternatefilename") != null)
				projectElement.Attribute("alternatefilename").Remove();
			doc.Save(projectsFile);
			return;
		}
		private bool ValidateCancelState(object state) {
			return true;
		}
		private bool ValidateClearAlternateFileState(object state) {
			return IsAlternateFile;
		}
		private bool ValidateOKState(object state) {
			return true;
		}
		private bool ValidateSelectFileState(object state) {
			return true;
		}
		#endregion Private Methods

		#region Public Events
		public event ExecuteUIActionHandler ExecuteUIAction;
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion Public Events

		#region Private Fields
		private Visibility _AssemblyBuildFixedVisibility;
		private string _AssemblyInfoFileName;
		private Visibility _AssemblyMajorFixedVisibility;
		private Visibility _AssemblyMinorFixedVisibility;
		private Visibility _AssemblyRevisionFixedVisibility;
		private VersionSchema _AssemblySchema;
		private string _AssemblyVersionTitle;
		private DelegateCommand _CancelCommand = null;
		private DelegateCommand _ClearAlternateFileCommand = null;
		private Version _CurrentAssemblyVersion;
		private Version _CurrentFileVersion;
		private Project _CurrentProject;
		private bool? _DialogResult;
		private string _ErrorText;
		private Visibility _ErrorVisibility;
		private Visibility _FileBuildFixedVisibility;
		private Visibility _FileMajorFixedVisibility;
		private Visibility _FileMinorFixedVisibility;
		private Visibility _FileRevisionFixedVisibility;
		private VersionSchema _FileSchema;
		private Visibility _FileVersionVisibility;
		private bool _IsAlternateFile;
		private bool _IsSameSchemaUsed;
		private DateTime? _LastUpdate;
		private DelegateCommand _OKCommand = null;
		private ProjectTypes _ProjectType;
		private DelegateCommand _SelectFileCommand = null;
		private Visibility _SynchVisibility;
		private ObservableCollection<VersionMethods> _VerMethods;
		private IVersionEngine _VersionEngine;
		#endregion Private Fields

		#region Public Properties
		public Visibility AssemblyBuildFixedVisibility {
			get {
				return _AssemblyBuildFixedVisibility;
			}
			set {
				_AssemblyBuildFixedVisibility = value;
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("AssemblyBuildFixedVisibility"));
			}
		}
		public string AssemblyInfoFileName {
			get {
				return _AssemblyInfoFileName;
			}
			set {
				_AssemblyInfoFileName = value;
				if (!string.IsNullOrEmpty(_AssemblyInfoFileName)) {
					var helper = new AssemblyInfoHelper(AssemblyInfoFileName);
					CurrentAssemblyVersion = helper.GetVersion(ProjectType, false);
					CurrentFileVersion = helper.GetVersion(ProjectType, true);
					LastUpdate = helper.LastUpdate;
					AssemblySchema = GetSchema(false);
					AssemblySchema.PropertyChanged += AssemblySchema_PropertyChanged;
					FileSchema = GetSchema(!IsSameSchemaUsed);
					FileSchema.PropertyChanged += FileSchema_PropertyChanged;
				}
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("AssemblyInfoFileName"));
			}
		}
		public Visibility AssemblyMajorFixedVisibility {
			get {
				return _AssemblyMajorFixedVisibility;
			}
			set {
				_AssemblyMajorFixedVisibility = value;
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("AssemblyMajorFixedVisibility"));
			}
		}
		public Visibility AssemblyMinorFixedVisibility {
			get {
				return _AssemblyMinorFixedVisibility;
			}
			set {
				_AssemblyMinorFixedVisibility = value;
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("AssemblyMinorFixedVisibility"));
			}
		}
		public Visibility AssemblyRevisionFixedVisibility {
			get {
				return _AssemblyRevisionFixedVisibility;
			}
			set {
				_AssemblyRevisionFixedVisibility = value;
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("AssemblyRevisionFixedVisibility"));
			}
		}
		public VersionSchema AssemblySchema {
			get {
				return _AssemblySchema;
			}
			set {
				_AssemblySchema = value;
				if (value != null) {
					AssemblyMajorFixedVisibility = value.Major == VersionMethods.Fixed ? Visibility.Visible : Visibility.Collapsed;
					AssemblyMinorFixedVisibility = value.Minor == VersionMethods.Fixed ? Visibility.Visible : Visibility.Collapsed;
					AssemblyBuildFixedVisibility = value.Build == VersionMethods.Fixed ? Visibility.Visible : Visibility.Collapsed;
					AssemblyRevisionFixedVisibility = value.Revision == VersionMethods.Fixed ? Visibility.Visible : Visibility.Collapsed;
					if (IsSameSchemaUsed)
						FileSchema = value;
				}
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("AssemblySchema"));
			}
		}
		public string AssemblyVersionTitle {
			get {
				return _AssemblyVersionTitle;
			}
			set {
				_AssemblyVersionTitle = value;
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("AssemblyVersionTitle"));
			}
		}
		public DelegateCommand CancelCommand {
			get {
				if (_CancelCommand == null)
					_CancelCommand = new DelegateCommand(Cancel, ValidateCancelState);
				return _CancelCommand as DelegateCommand;
			}
		}
		public DelegateCommand ClearAlternateFileCommand {
			get {
				if (_ClearAlternateFileCommand == null)
					_ClearAlternateFileCommand = new DelegateCommand(ClearAlternateFile, ValidateClearAlternateFileState);
				return _ClearAlternateFileCommand as DelegateCommand;
			}
		}
		public Version CurrentAssemblyVersion {
			get {
				return _CurrentAssemblyVersion;
			}
			set {
				_CurrentAssemblyVersion = value;
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("CurrentAssemblyVersion"));
			}
		}
		public Version CurrentFileVersion {
			get {
				return _CurrentFileVersion;
			}
			set {
				_CurrentFileVersion = value;
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("CurrentFileVersion"));
			}
		}
		public Project CurrentProject {
			get {
				return _CurrentProject;
			}
			set {
				_CurrentProject = value;
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("CurrentProject"));
			}
		}
		public bool? DialogResult {
			get {
				return _DialogResult;
			}
			set {
				_DialogResult = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("DialogResult"));
			}
		}
		public string ErrorText {
			get {
				return _ErrorText;
			}
			set {
				_ErrorText = value;
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ErrorText"));
			}
		}
		public Visibility ErrorVisibility {
			get {
				return _ErrorVisibility;
			}
			set {
				_ErrorVisibility = value;
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ErrorVisibility"));
			}
		}
		public Visibility FileBuildFixedVisibility {
			get {
				return _FileBuildFixedVisibility;
			}
			set {
				_FileBuildFixedVisibility = value;
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("FileBuildFixedVisibility"));
			}
		}
		public Visibility FileMajorFixedVisibility {
			get {
				return _FileMajorFixedVisibility;
			}
			set {
				_FileMajorFixedVisibility = value;
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("FileMajorFixedVisibility"));
			}
		}
		public Visibility FileMinorFixedVisibility {
			get {
				return _FileMinorFixedVisibility;
			}
			set {
				_FileMinorFixedVisibility = value;
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("FileMinorFixedVisibility"));
			}
		}
		public Visibility FileRevisionFixedVisibility {
			get {
				return _FileRevisionFixedVisibility;
			}
			set {
				_FileRevisionFixedVisibility = value;
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("FileRevisionFixedVisibility"));
			}
		}
		public VersionSchema FileSchema {
			get {
				return _FileSchema;
			}
			set {
				_FileSchema = value;
				if (value != null) {
					FileMajorFixedVisibility = value.Major == VersionMethods.Fixed ? Visibility.Visible : Visibility.Collapsed;
					FileMinorFixedVisibility = value.Minor == VersionMethods.Fixed ? Visibility.Visible : Visibility.Collapsed;
					FileBuildFixedVisibility = value.Build == VersionMethods.Fixed ? Visibility.Visible : Visibility.Collapsed;
					FileRevisionFixedVisibility = value.Revision == VersionMethods.Fixed ? Visibility.Visible : Visibility.Collapsed;
				}
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("FileSchema"));
			}
		}
		public Visibility FileVersionVisibility {
			get {
				return _FileVersionVisibility;
			}
			set {
				_FileVersionVisibility = value;
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("FileVersionVisibility"));
			}
		}
		public bool IsAlternateFile {
			get {
				return _IsAlternateFile;
			}
			set {
				_IsAlternateFile = value;
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("IsAlternateFile"));
			}
		}
		public bool IsSameSchemaUsed {
			get {
				return _IsSameSchemaUsed;
			}
			set {
				_IsSameSchemaUsed = value;
				AssemblyVersionTitle = _IsSameSchemaUsed ? "Version" : "Assembly Version";
				FileVersionVisibility = _IsSameSchemaUsed ? Visibility.Collapsed : Visibility.Visible;
				SynchVisibility = _IsSameSchemaUsed ? Visibility.Visible : Visibility.Collapsed;
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("IsSameSchemaUsed"));
			}
		}
		public DateTime? LastUpdate {
			get {
				return _LastUpdate;
			}
			set {
				_LastUpdate = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("LastUpdate"));
			}
		}
		public DelegateCommand OKCommand {
			get {
				if (_OKCommand == null)
					_OKCommand = new DelegateCommand(OK, ValidateOKState);
				return _OKCommand as DelegateCommand;
			}
		}
		public ProjectTypes ProjectType {
			get {
				return _ProjectType;
			}
			set {
				_ProjectType = value;
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ProjectType"));
			}
		}
		public DelegateCommand SelectFileCommand {
			get {
				if (_SelectFileCommand == null)
					_SelectFileCommand = new DelegateCommand(SelectFile, ValidateSelectFileState);
				return _SelectFileCommand as DelegateCommand;
			}
		}
		public Visibility SynchVisibility {
			get {
				return _SynchVisibility;
			}
			set {
				_SynchVisibility = value;
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("SynchVisibility"));
			}
		}
		public ObservableCollection<VersionMethods> VerMethods {
			get {
				return _VerMethods;
			}
			set {
				_VerMethods = value;
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("VerMethods"));
			}
		}
		public IVersionEngine VersionEngine {
			get {
				return _VersionEngine;
			}
			set {
				_VersionEngine = value;
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("VersionEngine"));
			}
		}
		#endregion Public Properties
	}
}
