// <copyright file="SecurityItemBase.cs" company="">
// Copyright (c) 2020 All rights reserved
// </copyright>
// <author>IDOTCENTRAL\gosborn</author>
// <date>3/2/2020</date>

namespace GregOsborne.PasswordManager.Data {
    using System.ComponentModel;
    using System.Windows.Media;
    using GregOsborne.Application;

    public abstract class SecurityItemBase : INotifyPropertyChanged,ISecurityView {
        private string description = default;
        private string name = default;
        public event PropertyChangedEventHandler PropertyChanged;
        public string Description {
            get => this.description;
            set {
                this.description = value;
                InvokePropertyChanged(Reflection.GetPropertyName());
            }
        }

        public string Name {
            get => this.name;
            set {
                this.name = value;
                InvokePropertyChanged(Reflection.GetPropertyName());
            }
        }

        protected void InvokePropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public SolidColorBrush ControlBorderBrush { get; set; }

        public double FontSize { get; set; }

        public SolidColorBrush WindowBrush { get; set; }

        public SolidColorBrush WindowTextBrush { get; set; }

        public double ItemTitleFontSize { get; set; }
    }
}
