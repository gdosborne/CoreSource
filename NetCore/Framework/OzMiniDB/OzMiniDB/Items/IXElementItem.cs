using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OzMiniDB.Items {
    public interface IXElementItem {
        XElement ToXElement();
    }
}
