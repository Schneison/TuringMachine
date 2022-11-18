using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace TuringMachine.Data;

public class Mesh
{
	private readonly  ImmutableDictionary<ITuple, Transition> _transitions;

	public Mesh(State state, IEnumerable<Transition> transitions)
	{
		State = state;
		_transitions = BuildTransitions(transitions);
	}

	private static ImmutableDictionary<ITuple, Transition> BuildTransitions(IEnumerable<Transition> transitions)
	{
		var trxBuilder = ImmutableDictionary.CreateBuilder<ITuple, Transition>();
		foreach (var trx in transitions)
		{
			foreach (var comb in trx.GetInputCombinations())
			{
				trxBuilder[comb] = trx;
			}
		}

		return trxBuilder.ToImmutable();
	}

	public State State
	{
		get;
	}
	
	public Transition Find(ISymbol[] symbols)
	{
		return Transition.None;
	}
}