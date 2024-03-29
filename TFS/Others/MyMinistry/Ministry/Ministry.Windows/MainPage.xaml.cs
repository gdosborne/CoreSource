using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Ministry
{
    public sealed partial class MainPage : Page
    {
        #region Public Constructors

        public MainPage()
        {
            this.InitializeComponent();
        }

        #endregion Public Constructors

        #region Private Methods

        private void PlacementButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
        }

        private void RVButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
        }

        private void TerritoryButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
        }

        private void TimeButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
        }

        private void UserButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            UsersStackPanel.Visibility = UsersStackPanel.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        }

        private void VideoButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
        }

        #endregion Private Methods
    }
}
