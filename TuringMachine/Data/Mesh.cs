using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace TuringMachine.Data;

/// <summary>
///     Network of transitions for one state of the turing machine.
///     Main use is to move from one state to the other.
/// </summary>
public class Mesh {
	/// <summary>
	///     All transitions of this mesh
	/// </summary>
	private readonly ImmutableDictionary<ITuple, Transition> _transitions;

	private Mesh(State state, int tapeCount, ImmutableDictionary<ITuple, Transition> transitions) {
		State = state;
		TapeCount = tapeCount;
		_transitions = transitions;
	}

	/// <summary>
	///     Retrieves valid transition for the specific tuple and throws exception if none was found.
	///     <p />
	///     Please check <see cref="Accepts" /> if you don't know if the tuple is valid.
	/// </summary>
	/// <param name="tuple">Valid input tuple for the mesh</param>
	public Transition this[ITuple tuple] => _transitions[tuple];

	/// <summary>
	///     State of the turing machine that the mesh belongs to.
	/// </summary>
	public State State { get; }

	/// <summary>
	///     Amount of tapes that this mesh accepts.
	///     Every transition has to have this amount of mutations defined.
	/// </summary>
	public int TapeCount { get; }

	/// <summary>
	///     Creates new builder for the given mesh.
	///     <p />
	///     Only way to create a mesh instance.
	/// </summary>
	/// <returns>A fresh builder instance.</returns>
	public static Builder CreateBuilder() {
		return new Builder();
	}

	/// <summary>
	///     Checks if the given tuple is a valid input for this mesh.
	/// </summary>
	/// <param name="tuple">Possible input</param>
	/// <returns>True if the tuple is valid.</returns>
	public bool Accepts(ITuple tuple) {
		return tuple.Length == TapeCount && _transitions.ContainsKey(tuple);
	}

	/// <summary>
	///     Returns a transition for the given input array of symbols.
	///     <p />
	///     Used by the turing machine to transition from one state to the other if all tapes contain the valid symbol for
	///     the equivalent mutation in the transition.
	/// </summary>
	/// <param name="symbols">Input array</param>
	/// <returns>Transition that contains the valid mutation for the equivalent symbol in the input array.</returns>
	public Transition Find(ISymbol[] symbols) {
		return Transition.None;
	}

	/// <summary>
	///     Builder that is the only way to create a mesh.
	/// </summary>
	public class Builder {
		private readonly HashSet<Transition> _transitions = new();
		private State _state = State.Empty;
		private int _tapeCount = 1;

		public Builder WithTransition(Transition transition) {
			_transitions.Add(transition);
			return this;
		}

		/// <summary>
		///     Defines the amount of tapes that this mesh accepts.
		///     Every transition has to have this amount of mutations defined.
		/// </summary>
		/// <param name="tapeCount">Amount of tapes</param>
		/// <returns>The builder instance for builder chaining.</returns>
		public Builder FromTape(int tapeCount) {
			_tapeCount = tapeCount;
			return this;
		}

		/// <summary>
		///     Defines the state of the turing machine that the mesh belongs to.
		/// </summary>
		/// <param name="state">A state of a turing machine</param>
		/// <returns>The builder instance for builder chaining.</returns>
		public Builder FromState(State state) {
			_state = _state;
			return this;
		}

		/// <summary>
		///     Creates transition dictionary that contains a key value pair for every possible input combination of
		///     all transitions to there transition.
		///     <p />
		///     Additionally checks if every transition has the right amount of mutations which has to equal the tape count.
		/// </summary>
		/// <returns>
		///     Dictionary that contains a key value pair for every possible input combination of
		///     all transitions to there transition.
		/// </returns>
		/// <exception cref="NotSupportedException"></exception>
		private ImmutableDictionary<ITuple, Transition> BuildTransitions() {
			var trxBuilder = ImmutableDictionary.CreateBuilder<ITuple, Transition>();
			foreach (var trx in _transitions) {
				foreach (var comb in trx.GetInputCombinations()) {
					if (comb.Length != _tapeCount) {
						throw new NotSupportedException(
							$"Mutation amount of a transition does not equal the amount of tapes {_tapeCount}: {trx}");
					}

					trxBuilder[comb] = trx;
				}
			}

			return trxBuilder.ToImmutable();
		}

		/// <summary>
		///     Creates a mesh from the content of this builder which was verified.
		/// </summary>
		/// <returns>Newly created valid mesh</returns>
		public Mesh Create() {
			return new Mesh(_state, _tapeCount, BuildTransitions());
		}
	}
}