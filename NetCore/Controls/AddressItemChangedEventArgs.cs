using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Controls.Core {
    public class AddressItemChangedEventArgs : TextChangedEventArgs {
        public delegate void AddressItemChangedHandler(object sender, AddressItemChangedEventArgs e);
        public enum AddressParts {
            Address,
            City,
            StateProvence,
            PostalCode
        }

        /// <summary>Gets the AddressPart.</summary>
        /// <value>The AddressPart.</value>
        public AddressParts AddressPart { get; private set; }


        public AddressItemChangedEventArgs(RoutedEvent id, UndoAction action, AddressParts part) 
            : base(id, action) {
            AddressPart = part;
        }

        public AddressItemChangedEventArgs(RoutedEvent id, UndoAction action, ICollection<TextChange> changes, AddressParts part) 
            : base(id, action, changes) {
            AddressPart = part;
        }
    }
}
