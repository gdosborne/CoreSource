using System;

namespace Life.Savings.Events {
    public delegate void ShowSettingsHandler(object sender, ShowSettingsEventArgs e);

    public class ShowSettingsEventArgs : EventArgs {
    }
}