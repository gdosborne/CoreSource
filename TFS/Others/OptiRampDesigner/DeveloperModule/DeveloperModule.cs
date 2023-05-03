namespace DeveloperModule
{
    using GregOsborne.Application.Media;
    using MVVMFramework;
    using OptiRampDesignerModel;
    using OptiRampDesignerModel.Concrete;
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Ribbon;
    using System.Windows.Media;

    public class DeveloperModule : IModule
    {
        #region Private Fields

        private DelegateCommand _AddLineCommand = null;

        private string _ApplicationDirectory;

        #endregion Private Fields

        #region Public Constructors

        public DeveloperModule()
        {
            Name = "Developer";
            FullPath = this.GetType().Assembly.Location;
            Version = this.GetType().Assembly.GetName().Version;
        }

        #endregion Public Constructors

        #region Public Events

        public event OptiRampDesignerModel.Events.InitializationCompleteHandler InitializationComplete;

        #endregion Public Events

        #region Public Properties

        public DelegateCommand AddLineCommand {
            get {
                if (_AddLineCommand == null)
                    _AddLineCommand = new DelegateCommand(AddLine, ValidateAddLineState);
                return _AddLineCommand as DelegateCommand;
            }
        }

        public string ApplicationDirectory {
            get { return _ApplicationDirectory; }
            set { _ApplicationDirectory = value; }
        }

        public string FullPath { get; private set; }
        public ILog Log { get; set; }
        public string Name { get; private set; }
        public IOptionSet Options { get; private set; }
        public IDesignerProject Project { get; set; }
        public RibbonTab Tab { get; private set; }
        public FrameworkElement UserControlContainer { get; private set; }
        public Version Version { get; private set; }

        #endregion Public Properties

        #region Public Methods

        public void Initialize()
        {
            Options = new OptionSet(this.Name);
            var genCat = new OptionCategory("General");
            Options.Categories.Add(genCat);

            WriteToLog("Initializing {0} module, version {1}", this.Name, this.Version);
            Tab = new RibbonTab
            {
                Header = this.Name
            };
            WriteToLog("Creating images ribbon group");
            var imagesGroup = new RibbonGroup
            {
                Header = "Images"
            };
            var dynImageBtn = new RibbonButton
            {
                Label = "Dynamic",
                LargeImageSource = this.GetType().Assembly.GetImageSourceByName(@"\images\48\dynamicImage.png"),
                SmallImageSource = this.GetType().Assembly.GetImageSourceByName(@"\images\24\dynamicImage.png")
            };
            var statImageBtn = new RibbonButton
            {
                Label = "Static",
                LargeImageSource = this.GetType().Assembly.GetImageSourceByName(@"\images\48\staticImage.png"),
                SmallImageSource = this.GetType().Assembly.GetImageSourceByName(@"\images\24\staticImage.png")
            };
            var runImageBtn = new RibbonButton
            {
                Label = "Runtime",
                LargeImageSource = this.GetType().Assembly.GetImageSourceByName(@"\images\48\runtimeImage.png"),
                SmallImageSource = this.GetType().Assembly.GetImageSourceByName(@"\images\24\runtimeImage.png")
            };
            imagesGroup.Items.Add(dynImageBtn);
            imagesGroup.Items.Add(statImageBtn);
            imagesGroup.Items.Add(runImageBtn);

            WriteToLog("Creating shapes ribbon group");
            var shapesGroup = new RibbonGroup
            {
                Header = "Shapes"
            };
            var lineBtn = new RibbonButton
            {
                Label = "Line",
                Command = AddLineCommand,
                LargeImageSource = this.GetType().Assembly.GetImageSourceByName(@"\images\48\staticLine.png"),
                SmallImageSource = this.GetType().Assembly.GetImageSourceByName(@"\images\24\staticLine.png")
            };
            var rectangleBtn = new RibbonButton
            {
                Label = "Rectangle",
                LargeImageSource = this.GetType().Assembly.GetImageSourceByName(@"\images\48\rectangle.png"),
                SmallImageSource = this.GetType().Assembly.GetImageSourceByName(@"\images\24\rectangle.png")
            };
            var ellipseBtn = new RibbonButton
            {
                Label = "Ellipse",
                LargeImageSource = this.GetType().Assembly.GetImageSourceByName(@"\images\48\ellipse.png"),
                SmallImageSource = this.GetType().Assembly.GetImageSourceByName(@"\images\24\ellipse.png")
            };
            shapesGroup.Items.Add(lineBtn);
            shapesGroup.Items.Add(rectangleBtn);
            shapesGroup.Items.Add(ellipseBtn);

            WriteToLog("Creating texts ribbon group");
            var textsGroup = new RibbonGroup
            {
                Header = "Texts"
            };
            var statTextBtn = new RibbonButton
            {
                Label = "Static",
                LargeImageSource = this.GetType().Assembly.GetImageSourceByName(@"\images\48\staticText.png"),
                SmallImageSource = this.GetType().Assembly.GetImageSourceByName(@"\images\24\staticText.png")
            };
            var dynTextBtn = new RibbonButton
            {
                Label = "Dynamic",
                LargeImageSource = this.GetType().Assembly.GetImageSourceByName(@"\images\48\dynamicText.png"),
                SmallImageSource = this.GetType().Assembly.GetImageSourceByName(@"\images\24\dynamicText.png")
            };
            var updateTextBtn = new RibbonButton
            {
                Label = "Updatable",
                LargeImageSource = this.GetType().Assembly.GetImageSourceByName(@"\images\48\updatableText.png"),
                SmallImageSource = this.GetType().Assembly.GetImageSourceByName(@"\images\24\updatableText.png")
            };
            textsGroup.Items.Add(statTextBtn);
            textsGroup.Items.Add(dynTextBtn);
            textsGroup.Items.Add(updateTextBtn);

            WriteToLog("Creating tables ribbon group");
            var tablesGroup = new RibbonGroup
            {
                Header = "Tables"
            };
            var refTableBtn = new RibbonButton
            {
                Label = "Reference",
                LargeImageSource = this.GetType().Assembly.GetImageSourceByName(@"\images\48\refTableObject.png"),
                SmallImageSource = this.GetType().Assembly.GetImageSourceByName(@"\images\24\refTableObject.png")
            };
            var histTableBtn = new RibbonButton
            {
                Label = "Historical",
                LargeImageSource = this.GetType().Assembly.GetImageSourceByName(@"\images\48\refTableObject.png"),
                SmallImageSource = this.GetType().Assembly.GetImageSourceByName(@"\images\24\refTableObject.png")
            };
            var realtimeTableBtn = new RibbonButton
            {
                Label = "Realtime",
                LargeImageSource = this.GetType().Assembly.GetImageSourceByName(@"\images\48\refTableObject.png"),
                SmallImageSource = this.GetType().Assembly.GetImageSourceByName(@"\images\24\refTableObject.png")
            };
            tablesGroup.Items.Add(refTableBtn);
            tablesGroup.Items.Add(histTableBtn);
            tablesGroup.Items.Add(realtimeTableBtn);

            Tab.Items.Add(shapesGroup);
            Tab.Items.Add(textsGroup);
            Tab.Items.Add(imagesGroup);
            Tab.Items.Add(tablesGroup);

            UserControlContainer = new Canvas
            {
                Background = new SolidColorBrush(Colors.White)
            };

            if (InitializationComplete != null)
                InitializationComplete(this, new OptiRampDesignerModel.Events.InitializationCompleteEventArgs(Tab, UserControlContainer));
        }

        public void Release()
        {
            WriteToLog("Releasing module {0}", this.Name);
            Tab = null;
        }

        #endregion Public Methods

        #region Private Methods

        private void AddLine(object state)
        {
            MessageBox.Show("Add line");
        }

        private bool ValidateAddLineState(object state)
        {
            return true;
        }

        private void WriteToLog(string format, params object[] p)
        {
            if (Log != null)
                Log.WriteMessage(format, p);
        }

        private void WriteToLog(string message)
        {
            if (Log != null)
                Log.WriteMessage(message);
        }

        #endregion Private Methods
    }
}