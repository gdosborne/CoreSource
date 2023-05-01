using System.Text;

namespace Common.AppFramework.Text {
    /// <summary>
    /// The text paragraph.
    /// </summary>
    public class TextParagraph : TextItemBase {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextParagraph"/> class.
        /// </summary>
        public TextParagraph() { }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="TextParagraph"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        public TextParagraph(string text) {
            Data = new StringBuilder(text);
        }

        /// <summary>
        /// Gets or sets a value indicating whether add space is after.
        /// </summary>
        public bool IsAddSpaceAfter { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether add space is before.
        /// </summary>
        public bool IsAddSpaceBefore { get; set; } = false;
    }
}
