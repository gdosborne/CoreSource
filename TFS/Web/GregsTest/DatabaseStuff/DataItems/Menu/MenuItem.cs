namespace DatabaseStuff.DataItems.Menu {
    using System;

    public sealed class MenuItem {
        public MenuItem(string text, Uri uri) {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException("text");
            Text = text;
            Uri = uri;
        }
        public MenuItem(string text, string uri)
            : this(text, new Uri(uri)) {

        }
        public MenuItem(string text)
            : this(text, (string)null) {

        }
        public string Text { get; private set; }
        public Uri Uri { get; private set; }
        public override string ToString() => $"{Text}{(Uri == null ? string.Empty : $" ({Uri.ToString()})")}";
    }
}