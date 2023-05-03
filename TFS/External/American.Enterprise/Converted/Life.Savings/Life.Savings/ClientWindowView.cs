using System.Collections.ObjectModel;
using System.Reflection;
using GregOsborne.MVVMFramework;
using Life.Savings.Data;
using Life.Savings.Data.Model;
using System.Linq;
using Life.Savings.Events;

namespace Life.Savings
{
    public sealed class ClientWindowView : ViewModelBase
    {
        private ObservableCollection<Client> _clients;
        private bool? _dialogResult;
        private DelegateCommand _exitCommand;
        private IRepository _repository;
        private DelegateCommand _selectClientCommand;
        private Client _selectedClient;
        public event DeleteClientHandler DeleteTheClient;

        public ClientWindowView()
        {
            UpdateInterface();
        }
        public IRepository Repository {
            get => _repository;
            set {
                _repository = value;
                if (value != null)
                    Clients = new ObservableCollection<Client>(Repository.Clients.OrderBy(x => x.ClientData.LastName).ThenBy(x => x.ClientData.FirstName));
                InvokePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
            }
        }
        private DelegateCommand _DeleteClientCommand;
        public DelegateCommand DeleteClientCommand => _DeleteClientCommand ?? (_DeleteClientCommand = new DelegateCommand(DeleteClient, ValidateDeleteClientState));
        private void DeleteClient(object state)
        {
            var e = new DeleteClientEventArgs();
            DeleteTheClient?.Invoke(this, e);
            if (!e.IsContinueDelete)
                return;
            var clientId = SelectedClient.Id;
            Clients.Remove(SelectedClient);
            App.Repository.Clients.Remove(App.Repository.Clients.FirstOrDefault(x => x.Id == clientId));
            App.Repository.SaveClients();
        }
        private bool ValidateDeleteClientState(object state)
        {
            return SelectedClient != null;
        }

        public ObservableCollection<Client> Clients {
            get => _clients;
            set {
                _clients = value;
                InvokePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
            }
        }

        public Client SelectedClient {
            get => _selectedClient;
            set {
                _selectedClient = value;
                UpdateInterface();
                InvokePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
            }
        }

        public DelegateCommand SelectClientCommand => _selectClientCommand ?? (_selectClientCommand = new DelegateCommand(SelectClient, ValidateSelectClientState));
        public DelegateCommand ExitCommand => _exitCommand ?? (_exitCommand = new DelegateCommand(Exit, ValidateExitState));

        public bool? DialogResult {
            get => _dialogResult;
            set {
                _dialogResult = value;
                InvokePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
            }
        }

        private void SelectClient(object state)
        {
            DialogResult = true;
        }

        private bool ValidateSelectClientState(object state)
        {
            return SelectedClient != null;
        }

        private void Exit(object state)
        {
            DialogResult = false;
        }

        private static bool ValidateExitState(object state)
        {
            return true;
        }
    }
}