namespace Ookii.Dialogs.Wpf
{
    partial class TaskDialog
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            try
            {
                if( disposing )
                {
                    if( components != null )
                    {
                        components.Dispose();
                        components = null;
                    }
                    if( _buttons != null )
                    {
                        foreach( TaskDialogButton button in _buttons )
                        {
                            button.Dispose();
                        }
                        _buttons.Clear();
                    }
                    if( _radioButtons != null )
                    {
                        foreach( TaskDialogRadioButton radioButton in _radioButtons )
                        {
                            radioButton.Dispose();
                        }
                        _radioButtons.Clear();
                    }
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
        }
    }
}
