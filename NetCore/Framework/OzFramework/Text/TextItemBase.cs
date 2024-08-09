/* File="TextItemBase"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Text {
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
