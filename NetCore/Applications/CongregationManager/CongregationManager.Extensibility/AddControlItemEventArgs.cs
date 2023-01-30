using System;
using System.Windows.Input;

namespace CongregationManager.Extensibility {
    public delegate void AddControlItemHandler(object sender, AddControlItemEventArgs e);
    public class AddControlItemEventArgs : EventArgs {
        public enum ControlTypes {
            TopLevelMenuItem = 1,
            MenuItem = 2,
            MenuSeparator = 99,
            ToolbarButton = 100,
            ToolbarLabel = 101,
            ToolbarSeparator = 199
        }

        public AddControlItemEventArgs(ControlTypes controlType, string text, ICommand command, 
                object parent, string itemGlyph) {
            ControlType = controlType;
            Text = text;
            Command = command;
            Parent = parent;
            ItemGlyph = itemGlyph;
        }

        public AddControlItemEventArgs(ControlTypes controlType) {
            if (!controlType.Equals(ControlTypes.MenuSeparator) && !controlType.Equals(ControlTypes.ToolbarSeparator))
                return;
            ControlType = controlType;
        }

        public ControlTypes ControlType { get; private set; }
        public string Text { get; private set; }
        public ICommand Command { get; private set; }
        public object Parent { get; private set; }
        public object ManagableItem { get; set; }
        public string ItemGlyph { get; private set; }
    }
}
