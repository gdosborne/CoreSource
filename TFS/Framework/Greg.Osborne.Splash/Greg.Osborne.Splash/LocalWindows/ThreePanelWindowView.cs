namespace Greg.Osborne.Splash.LocalWindows {
    using System.Windows;
    using Application;
    using GregOsborne.Application;
    using GregOsborne.MVVMFramework;

    public class ThreePanelWindowView : ViewModelBase {
        public override void Initialize() => base.Initialize();

        private CornerRadius _CornerRadius = default(CornerRadius);
        public CornerRadius CornerRadius {
            get => _CornerRadius;
            set {
                _CornerRadius = value;
                InvokePropertyChanged(Reflection.GetPropertyName());
            }
        }
    }
}
