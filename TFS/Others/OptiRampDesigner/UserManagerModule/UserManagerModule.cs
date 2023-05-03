namespace UserManagerModule
{
    using GregOsborne.Application.Media;
    using OptiRampDesignerModel;
    using OptiRampDesignerModel.Concrete;
    using OptiRampDesignerModel.Events;
    using System;
    using System.Windows;
    using System.Windows.Controls.Ribbon;

    public class UserManagerModule : IModule
    {
        #region Public Constructors

        public UserManagerModule()
        {
            Name = "User Manager";
            FullPath = this.GetType().Assembly.Location;
            Version = this.GetType().Assembly.GetName().Version;
        }

        #endregion Public Constructors

        #region Public Events

        public event InitializationCompleteHandler InitializationComplete;

        #endregion Public Events

        #region Public Properties

        public string ApplicationDirectory { get; set; }

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
            WriteToLog("Creating file ribbon group");
            var fileGroup = new RibbonGroup
            {
                Header = "File"
            };
            var newButton = new RibbonButton
            {
                Label = "New",
                LargeImageSource = this.GetType().Assembly.GetImageSourceByName(@"\images\48\new.png"),
                SmallImageSource = this.GetType().Assembly.GetImageSourceByName(@"\images\24\new.png")
            };
            var opnButton = new RibbonButton
            {
                Label = "Open",
                LargeImageSource = this.GetType().Assembly.GetImageSourceByName(@"\images\48\open.png"),
                SmallImageSource = this.GetType().Assembly.GetImageSourceByName(@"\images\24\open.png")
            };
            var savButton = new RibbonButton
            {
                Label = "Save",
                LargeImageSource = this.GetType().Assembly.GetImageSourceByName(@"\images\48\save.png"),
                SmallImageSource = this.GetType().Assembly.GetImageSourceByName(@"\images\24\save.png")
            };
            var savAsButton = new RibbonButton
            {
                Label = "Save as",
                LargeImageSource = this.GetType().Assembly.GetImageSourceByName(@"\images\48\saveas.png"),
                SmallImageSource = this.GetType().Assembly.GetImageSourceByName(@"\images\24\saveas.png")
            };
            fileGroup.Items.Add(newButton);
            fileGroup.Items.Add(opnButton);
            fileGroup.Items.Add(savButton);
            fileGroup.Items.Add(savAsButton);

            WriteToLog("Creating entity ribbon group");
            var entityGroup = new RibbonGroup
            {
                Header = "Entities"
            };
            var newUserButton = new RibbonButton
            {
                Label = "User",
                LargeImageSource = this.GetType().Assembly.GetImageSourceByName(@"\images\48\person.png"),
                SmallImageSource = this.GetType().Assembly.GetImageSourceByName(@"\images\24\person.png")
            };
            var newRoleButton = new RibbonButton
            {
                Label = "Role",
                LargeImageSource = this.GetType().Assembly.GetImageSourceByName(@"\images\48\persons.png"),
                SmallImageSource = this.GetType().Assembly.GetImageSourceByName(@"\images\24\persons.png")
            };
            entityGroup.Items.Add(newUserButton);
            entityGroup.Items.Add(newRoleButton);

            Tab.Items.Add(fileGroup);
            Tab.Items.Add(entityGroup);

            UserControlContainer = new UserManagerControl();

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