using System.Windows;

namespace GregOsborne.Dialog {
    public sealed class AddressDialog {
        public AddressDialog() {
            Title = "Address";
        }

        public string Address { get; set; }
        public string Title { get; set; }

        public bool? ShowDialog(Window owner) {
            var dlg = new SetAddressDialog();
            if (owner != null) {
                dlg.Owner = owner;
                dlg.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            }
            else {
                dlg.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            }
            dlg.Title = Title;
            dlg.View.Address = Address;
            var result = dlg.ShowDialog();
            if (!result.GetValueOrDefault())
                return result;
            Address = dlg.View.Address;
            dlg.Close();
            return result;
        }

        public void Show() {
            var dlg = new SetAddressDialog {
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                Title = Title
            };
            dlg.View.Address = Address;
            dlg.ShowDialog();
        }
    }
}