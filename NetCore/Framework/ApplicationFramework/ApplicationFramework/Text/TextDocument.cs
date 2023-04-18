using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.OzApplication.Text {
    /// <summary>
    /// The text dcument
    /// </summary>
    public class TextDocument : TextItemBase {
        /// <summary>
        /// Creates a new instance of <see cref="TextDocument"/>
        /// </summary>
        public TextDocument() {
            Pages = new List<TextPage>();
            Data = new StringBuilder();
        }
            
        /// <summary>
        /// Gets the pages
        /// </summary>
        public List<TextPage> Pages { get; private set; }

        /// <summary>
        /// Gets the data
        /// </summary>
        public override StringBuilder Data {
            get {
                var result = new StringBuilder();
                if (!string.IsNullOrEmpty(Title)) {
                    result.AppendLine(Title);
                    result.AppendLine();
                }
                foreach (var page in Pages) {
                    result.Append(page.Data);
                }
                return result;
            }
            protected set => base.Data = value; 
        }

        /// <summary>
        /// Inserts a blank page
        /// </summary>
        public void InsertBlankPage() {
            AddPage();
            AddPage();
        }

        /// <summary>
        /// Adds a page page
        /// </summary>
        /// <returns>The new page</returns>
        public TextPage AddPage() {
            var result = new TextPage();
            Pages.Add(result);
            return result;
        }

        /// <summary>
        /// Deletes a page
        /// </summary>
        /// <param name="pageNumber"></param>
        public void DeletePage(int pageNumber) {
            if (Pages.Count > pageNumber - 1) {
                Pages.RemoveAt(pageNumber - 1);
            }
        }

        /// <summary>
        /// The title
        /// </summary>
        public string Title { get; set; }
    }
}
