/* File="RecycledCompositeIcon"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

using OzFramework.Primitives;
using System.IO;
using System.Windows;

namespace OzFramework.Media {
    public class RecycledCompositeIcon : CompositeIcon {
        public static RecycledCompositeIcon FromFileAsRecycled(string filename, double scaleValue) {
            var res = default(RecycledCompositeIcon);
            var json = File.ReadAllText(filename);

            var result = FromJson(json);
            if (!result.IsNull()) {
                res = new RecycledCompositeIcon {
                    RecycleBinFilename = filename,
                    LastDateSaved = new FileInfo(filename).LastWriteTime,
                    ItemHeight = 330 * scaleValue,
                    FullPath = result.FullPath,
                    ShortName = Path.GetFileNameWithoutExtension(result.FullPath),
                    Filename = Path.GetFileName(result.FullPath),
                    PrimaryGlyph = result.PrimaryGlyph,
                    SecondaryGlyph = result.SecondaryGlyph,
                    IconType = result.IconType,
                    IsSingleBrushInUse = result.IsSingleBrushInUse,
                    RenameValue = result.RenameValue,
                    SurfaceBrush = result.SurfaceBrush,
                    PrimaryFontFamily = result.PrimaryFontFamily,
                    SecondaryFontFamily = result.SecondaryFontFamily,
                    PrimarySize = result.PrimarySize * scaleValue,
                    SecondarySize = result.SecondarySize * scaleValue,
                    PrimaryBrush = result.PrimaryBrush,
                    SecondaryBrush = result.SecondaryBrush
                };
                res.SecondaryFontFamily ??= res.PrimaryFontFamily;
                if (result.SecondarySize <= 0) {
                    result.SecondarySize = res.PrimarySize;
                    res.SecondarySize = result.SecondarySize;
                }
                result.SecondaryBrush ??= result.PrimaryBrush;

                if (result.IconType == CompositeIconData.IconTypes.FullOverlay) {
                    res.CenteredVisibility = Visibility.Visible;
                    res.SubscriptVisibility = Visibility.Collapsed;
                    res.SecondaryMargin = new Thickness(result.SecondaryHorizontalOffset * scaleValue,
                        result.SecondaryVerticalOffset * scaleValue, 0, 0);
                }
                else {
                    res.CenteredVisibility = Visibility.Collapsed;
                    res.SubscriptVisibility = Visibility.Visible;
                }
                res.IsLoadComplete = true;
            }
            return res;
        }

        #region RecycleBinFilename Property
        private string _RecycleBinFilename = default;
        /// <summary>Gets/sets the RecycleBinFilename.</summary>
        /// <value>The RecycleBinFilename.</value>
        public string RecycleBinFilename {
            get => _RecycleBinFilename;
            set {
                _RecycleBinFilename = value;
            }
        }
        #endregion

        #region SecondaryMargin Property
        private Thickness _SecondaryMargin = default;
        /// <summary>Gets/sets the SecondaryMargin.</summary>
        /// <value>The SecondaryMargin.</value>
        public Thickness SecondaryMargin {
            get => _SecondaryMargin;
            set {
                _SecondaryMargin = value;
            }
        }
        #endregion

        #region CenteredVisibility Property
        private Visibility _CenteredVisibility = default;
        /// <summary>Gets/sets the CenteredVisibility.</summary>
        /// <value>The CenteredVisibility.</value>
        public Visibility CenteredVisibility {
            get => _CenteredVisibility;
            set {
                _CenteredVisibility = value;
            }
        }
        #endregion

        #region SubscriptVisibility Property
        private Visibility _SubscriptVisibility = default;
        /// <summary>Gets/sets the SubscriptVisibility.</summary>
        /// <value>The SubscriptVisibility.</value>
        public Visibility SubscriptVisibility {
            get => _SubscriptVisibility;
            set {
                _SubscriptVisibility = value;
            }
        }
        #endregion

        #region ItemHeight Property
        private double _ItemHeight = default;
        /// <summary>Gets/sets the ItemHeight.</summary>
        /// <value>The ItemHeight.</value>
        public double ItemHeight {
            get => _ItemHeight;
            set {
                _ItemHeight = value;
            }
        }
        #endregion
    }
}
