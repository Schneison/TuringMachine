using System.Collections.Generic;

namespace TuringMachine.Data;

public class Machine
{
	private readonly Dictionary<State, Mesh> _meshes;
	private readonly Tape[] _tapes;

	public Machine(Design design)
	{
		CurrentState = State.Empty;
		_tapes = new Tape[design.TapeCount];
		for (var i = 0; i < _tapes.Length; i++)
		{
			_tapes[i] = Tape.CreateBlank();
		}
	}

	public State CurrentState
	{
		get;
		internal set;
	}

	public void TryApplyTransition()
	{
		
	}
}
