using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Threading.Tasks;

namespace Territory.Checkout.Data {
	internal class AreaItem : DataItemBase {
		public AreaItem(long id) : base(id) { }

		#region Name Property
		private string _Name = default;
		public string Name {
			get => _Name;
			set {
				if (Name == value) return;
				_Name = value;
				if (!IsLoading) Status = Statuses.Changed;
				InvokePropertyChanged();
			}
		}
		#endregion

		#region Description Property
		private string _Description = default;
		public string Description {
			get => _Description;
			set {
				if (Description == value) return;
				_Description = value;
				if (!IsLoading) Status = Statuses.Changed;
				InvokePropertyChanged();
			}
		}
		#endregion

		private readonly static string GetAllSql = "SELECT ID, AreaName, Description " +
			"FROM Area ORDER BY ID";
		private readonly static string InsertSql = "INSERT INTO Area (" +
			"[AreaName], [Description]" +
			") VALUES (" +
			"@Name, @Description)";
		private readonly static string UpdateSql = "UPDATE [Area] SET [AreaName] = @Name, " +
			"[Description] = @Description " +
			"WHERE ID = @ID";
		private readonly static string DeleteSql = "DELETE FROM Area " +
			"WHERE ID = @ID";

		public override async Task Update() {
			await Task.Yield();
			if (Status == Statuses.Unchanged) return;
			var cmd = new OleDbCommand(UpdateSql, Connection);
			cmd.Parameters.Add(GetParameter("@Name", Name));
			cmd.Parameters.Add(GetParameter("@Description", Description));
			cmd.Parameters.Add(GetParameter("@ID", ID));
			try {
				cmd.ExecuteNonQuery();
				Status = Statuses.Unchanged;
			}
			catch (Exception ex) {
				throw new ApplicationException($"Problems updating Area (ID={ID})", ex);
			}
		}

		public override async Task Delete() {
			await Task.Yield();
			var cmd = new OleDbCommand(DeleteSql, Connection);
			cmd.Parameters.Add(GetParameter("@ID", ID));
			try {
				cmd.ExecuteNonQuery();
				Status = Statuses.Unchanged;
			}
			catch (Exception ex) {
				throw new ApplicationException($"Problems deleting Area (ID={ID})", ex);
			}
		}

		public async static Task<AreaItem> Add(string name, string description) {
			var cmd = new OleDbCommand(InsertSql, Connection);
			cmd.Parameters.Add(GetParameter("@Name", name));
			cmd.Parameters.Add(GetParameter("@Description", description));
			cmd.ExecuteNonQuery();
			var cmd1 = new OleDbCommand("SELECT @@IDENTITY", Connection);
			var reader = cmd1.ExecuteReader();
			var result = default(AreaItem);
			if (reader.HasRows) {
				reader.Read();
				var newId = reader.GetInt32(0);
				result = new AreaItem(newId) {
					Name = name,
					Description = description,
				};
				result.IsLoading = false;
			}
			return result;
		}
		private static IEnumerable<AreaItem> cachedItems = default;
		public async static Task<IEnumerable<AreaItem>> GetAllAsync() {
			if (cachedItems != null) return cachedItems;
			var result = new List<AreaItem>();
			await Task.Run(() => {
				var cmd = new OleDbCommand(GetAllSql, Connection);
				var reader = cmd.ExecuteReader();
				if (reader != null) {
					while (reader.Read()) {
						var item = new AreaItem(reader.GetInt32(reader.GetOrdinal("ID")));
						item.Name = reader.GetString(reader.GetOrdinal("AreaName"));
						item.Description = reader.GetString(reader.GetOrdinal("Description"));
						item.IsLoading = false;
						result.Add(item);
					}
				}
			});
			cachedItems = result;
			return result.OrderBy(x => x.Name);
		}
	}
}
