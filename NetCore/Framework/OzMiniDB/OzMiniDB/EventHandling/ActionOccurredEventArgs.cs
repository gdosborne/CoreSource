using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzMiniDB.EventHandling {
    public delegate void ActionOccurredHandler(object sender, ActionOccurredEventArgs e);
    public class ActionOccurredEventArgs : EventArgs {
        public enum ActionTypes {
            DBEngineCreated,
            ClassAddStart,
            ClassAddEnd,
            PropertyCreated
        }
        public ActionOccurredEventArgs(ActionTypes action, string itemName, string additionalInfo, int indent) {
            Action = action;
            ItemName = itemName;
            AdditionalInfo = additionalInfo;
            Indent = indent;
        }
        public ActionOccurredEventArgs(ActionTypes action, string itemName, int indent)
            : this(action, itemName, default, indent) { }

        public ActionTypes Action { get; private set; }

        public string ItemName { get; private set; }
        public string AdditionalInfo { get; private set; }
        public int Indent { get; private set; }

    }
}
