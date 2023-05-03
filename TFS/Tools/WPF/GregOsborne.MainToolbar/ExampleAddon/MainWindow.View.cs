// <copyright file="MainWindow.View.cs" company="">
// Copyright (c) 2020 All rights reserved
// </copyright>
// <author>IDOTCENTRAL\gosborn</author>
// <date>3/16/2020</date>

namespace ExampleAddon {
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;

    using GregOsborne.Application;
    using GregOsborne.MVVMFramework;

    public partial class MainWindowView : ViewModelBase {
        private DayOfWeek selectedDayOfTheWeek = default;
        private ObservableCollection<DayOfWeek> weekDays = default;
        private string windowTitle = default;
        public DayOfWeek SelectedDayOfTheWeek {
            get => this.selectedDayOfTheWeek;
            set {
                this.selectedDayOfTheWeek = value;
                InvokePropertyChanged(Reflection.GetPropertyName());
            }
        }

        public ObservableCollection<DayOfWeek> WeekDays {
            get => this.weekDays;
            set {
                this.weekDays = value;
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

        public override void Initialize() {
            WeekDays = new ObservableCollection<DayOfWeek>();
            Enum.GetNames(typeof(DayOfWeek)).ToList().ForEach(x => WeekDays.Add((DayOfWeek)Enum.Parse(typeof(DayOfWeek), x)));
        }
    }
}
