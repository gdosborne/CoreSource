namespace MyMinistry.Utilities
{
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using System.Xml.Linq;

	public class MyMinistryContact
	{
		#region Public Constructors

		public MyMinistryContact()
		{
			Id = Guid.NewGuid();
		}

		#endregion Public Constructors

		#region Public Properties

		public string Address1 { get; set; }
		public string Address2 { get; set; }
		public string City { get; set; }
		public string Country { get; set; }
		public string FirstName { get; set; }
		public Guid Id { get; set; }
		public string LastName { get; set; }
		public string Notes { get; set; }
		public string SpouseName { get; set; }
		public StateItem State { get; set; }
		public string TelephoneHome { get; set; }
		public string TelephoneMobile { get; set; }
		public string ZipCode { get; set; }

		#endregion Public Properties

		#region Public Methods

		public static MyMinistryContact FromXElement(XElement item)
		{
			var result = new MyMinistryContact
			{
				Id = Guid.Parse(item.Attribute("id").Value),
				FirstName = item.Element("FirstName").Value,
				LastName = item.Element("LastName").Value,
				SpouseName = item.Element("SpouseName").Value,
				Address1 = item.Element("Address1").Value,
				Address2 = item.Element("Address2").Value,
				City = item.Element("City").Value,
				State = StateItem.GetStates().First(x => x.Abbreviation.Equals(item.Element("State").Value)),
				ZipCode = item.Element("ZipCode").Value,
				TelephoneHome = item.Element("TelephoneHome").Value,
				TelephoneMobile = item.Element("TelephoneMobile").Value,
				Notes = item.Element("Notes").Value,
			};
			return result;
		}

		public override string ToString()
		{
			return ToXElement().ToString();
		}

		public XElement ToXElement()
		{
			var result = new XElement("Contact");
			result.Add(new XAttribute("id", Id));
			result.Add(new XElement("FirstName", string.IsNullOrEmpty(FirstName) ? string.Empty : FirstName));
			result.Add(new XElement("LastName", string.IsNullOrEmpty(LastName) ? string.Empty : LastName));
			result.Add(new XElement("SpouseName", string.IsNullOrEmpty(SpouseName) ? string.Empty : SpouseName));
			result.Add(new XElement("Address1", string.IsNullOrEmpty(Address1) ? string.Empty : Address1));
			result.Add(new XElement("Address2", string.IsNullOrEmpty(Address2) ? string.Empty : Address2));
			result.Add(new XElement("City", string.IsNullOrEmpty(City) ? string.Empty : City));
			result.Add(new XElement("State", State));
			result.Add(new XElement("ZipCode", string.IsNullOrEmpty(ZipCode) ? string.Empty : ZipCode));
			result.Add(new XElement("TelephoneHome", string.IsNullOrEmpty(TelephoneHome) ? string.Empty : TelephoneHome));
			result.Add(new XElement("TelephoneMobile", string.IsNullOrEmpty(TelephoneMobile) ? string.Empty : TelephoneMobile));
			result.Add(new XElement("Notes", string.IsNullOrEmpty(Notes) ? string.Empty : Notes));
			return result;
		}

		#endregion Public Methods
	}
}
