namespace Territory.Checkout.Data {
	using System;
	using System.Collections.Generic;
	using System.Data.OleDb;
	using System.Linq;
	using System.Threading.Tasks;
	using Universal.Common;

	internal class CheckoutItem : DataItemBase {
		public CheckoutItem(long id) : base(id) { }

		#region Territory Property
		private TerritoryItem _Territory = default;
		public TerritoryItem Territory {
			get => _Territory;
			set {
				_Territory = value;
				InvokePropertyChanged();
			}
		}
		#endregion

		#region CheckedOut Property
		private DateTime _CheckedOut = default;
		public DateTime CheckedOut {
			get => _CheckedOut;
			set {
				_CheckedOut = value;
				InvokePropertyChanged();
			}
		}
		#endregion

		#region Person Property
		private PersonItem _Person = default;
		public PersonItem Person {
			get => _Person;
			set {
				_Person = value;
				InvokePropertyChanged();
			}
		}
		#endregion

		#region CheckedIn Property
		private DateTime? _CheckedIn = default;
		public DateTime? CheckedIn {
			get => _CheckedIn;
			set {
				_CheckedIn = value;
				InvokePropertyChanged();
			}
		}
		#endregion

		private readonly static string GetAllSql = "SELECT ID, Territory, [Checked Out], " +
			"[Checked Out By], [Checked In] " +
			"FROM Checkout ORDER BY ID";
		private readonly static string InsertSql = "INSERT INTO Checkout (" +
			"Territory, [Checked Out], [Checked Out By], [Checked In]" +
			") VALUES (" +
			"@tID, @checkedOut, @pID, @checkedIn)";
		private readonly static string UpdateSql = "UPDATE Checkout SET [Checked In] = @Checkin " +
			"WHERE ID = @ID";

		public async override Task Update() {
			await Task.Yield();
			if (!CheckedIn.HasValue) return;
			var checkInValue = CheckedIn.Value;
			var cmd = new OleDbCommand(UpdateSql, Connection);
			cmd.Parameters.Add(GetParameter("@Checkin", CheckedIn.Value, OleDbType.Date));
			cmd.Parameters.Add(GetParameter("@ID", ID));
			try {
				cmd.ExecuteNonQuery();
				Status = Statuses.Unchanged;
			}
			catch (Exception ex) {
				throw new ApplicationException($"Problems updating Checkout (ID={ID})", ex);
			}
		}
		public async static Task<CheckoutItem> Add(int terrID, int persID,
				DateTime checkedOut, DateTime? checkedIn = null) {
			var persons = await PersonItem.GetAllAsync();
			var territories = await TerritoryItem.GetAllAsync();
			var cmd = new OleDbCommand(InsertSql, Connection);
			cmd.Parameters.Add(GetParameter("@tID", terrID, OleDbType.Integer));
			cmd.Parameters.Add(GetParameter("@checkedOut", checkedOut, OleDbType.Date));
			cmd.Parameters.Add(GetParameter("@pID", persID, OleDbType.Integer));
			cmd.Parameters.Add(GetParameter("@checkedIn", !checkedIn.HasValue ? DBNull.Value : checkedIn.Value, OleDbType.Date));
			var result = default(CheckoutItem);
			try {
				var id = cmd.ExecuteNonQuery();
				var cmd1 = new OleDbCommand("SELECT @@IDENTITY", Connection);
				var reader = cmd1.ExecuteReader();
				result = default(CheckoutItem);
				if (reader.HasRows) {
					reader.Read();
					var newId = reader.GetInt32(0);
					result = new CheckoutItem(newId) {
						Territory = territories.FirstOrDefault(x => x.ID == terrID),
						CheckedOut = checkedOut,
						CheckedIn = checkedIn,
						Person = persons.FirstOrDefault(x => x.ID == persID)
					};
				}
			}
			catch (Exception ex) {
				throw new ApplicationException($"Problems creating Territory Checkout (ID={terrID})", ex);
			}
			return result;
		}

		private static IEnumerable<CheckoutItem> cachedItems = default;
		public async static Task<IEnumerable<CheckoutItem>> GetAllAsync() {
			if (cachedItems != null) return cachedItems;
			var territories = await TerritoryItem.GetAllAsync();
			var persons = await PersonItem.GetAllAsync();
			var result = new List<CheckoutItem>();
			await Task.Run(() => {
				var cmd = new OleDbCommand(GetAllSql, Connection);
				var reader = cmd.ExecuteReader();
				if (reader != null) {
					while (reader.Read()) {
						var item = new CheckoutItem(reader.GetInt32(reader.GetOrdinal("ID")));
						var terrId = reader.GetInt32(reader.GetOrdinal("Territory"));
						item.Territory = territories.FirstOrDefault(x => x.ID == terrId);
						item.CheckedOut = reader.GetDateTime(reader.GetOrdinal("Checked Out"));
						var personID = reader.GetInt32(reader.GetOrdinal("Checked Out By"));
						item.Person = persons.FirstOrDefault(x => x.ID == personID);
						if (!reader.IsDBNull(reader.GetOrdinal("Checked In")))
							item.CheckedIn = reader.GetDateTime(reader.GetOrdinal("Checked In"));
						result.Add(item);
					}
				}
			});
			cachedItems = result;
			return result;
		}

		public async static Task<CheckoutItem> GetSingleItem(int id) {
			if (cachedItems == null) cachedItems = await GetAllAsync();
			if (cachedItems.Any()) return cachedItems.FirstOrDefault(x => x.ID == id);
			return default;
		}
	}
}
