namespace Territory.Checkout.Data {
	using System;
	using System.Collections.Generic;
	using System.Data.OleDb;
	using System.Linq;
	using System.Threading.Tasks;

	internal class TerritoryItem : DataItemBase {
		public TerritoryItem(long id) : base(id) { }

		#region Number Property
		private string _Number = default;
		public string Number {
			get => _Number;
			set {
				if (Number == value) return;
				_Number = value;
				if (!IsLoading) Status = Statuses.Changed;
				InvokePropertyChanged();
			}
		}
		#endregion

		#region Area Property
		private AreaItem _Area = default;
		public AreaItem Area {
			get => _Area;
			set {
				if (Area == value) return;
				_Area = value;
				if (!IsLoading) Status = Statuses.Changed;
				InvokePropertyChanged();
			}
		}
		#endregion

		private readonly static string GetAllSql = "SELECT ID, Number, Area " +
			"FROM Territory ORDER BY ID";
		private readonly static string InsertSql = "INSERT INTO Territory (" +
			"[Number], [Area]" +
			") VALUES (" +
			"@Number, @AreaID)";
		private readonly static string UpdateSql = "UPDATE Territory SET " +
			"[Number] = @Number, [Area] = @Area " +
			"WHERE ID = @ID";
		private readonly static string DeleteSql = "DELETE FROM Territory " +
			"WHERE ID = @ID";

		public override async Task Update() {
			await Task.Yield();
			if (Status == Statuses.Unchanged) return;
			var cmd = new OleDbCommand(UpdateSql, Connection);
			cmd.Parameters.Add(GetParameter("@Number", Number));
			cmd.Parameters.Add(GetParameter("@Area", Area.ID));
			cmd.Parameters.Add(GetParameter("@ID", ID));
			try {
				cmd.ExecuteNonQuery();
				Status = Statuses.Unchanged;
			}
			catch (Exception ex) {
				throw new ApplicationException($"Problems updating Territory (ID={ID})", ex);
			}
		}

		public async override Task Delete() {
			await Task.Yield();
			var cmd = new OleDbCommand(DeleteSql, Connection);
			cmd.Parameters.Add(GetParameter("@ID", ID));
			try {
				cmd.ExecuteNonQuery();
				Status = Statuses.Unchanged;
			}
			catch (Exception ex) {
				throw new ApplicationException($"Problems deleting Territory (ID={ID})", ex);
			}
		}

		public async static Task<TerritoryItem> Add(string number, int areaId) {
			var areas = await AreaItem.GetAllAsync();
			var cmd = new OleDbCommand(InsertSql, Connection);
			cmd.Parameters.Add(GetParameter("@Number", number));
			cmd.Parameters.Add(GetParameter("@AreaID", areaId));
			var id = cmd.ExecuteNonQuery();
			var cmd1 = new OleDbCommand("SELECT @@IDENTITY", Connection);
			var reader = cmd1.ExecuteReader();
			var result = default(TerritoryItem);
			if (reader.HasRows) {
				reader.Read();
				var newId = reader.GetInt32(0);
				result = new TerritoryItem(newId) {
					Number = number,
					Area = areas.FirstOrDefault(x => x.ID == areaId)
				};
				result.IsLoading = false;
			}
			return result;
		}

		private static IEnumerable<TerritoryItem> cachedItems = default;
		public async static Task<IEnumerable<TerritoryItem>> GetAllAsync() {
			if (cachedItems != null) return cachedItems;
			var result = new List<TerritoryItem>();
			var areas = await AreaItem.GetAllAsync();
			await Task.Run(() => {
				var cmd = new OleDbCommand(GetAllSql, Connection);
				var reader = cmd.ExecuteReader();
				if (reader != null) {
					while (reader.Read()) {
						var item = new TerritoryItem(reader.GetInt32(reader.GetOrdinal("ID")));
						item.Number = reader.GetString(reader.GetOrdinal("Number"));
						var areaId = reader.GetInt32(reader.GetOrdinal("Area"));
						item.Area = areas.FirstOrDefault(x => x.ID == areaId);
						item.IsLoading = false;
						result.Add(item);
					}
				}
			});
			cachedItems = result;
			return result;
		}
	}
}
