using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Controls.Core {
    public partial class AddressData : UserControl {
        public AddressData() {
            InitializeComponent();
        }


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
            var obj = (Address)d;
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

        #region POstalCodeProperty
        /// <summary>Gets the POstalCode dependency property.</summary>
        /// <value>The POstalCode dependency property.</value>
        public static readonly DependencyProperty POstalCodeProperty = DependencyProperty.Register("POstalCode", typeof(string), typeof(AddressData), new PropertyMetadata(default(string), OnPOstalCodePropertyChanged));
        /// <summary>Gets/sets the POstalCode.</summary>
        /// <value>The POstalCode.</value>
        public string POstalCode {
            get => (string)GetValue(POstalCodeProperty);
            set => SetValue(POstalCodeProperty, value);
        }
        private static void OnPOstalCodePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (AddressData)d;
            var val = (string)e.NewValue;
            obj.PostalCodeTextBox.Text = val;
        }
        #endregion

    }
}
