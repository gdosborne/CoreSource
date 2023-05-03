using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using Greg.Osborne.Installer.Builder.Controls;
using GregOsborne.Application.Primitives;
using GregOsborne.MVVMFramework;

namespace Greg.Osborne.Installer.Builder {
	public partial class SelectIconWindowView : ViewModelBase {
		public SelectIconWindowView() {
			this.FontIcons = new ObservableCollection<FontItem>();
			this.GridSize = 10;
		}

		public override void Initialize() => this.PageNumber = 0;

		//private string currentHex = default;
		//public string CurrentHex {
		//	get => this.currentHex;
		//	set {
		//		this.currentHex = value;
		//		this.InvokePropertyChanged(Reflection.GetPropertyName());
		//	}
		//}

		private int currentIndex = default;
		public int CurrentIndex {
			get => this.currentIndex;
			set {
				this.currentIndex = value;
				this.CurrentImage = char.ConvertFromUtf32(this.CurrentIndex);
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		private string currentImage = default;
		public string CurrentImage {
			get => this.currentImage;
			set {
				this.currentImage = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		private int actualPageNumber = default;
		public int ActualPageNumber {
			get => this.actualPageNumber;
			set {
				this.actualPageNumber = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		private int pageNumber = default;
		public int PageNumber {
			get => this.pageNumber;
			set {
				this.pageNumber = value;
				this.PreviousVisibility = this.PageNumber > 0 ? Visibility.Visible : Visibility.Hidden;
				this.NextVisibility = this.PageNumber < this.TotalPages - 1 ? Visibility.Visible : Visibility.Hidden;
				this.BuildPage();
				this.ActualPageNumber = this.ActualPageNumber = this.PageNumber + 1;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		private int gridSize = default;
		public int GridSize {
			get => this.gridSize;
			set {
				this.gridSize = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		private Visibility nextVisibility = default;
		public Visibility NextVisibility {
			get => this.nextVisibility;
			set {
				this.nextVisibility = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		private Visibility previousVisibility = default;
		public Visibility PreviousVisibility {
			get => this.previousVisibility;
			set {
				this.previousVisibility = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public event ExecuteUiActionHandler ExecuteUiAction;
		private List<FontItem> characters = default;

		private void BuildPage() {
			this.FontIcons.Clear();
			if (this.GridSize < 10) {
				this.GridSize = 10;
			}
			var p = new Dictionary<string, object> {
				{ "areawidth", default(double) }
			};
			ExecuteUiAction?.Invoke(this, new ExecuteUiActionEventArgs("GetIconAreaWidth", p));
			var bdrWidth = (double)p["areawidth"];
			var numItemsPerPage = this.GridSize * this.GridSize;
			var gridWidth = bdrWidth / (this.GridSize);
			var fontSize = gridWidth - 8;
			if (this.characters == null) {
				this.characters = new List<FontItem>();
				var fontFamily = new FontFamily("Segoe MDL2 Assets");
				GlyphTypeface gtf = null;
				foreach (var tf in fontFamily.GetTypefaces()) {
					tf.TryGetGlyphTypeface(out gtf);
					if (gtf != null) {
						var charMap = gtf.CharacterToGlyphMap;
						foreach (var kvp in charMap) {
							if (kvp.Key == 13 || kvp.Key == 32) {
								continue;
							}
							var f = new FontItem {
								Item = char.ConvertFromUtf32(kvp.Key),
								FontSize = fontSize,
								Index = kvp.Key
							};
							f.ExecuteUiAction += this.F_ExecuteUiAction;
							this.characters.Add(f);
						}
						break;
					}
				}
				this.TotalPages = (this.characters.Count / numItemsPerPage);
				if (this.characters.Count > this.TotalPages * numItemsPerPage) {
					this.TotalPages++;
				}
				this.NextVisibility = this.PageNumber < this.TotalPages - 1 ? Visibility.Visible : Visibility.Hidden;
			}
			var items = this.characters.Skip(this.PageNumber * numItemsPerPage).Take(numItemsPerPage);
			items.ToList().ForEach(x => this.FontIcons.Add(x));
			ExecuteUiAction?.Invoke(this, new ExecuteUiActionEventArgs("CenterParent"));
		}

		private void F_ExecuteUiAction(object sender, ExecuteUiActionEventArgs e) => this.ExecuteUiAction(this, e);

		private bool? dialogResult = default;
		public bool? DialogResult {
			get => this.dialogResult;
			set {
				this.dialogResult = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		private ObservableCollection<FontItem> fontIcons = default;
		public ObservableCollection<FontItem> FontIcons {
			get => this.fontIcons;
			set {
				this.fontIcons = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		private int totalPages = default;
		public int TotalPages {
			get => this.totalPages;
			set {
				this.totalPages = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}
	}
}
