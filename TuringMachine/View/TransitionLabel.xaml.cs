using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TuringMachine.View {
	/// <summary>
	/// Interaction logic for TransitionLabel.xaml
	/// </summary>
	public partial class TransitionLabel : UserControl {
		private readonly TextBlock _textBlock;

		public TransitionLabel() {
			InitializeComponent();
			DataContext = this;
			_textBlock = (TextBlock) FindName("label");
		}

		private Typeface CreateTypeface() {
			return new Typeface(_textBlock.FontFamily, _textBlock.FontStyle, _textBlock.FontWeight,
				_textBlock.FontStretch);
		}

		/// <summary>
		/// Get the required height and width of the specified text. Uses FortammedText
		/// </summary>
		public Size MeasureTextSize(string text, double fontSize) {
			var ft = new FormattedText(text,
				CultureInfo.CurrentCulture,
				FlowDirection.LeftToRight,
				CreateTypeface(),
				fontSize,
				Brushes.Black, VisualTreeHelper.GetDpi(_textBlock).PixelsPerDip);
			return new Size(ft.Width, ft.Height);
		}

		/// <summary>
		/// Get the required height and width of the specified text. Uses Glyph's
		/// </summary>
		public Size MeasureText(string text, double fontSize) {
			var typeface = CreateTypeface();

			if (!typeface.TryGetGlyphTypeface(out var glyphTypeface)) {
				return MeasureTextSize(text, fontSize);
			}

			double totalWidth = 0;
			double height = 0;

			foreach (var textChar in text) {
				var glyphIndex = glyphTypeface.CharacterToGlyphMap[textChar];

				var width = glyphTypeface.AdvanceWidths[glyphIndex] * fontSize;

				var glyphHeight = glyphTypeface.AdvanceHeights[glyphIndex] * fontSize;

				if (glyphHeight > height) {
					height = glyphHeight;
				}

				totalWidth += width;
			}

			return new Size(totalWidth, height);
		}
	}
}
