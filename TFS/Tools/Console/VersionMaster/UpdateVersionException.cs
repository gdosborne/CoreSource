namespace VersionMaster {
    using System;
    using System.ComponentModel;
    using System.Runtime.Serialization;
    using ConsoleUtilities;

    public class UpdateVersionException : ApplicationException {
        public UpdateVersionException(ErrorValues errorNumber, string additionalText = null) : base(errorNumber.Description()) {
            ErrorNumber = errorNumber;
            AdditionalText = additionalText;
        }
        public UpdateVersionException(Exception innerException, ErrorValues errorNumber, string additionalText = null) : base(errorNumber.Description(), innerException) {
            ErrorNumber = errorNumber;
            AdditionalText = additionalText;

        }
        public UpdateVersionException(SerializationInfo info, StreamingContext context, ErrorValues errorNumber, string additionalText = null) : base(info, context) {
            ErrorNumber = errorNumber;
            info.AddValue("ErrorDescription", errorNumber.Description());
            info.AddValue("AdditionalText", additionalText);
            AdditionalText = additionalText;
        }

        public enum ErrorValues {
            [Description("Path to project or updateversion file is required")]
            ProjectPathInvalid = 1,

            [Description("Project folder does not exist")]
            ProjectFolderInvalid = 2,

            [Description("AssemblyInfo file missing")]
            AssemblyInfoFileMissing = 3,

            [Description("Project file does not exist")]
            ProjectFileInvalid = 4,

            [Description("Error getting assembly version")]
            CannotGetAssemblyVersion = 5,

            [Description("Schema name or schema definition is missing")]
            NoSchemaNameOrDefinition = 6,

            [Description("Connot determine the schema methods")]
            CannotDetermineMethods = 7,

            [Description("Unknown error - see console")]
            UnKnown = 100
        }

        public ErrorValues ErrorNumber { get; private set; } = ErrorValues.UnKnown;

        public string AdditionalText { get; private set; } = null;


    }
}
