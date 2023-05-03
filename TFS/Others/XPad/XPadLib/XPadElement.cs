namespace XPadLib
{
	using GregOsborne.Application.Media;
	using System;
	using System.Collections.ObjectModel;
	using System.Drawing;
	using System.IO;
	using System.Linq;
	using System.Windows.Media;
	using System.Xml.Linq;

	public class XPadElement : XPadItem
	{
		public XElement ToXElement()
		{
			var result = new XElement(Name);
			Attributes.ToList().ForEach(x => result.Add(new XAttribute(x.Name, x.Value)));
			return result;
		}
		public XPadElement(XElement element)
		{
			Items = new ObservableCollection<XPadElement>();
			Attributes = new ObservableCollection<XPadAttribute>();
			Element = element;
			Name = element.Name.LocalName;
			if (element.Nodes().OfType<XText>().Any())
				Value = element.Nodes().OfType<XText>().First().Value;
			element.Attributes().ToList().ForEach(x => Attributes.Add(new XPadAttribute(x)));

			if (!string.IsNullOrEmpty(Value) && IsBase64(Value))
			{
				byte[] imageBytes = Convert.FromBase64String(Value);
				MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
				ms.Write(imageBytes, 0, imageBytes.Length);
				Image = Image.FromStream(ms, true);
			}

			Attributes.CollectionChanged += Attributes_CollectionChanged;
		}

		void Attributes_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			OnPropertyChanged("Attributes");
		}
		public static bool IsBase64(string base64String)
		{
			try
			{
				Convert.FromBase64String(base64String);
				return true;
			}
			catch { }
			return false;
		}
		public ObservableCollection<XPadElement> Items { get; set; }
		public XElement Element { get; private set; }
		public ObservableCollection<XPadAttribute> Attributes { get; set; }
		private Image _Image = null;
		public Image Image
		{
			get { return _Image; }
			private set
			{
				_Image = value;
				ImageSource = Image.GetImageSource();
			}
		}
		public ImageSource ImageSource { get; private set; }
	}
}
