/* File="RecentItem"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

using System.Windows.Input;

namespace Common.RecentItems {
    /// <summary>
    /// The recent item.
    /// </summary>
    public class RecentItem {
        /// <summary>
        /// Creates the.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="command">The command.</param>
        /// <param name="sequence">The sequence.</param>
        /// <returns>A RecentItem.</returns>
        public static RecentItem Create(string text, ICommand command, int sequence) =>
            new RecentItem(text, command, sequence);

        /// <summary>
        /// Prevents a default instance of the <see cref="RecentItem"/> class from being created.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="command">The command.</param>
        /// /// <param name="sequence">The sequence.</param>
        private RecentItem(string text, ICommand command, int sequence) {
            Text = text;
            Command = command;
            CommandParameter = text;
            Sequence = sequence;
        }
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Gets or sets the command.
        /// </summary>
        public ICommand Command { get; set; }
        /// <summary>
        /// Gets the command parameter.
        /// </summary>
        public object CommandParameter { get; private set; }
        /// <summary>
        /// Gets or sets the sequence.
        /// </summary>
        public int Sequence { get; set; }
    }
}
