using System;
using Life.Savings.Data.Model;

namespace Life.Savings.Events {
    public delegate void WarnClientExistsHandler(object sender, WarnClientExistsEventArgs e);
    public class WarnClientExistsEventArgs : EventArgs {
        public WarnClientExistsEventArgs(Client client) {
            Client = client;
        }

        public Client Client { get; }
        public bool Answer { get; set; }
    }
}