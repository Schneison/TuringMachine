using System;
using System.Numerics;
using System.Windows;

namespace TuringMachine.Utils; 

public static class MathExtensions {
    /// <summary>
    ///     Calculates the angle between to vectors in radians.
    /// </summary>
    public static double Angle(this Vector2 a, Vector2 b) {
        return Math.Atan2(b.Y - a.Y, b.X - a.X);
    }


    /// <summary>
    ///     Calculates the angle between to vectors in degree.
    /// </summary>
    public static double AngleInDegree(this Vector2 a, Vector2 b) {
        return Angle(a, b) * 180 / Math.PI;
    }

    /// <summary>
    ///     Converts vector into point.
    /// </summary>
    public static Point ToPoint(this Vector2 value) {
        return new(value.X, value.Y);
    }
}