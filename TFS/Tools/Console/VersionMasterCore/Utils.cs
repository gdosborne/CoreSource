using System.ComponentModel;
using static VersionMaster.UpdateVersionException;

namespace VersionMasterCore {
    public static class Utils {
        public static string Description(this ErrorValues value) {
            var fi = value.GetType().GetField(value.ToString());
            var attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];
            if (attributes != null && attributes.Any()) {
                return attributes.First().Description;
            }
            return value.ToString();
        }
    }
}
