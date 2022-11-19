using System.Collections.Generic;

namespace TuringMachine.Data;

/// <summary>
///     Main class for the turing machine simulation.
/// </summary>
public class Machine {
	/// <summary>
	///     Meshes for every state.
	/// </summary>
	private readonly Dictionary<State, Mesh> _meshes;

	/// <summary>
	///     All tapes of the machine.
	/// </summary>
	private readonly Tape[] _tapes;

	/// <summary>
	///     Creates a new machine for the given design.
	/// </summary>
	/// <param name="design">Config of the machine.</param>
	public Machine(Design design) {
		CurrentState = State.Empty;
		_tapes = new Tape[design.TapeCount];
		for (var i = 0; i < _tapes.Length; i++) {
			_tapes[i] = Tape.CreateBlank();
		}
	}

	/// <summary>
	///     Current state of the machine.
	/// </summary>
	public State CurrentState { get; }

	/// <summary>
	///     Tries to execute the next step of the machine.
	/// </summary>
	public void TryApplyTransition() {
		// Current empty
	}
}