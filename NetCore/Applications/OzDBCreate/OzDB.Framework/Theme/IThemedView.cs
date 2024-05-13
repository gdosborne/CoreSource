namespace OzDB.Application.Theme {
	using System.Windows.Media;

	public interface IThemedView {
		SolidColorBrush ActiveCaptionBrush {
			get; set;
		}

		SolidColorBrush ActiveCaptionTextBrush {
			get; set;
		}

		SolidColorBrush BorderBrush {
			get; set;
		}

		SolidColorBrush ControlBorderBrush {
			get; set;
		}

		double FontSize {
			get; set;
		}

		ApplicationTheme Theme {
			get; set;
		}

		SolidColorBrush WindowBrush {
			get; set;
		}

		SolidColorBrush WindowTextBrush {
			get; set;
		}

		void ApplyVisualElement<T>(VisualElement<T> element);
	}
}
