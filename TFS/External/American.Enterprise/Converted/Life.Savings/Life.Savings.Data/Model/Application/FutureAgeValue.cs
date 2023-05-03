using System.ComponentModel;
using GregOsborne.MVVMFramework;

namespace Life.Savings.Data.Model.Application
{
    public class FutureAgeValue : INotifyPropertyChanged
    {
        private int? _Age;
        public int? Age {
            get => _Age;
            set {
                _Age = value;
                InvokePropertyChanged(nameof(Age));
            }
        }

        private double? _Value;
        public double? Value {
            get => _Value;
            set {
                _Value = value;
                InvokePropertyChanged(nameof(Value));
            }
        }
        private DelegateCommand _ClearItemCommand;
        public DelegateCommand ClearItemCommand => _ClearItemCommand ?? (_ClearItemCommand = new DelegateCommand(ClearItem, ValidateClearItemState));
        private void ClearItem(object state)
        {
            Age = null;
            Value = null;
        }
        private static bool ValidateClearItemState(object state)
        {
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void InvokePropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public override string ToString() {
            var theValue = Value.HasValue ? Value.Value : 0.0;
            return $"{Age},{theValue}";
        }
    }
    public class FutureAgeValueWithEndAge : FutureAgeValue, INotifyPropertyChanged
    {
        public new event PropertyChangedEventHandler PropertyChanged;

        private int? _EndAge;
        public int? EndAge {
            get => _EndAge;
            set {
                _EndAge = value;
                InvokePropertyChanged(nameof(EndAge));
            }
        }
        public override string ToString(){
            return $"{base.ToString()},{EndAge}";
        }
        private DelegateCommand _ClearItemCommand;
        public new DelegateCommand ClearItemCommand => _ClearItemCommand ?? (_ClearItemCommand = new DelegateCommand(ClearItem, ValidateClearItemState));
        private void ClearItem(object state)
        {
            Age = null;
            Value = null;
            EndAge = null;
        }
        private static bool ValidateClearItemState(object state)
        {
            return true;
        }
        public bool IsValid() {
            return Age.HasValue && Age.Value > 0 && EndAge.HasValue && EndAge.Value > Age && Value.HasValue && Value.Value > 0;
        }
    }
}
