using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Media;
using GregOsborne.MVVMFramework;

namespace GregOsborne.Dialog {
    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
    internal class MessageDialogView : INotifyPropertyChanged {
        private string _additionalInformation;
        private Visibility _additionalInfoVisibility;
        private Button _button1;
        private DelegateCommand _button1Command;
        private string _button1Text;
        private Visibility _button1Visibility;
        private double _button1Width;
        private Button _button2;
        private DelegateCommand _button2Command;
        private string _button2Text;
        private Visibility _button2Visibility;
        private double _button2Width;
        private Button _button3;
        private DelegateCommand _button3Command;
        private string _button3Text;
        private Visibility _button3Visibility;
        private double _button3Width;
        private int _buttonValue;
        private bool _isAdditionalInformationExpanded;
        private string _messageText;
        private ImageSource _source;

        public MessageDialogView() {
            Source = TaskDialog.GetImageSourceByName("Information.png");
            Button1Visibility = Visibility.Collapsed;
            Button2Visibility = Visibility.Collapsed;
            Button3Visibility = Visibility.Collapsed;
            AdditionalInfoVisibility = Visibility.Collapsed;
            Button1Width = 60;
            Button2Width = 60;
            Button3Width = 60;
        }

        public int ButtonValue {
            get => _buttonValue;
            set {
                _buttonValue = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ButtonValue"));
            }
        }

        public ImageSource Source {
            get => _source;
            set {
                _source = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Source"));
            }
        }

        public string MessageText {
            get => _messageText;
            set {
                _messageText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MessageText"));
            }
        }

        public double Button1Width {
            get => _button1Width;
            set {
                _button1Width = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Button1Width"));
            }
        }

        public double Button2Width {
            get => _button2Width;
            set {
                _button2Width = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Button2Width"));
            }
        }

        public double Button3Width {
            get => _button3Width;
            set {
                _button3Width = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Button3Width"));
            }
        }

        public Button Button1 {
            get => _button1;
            set {
                _button1 = value;
                Button1Text = value.Text;
                if (value.Width > 0)
                    Button1Width = value.Width;
                Button1Visibility = Visibility.Visible;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Button1"));
            }
        }

        public Button Button2 {
            get => _button2;
            set {
                _button2 = value;
                Button2Text = value.Text;
                if (value.Width > 0)
                    Button2Width = value.Width;
                Button2Visibility = Visibility.Visible;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Button2"));
            }
        }

        public Button Button3 {
            get => _button3;
            set {
                _button3 = value;
                Button3Text = value.Text;
                if (value.Width > 0)
                    Button3Width = value.Width;
                Button3Visibility = Visibility.Visible;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Button3"));
            }
        }

        public string Button1Text {
            get => _button1Text;
            set {
                _button1Text = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Button1Text"));
            }
        }

        public string Button2Text {
            get => _button2Text;
            set {
                _button2Text = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Button2Text"));
            }
        }

        public string Button3Text {
            get => _button3Text;
            set {
                _button3Text = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Button3Text"));
            }
        }

        public Visibility Button1Visibility {
            get => _button1Visibility;
            set {
                _button1Visibility = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Button1Visibility"));
            }
        }

        public Visibility Button2Visibility {
            get => _button2Visibility;
            set {
                _button2Visibility = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Button2Visibility"));
            }
        }

        public Visibility Button3Visibility {
            get => _button3Visibility;
            set {
                _button3Visibility = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Button3Visibility"));
            }
        }

        public DelegateCommand Button1Command => _button1Command ?? (_button1Command = new DelegateCommand(Button1X, ValidateButton1State));

        public DelegateCommand Button2Command => _button2Command ?? (_button2Command = new DelegateCommand(Button2X, ValidateButton2State));

        public DelegateCommand Button3Command => _button3Command ?? (_button3Command = new DelegateCommand(Button3X, ValidateButton3State));

        public string AdditionalInformation {
            get => _additionalInformation;
            set {
                _additionalInformation = value;
                AdditionalInfoVisibility = string.IsNullOrEmpty(_additionalInformation) ? Visibility.Collapsed : Visibility.Visible;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AdditionalInformation"));
            }
        }

        public Visibility AdditionalInfoVisibility {
            get => _additionalInfoVisibility;
            set {
                _additionalInfoVisibility = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AdditionalInfoVisibility"));
            }
        }

        public bool IsAdditionalInformationExpanded {
            get => _isAdditionalInformationExpanded;
            set {
                _isAdditionalInformationExpanded = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsAdditionalInformationExpanded"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void UpdateInterface() {
        }

        public void InitView() {
        }

        private void Button1X(object state) {
            ButtonValue = Button1.CustomValue;
        }

        private static bool ValidateButton1State(object state) {
            return true;
        }

        private void Button2X(object state) {
            ButtonValue = Button2.CustomValue;
        }

        private static bool ValidateButton2State(object state) {
            return true;
        }

        private void Button3X(object state) {
            ButtonValue = Button3.CustomValue;
        }

        private static bool ValidateButton3State(object state) {
            return true;
        }
    }
}