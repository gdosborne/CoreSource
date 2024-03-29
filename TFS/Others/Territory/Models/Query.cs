using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Entities;
namespace Models
{
	public static class Query
	{
		private static IEnumerable<Publisher> publishers = null;
		public static IEnumerable<Publisher> GetPublishers()
		{
			if (publishers == null)
			{
				using (var ctx = new TerritoryDataContext())
				{
					var result = new List<Publisher>();
					var q = (from a in ctx.T_Publishers
							 select a).ToList();
					q.ForEach(x => result.Add(Publisher.FromDatabase(x)));
					publishers = result;
				}
			}
			return publishers;
		}
		private static IEnumerable<Area> areas = null;
		public static IEnumerable<Area> GetAreas()
		{
			if (areas == null)
			{
				using (var ctx = new TerritoryDataContext())
				{
					var result = new List<Area>();
					var q = (from a in ctx.T_Areas
							 select a).ToList();
					q.ForEach(x => result.Add(Area.FromDataBase(x)));
					areas = result;
				}
			}
			return areas;
		}
		private static IEnumerable<Territory> territories = null;
		public static IEnumerable<Territory> GetTerritories()
		{
			if (territories == null)
			{
				using (var ctx = new TerritoryDataContext())
				{
					var result = new List<Territory>();
					var q = (from a in ctx.T_Territories
							 select a).ToList();
					q.ToList().ForEach(x => result.Add(Territory.FromDataBase(x)));
					territories = result;
				}
			}
			return territories;
		}
		public static IEnumerable<Territory> GetTerritoriesNeedingWorkForArea(int areaId)
		{
			var terr = GetTerritories();
			var result = terr.Where(x => x.LastWorked.HasValue && x.Area.ID == areaId).ToList();
			return result;
		}
		public static IEnumerable<Territory> GetTerritoriesNeedingWork(TimeSpan t)
		{
			var terr = GetTerritories();
			var result = terr.Where(x => x.LastWorked.HasValue && x.LastWorked.Value < DateTime.Now.Subtract(t)).ToList();
			return result;
		}
		public static IEnumerable<Territory> GetTerritoriesBeingWorked()
		{
			var terr = GetTerritories();
			var result = terr.Where(x => !x.LastWorked.HasValue).ToList();
			return result;
		}
	}
}
