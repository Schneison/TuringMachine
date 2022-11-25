using System;
using System.Numerics;

namespace TuringMachine.Utils;

/// <summary>
///  Contains math utilities
/// </summary>
public static class MathUtils {
	/// <summary>
	///  Calculates the intersection of two circles.
	///  <p />
	///  The circles are defined by their center and radius. The intersection points are returned in an array.
	///  Possible results are:
	///  Empty array: The circles are identical or don't intersect.
	///  One point: The circles intersect in one point.
	///  Two points: The circles intersect in two points.
	/// </summary>
	/// <returns>
	///  Array with zero, one or two intersection points.
	/// </returns>
	public static Vector2[] IntersectCircle(Vector2 centerA, float radiusA, Vector2 centerB, float radiusB) {
		var centerDx = centerA.X - centerB.X;
		var centerDy = centerA.Y - centerB.Y;
		var r = MathF.Sqrt(centerDx * centerDx + centerDy * centerDy);

		// no intersection
		if (!(Math.Abs(radiusA - radiusB) <= r && r <= radiusA + radiusB)) {
			return Array.Empty<Vector2>();
		}

		var r2d = r * r;
		var r4d = r2d * r2d;
		var rASquared = radiusA * radiusA;
		var rBSquared = radiusB * radiusB;
		var a = (rASquared - rBSquared) / (2 * r2d);
		var r2R2 = rASquared - rBSquared;
		var c = MathF.Sqrt(2 * (rASquared + rBSquared) / r2d - r2R2 * r2R2 / r4d - 1);

		var fx = (centerA.X + centerB.X) / 2 + a * (centerB.X - centerA.X);
		var gx = c * (centerB.Y - centerA.Y) / 2;
		var ix1 = fx + gx;
		var ix2 = fx - gx;

		var fy = (centerA.Y + centerB.Y) / 2 + a * (centerB.Y - centerA.Y);
		var gy = c * (centerA.X - centerB.X) / 2;
		var iy1 = fy + gy;
		var iy2 = fy - gy;

		// if gy == 0 and gx == 0 then the circles are tangent and there is only one solution
		if (Math.Abs(gx) < float.Epsilon && Math.Abs(gy) < float.Epsilon) {
			return new[] {
				new Vector2(ix1, iy1)
			};
		}

		return new[] {
			new Vector2(ix1, iy1),
			new Vector2(ix2, iy2)
		};
	}
}