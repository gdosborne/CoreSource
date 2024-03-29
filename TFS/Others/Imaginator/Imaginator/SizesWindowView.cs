namespace Imaginator.Views
{
    using System;
    using System.ComponentModel;
    using System.Windows;
    using MVVMFramework;
    using GregOsborne.Application.Primitives;
    using GregOsborne.Application.Media;
    using System.Windows.Media;
    using System.Collections.Generic;

    public class SizesWindowView : INotifyPropertyChanged
    {
        public SizesWindowView()
        {
            Use128 = true;
            Use64 = true;
            Use48 = true;
            Use32 = true;
            Use24 = true;
            Use16 = true;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void UpdateInterface()
        {

        }
        public void InitView()
        {

        }
        public void Initialize(Window window)
        {

        }
        public void Persist(Window window)
        {

        }
        private ImageSource _ImageSource;
        public ImageSource ImageSource
        {
            get { return _ImageSource; }
            set
            {
                _ImageSource = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
            }
        }
        private string _TemporaryFileName;
        public string TemporaryFileName
        {
            get { return _TemporaryFileName; }
            set
            {
                _TemporaryFileName = value;
                if (!string.IsNullOrEmpty(value) && System.IO.File.Exists(value))
                {
                    var src = value.GetImageSourceAbsolute();
                    ImageSource = src;
                }
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
            }
        }
        private bool? _DialogResult;
        public bool? DialogResult
        {
            get { return _DialogResult; }
            set
            {
                _DialogResult = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
            }
        }
        private DelegateCommand _OKCommand = null;
        public DelegateCommand OKCommand
        {
            get
            {
                if (_OKCommand == null)
                    _OKCommand = new DelegateCommand(OK, ValidateOKState);
                return _OKCommand as DelegateCommand;
            }
        }
        private void OK(object state)
        {
            var temp = new List<int>();
            if (Use128)
                temp.Add(128);
            if (Use64)
                temp.Add(64);
            if (Use48)
                temp.Add(48);
            if (Use32)
                temp.Add(32);
            if (Use24)
                temp.Add(24);
            if (Use16)
                temp.Add(16);
            Sizes = temp.ToArray();
            DialogResult = true;
        }
        private bool ValidateOKState(object state)
        {
            return true;
        }
        private int[] _Sizes;
        public int[] Sizes
        {
            get { return _Sizes; }
            set
            {
                _Sizes = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
            }
        }
        private DelegateCommand _CancelCommand = null;
        public DelegateCommand CancelCommand
        {
            get
            {
                if (_CancelCommand == null)
                    _CancelCommand = new DelegateCommand(Cancel, ValidateCancelState);
                return _CancelCommand as DelegateCommand;
            }
        }
        private void Cancel(object state)
        {
            DialogResult = false;
        }
        private bool ValidateCancelState(object state)
        {
            return true;
        }
        private bool _Use128;
        public bool Use128
        {
            get { return _Use128; }
            set
            {
                _Use128 = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
            }
        }
        private bool _Use64;
        public bool Use64
        {
            get { return _Use64; }
            set
            {
                _Use64 = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
            }
        }
        private bool _Use48;
        public bool Use48
        {
            get { return _Use48; }
            set
            {
                _Use48 = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
            }
        }
        private bool _Use32;
        public bool Use32
        {
            get { return _Use32; }
            set
            {
                _Use32 = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
            }
        }
        private bool _Use24;
        public bool Use24
        {
            get { return _Use24; }
            set
            {
                _Use24 = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
            }
        }
        private bool _Use16;
        public bool Use16
        {
            get { return _Use16; }
            set
            {
                _Use16 = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
            }
        }
    }
}
