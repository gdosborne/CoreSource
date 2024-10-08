namespace VersionEngine
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Linq;
	using System.Xml.Linq;

	public class VersionSchema : INotifyPropertyChanged
	{
		#region Public Constructors
		public VersionSchema() {
			Major = VersionMethods.Fixed;
			Minor = VersionMethods.Fixed;
			Build = VersionMethods.Fixed;
			Revision = VersionMethods.Fixed;
			MajorFixed = 1;
			MinorFixed = 0;
			BuildFixed = 0;
			RevisionFixed = 0;
		}
		#endregion Public Constructors

		#region Public Methods
		public static VersionSchema FromXElement(XElement element, bool isFile) {
			var type = isFile ? "file" : "assembly";
			VersionSchema result = new VersionSchema();
			result.Major = (VersionMethods)Enum.Parse(typeof(VersionMethods), element.Element(type).Attribute("major").Value, true);
			result.Minor = (VersionMethods)Enum.Parse(typeof(VersionMethods), element.Element(type).Attribute("minor").Value, true);
			result.Build = (VersionMethods)Enum.Parse(typeof(VersionMethods), element.Element(type).Attribute("build").Value, true);
			result.Revision = (VersionMethods)Enum.Parse(typeof(VersionMethods), element.Element(type).Attribute("revision").Value, true);
			result.MajorFixed = int.Parse(element.Element(type).Attribute("majorfixed").Value);
			result.MinorFixed = int.Parse(element.Element(type).Attribute("minorfixed").Value);
			result.BuildFixed = int.Parse(element.Element(type).Attribute("buildfixed").Value);
			result.RevisionFixed = int.Parse(element.Element(type).Attribute("revisionfixed").Value);
			return result;
		}
		public void SetXElement(XElement element) {
			element.Attribute("major").Value = Major.ToString();
			element.Attribute("minor").Value = Minor.ToString();
			element.Attribute("build").Value = Build.ToString();
			element.Attribute("revision").Value = Revision.ToString();
			element.Attribute("majorfixed").Value = MajorFixed.ToString();
			element.Attribute("minorfixed").Value = MinorFixed.ToString();
			element.Attribute("buildfixed").Value = BuildFixed.ToString();
			element.Attribute("revisionfixed").Value = RevisionFixed.ToString();
		}
		public XElement ToXElement(bool isFile) {
			return new XElement(isFile ? "file" : "assembly",
				new XAttribute("major", Major),
				new XAttribute("minor", Minor),
				new XAttribute("build", Build),
				new XAttribute("revision", Revision),
				new XAttribute("majorfixed", MajorFixed),
				new XAttribute("minorfixed", MinorFixed),
				new XAttribute("buildfixed", BuildFixed),
				new XAttribute("revisionfixed", RevisionFixed));
		}
		#endregion Public Methods

		#region Public Events
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion Public Events

		#region Private Fields
		private VersionMethods _Build;
		private int _BuildFixed;
		private VersionMethods _Major;
		private int _MajorFixed;
		private VersionMethods _Minor;
		private int _MinorFixed;
		private VersionMethods _Revision;
		private int _RevisionFixed;
		#endregion Private Fields

		#region Public Properties
		public VersionMethods Build {
			get {
				return _Build;
			}
			set {
				_Build = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Build"));
			}
		}
		public int BuildFixed {
			get {
				return _BuildFixed;
			}
			set {
				_BuildFixed = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("BuildFixed"));
			}
		}
		public VersionMethods Major {
			get {
				return _Major;
			}
			set {
				_Major = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Major"));
			}
		}
		public int MajorFixed {
			get {
				return _MajorFixed;
			}
			set {
				_MajorFixed = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("MajorFixed"));
			}
		}
		public VersionMethods Minor {
			get {
				return _Minor;
			}
			set {
				_Minor = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Minor"));
			}
		}
		public int MinorFixed {
			get {
				return _MinorFixed;
			}
			set {
				_MinorFixed = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("MinorFixed"));
			}
		}
		public VersionMethods Revision {
			get {
				return _Revision;
			}
			set {
				_Revision = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Revision"));
			}
		}
		public int RevisionFixed {
			get {
				return _RevisionFixed;
			}
			set {
				_RevisionFixed = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("RevisionFixed"));
			}
		}
		#endregion Public Properties
	}
}
