using Common.Applicationn.Primitives;
using CongregationManager.Data;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CongregationExtension {
    public partial class CongregationControl : UserControl {
        public CongregationControl() {
            InitializeComponent();

            MeetingDayComboBox.Items.Add(DayOfWeek.Sunday);
            MeetingDayComboBox.Items.Add(DayOfWeek.Monday);
            MeetingDayComboBox.Items.Add(DayOfWeek.Tuesday);
            MeetingDayComboBox.Items.Add(DayOfWeek.Wednesday);
            MeetingDayComboBox.Items.Add(DayOfWeek.Thursday);
            MeetingDayComboBox.Items.Add(DayOfWeek.Friday);
            MeetingDayComboBox.Items.Add(DayOfWeek.Saturday);
            MeetingDayComboBox.SelectedItem = DayOfWeek.Sunday;

            var actualHr = 0;
            var actualAP = "AM";
            for (int i = 0; i < 24; i++) {
                actualHr = i;
                if (actualHr > 12) {
                    actualHr -= 12;
                    actualAP = "PM";
                }
                MeetingTimeComboBox.Items.Add(ActualMeetingTime(new TimeSpan(i, 0, 0)));
                MeetingTimeComboBox.Items.Add(ActualMeetingTime(new TimeSpan(i, 15, 0)));
                MeetingTimeComboBox.Items.Add(ActualMeetingTime(new TimeSpan(i, 30, 0)));
                MeetingTimeComboBox.Items.Add(ActualMeetingTime(new TimeSpan(i, 45, 0)));
            }
            MeetingTimeComboBox.SelectedItem = ActualMeetingTime(new TimeSpan(10, 0, 0));
        }

        #region CongregationProperty
        /// <summary>Gets the Congregation dependency property.</summary>
        /// <value>The Congregation dependency property.</value>
        public static readonly DependencyProperty CongregationProperty = DependencyProperty.Register("Congregation", typeof(Congregation), typeof(CongregationControl), new PropertyMetadata(default(Congregation), OnCongregationPropertyChanged));
        /// <summary>Gets/sets the Congregation.</summary>
        /// <value>The Congregation.</value>
        public Congregation Congregation {
            get => (Congregation)GetValue(CongregationProperty);
            set => SetValue(CongregationProperty, value);
        }
        private static void OnCongregationPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (CongregationControl)d;
            var val = (Congregation)e.NewValue;
            if (val != null) {
                obj.CongregationNameTextBox.Text = val.Name;
                obj.AddressTextBox.Text = val.Address;
                obj.CityTextBox.Text = val.City;
                obj.StateProvenceTextBox.Text = val.StateProvence;
                obj.PostalCodeTextBox.Text = val.PostalCode;
                obj.TelephoneTextBox.Text = val.Telephone;
                obj.IsLocalCheckBox.IsChecked = val.IsLocal;
                obj.MeetingDayComboBox.SelectedItem = val.MeetingDay;
                obj.MeetingTimeComboBox.SelectedItem = ActualMeetingTime(val.MeetingTime);
            }
        }
        #endregion

        private static string ActualMeetingTime(TimeSpan ts) {
            var pattern = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortTimePattern;
            var dt = DateTime.Now.Date.Add(ts);
            return dt.ToString(pattern);
        }

        private void TextBoxGotFocus(object sender, RoutedEventArgs e) {
            sender.As<TextBox>().SelectAll();
        }

        #region UpdateCommandProperty
        /// <summary>Gets the UpdateCommand dependency property.</summary>
        /// <value>The UpdateCommand dependency property.</value>
        public static readonly DependencyProperty UpdateCommandProperty = DependencyProperty.Register("UpdateCommand", typeof(ICommand), typeof(CongregationControl), new PropertyMetadata(default(ICommand), OnUpdateCommandPropertyChanged));
        /// <summary>Gets/sets the UpdateCommand.</summary>
        /// <value>The UpdateCommand.</value>
        public ICommand UpdateCommand {
            get => (ICommand)GetValue(UpdateCommandProperty);
            set => SetValue(UpdateCommandProperty, value);
        }
        private static void OnUpdateCommandPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (CongregationControl)d;
            var val = (ICommand)e.NewValue;
            obj.UpdateButton.Command = val;
        }
        #endregion

        #region RevertCommandProperty
        /// <summary>Gets the RevertCommand dependency property.</summary>
        /// <value>The RevertCommand dependency property.</value>
        public static readonly DependencyProperty RevertCommandProperty = DependencyProperty.Register("RevertCommand", typeof(ICommand), typeof(CongregationControl), new PropertyMetadata(default(ICommand), OnRevertCommandPropertyChanged));
        /// <summary>Gets/sets the RevertCommand.</summary>
        /// <value>The RevertCommand.</value>
        public ICommand RevertCommand {
            get => (ICommand)GetValue(RevertCommandProperty);
            set => SetValue(RevertCommandProperty, value);
        }
        private static void OnRevertCommandPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (CongregationControl)d;
            var val = (ICommand)e.NewValue;
            obj.RevertButton.Command = val;
        }
        #endregion


        #region EditCommandProperty
        /// <summary>Gets the EditCommand dependency property.</summary>
        /// <value>The EditCommand dependency property.</value>
        public static readonly DependencyProperty EditCommandProperty = DependencyProperty.Register("EditCommand", typeof(ICommand), typeof(CongregationControl), new PropertyMetadata(default(ICommand), OnEditCommandPropertyChanged));
        /// <summary>Gets/sets the EditCommand.</summary>
        /// <value>The EditCommand.</value>
        public ICommand EditCommand {
            get => (ICommand)GetValue(EditCommandProperty);
            set => SetValue(EditCommandProperty, value);
        }
        private static void OnEditCommandPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (CongregationControl)d;
            var val = (ICommand)e.NewValue;
            obj.EditButton.Command = val;
        }
        #endregion


        private void TextBoxTextChanged(object sender, TextChangedEventArgs e) {
            if (sender == CongregationNameTextBox) {
                Congregation.Name = sender.As<TextBox>().Text;
            }
            else if (sender == TelephoneTextBox) {
                Congregation.Telephone = sender.As<TextBox>().Text;
            }
            else if (sender == AddressTextBox) {
                Congregation.Address = sender.As<TextBox>().Text;
            }
            else if (sender == CityTextBox) {
                Congregation.City = sender.As<TextBox>().Text;
            }
            else if (sender == StateProvenceTextBox) {
                Congregation.StateProvence = sender.As<TextBox>().Text;
            }
            else if (sender == PostalCodeTextBox) {
                Congregation.PostalCode = sender.As<TextBox>().Text;
            }
        }

        #region ButtonVisibilityProperty
        /// <summary>Gets the ButtonVisibility dependency property.</summary>
        /// <value>The ButtonVisibility dependency property.</value>
        public static readonly DependencyProperty ButtonVisibilityProperty = DependencyProperty.Register("ButtonVisibility", typeof(Visibility), typeof(CongregationControl), new PropertyMetadata(Visibility.Visible, OnButtonVisibilityPropertyChanged));
        /// <summary>Gets/sets the ButtonVisibility.</summary>
        /// <value>The ButtonVisibility.</value>
        public Visibility ButtonVisibility {
            get => (Visibility)GetValue(ButtonVisibilityProperty);
            set => SetValue(ButtonVisibilityProperty, value);
        }
        private static void OnButtonVisibilityPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (CongregationControl)d;
            var val = (Visibility)e.NewValue;
            obj.ButtonPanel.Visibility = val;
        }
        #endregion

        private void IsLocalCheckBox_Checked(object sender, RoutedEventArgs e) {
            if (Congregation != null) {
                Congregation.IsLocal = true;
            }
        }

        private void IsLocalCheckBox_Unchecked(object sender, RoutedEventArgs e) {
            if (Congregation != null) {
                Congregation.IsLocal = false;
            }
        }

        private void MeetingDayComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (Congregation != null) {
                Congregation.MeetingDay = (DayOfWeek)sender.As<ComboBox>().SelectedItem;
            }
        }

        private void MeetingTimeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (Congregation != null) {
                Congregation.MeetingTime = ConvertToTimeSpan(sender.As<ComboBox>().SelectedItem.CastTo<string>());
            }
        }

        private TimeSpan ConvertToTimeSpan(string value) {
            if (value == null) {
                return TimeSpan.Zero;
            }
            var val = DateTime.Parse(value);
            return val.TimeOfDay;
        }
    }
}
