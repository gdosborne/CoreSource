using OzFramework.Primitives;
using OzFramework.Timers;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

using static OzMiniDB.Items.Field;

namespace OzMiniDB.Builder.Controls {
    public partial class DataFieldEditor : UserControl {
        public DataFieldEditor() {
            InitializeComponent();

            NameTextBox.GotFocus += (s, e) => {
                s.As<TextBox>().SelectAll();
            };

            DataTypeComboBox.SelectionChanged += (s, e) => {
                FieldDataType = (DBDataType)DataTypeComboBox.SelectedItem;
            };

            NameTextBox.TextChanged += (s, e) => {
                FieldName = s.As<TextBox>().Text;
            };

            LengthTextBox.ValueEntered += (s, e) => {
                FieldLength = e.Value.CastTo<int>();
            };

            IsRequiredCheckBox.Checked += (s, e) => {
                FieldIsRequired = true;
            };

            IsRequiredCheckBox.Unchecked += (s, e) => {
                FieldIsRequired = false;
            };

            IsIdentityCheckBox.Checked += (s, e) => {
                FieldIsIdentity = true;
            };

            IsIdentityCheckBox.Unchecked += (s, e) => {
                FieldIsIdentity = false;
            };

            this.GotFocus += (s, e) => {
                BackBorder.Background = FocusedBackground;
                BackBorder.BorderThickness = new Thickness(1);
                IsSelected = true;
            };

            this.LostFocus += (s, e) => {
                BackBorder.Background = defaultBackgroundBrush;
                BackBorder.BorderThickness = new Thickness(0);
            };
        }

        public void Select() {
            TimeSpan.FromMilliseconds(100).GetAutoStartStopTimer().Tick += (s, e) => {
                NameTextBox.Focus();
            };
        }

        #region Foreground Dependency Property
        public static new readonly DependencyProperty ForegroundProperty = DependencyProperty.Register("Foreground", typeof(Brush), typeof(DataFieldEditor), new PropertyMetadata(default(Brush), new PropertyChangedCallback(OnForegroundChanged)));
        public new Brush Foreground {
            get { return (Brush)GetValue(ForegroundProperty); }
            set { SetValue(ForegroundProperty, value); }
        }
        private static void OnForegroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (DataFieldEditor)d;
            var val = (Brush)e.NewValue;
            obj.NameLabel.Foreground = val;
            obj.DataTypeLabel.Foreground = val;
            obj.LengthLabel.Foreground = val;
            obj.IsRequiredLabel.Foreground = val;
            obj.IsIdentityLabel.Foreground = val;

            obj.NameTextBox.Foreground = val;
            obj.DataTypeComboBox.Foreground = val;
            obj.LengthTextBox.Foreground = val;
            obj.IsRequiredCheckBox.Foreground = val;
            obj.IsIdentityCheckBox.Foreground = val;
        }
        #endregion

        #region FontFamily Dependency Property
        public static new readonly DependencyProperty FontFamilyProperty = DependencyProperty.Register("FontFamily", typeof(FontFamily), typeof(DataFieldEditor), new PropertyMetadata(default(FontFamily), new PropertyChangedCallback(OnFontFamilyChanged)));
        public new FontFamily FontFamily {
            get { return (FontFamily)GetValue(FontFamilyProperty); }
            set { SetValue(FontFamilyProperty, value); }
        }
        private static void OnFontFamilyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (DataFieldEditor)d;
            var val = (FontFamily)e.NewValue;
            obj.NameLabel.FontFamily = val;
            obj.DataTypeLabel.FontFamily = val;
            obj.LengthLabel.FontFamily = val;
            obj.IsRequiredLabel.FontFamily = val;
            obj.IsIdentityLabel.FontFamily = val;

            obj.NameTextBox.FontFamily = val;
            obj.DataTypeComboBox.FontFamily = val;
            obj.LengthTextBox.FontFamily = val;
            obj.IsRequiredCheckBox.FontFamily = val;
            obj.IsIdentityCheckBox.FontFamily = val;

        }
        #endregion

        #region Background Dependency Property
        private Brush defaultBackgroundBrush = default;
        public static new readonly DependencyProperty BackgroundProperty = DependencyProperty.Register("Background", typeof(Brush), typeof(DataFieldEditor), new PropertyMetadata(default(Brush), new PropertyChangedCallback(OnBackgroundChanged)));
        public new Brush Background {
            get { return (Brush)GetValue(BackgroundProperty); }
            set { SetValue(BackgroundProperty, value); }
        }
        private static void OnBackgroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (DataFieldEditor)d;
            var val = (Brush)e.NewValue;
            obj.defaultBackgroundBrush ??= val;
            obj.BackBorder.Background = val;
            obj.Background = val;
            obj.NameLabel.Background = val;
            obj.DataTypeLabel.Background = val;
            obj.LengthLabel.Background = val;
            obj.IsRequiredLabel.Background = val;
            obj.IsIdentityLabel.Background = val;

            obj.NameTextBox.Background = val;
            obj.IsRequiredCheckBox.Background = val;
            obj.IsIdentityCheckBox.Background = val;
        }
        #endregion

        #region LabelsVisibility Dependency Property
        public static readonly DependencyProperty LabelsVisibilityProperty = DependencyProperty.Register("LabelsVisibility", typeof(Visibility), typeof(DataFieldEditor), new PropertyMetadata(default(Visibility), new PropertyChangedCallback(OnLabelsVisibilityChanged)));
        public Visibility LabelsVisibility {
            get { return (Visibility)GetValue(LabelsVisibilityProperty); }
            set { SetValue(LabelsVisibilityProperty, value); }
        }
        private static void OnLabelsVisibilityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (DataFieldEditor)d;
            var val = (Visibility)e.NewValue;
            obj.NameLabel.Visibility = val;
            obj.DataTypeLabel.Visibility = val;
            obj.LengthLabel.Visibility = val;
            obj.IsRequiredLabel.Visibility = val;
            obj.IsIdentityLabel.Visibility = val;
        }
        #endregion

        #region FontSize Dependency Property
        public static new readonly DependencyProperty FontSizeProperty = DependencyProperty.Register("FontSize", typeof(double), typeof(DataFieldEditor), new PropertyMetadata(default(double), new PropertyChangedCallback(OnFontSizeChanged)));
        public new double FontSize {
            get { return (double)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }
        private static void OnFontSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (DataFieldEditor)d;
            var val = (double)e.NewValue;
            obj.NameLabel.FontSize = val;
            obj.DataTypeLabel.FontSize = val;
            obj.LengthLabel.FontSize = val;
            obj.IsRequiredLabel.FontSize = val;
            obj.IsIdentityLabel.FontSize = val;

            obj.NameTextBox.FontSize = val;
            obj.DataTypeComboBox.FontSize = val;
            obj.LengthTextBox.FontSize = val;
            obj.IsRequiredCheckBox.FontSize = val;
            obj.IsIdentityCheckBox.FontSize = val;
        }
        #endregion

        #region DataTpe Dependency Property
        public static readonly DependencyProperty DataTpeProperty = DependencyProperty.Register("DataTpe", typeof(DBDataType), typeof(DataFieldEditor), new PropertyMetadata(default(DBDataType), new PropertyChangedCallback(OnDataTpeChanged)));
        public DBDataType DataTpe {
            get { return (DBDataType)GetValue(DataTpeProperty); }
            set { SetValue(DataTpeProperty, value); }
        }
        private static void OnDataTpeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (DataFieldEditor)d;
            var val = (DBDataType)e.NewValue;
            obj.DataTypeComboBox.SelectedItem = val;
        }
        #endregion

        #region DataTypeItemSource Dependency Property
        public static readonly DependencyProperty DataTypeItemSourceProperty = DependencyProperty.Register("DataTypeItemSource", typeof(IEnumerable<DBDataType>), typeof(DataFieldEditor), new PropertyMetadata(default(IEnumerable<DBDataType>), new PropertyChangedCallback(OnDataTypeItemSourceChanged)));
        public IEnumerable<DBDataType> DataTypeItemSource {
            get { return (IEnumerable<DBDataType>)GetValue(DataTypeItemSourceProperty); }
            set { SetValue(DataTypeItemSourceProperty, value); }
        }
        private static void OnDataTypeItemSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (DataFieldEditor)d;
            var val = (IEnumerable<DBDataType>)e.NewValue;
            obj.DataTypeComboBox.ItemsSource = val;
        }
        #endregion

        #region FieldName Dependency Property
        public static readonly DependencyProperty FieldNameProperty = DependencyProperty.Register("FieldName", typeof(string), typeof(DataFieldEditor), new PropertyMetadata(default(string), new PropertyChangedCallback(OnFieldNameChanged)));
        public string FieldName {
            get { return (string)GetValue(FieldNameProperty); }
            set { SetValue(FieldNameProperty, value); }
        }
        private static void OnFieldNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (DataFieldEditor)d;
            var val = (string)e.NewValue;
            obj.NameTextBox.Text = val;
        }
        #endregion

        #region FieldDataType Dependency Property
        public static readonly DependencyProperty FieldDataTypeProperty = DependencyProperty.Register("FieldDataType", typeof(DBDataType), typeof(DataFieldEditor), new PropertyMetadata(default(DBDataType), new PropertyChangedCallback(OnFieldDataTypeChanged)));
        public DBDataType FieldDataType {
            get { return (DBDataType)GetValue(FieldDataTypeProperty); }
            set { SetValue(FieldDataTypeProperty, value); }
        }
        private static void OnFieldDataTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (DataFieldEditor)d;
            var val = (DBDataType)e.NewValue;
            obj.DataTypeComboBox.SelectedItem = val;
            obj.LengthTextBox.IsEnabled = new DBDataType[] { DBDataType.String, DBDataType.FixedString }.Contains(val);
            if (obj.LengthTextBox.IsEnabled) {
                obj.LengthTextBox.Focus();
            }
        }
        #endregion

        #region FieldLength Dependency Property
        public static readonly DependencyProperty FieldLengthProperty = DependencyProperty.Register("FieldLength", typeof(int), typeof(DataFieldEditor), new PropertyMetadata(default(int), new PropertyChangedCallback(OnFieldLengthChanged)));
        public int FieldLength {
            get { return (int)GetValue(FieldLengthProperty); }
            set { SetValue(FieldLengthProperty, value); }
        }
        private static void OnFieldLengthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (DataFieldEditor)d;
            var val = (int)e.NewValue;
            obj.LengthTextBox.Text = val == 0 ? string.Empty : val.ToString();
        }
        #endregion

        #region FieldIsRequired Dependency Property
        public static readonly DependencyProperty FieldIsRequiredProperty = DependencyProperty.Register("FieldIsRequired", typeof(bool), typeof(DataFieldEditor), new PropertyMetadata(default(bool), new PropertyChangedCallback(OnFieldIsRequiredChanged)));
        public bool FieldIsRequired {
            get { return (bool)GetValue(FieldIsRequiredProperty); }
            set { SetValue(FieldIsRequiredProperty, value); }
        }
        private static void OnFieldIsRequiredChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (DataFieldEditor)d;
            var val = (bool)e.NewValue;
            obj.IsRequiredCheckBox.IsChecked = val;
        }
        #endregion

        #region FieldIsIdentity Dependency Property
        public static readonly DependencyProperty FieldIsIdentityProperty = DependencyProperty.Register("FieldIsIdentity", typeof(bool), typeof(DataFieldEditor), new PropertyMetadata(default(bool), new PropertyChangedCallback(OnFieldIsIdentityChanged)));
        public bool FieldIsIdentity {
            get { return (bool)GetValue(FieldIsIdentityProperty); }
            set { SetValue(FieldIsIdentityProperty, value); }
        }
        private static void OnFieldIsIdentityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (DataFieldEditor)d;
            var val = (bool)e.NewValue;
            obj.IsIdentityCheckBox.IsChecked = val;
        }
        #endregion

        #region FocusedBackground Dependency Property
        public static readonly DependencyProperty FocusedBackgroundProperty = DependencyProperty.Register("FocusedBackground", typeof(Brush), typeof(DataFieldEditor), new PropertyMetadata(default(Brush), new PropertyChangedCallback(OnFocusedBackgroundChanged)));
        public Brush FocusedBackground {
            get { return (Brush)GetValue(FocusedBackgroundProperty); }
            set { SetValue(FocusedBackgroundProperty, value); }
        }
        private static void OnFocusedBackgroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (DataFieldEditor)d;
            var val = (Brush)e.NewValue;
            //set value here
        }
        #endregion

        #region FocusedBorderBrush Dependency Property
        public static readonly DependencyProperty FocusedBorderBrushProperty = DependencyProperty.Register("FocusedBorderBrush", typeof(Brush), typeof(DataFieldEditor), new PropertyMetadata(default(Brush), new PropertyChangedCallback(OnFocusedBorderBrushChanged)));
        public Brush FocusedBorderBrush {
            get { return (Brush)GetValue(FocusedBorderBrushProperty); }
            set { SetValue(FocusedBorderBrushProperty, value); }
        }
        private static void OnFocusedBorderBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (DataFieldEditor)d;
            var val = (Brush)e.NewValue;
            obj.BackBorder.BorderBrush = val;
        }
        #endregion

        #region IsSelected Dependency Property
        public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register("IsSelected", typeof(bool), typeof(DataFieldEditor), new PropertyMetadata(default(bool), new PropertyChangedCallback(OnIsSelectedChanged)));
        public bool IsSelected {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }
        private static void OnIsSelectedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (DataFieldEditor)d;
            var val = (bool)e.NewValue;
            if (val && obj.BackBorder.Background != obj.FocusedBackground) {
                obj.Select();
            }
        }
        #endregion

    }
}
