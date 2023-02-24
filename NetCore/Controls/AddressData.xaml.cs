using Common.Application.Primitives;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using static Controls.Core.AddressItemChangedEventArgs;

namespace Controls.Core {
    public partial class AddressData : UserControl {
        public AddressData() {
            InitializeComponent();
        }

        public event AddressItemChangedHandler AddressItemChanged;

        #region AddressProperty
        /// <summary>Gets the Address dependency property.</summary>
        /// <value>The Address dependency property.</value>
        public static readonly DependencyProperty AddressProperty = DependencyProperty.Register("Address", typeof(string), typeof(AddressData), new PropertyMetadata(default(string), OnAddressPropertyChanged));
        /// <summary>Gets/sets the Address.</summary>
        /// <value>The Address.</value>
        public string Address {
            get => (string)GetValue(AddressProperty);
            set => SetValue(AddressProperty, value);
        }
        private static void OnAddressPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (AddressData)d;
            var val = (string)e.NewValue;
            obj.AddressTextBox.Text = val;
        }
        #endregion

        #region CityProperty
        /// <summary>Gets the City dependency property.</summary>
        /// <value>The City dependency property.</value>
        public static readonly DependencyProperty CityProperty = DependencyProperty.Register("City", typeof(string), typeof(AddressData), new PropertyMetadata(default(string), OnCityPropertyChanged));
        /// <summary>Gets/sets the City.</summary>
        /// <value>The City.</value>
        public string City {
            get => (string)GetValue(CityProperty);
            set => SetValue(CityProperty, value);
        }
        private static void OnCityPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (AddressData)d;
            var val = (string)e.NewValue;
            obj.CityTextBox.Text = val;
        }
        #endregion

        #region StateProvenceProperty
        /// <summary>Gets the StateProvence dependency property.</summary>
        /// <value>The StateProvence dependency property.</value>
        public static readonly DependencyProperty StateProvenceProperty = DependencyProperty.Register("StateProvence", typeof(string), typeof(AddressData), new PropertyMetadata(default(string), OnStateProvencePropertyChanged));
        /// <summary>Gets/sets the StateProvence.</summary>
        /// <value>The StateProvence.</value>
        public string StateProvence {
            get => (string)GetValue(StateProvenceProperty);
            set => SetValue(StateProvenceProperty, value);
        }
        private static void OnStateProvencePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (AddressData)d;
            var val = (string)e.NewValue;
            obj.StateProvenceTextBox.Text = val;
        }
        #endregion

        #region PostalCodeProperty
        /// <summary>Gets the POstalCode dependency property.</summary>
        /// <value>The POstalCode dependency property.</value>
        public static readonly DependencyProperty PostalCodeProperty = DependencyProperty.Register("PostalCode", typeof(string), typeof(AddressData), new PropertyMetadata(default(string), OnPostalCodePropertyChanged));
        /// <summary>Gets/sets the POstalCode.</summary>
        /// <value>The POstalCode.</value>
        public string PostalCode {
            get => (string)GetValue(PostalCodeProperty);
            set => SetValue(PostalCodeProperty, value);
        }
        private static void OnPostalCodePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (AddressData)d;
            var val = (string)e.NewValue;
            obj.PostalCodeTextBox.Text = val;
        }
        #endregion

        #region TextBoxStyleProperty
        /// <summary>Gets the TextBoxStyle dependency property.</summary>
        /// <value>The TextBoxStyle dependency property.</value>
        public static readonly DependencyProperty TextBoxStyleProperty = DependencyProperty.Register("TextBoxStyle", typeof(Style), typeof(AddressData), new PropertyMetadata(default(Style), OnTextBoxStylePropertyChanged));
        /// <summary>Gets/sets the TextBoxStyle.</summary>
        /// <value>The TextBoxStyle.</value>
        public Style TextBoxStyle {
            get => (Style)GetValue(TextBoxStyleProperty);
            set => SetValue(TextBoxStyleProperty, value);
        }
        private static void OnTextBoxStylePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (AddressData)d;
            var val = (Style)e.NewValue;
            obj.PostalCodeTextBox.Style= val;
            obj.StateProvenceTextBox.Style= val;
            obj.CityTextBox.Style= val;
            obj.AddressTextBox.Style= val;
        }
        #endregion

        #region TextBlockStyleProperty
        /// <summary>Gets the TextBlockStyle dependency property.</summary>
        /// <value>The TextBlockStyle dependency property.</value>
        public static readonly DependencyProperty TextBlockStyleProperty = DependencyProperty.Register("TextBlockStyle", typeof(Style), typeof(AddressData), new PropertyMetadata(default(Style), OnTextBlockStylePropertyChanged));
        /// <summary>Gets/sets the TextBlockStyle.</summary>
        /// <value>The TextBlockStyle.</value>
        public Style TextBlockStyle {
            get => (Style)GetValue(TextBlockStyleProperty);
            set => SetValue(TextBlockStyleProperty, value);
        }
        private static void OnTextBlockStylePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (AddressData)d;
            var val = (Style)e.NewValue;
            obj.AddressTextBlock.Style = val;
            obj.CityTextBlock.Style = val;
            obj.StateProvenceTextBlock.Style = val;
            obj.PostalCodeTextBlock.Style = val;
        }
        #endregion

        #region AddressLabelProperty
        /// <summary>Gets the AddressLabel dependency property.</summary>
        /// <value>The AddressLabel dependency property.</value>
        public static readonly DependencyProperty AddressLabelProperty = DependencyProperty.Register("AddressLabel", typeof(string), typeof(AddressData), new PropertyMetadata(default(string), OnAddressLabelPropertyChanged));
        /// <summary>Gets/sets the AddressLabel.</summary>
        /// <value>The AddressLabel.</value>
        public string AddressLabel {
            get => (string)GetValue(AddressLabelProperty);
            set => SetValue(AddressLabelProperty, value);
        }
        private static void OnAddressLabelPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (AddressData)d;
            var val = (string)e.NewValue;
            obj.AddressTextBlock.Text = val;
        }
        #endregion

        #region CityLabelProperty
        /// <summary>Gets the CityLabel dependency property.</summary>
        /// <value>The CityLabel dependency property.</value>
        public static readonly DependencyProperty CityLabelProperty = DependencyProperty.Register("CityLabel", typeof(string), typeof(AddressData), new PropertyMetadata(default(string), OnCityLabelPropertyChanged));
        /// <summary>Gets/sets the CityLabel.</summary>
        /// <value>The CityLabel.</value>
        public string CityLabel {
            get => (string)GetValue(CityLabelProperty);
            set => SetValue(CityLabelProperty, value);
        }
        private static void OnCityLabelPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (AddressData)d;
            var val = (string)e.NewValue;
            obj.CityTextBlock.Text = val;
        }
        #endregion

        #region StateProvenceLabelProperty
        /// <summary>Gets the StateProvenceLabel dependency property.</summary>
        /// <value>The StateProvenceLabel dependency property.</value>
        public static readonly DependencyProperty StateProvenceLabelProperty = DependencyProperty.Register("StateProvenceLabel", typeof(string), typeof(AddressData), new PropertyMetadata(default(string), OnStateProvenceLabelPropertyChanged));
        /// <summary>Gets/sets the StateProvenceLabel.</summary>
        /// <value>The StateProvenceLabel.</value>
        public string StateProvenceLabel {
            get => (string)GetValue(StateProvenceLabelProperty);
            set => SetValue(StateProvenceLabelProperty, value);
        }
        private static void OnStateProvenceLabelPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (AddressData)d;
            var val = (string)e.NewValue;
            obj.StateProvenceTextBlock.Text = val;
        }
        #endregion

        #region PostalCodeLabelProperty
        /// <summary>Gets the PostalCodeLabel dependency property.</summary>
        /// <value>The PostalCodeLabel dependency property.</value>
        public static readonly DependencyProperty PostalCodeLabelProperty = DependencyProperty.Register("PostalCodeLabel", typeof(string), typeof(AddressData), new PropertyMetadata(default(string), OnPostalCodeLabelPropertyChanged));
        /// <summary>Gets/sets the PostalCodeLabel.</summary>
        /// <value>The PostalCodeLabel.</value>
        public string PostalCodeLabel {
            get => (string)GetValue(PostalCodeLabelProperty);
            set => SetValue(PostalCodeLabelProperty, value);
        }
        private static void OnPostalCodeLabelPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (AddressData)d;
            var val = (string)e.NewValue;
            obj.PostalCodeTextBlock.Text = val;
        }
        #endregion

        private void AddressItemTextChanged(object sender, TextChangedEventArgs e) {
            if (sender == AddressTextBox)
                AddressItemChanged?.Invoke(this,
                    new AddressItemChangedEventArgs(e.RoutedEvent, e.UndoAction, AddressParts.Address));
            else if(sender == CityTextBox)
                AddressItemChanged?.Invoke(this,
                    new AddressItemChangedEventArgs(e.RoutedEvent, e.UndoAction, AddressParts.City));
            else if (sender == StateProvenceTextBox)
                AddressItemChanged?.Invoke(this,
                    new AddressItemChangedEventArgs(e.RoutedEvent, e.UndoAction, AddressParts.StateProvence));
            else if (sender == PostalCodeTextBox)
                AddressItemChanged?.Invoke(this,
                    new AddressItemChangedEventArgs(e.RoutedEvent, e.UndoAction, AddressParts.PostalCode));
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e) {
            if (sender == AddressTextBox)
                return;
            sender.As<TextBox>().SelectAll();
        }
    }
}
