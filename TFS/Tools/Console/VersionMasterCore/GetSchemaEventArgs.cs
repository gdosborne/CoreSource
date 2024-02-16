namespace VersionMaster {
    public delegate void GetSchemaHandler(object sender, GetSchemaEventArgs e);

    public class GetSchemaEventArgs : EventArgs {
        public GetSchemaEventArgs(string schemaName) {
            SchemaName = schemaName;
        }
        public string SchemaName { get; private set; }
        public SchemaItem SchemaItem { get; set; }
    }
}
