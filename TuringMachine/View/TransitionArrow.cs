using System.ComponentModel;
using System.Numerics;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using TuringMachine.Model;
using TuringMachine.Utils;

namespace TuringMachine.View;

public class TransitionArrow : Shape {
	private const double ArrowHeadWidth = 4;
	private const double ArrowHeadHeight = 7;
	private const double ArrowHeadAngle = 90;
	private const int ArrowSelfRadius = StateElement.StateRadius - 5;

	public static readonly DependencyProperty TransitionProperty =
		DependencyProperty.Register(
			nameof(Transition),
			typeof(ConnectionElement),
			typeof(TransitionArrow),
			new FrameworkPropertyMetadata(null,
				FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender,
				OnTransitionChanged));

	private readonly PropertyChangedEventHandler _changeHandler;

	public TransitionArrow() {
		_changeHandler = (_, _) => {
			InvalidateVisual();
			InvalidateMeasure();
		};
	}


	protected override Geometry DefiningGeometry =>
		Transition.IsSelfLoop ? DrawSelfLinkArrow(Transition) : DrawLinkArrow(Transition);

	public ConnectionElement Transition {
		get => (ConnectionElement)GetValue(TransitionProperty);
		set => SetValue(TransitionProperty, value);
	}

	private static void OnTransitionChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e) {
		//TODO: Find a way to work around this hack, but should be save because the arrow should be removed with the connection.
		if (obj is not TransitionArrow arrow) {
			return;
		}

		var oldValue = e.OldValue;
		if (oldValue is ConnectionElement oldConnection) {
			oldConnection.PropertyChanged -= arrow._changeHandler;
		}

		var newValue = e.NewValue;
		if (newValue is ConnectionElement newConnection) {
			newConnection.PropertyChanged += arrow._changeHandler;
		}
	}

	// https://learn.microsoft.com/de-de/dotnet/api/system.windows.media.arcsegment?view=windowsdesktop-7.0
	private static Geometry DrawSelfLinkArrow(ConnectionElement? element) {
		if (element == null) {
			return Geometry.Empty;
		}

		var lineGroup = new GeometryGroup();
		var intersections = MathUtils.IntersectCircle(
			new Vector2(StateElement.StateRadius, StateElement.StateRadius),
			StateElement.StateRadius,
			new Vector2(StateElement.StateRadius, element.AlternativeDirection ? 2 * StateElement.StateRadius : 0),
			ArrowSelfRadius
		);
		var geometry = new StreamGeometry();
		var p1 = intersections[0];
		var p2 = intersections[1];
		var theta = p2.AngleInDegree(Vector2.One) + (element.AlternativeDirection ? 45 / 2 : -90);
		using (var ctx = geometry.Open()) {
			ctx.BeginFigure(p1.ToPoint(), false, false);
			ctx.ArcTo(p2.ToPoint(), new Size(ArrowSelfRadius, ArrowSelfRadius),
				0, true,
				SweepDirection.Clockwise, true, true);
		}

		geometry.Freeze();

		lineGroup.Children.Add(geometry);
		lineGroup.Children.Add(DrawArrowHead(p2.ToPoint(), theta));
		return lineGroup;
	}

	private static Geometry DrawLinkArrow(ConnectionElement? element) {
		var p1 = element?.StartPoint ?? Vector2.Zero;
		var p2 = element?.EndPoint ?? Vector2.Zero;
		var lineGroup = new GeometryGroup();
		var theta = p1.AngleInDegree(p2);

		var p = new Point(p2.X, p2.Y);

		lineGroup.Children.Add(DrawArrowHead(p, theta));

		var connectorGeometry = new LineGeometry {
			StartPoint = p1.ToPoint(),
			EndPoint = p2.ToPoint()
		};
		lineGroup.Children.Add(connectorGeometry);

		return lineGroup;
	}

	private static Geometry DrawArrowHead(Point p, double theta) {
		var pathGeometry = new PathGeometry();
		var pathFigure = new PathFigure {
			StartPoint = p,
			IsClosed = true
		};
		var lPoint = new Point(p.X + ArrowHeadWidth, p.Y + ArrowHeadHeight);
		var rPoint = new Point(p.X - ArrowHeadWidth, p.Y + ArrowHeadHeight);
		var seg1 = new LineSegment {
			Point = lPoint
		};
		pathFigure.Segments.Add(seg1);

		var seg2 = new LineSegment {
			Point = rPoint
		};
		pathFigure.Segments.Add(seg2);

		var seg3 = new LineSegment {
			Point = p
		};
		pathFigure.Segments.Add(seg3);

		pathGeometry.Figures.Add(pathFigure);
		var transform = new RotateTransform {
			Angle = theta + ArrowHeadAngle,
			CenterX = p.X,
			CenterY = p.Y
		};
		pathGeometry.Transform = transform;

		return pathGeometry;
	}
}