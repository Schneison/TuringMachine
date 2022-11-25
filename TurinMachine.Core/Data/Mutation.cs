using System;
using System.Collections.Generic;

namespace TuringMachine.Data;

/// <summary>
///     Mutation is a key component of transition. It defines which symbols the transition accepts and which symbols serve
///     as possible replacements on the tape.
///     <p />
///     Furthermore, the mutation defines a direction in which the head
///     of the tape moves after the transition is completed.
/// </summary>
public abstract class Mutation : ITapeTransformer {
	/// <summary>
	///     An always invalid mutation that throws an error if someone should try to place it onto the tape.
	/// </summary>
	public static readonly Mutation None = new MutationNone();

	protected Mutation(Direction dir) {
		Dir = dir;
	}

	/// <summary>
	///     Direction the tape should move after accepting and applying this mutation.
	/// </summary>
	public Direction Dir { get; }

	/// <summary>
	///     Retrieves the symbol that should be placed onto the tape if this mutation input matches the symbol on the
	///     corresponding tape.
	/// </summary>
	/// <param name="input">Symbol on the tape that matched <see cref="Matches" /> of this mutation</param>
	/// <returns>Symbol for the tape</returns>
	public abstract ISymbol ToTape(ISymbol input);

	protected bool Equals(Mutation? other) {
		return other != null && Dir == other.Dir;
	}

	public override bool Equals(object? obj) {
		if (ReferenceEquals(null, obj)) {
			return false;
		}

		if (ReferenceEquals(this, obj)) {
			return true;
		}

		if (obj.GetType() != GetType()) {
			return false;
		}

		return Equals((Mutation)obj);
	}

	public override int GetHashCode() {
		return (int)Dir;
	}

	/// <summary>
	///     Checks if the given other symbol matches the criteria for this symbol.
	///     <p />
	///     This is used for example by wildcard symbols to match more than one symbol.
	/// </summary>
	/// <param name="symbol">Other symbol</param>
	/// <returns>True if the other symbol is an equivalent representation of this symbol.</returns>
	public abstract bool Matches(ISymbol symbol);

	/// <summary>
	///     Should return all symbols which gets accepted by <see cref="Matches" />.
	/// </summary>
	/// <returns>All symbols which gets accepted by this mutation.</returns>
	public abstract IEnumerable<ISymbol> GetVariations();

	/// <summary>
	///     Implementation for an always invalid mutation that throws an error if someone should try to place it onto the
	///     tape.
	/// </summary>
	private class MutationNone : Mutation {
		public MutationNone() : base(Direction.None) {
		}

		public override bool Matches(ISymbol symbol) {
			return false;
		}

		public override ISymbol ToTape(ISymbol input) {
			throw new NotSupportedException(
				"Tried to apply none mutation to the tape, this is not allowed an must be an error.");
		}

		public override IEnumerable<ISymbol> GetVariations() {
			yield break;
		}
	}
}