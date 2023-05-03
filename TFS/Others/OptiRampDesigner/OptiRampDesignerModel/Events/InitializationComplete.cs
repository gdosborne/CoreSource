namespace OptiRampDesignerModel.Events
{
    using System;
    using System.Windows;
    using System.Windows.Controls.Ribbon;

    public delegate void InitializationCompleteHandler(object sender, InitializationCompleteEventArgs e);

    public class InitializationCompleteEventArgs : EventArgs
    {
        public InitializationCompleteEventArgs(RibbonTab tab, FrameworkElement container)
        {
            Tab = tab;
            UserControlContainer = container;
        }

        public RibbonTab Tab { get; private set; }
        public FrameworkElement UserControlContainer { get; set; }
    }
}