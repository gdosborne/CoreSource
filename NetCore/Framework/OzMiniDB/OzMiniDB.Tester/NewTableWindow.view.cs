using Common.MVVMFramework;

namespace OzMiniDB.Builder {
    public partial class NewTableWindowView : ViewModelBase {
        public NewTableWindowView() {
            Title = "New Table [designer]";
        }

        public override void Initialize() {
            base.Initialize();
            Title = "New Table";

        }

        #region DialogResult Property
        private bool? _DialogResult = default;
        public bool? DialogResult {
            get => _DialogResult;
            set {
                _DialogResult = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region TableName Property
        private string _TableName = default;
        public string TableName {
            get => _TableName;
            set {
                _TableName = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region TableDescription Property
        private string _TableDescription = default;
        public string TableDescription {
            get => _TableDescription;
            set {
                _TableDescription = value;
                OnPropertyChanged();
            }
        }
        #endregion

    }
}
