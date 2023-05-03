using System.Xml.Linq;

namespace Life.Savings.Data.Model
{
    public interface IToXElement
    {
        XElement ToXElement();
    }
    public interface IToNamedXElement
    {
        XElement ToXElement(string name);
    }
}
