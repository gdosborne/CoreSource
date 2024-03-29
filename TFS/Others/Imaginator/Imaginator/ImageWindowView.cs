namespace Imaginator.Views
{
    using GregOsborne.Application.Media;
    using GregOsborne.Application.Primitives;
    using MVVMFramework;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Media;

    public class ImageWindowView : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event ExecuteUIActionHandler ExecuteUIAction;
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
        private static bool ThumbnailCallback()
        {
            return false;
        }
        private void OK(object state)
        {
            using (var bmp = System.Drawing.Bitmap.FromFile(FileName))
            {
                using (var bmp1 = new System.Drawing.Bitmap(CropRectangle.Size.Width, CropRectangle.Size.Height))
                using (var g1 = System.Drawing.Graphics.FromImage(bmp1))
                {
                    var r = new System.Drawing.Rectangle(0, 0, CropRectangle.Size.Width, CropRectangle.Size.Height);
                    g1.DrawImage(bmp, r, CropRectangle, System.Drawing.GraphicsUnit.Pixel);
                    System.Drawing.Image.GetThumbnailImageAbort myCallback = new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback);
                    OutputImage = bmp1.GetThumbnailImage(128, 128, myCallback, IntPtr.Zero);
                    TemporaryFileName = System.IO.Path.Combine(App.DataFolder, "temporary.png");
                    OutputImage.Save(TemporaryFileName, System.Drawing.Imaging.ImageFormat.Png);
                }
            }
            if (ExecuteUIAction != null)
            {
                var p = new Dictionary<string, object> { 
                    { "result", false }, 
                    { "tempfilename", TemporaryFileName }, 
                    { "sizes", null } 
                };
                ExecuteUIAction(this, new ExecuteUIActionEventArgs("GetConversionSizes", p));
                if (!(bool)p["result"])
                    return;
                Sizes = (int[])p["sizes"];
            }
            DialogResult = true;
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
        private bool ValidateOKState(object state)
        {
            return true;
        }
        private string _TemporaryFileName;
        public string TemporaryFileName
        {
            get { return _TemporaryFileName; }
            set
            {
                _TemporaryFileName = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
            }
        }
        private System.Drawing.Image _OutputImage;
        public System.Drawing.Image OutputImage
        {
            get { return _OutputImage; }
            set
            {
                _OutputImage = value;
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
        private System.Drawing.Rectangle _CropRectangle;
        public System.Drawing.Rectangle CropRectangle
        {
            get { return _CropRectangle; }
            set
            {
                _CropRectangle = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
            }
        }
        private double _ImageWidth;
        public double ImageWidth
        {
            get { return _ImageWidth; }
            set
            {
                _ImageWidth = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
            }
        }
        private double _ImageHeight;
        public double ImageHeight
        {
            get { return _ImageHeight; }
            set
            {
                _ImageHeight = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
            }
        }
        private ImageSource _ImportImageSource;
        public ImageSource ImportImageSource
        {
            get { return _ImportImageSource; }
            set
            {
                _ImportImageSource = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
            }
        }
        private string _FileName;
        public string FileName
        {
            get { return _FileName; }
            set
            {
                _FileName = value;
                if (!string.IsNullOrEmpty(value) && System.IO.File.Exists(value))
                {
                    var size = value.ImageDimensions();
                    var src = value.GetImageSourceAbsolute();
                    ImageWidth = size.Width;
                    ImageHeight = size.Height;
                    ImportImageSource = src;
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
    }
}
