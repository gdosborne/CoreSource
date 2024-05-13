/* File="TextPage"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzFramework.Text {
    /// <summary>
    /// Text page
    /// </summary>
    public class TextPage : TextItemBase {
        /// <summary>
        /// Initializes a new instance of <see cref="TextPage"/>
        /// </summary>
        public TextPage()
            :this(1) { }

        /// <summary>
        /// Initializes a new instance of <see cref="TextPage"/>
        /// </summary>
        /// <param name="startPageNumber">The start page number</param>
        public TextPage(int startPageNumber) {
            Number = startPageNumber;
            Paragraphs = new List<TextParagraph>();
        }

        /// <summary>
        /// Gets the page number
        /// </summary>
        public int Number { get; private set; }

        
        /// <summary>
        /// Gets the paragraphs
        /// </summary>
        public List<TextParagraph> Paragraphs { get; private set; }

        /// <summary>
        /// Gets the data
        /// </summary>
        public override StringBuilder Data {
            get {
                var result = new StringBuilder();
                foreach (var para in Paragraphs) {
                    if (para.IsAddSpaceBefore) {
                        result.AppendLine();
                    }

                    result.AppendLine(para.Data.ToString());
                    
                    if (para.IsAddSpaceAfter) {
                        result.AppendLine();
                    }
                }
                return result;
            }
            protected set => base.Data = value;
        }
    }
}
