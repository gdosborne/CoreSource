using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Applicationn.Text {
    /// <summary>
    /// Text item base
    /// </summary>
    public class TextItemBase {
        /// <summary>
        /// Creates a new instance of <see cref="TextItemBase"/>
        /// </summary>
        public TextItemBase() {
            Data = new StringBuilder();
        }

        /// <summary>
        /// Gets the data
        /// </summary>
        public virtual StringBuilder Data { get; protected set; }
    }
}
