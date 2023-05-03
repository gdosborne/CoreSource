namespace Data {
	using System;
	using System.Collections.Generic;
	using System.Data.SqlClient;
	using System.Linq;
	using System.Threading.Tasks;
	using Data.Classes;

	public class DataSource {
		public DataSource() => this.allCounties = new List<County>();

		private readonly string connectionString = "Data Source=MLT19912\\SqlExpressExt;Initial Catalog=CountiesDB;Integrated Security=True";

		private SqlConnection Connection = default(SqlConnection);

		private readonly List<County> allCounties = default(List<County>);

		public IEnumerable<County> GetAllCounties() {
			if (!this.allCounties.Any()) {
				var task = Task.Factory.StartNew(() => {
					var conn = default(SqlConnection);
					try {
						using (conn = this.GetConnection())
						using (var sqlCmd = new SqlCommand("GetAllCounties", conn) {
							CommandType = System.Data.CommandType.StoredProcedure,
							CommandTimeout = 10000
						}) {
							var reader = sqlCmd.ExecuteReader();
							//get counties first
							while (reader.Read()) {
								var county = new County {
									ID = reader.GetInt32(reader.GetOrdinal("Id")),
									Name = reader.GetString(reader.GetOrdinal("CountyName"))
								};
								this.allCounties.Add(county);
							}
							//now link to adjacent couties
							this.allCounties.ForEach(cty => this.AddAdjacentCounties(cty));
						}
					}
					catch (Exception) {

					}
					finally {
						this.CloseConnection(conn);
					}
				});
				task.Wait();
			}
			return this.allCounties.OrderBy(x => x.ID);
		}

		private SqlConnection GetConnection() {
			var result = new SqlConnection(this.connectionString);
			result.Open();
			return result;
		}

		private void CloseConnection(SqlConnection conn) {
			if (conn != null && conn.State != System.Data.ConnectionState.Closed) {
				conn.Close();
			}
		}

		public void AddAdjacentCounties(County county) {
			var conn = default(SqlConnection);
			try {
				using (conn = this.GetConnection())
				using (var sqlCmd = new SqlCommand("GetAdjacentCounties", conn) {
					CommandType = System.Data.CommandType.StoredProcedure,
					CommandTimeout = 10000
				}) {
					sqlCmd.Parameters.Add(new SqlParameter("@parent", county.ID));
					var reader = sqlCmd.ExecuteReader();
					while (reader.Read()) {
						//get adjacent county id
						var id = reader.GetInt32(reader.GetOrdinal("Id"));
						//find the county in the all counties list
						var cty = this.allCounties.FirstOrDefault(x => x.ID == id);
						if (!county.AdjacentCounties.Any(x => x.ID == id)) {
							county.AdjacentCounties.Add(cty);
						}
					}
				}
			}
			catch (Exception) {

			}
			finally {
				this.CloseConnection(conn);
			}
		}
	}
}
