/* File="Extensions"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

using System.Data;

namespace Common.Sql {
    public enum DatabaseTypes {
        SQLServer
    }

    public static class Extensions {
        public static string ToSpecificDBType(this DbType type, DatabaseTypes dbType, int length = -1) {
            if (dbType == DatabaseTypes.SQLServer) {
                switch (type) {
                    case DbType.AnsiString:
                    case DbType.String:
                    case DbType.AnsiStringFixedLength:
                    case DbType.StringFixedLength:
                        return length > -1 ? $"[nvarchar]({length})" : $"[nvarchar](MAX)";
                    case DbType.Guid:
                        return "[uniqueidentifier]";
                    case DbType.Xml:
                        return "[xml]";
                    case DbType.Binary:
                    case DbType.Boolean:
                        return "[bit]";
                    case DbType.Int32:
                        return "[int]";
                    case DbType.Int64:
                        return "[bigint]";
                    case DbType.Int16:
                        return "[smallint]";
                    case DbType.Date:
                        return "[date]";
                    case DbType.DateTime:
                    case DbType.DateTime2:
                        return "[datetime]";
                    case DbType.Byte:
                        return "[tinyint]";
                    case DbType.Currency:
                        return "[money]";
                    case DbType.Decimal:
                        return "[decimal](18, 0)";
                    case DbType.Double:
                    case DbType.Single:
                        return "[real]";
                    case DbType.Time:
                        return "[time]";
                    default:
                        return "[object]";
                }
            }
            return null;
        }
    }
}
