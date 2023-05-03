using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GregsTest.Models {
    public interface IModel {
        Guid InstanceId { get; }
    }
}
