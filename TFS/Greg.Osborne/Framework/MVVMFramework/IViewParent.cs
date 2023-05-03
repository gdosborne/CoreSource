using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GregOsborne.MVVMFramework {
	public interface IViewParent {
		ViewModelBase View {
			get;
		}
	}
}
