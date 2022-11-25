namespace TuringMachine.Resources;

/// <summary>
///  Thrown when an error occurs while deserializing a turing machine script.
/// </summary>
public class ScriptException : Exception {
	/// <summary>
	///  Creates a new ScriptException with the given message.
	/// </summary>
	public ScriptException(string? message) : base(message) {
	}
}