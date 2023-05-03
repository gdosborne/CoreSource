// <copyright file="SecurityGroupWindow.view.cs" company="">
// Copyright (c) 2020 All rights reserved
// </copyright>
// <author>IDOTCENTRAL\gosborn</author>
// <date>2/26/2020</date>

namespace GregOsborne.PasswordManager {
    using System.Windows;
    using System.Windows.Media;

    using GregOsborne.Application;
    using GregOsborne.Application.Theme;
    using GregOsborne.MVVMFramework;

    public partial class SecurityGroupWindowView : ViewModelBase, IThemedView {
        private SolidColorBrush activeCaptionBrush = default;
        private SolidColorBrush activeCaptionTextBrush = default;
        private SolidColorBrush borderBrush = default;
        private SolidColorBrush controlBorderBrush = default;
        private string description = default;
        private bool? dialogResult = default;
        private double fontSize = default;
        private string groupName = default;
        private bool isInitializing = true;
        private ApplicationTheme theme = default;
        private SolidColorBrush windowBrush = default;
        private SolidColorBrush windowTextBrush = default;
        private string windowTitle = default;
        public SecurityGroupWindowView() {
            ActiveCaptionBrush = SystemColors.ActiveCaptionBrush;
            ActiveCaptionTextBrush = SystemColors.ActiveCaptionTextBrush;
            WindowBrush = SystemColors.WindowBrush;
            WindowTextBrush = SystemColors.WindowTextBrush;
            BorderBrush = SystemColors.ActiveBorderBrush;
            ControlBorderBrush = SystemColors.ControlDarkBrush;
            FontSize = 12.0;
            WindowTitle = "Security Group";
        }

        public SolidColorBrush ActiveCaptionBrush {
            get => this.activeCaptionBrush;
            set {
                this.activeCaptionBrush = value;
                InvokePropertyChanged(Reflection.GetPropertyName());
            }
        }

        public SolidColorBrush ActiveCaptionTextBrush {
            get => this.activeCaptionTextBrush;
            set {
                this.activeCaptionTextBrush = value;
                InvokePropertyChanged(Reflection.GetPropertyName());
            }
        }

        public SolidColorBrush BorderBrush {
            get => this.borderBrush;
            set {
                this.borderBrush = value;
                InvokePropertyChanged(Reflection.GetPropertyName());
            }
        }

        public SolidColorBrush ControlBorderBrush {
            get => this.controlBorderBrush;
            set {
                this.controlBorderBrush = value;
                InvokePropertyChanged(Reflection.GetPropertyName());
            }
        }

        public string Description {
            get => this.description;
            set {
                this.description = value;
                InvokePropertyChanged(Reflection.GetPropertyName());
            }
        }

        public bool? DialogResult {
            get => this.dialogResult;
            set {
                this.dialogResult = value;
                InvokePropertyChanged(Reflection.GetPropertyName());
            }
        }

        public double FontSize {
            get => this.fontSize;
            set {
                this.fontSize = value;
                InvokePropertyChanged(Reflection.GetPropertyName());
            }
        }

        public string GroupName {
            get => this.groupName;
            set {
                this.groupName = value;
                InvokePropertyChanged(Reflection.GetPropertyName());
            }
        }

        public ApplicationTheme Theme {
            get => this.theme;
            set {
                this.theme = value;
                ActiveCaptionBrush = Theme.ActiveCaptionBrush.Value;
                ActiveCaptionTextBrush = Theme.ActiveCaptionTextBrush.Value;
                BorderBrush = Theme.BorderBrush.Value;
                ControlBorderBrush = Theme.ControlBorderBrush.Value;
                WindowBrush = Theme.WindowBrush.Value;
                WindowTextBrush = Theme.WindowTextBrush.Value;
                FontSize = Theme.FontSize.Value;
                InvokePropertyChanged(Reflection.GetPropertyName());
            }
        }

        public SolidColorBrush WindowBrush {
            get => this.windowBrush;
            set {
                this.windowBrush = value;
                InvokePropertyChanged(Reflection.GetPropertyName());
            }
        }

        public SolidColorBrush WindowTextBrush {
            get => this.windowTextBrush;
            set {
                this.windowTextBrush = value;
                InvokePropertyChanged(Reflection.GetPropertyName());
            }
        }

        public string WindowTitle {
            get => this.windowTitle;
            set {
                this.windowTitle = value;
                InvokePropertyChanged(Reflection.GetPropertyName());
            }
        }

        public void ApplyVisualElement<T>(VisualElement<T> element) => throw new System.NotImplementedException();

        public override void Initialize() =>
            //do all initialization for your view here
            this.isInitializing = false;
    }
}
