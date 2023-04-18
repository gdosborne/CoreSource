namespace Territory.Checkout.Data {
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Data.OleDb;
	using System.Linq;
	using System.Threading.Tasks;
	using System.Windows.Controls;
	using System.Windows.Forms;

	internal class PersonItem : DataItemBase {
		public PersonItem(long id) : base(id) {
		}

		#region LastName Property
		private string _LastName = default;
		public string LastName {
			get => _LastName;
			set {
				if (LastName == value) return;
				_LastName = value;
				if (string.IsNullOrEmpty(FirstName)) FullName = LastName;
				else FullName = $"{FirstName} {LastName}";
				if (!IsLoading) Status = Statuses.Changed;
				InvokePropertyChanged();
			}
		}
		#endregion

		#region FirstName Property
		private string _FirstName = default;
		public string FirstName {
			get => _FirstName;
			set {
				if (FirstName == value) return;
				_FirstName = value;
				if (string.IsNullOrEmpty(LastName)) FullName = FirstName;
				else FullName = $"{FirstName} {LastName}";
				if (!IsLoading) Status = Statuses.Changed;
				InvokePropertyChanged();
			}
		}
		#endregion

		#region FullName Property
		private string _FullName = default;
		public string FullName {
			get => !string.IsNullOrEmpty(FirstName)
				? $"{FirstName} {(!string.IsNullOrEmpty(LastName) ? LastName : string.Empty)}"
				: !string.IsNullOrEmpty(LastName) ? LastName : string.Empty;
			set {
				//_FullName = value;
				//InvokePropertyChanged();
			}
		}
		#endregion

		#region TerritoryCheckouts Property
		private ObservableCollection<CheckoutItem> _TerritoryCheckouts = default;
		public ObservableCollection<CheckoutItem> TerritoryCheckouts {
			get => _TerritoryCheckouts;
			set {
				_TerritoryCheckouts = value;
				InvokePropertyChanged();
			}
		}

		#endregion

		private readonly static string GetAllSql = "SELECT ID, [Last Name], [First Name] " +
			"FROM Person ORDER BY ID";
		private readonly static string InsertSql = "INSERT INTO Person (" +
			"[Last Name], [First Name]" +
			") VALUES (" +
			"@LastName, @FirstName)";
		private readonly static string UpdateSql = "UPDATE Person SET " +
			"[Last Name] = @LastName, [First Name] = @FirstName " +
			"WHERE ID = @ID";
		private readonly static string DeleteSql = "DELETE FROM Person " +
			"WHERE ID = @ID";

		public async override Task Delete() {
			await Task.Yield();
			var cmd = new OleDbCommand(DeleteSql, Connection);
			cmd.Parameters.Add(GetParameter("@ID", ID));
			try {
				cmd.ExecuteNonQuery();
				Status = Statuses.Unchanged;
			}
			catch (Exception ex) {
				throw new ApplicationException($"Problems deleting Person (ID={ID})", ex);
			}
		}

		public async override Task Update() {
			await Task.Yield();
			if (Status == Statuses.Unchanged) return;
			var cmd = new OleDbCommand(UpdateSql, Connection);
			cmd.Parameters.Add(GetParameter("@LastName", LastName));
			cmd.Parameters.Add(GetParameter("@FirstName", FirstName));
			cmd.Parameters.Add(GetParameter("@ID", ID));
			try {
				cmd.ExecuteNonQuery();
				Status = Statuses.Unchanged;
			}
			catch (Exception ex) {
				throw new ApplicationException($"Problems updating Person (ID={ID})", ex);
			}
		}

		public async static Task<PersonItem> Add(string lastName, string firstName) {
			var cmd = new OleDbCommand(InsertSql, Connection);
			cmd.Parameters.Add(GetParameter("@LastName", lastName));
			cmd.Parameters.Add(GetParameter("@FirstName", firstName));
			var id = cmd.ExecuteNonQuery();
			var cmd1 = new OleDbCommand("SELECT @@IDENTITY", Connection);
			var reader = cmd1.ExecuteReader();
			var result = default(PersonItem);
			if (reader.HasRows) {
				reader.Read();
				var newId = reader.GetInt32(0);
				result = new PersonItem(newId) {
					LastName = lastName,
					FirstName = firstName
				};
				result.IsLoading = false;
			}
			return result;
		}

		private static IEnumerable<PersonItem> cachedItems = default;
		public async static Task<IEnumerable<PersonItem>> GetAllAsync() {
			if (cachedItems != null) return cachedItems;
			var result = new List<PersonItem>();
			await Task.Run(() => {
				var cmd = new OleDbCommand(GetAllSql, Connection);
				var reader = cmd.ExecuteReader();
				if (reader != null) {
					while (reader.Read()) {
						var item = new PersonItem(reader.GetInt32(reader.GetOrdinal("ID")));
						item.LastName = reader.GetString(reader.GetOrdinal("Last Name"));
						item.FirstName = reader.GetString(reader.GetOrdinal("First Name"));
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
