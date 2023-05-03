using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GregsTest.Models {
    public class HomeModel : IModel {
        internal HomeModel() {
            InstanceId = Guid.NewGuid();
        }

        private Guid _instanceId = default(Guid);
        public Guid InstanceId { get; private set; }
    }
}