namespace XPadLib
{
	using System.Xml.Linq;

	public class XPadAttribute : XPadItem
	{
		public XPadAttribute(XAttribute attribute)
		{
			Attribute = attribute;
			Name = attribute.Name.LocalName;
			Value = attribute.Value;
		}
		public XAttribute Attribute { get; private set; }
	}
}
