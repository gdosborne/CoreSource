using System.Text;

namespace GregOsborne.Application.Text {
    public static class Extension {
        public static void AppendLineFormat(this StringBuilder value, string format, object[] args) {
            value.AppendFormat(format, args);
            value.AppendLine();
        }

        public static void AppendLineFormat(this StringBuilder value, string format, object arg1) {
            value.AppendLineFormat(format, new[] {arg1});
        }

        public static void AppendLineFormat(this StringBuilder value, string format, object arg1, object arg2) {
            value.AppendLineFormat(format, new[] {arg1, arg2});
        }

        public static void AppendLineFormat(this StringBuilder value, string format, object arg1, object arg2, object arg3) {
            value.AppendLineFormat(format, new[] {arg1, arg2, arg3});
        }

        public static void Return(this StringBuilder value) {
            value.AppendLine();
        }
    }
}