using System.ComponentModel;
using System.Globalization;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TuringMachine.Model;

/// <summary>
/// Connection element model for the graph
/// </summary>
public class ConnectionElement : GraphElement {
	private readonly PropertyChangedEventHandler _changedHandler;
	private bool _alternativeDirection;
	private StateElement _end;
	private StateElement _start;

	public ConnectionElement(StateElement start, StateElement end) : this(start, end, true) {
	}

	public ConnectionElement(StateElement start, StateElement end, bool alternativeDirection) {
		_start = start;
		_end = end;
		_alternativeDirection = alternativeDirection;
		_changedHandler = (_, _) => {
			OnPropertyChanged(nameof(PositionX));
			OnPropertyChanged(nameof(PositionY));
			OnPropertyChanged(nameof(TextOffset));
		};
		_start.PropertyChanged += _changedHandler;
		_end.PropertyChanged += _changedHandler;
	}

	public bool IsSelfLoop => _start.Equals(_end);

	public Vector2 StartPoint => (_start.Position - _end.Position) / 2
	                                                                   + Dir * StateElement.StateRadius;

	public Vector2 EndPoint => (_end.Position - _start.Position) / 2
	                                                                 + -Dir * StateElement.StateRadius;

	public Vector2 Center => (_start.Position + _end.Position) / 2 + StateElement.StateCenter;

	public Vector2 Dir => Vector2.Normalize(_end.Position - _start.Position);

	public Vector2 Perpendicular {
		get {
			var dir = Dir;
			return AlternativeDirection ? new Vector2(dir.Y, -dir.X) : new Vector2(-dir.Y, dir.X);
		}
	}

	public float PositionX => Center.X;

	public float PositionY => Center.Y;

	public Thickness TextOffset {
		get {
			var overflow = TextOverflow();
			return new Thickness(overflow.X, overflow.Y, 0, 0);
		}
	}

	public bool AlternativeDirection {
		get => _alternativeDirection;
		set => SetProperty(ref _alternativeDirection, value);
	}

	public StateElement Start {
		get => _start;
		set => SetProperty(ref _start, value);
	}

	public StateElement End {
		get => _end;
		set => SetProperty(ref _end, value);
	}

	/// <summary>
	/// Get the required height and width of the specified text. Uses FortammedText
	/// </summary>
	public static Size MeasureTextSize(string text, FontFamily fontFamily, FontStyle fontStyle, FontWeight fontWeight, FontStretch fontStretch, double fontSize) {
		var ft = new FormattedText(text,
			CultureInfo.CurrentCulture,
			FlowDirection.LeftToRight,
			new Typeface(this.textBlock.FontFamily, this.textBlock.FontStyle, this.textBlock.FontWeight, this.textBlock.FontStretch),
			fontSize,
			Brushes.Black, VisualTreeHelper.GetDpi(this.textBlock).PixelsPerDip);
		return new Size(ft.Width, ft.Height);
	}

	/// <summary>
	/// Get the required height and width of the specified text. Uses Glyph's
	/// </summary>
	public static Size MeasureText(string text, FontFamily fontFamily, FontStyle fontStyle, FontWeight fontWeight, FontStretch fontStretch, double fontSize) {
		var typeface = new Typeface(fontFamily, fontStyle, fontWeight, fontStretch);

		if (!typeface.TryGetGlyphTypeface(out var glyphTypeface)) {
			return MeasureTextSize(text, fontFamily, fontStyle, fontWeight, fontStretch, fontSize);
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

	"a | b | 0"

	public Vector2 TextOverflow() {
		if (IsSelfLoop) {
			if (_alternativeDirection) {
				return new Vector2(0, StateElement.StateRadius * 2);
			}

			return new Vector2(0, -StateElement.StateRadius * 2);
		}

		return Perpendicular * 0;
	}

	public void OnRemoveNode() {
		_start.PropertyChanged -= _changedHandler;
		_end.PropertyChanged -= _changedHandler;
	}
}