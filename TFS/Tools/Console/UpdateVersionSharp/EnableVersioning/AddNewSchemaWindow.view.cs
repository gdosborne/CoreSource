namespace EnableVersioning {
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Windows;
	using GregOsborne.Application;
	using GregOsborne.MVVMFramework;
	using VersionMaster;
	using static VersionMaster.Enumerations;

	public partial class AddNewSchemaWindowView : ViewModelBase {
		//private VersionSchemaPartItem buildPart = default;
		private Visibility buildVisibility = default;
		private bool? dialogResult = default;
		//private VersionSchemaPartItem majorPart = default;
		private Visibility majorVisibility = default;
		private ObservableCollection<TransformTypes> methods = default;
		//private VersionSchemaPartItem minorPart = default;
		private Visibility minorVisibility = default;

		private Dictionary<TransformTypes, bool> requiresParameter = new Dictionary<TransformTypes, bool> {
			{ TransformTypes.Day, false },
			{ TransformTypes.DayValue, false },
			{ TransformTypes.DayvalueFrom, true },
			{ TransformTypes.Fixed, true },
			{ TransformTypes.Ignore, false },
			{ TransformTypes.Increment, false },
			{ TransformTypes.IncrementEachDay, false },
			{ TransformTypes.IncrementResetEachDay, false },
			{ TransformTypes.Month, false },
			{ TransformTypes.Random, false },
			{ TransformTypes.SecondValue, false },
			{ TransformTypes.SecondValueFrom, true },
			{ TransformTypes.TwoDigitYear, false },
			{ TransformTypes.Year, false },
		};

		//private VersionSchemaPartItem revisionPart = default;
		private Visibility revisionVisibility = default;
		private VersionMaster.SchemaItem schema = default;
		private string schemaName = default;

		//public VersionSchemaPartItem BuildPart {
		//	get => this.buildPart;
		//	set {
		//		this.buildPart = value;
		//		this.InvokePropertyChanged(Reflection.GetPropertyName());
		//	}
		//}

		public Visibility BuildVisibility {
			get => this.buildVisibility;
			set {
				this.buildVisibility = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public bool? DialogResult {
			get => this.dialogResult;
			set {
				this.dialogResult = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		//public VersionSchemaPartItem MajorPart {
		//	get => this.majorPart;
		//	set {
		//		this.majorPart = value;
		//		this.InvokePropertyChanged(Reflection.GetPropertyName());
		//	}
		//}

		public Visibility MajorVisibility {
			get => this.majorVisibility;
			set {
				this.majorVisibility = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public ObservableCollection<TransformTypes> Methods {
			get => this.methods;
			set {
				this.methods = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		//public VersionSchemaPartItem MinorPart {
		//	get => this.minorPart;
		//	set {
		//		this.minorPart = value;
		//		this.InvokePropertyChanged(Reflection.GetPropertyName());
		//	}
		//}

		public Visibility MinorVisibility {
			get => this.minorVisibility;
			set {
				this.minorVisibility = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		//public VersionSchemaPartItem RevisionPart {
		//	get => this.revisionPart;
		//	set {
		//		this.revisionPart = value;
		//		this.InvokePropertyChanged(Reflection.GetPropertyName());
		//	}
		//}

		public Visibility RevisionVisibility {
			get => this.revisionVisibility;
			set {
				this.revisionVisibility = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public SchemaItem Schema {
			get => this.schema;
			set {
				this.schema = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public string SchemaName {
			get => this.schemaName;
			set {
				this.schemaName = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public override void Initialize() {
			this.Methods = new ObservableCollection<TransformTypes> {
				TransformTypes.Day,
				TransformTypes.DayValue,
				TransformTypes.DayvalueFrom,
				TransformTypes.Fixed,
				TransformTypes.Ignore,
				TransformTypes.Increment,
				TransformTypes.IncrementEachDay,
				TransformTypes.IncrementResetEachDay,
				TransformTypes.Month,
				TransformTypes.Random,
				TransformTypes.SecondValue,
				TransformTypes.SecondValueFrom,
				TransformTypes.TwoDigitYear,
				TransformTypes.Year
			};
			this.MajorVisibility = this.MinorVisibility = this.BuildVisibility = this.RevisionVisibility = Visibility.Hidden;
		}
	}
}