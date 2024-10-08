using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMinistry.Utilities
{
    public class NavigationEventArgs : EventArgs
    {
        #region Public Constructors

        public NavigationEventArgs(Type type)
        {
            Type = type;
        }

        #endregion Public Constructors

        #region Public Properties

        public Type Type { get; private set; }

        #endregion Public Properties
    }
}
