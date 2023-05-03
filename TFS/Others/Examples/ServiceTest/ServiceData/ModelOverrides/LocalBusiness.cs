namespace ServiceData.ModelOverrides {
    using GregOsborne.Application.Primitives;
    using ServiceDataModel;
    using System.Data;

    internal class LocalBusiness : Business {
        public static Business FromDataRow(DataRow row) => new Business {
            Id = row["id"].CastTo<int>(),
            Name = row["firstname"].CastTo<string>()
        };
    }
}
