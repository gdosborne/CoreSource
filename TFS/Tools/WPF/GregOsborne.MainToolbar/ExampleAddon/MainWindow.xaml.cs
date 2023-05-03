// <copyright file="MainWindow.xaml.cs" company="">
// Copyright (c) 2020 All rights reserved
// </copyright>
// <author>IDOTCENTRAL\gosborn</author>
// <date>3/16/2020</date>

namespace ExampleAddon {
    using System.Windows;

    using GregOsborne.Application.Primitives;
    using GregOsborne.Application.Windows.Controls;

    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            //this.DataContext = View;
            View.Initialize();
            
        }

        public MainWindowView View => DataContext.As<MainWindowView>();

        //private MainWindowView view = default;
        //public MainWindowView View {
        //    get => DataContext.As<MainWindowView>();
        //    private set => view = value;
        //}
    }
}
