using System;
using System.Data;
using System.Xml;

namespace Common.AppFramework.SqlServer {
    public static class Extensions {
        public static Type ToType(this DbType type) {
            switch (type) {
                case DbType.AnsiString:
                case DbType.AnsiStringFixedLength:
                case DbType.String:
                case DbType.StringFixedLength:
                    return typeof(string);
                case DbType.Date:
                case DbType.DateTime:
                case DbType.DateTime2:
                case DbType.DateTimeOffset:
                    return typeof(DateTime);
                case DbType.Time:
                    return typeof(TimeSpan);
                case DbType.Binary:
                case DbType.Boolean:
                    return typeof(bool);
                case DbType.Byte:
                    return typeof(byte);
                case DbType.Currency:
                case DbType.Double:
                case DbType.VarNumeric:
                    return typeof(double);
                case DbType.Single:
                    return typeof(float);
                case DbType.Decimal:
                    return typeof(decimal);
                case DbType.Int16:
                    return typeof(short);
                case DbType.Int32:
                    return typeof(int);
                case DbType.Int64:
                    return typeof(long);
                case DbType.UInt16:
                    return typeof(ushort);
                case DbType.UInt32:
                    return typeof(uint);
                case DbType.UInt64:
                    return typeof(ulong);
                case DbType.Guid:
                    return typeof(Guid);
                case DbType.Object:
                    return typeof(object);
                case DbType.SByte:
                    return typeof(sbyte);
                case DbType.Xml:
                    return typeof(XmlDocument);
            }
            return null;
        }
    }
}
