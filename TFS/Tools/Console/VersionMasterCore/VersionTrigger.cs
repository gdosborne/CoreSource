namespace VersionMaster {
    public class VersionTrigger {
        public string TestText { get; set; }
        public string ActualText { get; set; }
        public Enumerations.VersionParts Part { get; set; }
        public Version Version { get; set; }
        public override string ToString() => $"{Part}=>{Version}";
    }
}
