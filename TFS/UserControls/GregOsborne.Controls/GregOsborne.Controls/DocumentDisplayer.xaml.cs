using GregOsborne.Application;
using GregOsborne.Application.Media;
using GregOsborne.Application.Primitives;
using GregOsborne.Controls.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml.Linq;
//using GregOsborne.Snippet.Events;

namespace GregOsborne.Controls {
    public partial class DocumentDisplayer : UserControl {
        public new static readonly DependencyProperty BorderBrushProperty = DependencyProperty.Register("BorderBrush", typeof(Brush), typeof(DocumentDisplayer), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure, OnBorderBrushChanged, CoerceBorderBrush));
        public new static readonly DependencyProperty BorderThicknessProperty = DependencyProperty.Register("BorderThickness", typeof(Thickness), typeof(DocumentDisplayer), new FrameworkPropertyMetadata(new Thickness(0), FrameworkPropertyMetadataOptions.AffectsMeasure, OnBorderThicknessChanged, CoerceBorderThickness));
        public new static readonly DependencyProperty BackgroundProperty = DependencyProperty.Register("Background", typeof(Brush), typeof(DocumentDisplayer), new FrameworkPropertyMetadata(Brushes.White, FrameworkPropertyMetadataOptions.AffectsMeasure, OnBackgroundChanged, CoerceBackground));
        public new static readonly DependencyProperty FontSizeProperty = DependencyProperty.Register("FontSize", typeof(double), typeof(DocumentDisplayer), new FrameworkPropertyMetadata(14.0, FrameworkPropertyMetadataOptions.AffectsMeasure, OnFontSizeChanged, CoerceFontSize));
        public new static readonly DependencyProperty FontFamilyProperty = DependencyProperty.Register("FontFamily", typeof(FontFamily), typeof(DocumentDisplayer), new FrameworkPropertyMetadata(new FontFamily("Segoe UI"), FrameworkPropertyMetadataOptions.AffectsMeasure, OnFontFamilyChanged, CoerceFontFamily));
        public new static readonly DependencyProperty FontStretchProperty = DependencyProperty.Register("FontStretch", typeof(FontStretch), typeof(DocumentDisplayer), new FrameworkPropertyMetadata(FontStretches.Normal, FrameworkPropertyMetadataOptions.AffectsMeasure, OnFontStretchChanged, CoerceFontStretch));
        public new static readonly DependencyProperty FontStyleProperty = DependencyProperty.Register("FontStyle", typeof(FontStyle), typeof(DocumentDisplayer), new FrameworkPropertyMetadata(FontStyles.Normal, FrameworkPropertyMetadataOptions.AffectsMeasure, OnFontStyleChanged, CoerceFontStyle));
        public static readonly DependencyProperty DocumentTypeProperty = DependencyProperty.Register("DocumentType", typeof(Enumerations.DocumentType), typeof(DocumentDisplayer), new FrameworkPropertyMetadata(Enumerations.DocumentTypes.Text, FrameworkPropertyMetadataOptions.AffectsMeasure, OnDocumentTypeChanged, CoerceDocumentType));

        public event CodeChangedHandler CodeChanged;
        public DocumentDisplayer() {
            InitializeComponent();
        }

        public new Brush BorderBrush {
            get => (Brush)GetValue(BorderBrushProperty);
            set => SetValue(BorderBrushProperty, value);
        }

        public new Thickness BorderThickness {
            get => (Thickness)GetValue(BorderThicknessProperty);
            set => SetValue(BorderThicknessProperty, value);
        }

        public new Brush Background {
            get => (Brush)GetValue(BackgroundProperty);
            set => SetValue(BackgroundProperty, value);
        }

        [TypeConverter(typeof(DocumentTypeTypeConverter))]
        public Enumerations.DocumentType DocumentType {
            get => (Enumerations.DocumentType)GetValue(DocumentTypeProperty);
            set => SetValue(DocumentTypeProperty, value);
        }

        public new double FontSize {
            get => (double)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        public new FontFamily FontFamily {
            get => (FontFamily)GetValue(FontFamilyProperty);
            set => SetValue(FontFamilyProperty, value);
        }

        public new FontStretch FontStretch {
            get => (FontStretch)GetValue(FontStretchProperty);
            set => SetValue(FontStretchProperty, value);
        }

        public new FontStyle FontStyle {
            get => (FontStyle)GetValue(FontStyleProperty);
            set => SetValue(FontStyleProperty, value);
        }

        private static void OnBorderBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (DocumentDisplayer)d;
            var val = (Brush)e.NewValue;
            obj._outerBorder.BorderBrush = val;
        }

        private static object CoerceBorderBrush(DependencyObject d, object value) {
            var val = (Brush)value;
            //coerce value here if necessary
            return val;
        }

        private static void OnBorderThicknessChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (DocumentDisplayer)d;
            var val = (Thickness)e.NewValue;
            obj._outerBorder.BorderThickness = val;
        }

        private static object CoerceBorderThickness(DependencyObject d, object value) {
            var val = (Thickness)value;
            //coerce value here if necessary
            return val;
        }

        private static void OnBackgroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (DocumentDisplayer)d;
            var val = (Brush)e.NewValue;
            obj._outerBorder.Background = val;
        }

        private static object CoerceBackground(DependencyObject d, object value) {
            var val = (Brush)value;
            //coerce value here if necessary
            return val;
        }

        private static void OnDocumentTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (DocumentDisplayer)d;
            var val = (Enumerations.DocumentType)e.NewValue;
            obj.Background = val.Palette.Brushes[Enumerations.PaletteParts.Background];
        }

        private static object CoerceDocumentType(DependencyObject d, object value) {
            var val = (Enumerations.DocumentType)value;
            //coerce value here if necessary
            return val;
        }

        private List<BrushData> DocumentBrushes { get; set; }

        public void UpdatePalette(BrushPalette palette) {
            DocumentType.Palette = palette;
            MakeBrushes();
            RefreshDocument();
        }

        private void MakeBrushes() {
            DocumentBrushes = new List<BrushData>();
            DocumentType.Palette.Brushes.ToList().ForEach(x => {
                var bd = new BrushData(x.Key.Value, x.Key.Description) {
                    Value = new SolidColorBrush(Settings.GetSetting("SnippetManager", "Editor", $"XmlColor{x.Key.Value}", x.Value.As<SolidColorBrush>().Color.ToHexValue()).ToColor())
                };
                DocumentBrushes.Add(bd);
            });
        }

        private ToTextBlockConverter converter;
        private void RefreshDocument() {
            var font = new FontFamily(Settings.GetSetting("SnippetManager", "Editor", "LastFontName", "Segoe UI"));
            var size = Settings.GetSetting("SnippetManager", "Editor", "LastFontSize", 14.0);
            var xmlTabSize = Settings.GetSetting("SnippetManager", "Editor", "XmlTabSize", 10);
            var codeTabSize = Settings.GetSetting("SnippetManager", "Editor", "CodeTabSize", 10);
            Background = DocumentBrushes.FirstOrDefault(x => x.Key == Enumerations.PaletteParts.Background.Value)?.Value;
            if (converter == null) {
                if (DocumentType == Enumerations.DocumentTypes.Xml) {
                    converter = new XDocumentToTextBlock(Data, DocumentBrushes, font, size, FontStretches.Normal, FontStyles.Normal, xmlTabSize, codeTabSize);
                    CodeChanged?.Invoke(this, new CodeChangedEventArgs(converter.As<XDocumentToTextBlock>().CDataText, Enumerations.CodeLanguages.CSharp, converter.As<XDocumentToTextBlock>().RawText));
                }
                else if (DocumentType == Enumerations.DocumentTypes.Text) {
                    converter = new TextToTextBlock(Data, DocumentBrushes, font, size, FontStretches.Normal, FontStyles.Normal, xmlTabSize, codeTabSize);
                }
                else if (DocumentType == Enumerations.DocumentTypes.CSharp) {
                    converter = new CSharpCodeToTextBlock(Data, DocumentBrushes, font, size, FontStretches.Normal, FontStyles.Normal, xmlTabSize, codeTabSize);
                }
            }
            if (converter == null) return;
            converter.FontSizeChanged += converter_FontSizeChanged;
            _scrollViewer.Content = converter.TextBlock;
        }
        private string Data { get; set; }
        public event FontSizeChangedHandler FontSizeChanged;
        public void SetDocumentSource(string data) {
            Data = data;
            MakeBrushes();
            RefreshDocument();
        }

        public void UpdateCode(string code) {
            if (DocumentType != Enumerations.DocumentTypes.Xml) return;
            this.converter.As<XDocumentToTextBlock>().UpdateCode(code);
            CodeChanged?.Invoke(this, new CodeChangedEventArgs(
                converter.As<XDocumentToTextBlock>().CDataText, 
                Enumerations.CodeLanguages.CSharp, 
                this.converter.As<XDocumentToTextBlock>().RawText));
        }
        private void converter_FontSizeChanged(object sender, FontSizeChangedEventArgs e) {
            FontSizeChanged?.Invoke(this, e);
        }

        public void SetDocumentSource(object data) {
            if (data.Is<string>())
                SetDocumentSource((string)data);
            else if (data.Is<XDocument>())
                SetDocumentSource(data.As<XDocument>().ToString());
            else
                throw new NotImplementedException("Source type not implemented");
        }

        public void SetDocumentFileName(string fileName) {
            if (!File.Exists(fileName))
                throw new FileNotFoundException($"Cannot find {fileName}");
            if (Application.IO.File.IsBinary(fileName)) return;
            using (var fs = Application.IO.File.OpenFile(fileName, FileMode.Open, FileAccess.Read, FileShare.None))
            using (var sr = new StreamReader(fs)) {
                if (sr.Peek() > -1)
                    SetDocumentSource(sr.ReadToEnd());
            }
        }

        private static void OnFontSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (DocumentDisplayer)d;
            var val = (double)e.NewValue;
            obj._scrollViewer.Content.As<TextBlock>().FontSize = val;
        }

        private static object CoerceFontSize(DependencyObject d, object value) {
            var val = (double)value;
            //coerce value here if necessary
            return val;
        }

        private static void OnFontFamilyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (DocumentDisplayer)d;
            var val = (FontFamily)e.NewValue;
            obj._scrollViewer.Content.As<TextBlock>().FontFamily = val;
        }

        private static object CoerceFontFamily(DependencyObject d, object value) {
            var val = (FontFamily)value;
            //coerce value here if necessary
            return val;
        }

        private static void OnFontStretchChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (DocumentDisplayer)d;
            var val = (FontStretch)e.NewValue;
            obj._scrollViewer.Content.As<TextBlock>().FontStretch = val;
        }

        private static object CoerceFontStretch(DependencyObject d, object value) {
            var val = (FontStretch)value;
            //coerce value here if necessary
            return val;
        }

        private static void OnFontStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (DocumentDisplayer)d;
            var val = (FontStyle)e.NewValue;
            obj._scrollViewer.Content.As<TextBlock>().FontStyle = val;
        }

        private static object CoerceFontStyle(DependencyObject d, object value) {
            var val = (FontStyle)value;
            //coerce value here if necessary
            return val;
        }
    }
}