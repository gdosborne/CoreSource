namespace Imaginator.Views
{
    using System;
    using System.ComponentModel;
    using System.Windows;
    using MVVMFramework;
    using GregOsborne.Application.Primitives;
    using System.Windows.Media;
    using System.Collections.Generic;

    public class PastedWindowView : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event ExecuteUIActionHandler ExecuteUIAction;
        public void UpdateInterface()
        {

        }
        private System.Drawing.Bitmap GetBitmap(System.Windows.Media.Imaging.BitmapSource source)
        {
            var pastedBitmap = new System.Drawing.Bitmap(source.PixelWidth, source.PixelHeight, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            System.Drawing.Imaging.BitmapData data = pastedBitmap.LockBits(new System.Drawing.Rectangle(System.Drawing.Point.Empty, pastedBitmap.Size), System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            source.CopyPixels(Int32Rect.Empty, data.Scan0, data.Height * data.Stride, data.Stride);
            pastedBitmap.UnlockBits(data);
            return pastedBitmap;
        }
        //private System.Drawing.Bitmap pastedBitmap = null;
        public void InitView()
        {
            var iob = Clipboard.GetData(DataFormats.Bitmap);
            TheBitmap = GetBitmap((System.Windows.Media.Imaging.BitmapSource)iob);
            ClipboardImageSource = (ImageSource)iob;
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
        public System.Drawing.Bitmap TheBitmap { get; set; }
        private void OK(object state)
        {

            using (var bmp1 = new System.Drawing.Bitmap(CropRectangle.Size.Width, CropRectangle.Size.Height))
            using (var g1 = System.Drawing.Graphics.FromImage(bmp1))
            {
                var r = new System.Drawing.Rectangle(0, 0, CropRectangle.Size.Width, CropRectangle.Size.Height);
                g1.DrawImage(TheBitmap, r, CropRectangle, System.Drawing.GraphicsUnit.Pixel);
                System.Drawing.Image.GetThumbnailImageAbort myCallback = new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback);
                OutputImage = bmp1.GetThumbnailImage(128, 128, myCallback, IntPtr.Zero);
                TemporaryFileName = System.IO.Path.Combine(App.DataFolder, "temporary.png");
                OutputImage.Save(TemporaryFileName, System.Drawing.Imaging.ImageFormat.Png);
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
                TemporaryFileName = (string)p["tempfilename"];
            }
            DialogResult = true;
        }
        private static bool ThumbnailCallback()
        {
            return false;
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

        }
        private bool ValidateCancelState(object state)
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
        private ImageSource _ClipboardImageSource;
        public ImageSource ClipboardImageSource
        {
            get { return _ClipboardImageSource; }
            set
            {
                _ClipboardImageSource = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
            }
        }
    }
}
