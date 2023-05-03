namespace AppSystem.Media {
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Windows.Storage;
    using Windows.UI.Xaml.Media;
    using Windows.UI.Xaml.Media.Imaging;

    public static class MediaExtensions {
        public static async Task<ImageSource> GetImageSourceAbsolute(this string fileName) {
            var folder = await StorageFolder.GetFolderFromPathAsync(Path.GetDirectoryName(fileName));
            var file = await folder.GetFileAsync(Path.GetFileName(fileName));
            using (var fileStream = await file.OpenAsync(FileAccessMode.Read)) {
                var bitmapImage = new BitmapImage();
                await bitmapImage.SetSourceAsync(fileStream);
                return bitmapImage;
            }
        }
    }
}
