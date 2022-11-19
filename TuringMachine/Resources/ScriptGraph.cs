using TuringMachine.Utils;

namespace TuringMachine.Resources;

public record Connection(string FromState, string ToState);

/// <summary>
///     Header of a turing Machine graph.
///     <p />
///     Defines the name, the initial state and the final state of the graph.
/// </summary>
/// <param name="Name">Name of the graph</param>
/// <param name="StartState">Initial state</param>
/// <param name="EndState">Final state</param>
public record GraphHeader(string Name, string StartState, string EndState) {
	public bool IsValid() {
		return StartState.Length > 0 && EndState.Length > 0;
	}
}

public record ScriptGraph(
	MultiDictionary<Connection, string> Connections,
	bool MultiTape,
	GraphHeader Header) {
}