using System.ComponentModel;

namespace OptiRampDesignerModel.Concrete
{
    public class OptionItem : IOptionItem
    {
        #region Private Fields

        private object _Value;

        #endregion Private Fields

        #region Public Constructors

        public OptionItem(string name)
        {
            Name = name;
        }

        public OptionItem(string name, object value)
            : this(name)
        {
            Value = value;
        }

        #endregion Public Constructors

        #region Public Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Public Events

        #region Public Properties

        public string Name { get; private set; }

        public object Value {
            get { return _Value; }
            set {
                _Value = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Value"));
            }
        }

        #endregion Public Properties
    }
}