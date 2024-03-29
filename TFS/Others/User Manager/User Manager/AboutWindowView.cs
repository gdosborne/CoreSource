using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MVVMFramework;

namespace User_Manager
{
	public class AboutWindowView : ViewModelBase, INotifyPropertyChanged
	{
		public override event PropertyChangedEventHandler PropertyChanged;
		public AboutWindowView()
		{
			var assy = this.GetType().Assembly;
			if (assy != null)
			{
				Version = string.Format("Version {0}", assy.GetName().Version.ToString());
				object[] customAttributes = assy.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
				if ((customAttributes != null) && (customAttributes.Length > 0))
					Description = ((AssemblyDescriptionAttribute)customAttributes[0]).Description;
				customAttributes = assy.GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
				if ((customAttributes != null) && (customAttributes.Length > 0))
					CompanyName = ((AssemblyCompanyAttribute)customAttributes[0]).Company;
			}
		}
		private string _Description;
		public string Description
		{
			get { return _Description; }
			set
			{
				_Description = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Description"));
			}
		}
		private string _Version;
		public string Version
		{
			get { return _Version; }
			set
			{
				_Version = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Version"));
			}
		}
		private string _CompanyName;
		public string CompanyName
		{
			get { return _CompanyName; }
			set
			{
				_CompanyName = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("CompanyName"));
			}
		}
	}
}
