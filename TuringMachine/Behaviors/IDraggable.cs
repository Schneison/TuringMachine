using System.Numerics;


namespace TuringMachine.Behaviors
{
    /// <summary>
    /// Interface for implementing drag and drop behavior of an element.
    /// </summary>
    public interface IDraggable {
        /// <summary>
        /// Called when the mouse button is released while moving this element.
        /// </summary>
        void Dropped();

        /// <summary>
        /// Called when the mouse button is pressed and the mouse is moving.
        /// </summary>
        /// <param name="x">New x position of the element</param>
        /// <param name="y">New y position of the element</param>
        void Moved(double x, double y);

        /// <summary>
        /// Current position of this element.
        /// </summary>
        Vector2 Position { get; }
    }
}
