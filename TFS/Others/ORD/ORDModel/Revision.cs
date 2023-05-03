// -----------------------------------------------------------------------
// Copyright ©  2016
// Created by: Greg Osborne
// -----------------------------------------------------------------------
//
//
//
namespace ORDModel
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Linq;
	using System.Xml.Linq;

	/// <summary>
	/// Class Revision. This class cannot be inherited.
	/// </summary>
	public sealed class Revision : INotifyPropertyChanged, IPropertyItem
	{
		#region Public Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Revision"/> class.
		/// </summary>
		public Revision() {
			Description = string.Empty;
			User = string.Empty;
			Major = 1;
			Minor = 0;
			Created = DateTime.Now;
		}
		#endregion Public Constructors

		#region Public Methods
		/// <summary>
		/// Froms the x element.
		/// </summary>
		/// <param name="element">The element.</param>
		/// <returns>Revision.</returns>
		public static Revision FromXElement(XElement element) {
			var result = new Revision();
			result.Major = int.Parse(element.Attribute("Major").Value);
			result.Minor = int.Parse(element.Attribute("Minor").Value);
			DateTime t1;
			if (DateTime.TryParse(element.Attribute("Date").Value, out t1))
				result.Created = t1;
			result.Description = element.Attribute("Description").Value;
			result.User = element.Attribute("User").Value;
			result.Version = new Version(result.Major, result.Minor);
			return result;
		}
		public System.Windows.Controls.UserControl GetPropertiesControl() {
			throw new NotImplementedException();
		}
		/// <summary>
		/// To the x element.
		/// </summary>
		/// <param name="num">The number.</param>
		/// <returns>XElement.</returns>
		public XElement ToXElement(int num) {
			return new XElement(string.Format("L{0}", num),
				new XAttribute("Major", Major),
				new XAttribute("Minor", Minor),
				new XAttribute("Date", Created),
				new XAttribute("User", User),
				new XAttribute("Description", Description));
		}
		#endregion Public Methods

		#region Public Events
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion Public Events

		#region Private Fields
		private DateTime _Created;
		private string _Description;
		private int _Major;
		private int _Minor;
		private string _User;
		private Version _Version;
		#endregion Private Fields

		#region Public Properties
		public DateTime Created {
			get {
				return _Created;
			}
			set {
				_Created = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Created"));
			}
		}
		public string Description {
			get {
				return _Description;
			}
			set {
				_Description = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Description"));
			}
		}
		public int Major {
			get {
				return _Major;
			}
			set {
				_Major = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Major"));
			}
		}
		public int Minor {
			get {
				return _Minor;
			}
			set {
				_Minor = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Minor"));
			}
		}
		public string User {
			get {
				return _User;
			}
			set {
				_User = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("User"));
			}
		}
		public Version Version {
			get {
				return _Version;
			}
			set {
				_Version = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Version"));
			}
		}
		#endregion Public Properties
	}
}