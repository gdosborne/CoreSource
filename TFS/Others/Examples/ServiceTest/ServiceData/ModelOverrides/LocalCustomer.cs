namespace ServiceData.ModelOverrides {
    using GregOsborne.Application.Primitives;
    using ServiceDataModel;
    using System.Data;

    internal class LocalCustomer : Customer {
        public static Customer FromDataRow(DataRow row) => new Customer {
            Id = row["id"].CastTo<int>(),
            FirstName = row["firstname"].CastTo<string>(),
            LastName = row["lastname"].CastTo<string>()
        };
    }
}
