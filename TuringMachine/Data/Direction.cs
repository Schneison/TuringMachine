namespace TuringMachine.Data;

/// <summary>
/// Describes a direction that the turing machine can move on one tape at a transition.
/// One direction is defined per mutation on one of the tapes.
/// </summary>
public enum Direction
{
	/// <summary>
	/// Tells the TM to don't move the head of the tape
	/// </summary>
	None, 
	/// <summary>
	/// Tells the TM to move the head of the tape one to the left 
	/// </summary>
	Left, 
	/// <summary>
	/// Tells the TM to move the head of the tape one to the right
	/// </summary>
	Right
}