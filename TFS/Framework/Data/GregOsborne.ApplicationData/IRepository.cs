using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GregOsborne.ApplicationData {
    public interface IRepository {
        bool Connect(params string[] connectionParameters);
        void Open();
        void Close();
    }
}
