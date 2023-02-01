﻿using CongregationManager.Extensibility;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace CongregationExtension {
    public class ExtensionPanel : IExtensionPanel, INotifyPropertyChanged {
        public ExtensionPanel(string title, string glyph, UserControl control) {
            Title = title;
            Glyph = glyph;
            Control = control;
        }

        #region Glyph Property
        private string _Glyph = default;
        /// <summary>Gets/sets the Glyph.</summary>
        /// <value>The Glyph.</value>
        public string Glyph {
            get => _Glyph;
            set {
                _Glyph = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Title Property
        private string _Title = default;
        /// <summary>Gets/sets the Extension Title.</summary>
        /// <value>The ExtensionName.</value>
        public string Title {
            get => _Title;
            set {
                _Title = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Control Property
        private UserControl _Control = default;
        /// <summary>Gets/sets the Control.</summary>
        /// <value>The Control.</value>
        public UserControl Control {
            get => _Control;
            set {
                _Control = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = default) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}