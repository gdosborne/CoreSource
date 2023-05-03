namespace OptiRampDesignerModel
{
    using OptiRampDesignerModel.Events;
    using System;
    using System.Windows;
    using System.Windows.Controls.Ribbon;

    public interface IModule
    {
        #region Public Events

        event InitializationCompleteHandler InitializationComplete;

        #endregion Public Events

        #region Public Properties

        string ApplicationDirectory { get; set; }
        string FullPath { get; }
        ILog Log { get; set; }
        string Name { get; }
        IOptionSet Options { get; }
        IDesignerProject Project { get; set; }
        RibbonTab Tab { get; }
        FrameworkElement UserControlContainer { get; }
        Version Version { get; }

        #endregion Public Properties

        #region Public Methods

        void Initialize();

        void Release();

        #endregion Public Methods
    }
}