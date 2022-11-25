using System.Windows;
using System.Windows.Input;
using Microsoft.Xaml.Behaviors;

namespace TuringMachine.Behaviors;

// Inspired by:
// https://www.thomasb.fr/2015/01/wpf-dragdrop-items-in-a-canvas-communicate-between-behavior-and-viewmodel/
// https://github.com/cosmo0/DragSnap/blob/master/DragSnap/Behaviors/DragOnCanvasBehavior.cs
public class DragBehavior : Behavior<DependencyObject> {
	public static readonly DependencyProperty DropHandlerProperty =
		DependencyProperty.RegisterAttached(
			"DropHandler",
			typeof(IDraggable),
			typeof(DragBehavior),
			new PropertyMetadata(OnDropHandlerChanged));


	/// <summary>
	///  Current position of the element.
	/// </summary>
	private Point _elementPos = new(-1, -1);

	/// <summary>
	///  Position of the mouse when the mouse button is pressed.
	/// </summary>
	private Point _mouseStartPos = new(-1, -1);

	/// <summary>
	///  Handler of this behavior.
	/// </summary>
	private IDraggable? DropHandler { get; init; }

	/// <summary>
	///  Retrieves the value from the attached property from the given element.
	///  <p />
	///  Needed by <see cref="DropHandlerProperty" /> for the binding.
	/// </summary>
	/// <returns>Drop handler of the given element.</returns>
	public static IDraggable GetDropHandler(UIElement target) {
		return (IDraggable)target.GetValue(DropHandlerProperty);
	}

	/// <summary>
	///  Sets the value of the attached property from the given element.
	///  <p />
	///  Needed by <see cref="DropHandlerProperty" /> for the binding.
	/// </summary>
	public static void SetDropHandler(UIElement target, bool value) {
		target.SetValue(DropHandlerProperty, value);
	}

	/// <summary>
	///  initialize the instance and attach events to the element
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="eventArgs"></param>
	private static void OnDropHandlerChanged(object sender, DependencyPropertyChangedEventArgs eventArgs) {
		var element = (UIElement)sender;
		var handler = (IDraggable)eventArgs.NewValue;


		var instance = new DragBehavior {
			DropHandler = handler
		};

		// attach or detach the handler to the element
		// remember that the handler can be removed at run-time, so we need handle null values
		if (instance.DropHandler != null) {
			element.MouseLeftButtonDown += instance.OnMouseDown;
			element.MouseLeftButtonUp += instance.OnMouseUp;
			element.MouseMove += instance.ElementOnMouseMove;
		}
		else {
			element.MouseLeftButtonDown -= instance.OnMouseDown;
			element.MouseLeftButtonUp -= instance.OnMouseUp;
			element.MouseMove -= instance.ElementOnMouseMove;
		}
	}

	/// <summary>
	///  When the user starts dragging
	/// </summary>
	private void OnMouseDown(object sender, MouseButtonEventArgs eventArgs) {
		// save the mouse position on button down
		// we only want a diff of the mouse position so we don't care much about which element we use as reference
		_mouseStartPos = GetMousePositionFromMainWindow(eventArgs);
		((UIElement)sender).CaptureMouse();
	}

	/// <summary>
	///  When the user stops dragging
	/// </summary>
	private void OnMouseUp(object sender, MouseButtonEventArgs eventArgs) {
		var element = (UIElement)sender;
		element.ReleaseMouseCapture();

		// Send a message to the viewmodel that the mouse button has been released
		DropHandler?.Dropped();
	}

	/// <summary>
	///  Checks if both coordinate values of the specified position are greater or equal to zero.
	/// </summary>
	/// <returns>True, if both values are greater or equal to zero.</returns>
	protected static bool IsValidPos(Point pos) {
		return pos.X >= 0 && pos.Y >= 0;
	}

	// while the user is dragging
	private void ElementOnMouseMove(object sender, MouseEventArgs eventArgs) {
		// don't do anything if no button is clicked (or there is no handler)
		if (!((UIElement)sender).IsMouseCaptured || DropHandler == null) {
			return;
		}

		// initialize the mouse start position if it's not already initialized
		if (!IsValidPos(_mouseStartPos)) {
			_mouseStartPos = GetMousePositionFromMainWindow(eventArgs);
		}

		// calculate element movement
		var mouseNewPos = GetMousePositionFromMainWindow(eventArgs);
		var movement = mouseNewPos - _mouseStartPos;

		// make sure the mouse has moved since last time we were here
		// (the MouseMove is run in loop while the button is clicked, even if the mouse isn't moving)
		if (movement.Length <= 0) {
			return;
		}

		// save current mouse position
		_mouseStartPos = mouseNewPos;

		// Initialise the position of the element if it hasn't been done yet
		if (!IsValidPos(_elementPos)) {
			_elementPos = new Point(DropHandler.Position.X, DropHandler.Position.Y);
		}

		// save the element movement
		var elementNewPos = _elementPos + movement;
		_elementPos = elementNewPos;

		// notify the viewmodel that the element has been moved
		DropHandler.Moved(elementNewPos.X, elementNewPos.Y);
	}

	/// <summary>
	///  Retrieves the mouse position relative to the main window.
	/// </summary>
	private static Point GetMousePositionFromMainWindow(MouseEventArgs eventArgs) {
		var mainWindow = Application.Current.MainWindow;
		return eventArgs.GetPosition(mainWindow);
	}
}