namespace UpdateVersionExtension {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using VersionMaster;

    public interface IExtender {
        string Name { get; }
        IList<VersionSchemaItem>
    }
}
