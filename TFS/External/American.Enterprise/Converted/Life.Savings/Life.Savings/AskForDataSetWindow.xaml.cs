using System;
using System.Windows;
using GregOsborne.Application.Primitives;
using GregOsborne.Application.Windows;

namespace Life.Savings
{
    public partial class AskForDataSetWindow : Window
    {
        public AskForDataSetWindow()
        {
            InitializeComponent();
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            this.HideMinimizeAndMaximizeButtons();
            this.HideControlBox();
        }

        private void AskForDataSetWindowView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("DialogResult"))
                this.Close();
        }
    }
}
