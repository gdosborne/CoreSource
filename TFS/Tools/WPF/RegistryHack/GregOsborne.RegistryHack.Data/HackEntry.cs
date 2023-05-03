namespace GregOsborne.RegistryHack.Data
{
    public sealed class HackEntry<T> : HackItemBase
    {
        private T _currentValue;
        private T _desiredValue;
        private HackFolder _parent;

        public HackEntry(HackFolder parent, string name, T value)
        {
            Parent = parent;
            HackItemType = HackTypes.Item;
            Name = name;
            DesiredValue = value;
        }

        public T CurrentValue {
            get {
                return _currentValue;
            }
            set {
                _currentValue = value;
                InvokePropertyChanged(nameof(CurrentValue));
            }
        }

        public T DesiredValue {
            get {
                return _desiredValue;
            }
            set {
                _desiredValue = value;
                InvokePropertyChanged(nameof(DesiredValue));
            }
        }

        public HackFolder Parent {
            get {
                return _parent;
            }
            set {
                _parent = value;
                InvokePropertyChanged(nameof(Parent));
            }
        }
    }
}