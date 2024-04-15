using ConsoleUtilities;
using GregOsborne.Application;
using GregOsborne.Application.IO;
using System.Reflection;
using System.Security.Policy;
using UpdateVersion;
using VersionMaster;
using static VersionMaster.Enumerations;
using static VersionMaster.UpdateVersionException;
using SysIO = System.IO;

AppDomain.CurrentDomain.UnhandledException += StaticValues.CurrentDomain_UnhandledException;

var assy = Assembly.GetEntryAssembly();
var version = assy.GetName().Version;

StaticValues.WriteLineToConsole($"Version Updater (c#) V{version}{Environment.NewLine}", StaticValues.projectName);

if (args.Length == 0) {
    StaticValues.ShowHelp();
}

StaticValues.appSingleton = new AppSingleton();
StaticValues.appSingleton.WaitForPreviousTermination();

var reader = default(ProjectConfigurationReader);
try {

    var executablePath = string.Empty;
    var arguments = CommandLine.GetArguments(Environment.CommandLine, '@', out executablePath);

#if DEBUG
    if (arguments.ContainsKey("p"))
        StaticValues.WriteLineToConsole($"{("Argument:").PadLeft(StaticValues.padSize)} {arguments["p"]}", StaticValues.projectName);
#endif

    StaticValues.ApplicationDirectory = SysIO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), StaticValues.ApplicationName);

    var session = new Session(StaticValues.ApplicationDirectory, StaticValues.ApplicationName,
        GregOsborne.Application.Logging.ApplicationLogger.StorageTypes.FlatFile,
        GregOsborne.Application.Logging.ApplicationLogger.StorageOptions.NewestFirstLogEntry);

    var useSharedVersionFile = session.ApplicationSettings.GetValue("Application", "UseSharedVersionFile", false);

    var dataDir = SysIO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Versioning");
    if (!SysIO.Directory.Exists(dataDir)) {
        SysIO.Directory.CreateDirectory(dataDir);
    }
    new DirectoryInfo(dataDir).CleanupDirectory(".xml");

    arguments.Select(x => x.Key).ToList().ForEach(x => {
        if (arguments.Any(y => y.Key == x)) {
            var item = arguments.FirstOrDefault(y => y.Key == x);
            switch (x) {

                case "p":
                    StaticValues.projectName = item.Value;
                    break;

                case "e":
                    StaticValues.ShowErrorCodes();
                    break;
            }
        }
    });

    var projectsFileName = default(string);
    if (useSharedVersionFile) {
        var sharedVersionFilePath = session.ApplicationSettings.GetValue("Application", "SharedVersionFilePath", default(string));
        if (string.IsNullOrWhiteSpace(sharedVersionFilePath)) {
            StaticValues.WriteLineToConsole("Use of shared project file is selected, but no path to the shared file exists. Run " +
                "ManageVersions to set the path to the shared project file.");
            StaticValues.ExitApp(-100, true, true, StaticValues.projectName);
            return;
        }
        projectsFileName = sharedVersionFilePath;
    }
    else {
        projectsFileName = SysIO.Path.Combine(dataDir, "UpdateVersion.Projects.xml");
    }

    if (!SysIO.File.Exists(projectsFileName)) {
        var source = SysIO.Path.Combine(SysIO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "UpdateVersion.Projects.xml");
        SysIO.File.Copy(source, projectsFileName);
    }

    reader = new ProjectConfigurationReader(projectsFileName);
    reader.DisplayMessage += StaticValues.Reader_DisplayMessage;
    reader.Initialize();

    var project = reader.Projects.FirstOrDefault(x => x.Name.Equals(StaticValues.projectName, StringComparison.OrdinalIgnoreCase));
    if (project == null) {
        StaticValues.ShowError(VersionMaster.UpdateVersionException.ErrorValues.ProjectNotRecognized, $"{StaticValues.projectName} doesn't exist", StaticValues.projectName);
    }

    if (string.IsNullOrEmpty(project.ProjectFileName) || string.IsNullOrEmpty(project.ProjectDirectory)) {
        StaticValues.ShowError(ErrorValues.ProjectPathInvalid, null, StaticValues.projectName);
    }

    if (!SysIO.Directory.Exists(project.ProjectDirectory)) {
        StaticValues.ShowError(ErrorValues.ProjectFolderInvalid, null, StaticValues.projectName);
    }

    StaticValues.WriteLineToConsole($"{("Project file:").PadLeft(StaticValues.padSize)} {project.ProjectFileName}", StaticValues.projectName);
    StaticValues.WriteLineToConsole($"{("Project folder:").PadLeft(StaticValues.padSize)} {project.ProjectDirectory}", StaticValues.projectName);

    if (!SysIO.File.Exists(project.FullPath)) {
        StaticValues.ShowError(ErrorValues.ProjectFileInvalid, null, StaticValues.projectName);
    }

    reader.SelectedSchema = reader.Schemas.FirstOrDefault(x => x.Name.Equals(project.SchemaName));

    StaticValues.WriteLineToConsole($"{("Schema:").PadLeft(StaticValues.padSize)} {reader.SelectedSchema.Name}", StaticValues.projectName);
    Environment.CurrentDirectory = project.ProjectDirectory;

    StaticValues.WriteLineToConsole($"{("AssemblyInfo file:").PadLeft(StaticValues.padSize)} {project.AssemblyInfoPath}", StaticValues.projectName);

    if (!SysIO.File.Exists(project.AssemblyInfoPath)) {
        StaticValues.ShowError(ErrorValues.AssemblyInfoFileMissing, null, StaticValues.projectName);
    }

    var assyVersion = project.CurrentAssemblyVersion;
    if (assyVersion == null) {
        StaticValues.ShowError(ErrorValues.CannotGetAssemblyVersion, null, StaticValues.projectName);
    }

    project.ReportProgress += StaticValues.Project_ReportProgress;

    StaticValues.WriteLineToConsole($"{Environment.NewLine}Modifying version for project {project.Name}", StaticValues.projectName);
    project.ModifyVersion();

    VersionParts.Assembly.UpdateAll(project.AssemblyInfoPath, assyVersion, project.ModifiedAssemblyVersion, project.LastBuildDate);

    project.UpdateLastBuildDate();
    reader.Save();
}
catch (Exception ex) {
    StaticValues.ShowError(ErrorValues.UnKnown, ex.Message, StaticValues.projectName);
}
finally {
    StaticValues.appSingleton.RemoveTriggerFile();
    if (reader != null) {
        reader.Dispose();
    }
}
var pause = false;
#if DEBUG
pause = true;
#endif
StaticValues.ExitApp(0, pause, true, StaticValues.projectName);

