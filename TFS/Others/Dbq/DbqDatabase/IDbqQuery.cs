using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbqDatabase {
    public interface IDbqQuery {
        string Name { get; set; }
        string Text { get; set; }
    }
}
