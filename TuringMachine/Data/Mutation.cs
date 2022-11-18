using System;
using System.Collections.Generic;

namespace TuringMachine.Data;

public abstract class Mutation : ITapeTransformer
{
	public static readonly Mutation None = new MutationNone();
	protected Mutation(Direction dir)
	{
		Dir = dir;
	}

	public Direction Dir
	{
		get;
	}

	protected bool Equals(Mutation? other)
	{
		return other != null && Dir == other.Dir;
	}

	public override bool Equals(object? obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != this.GetType()) return false;
		return Equals((Mutation)obj);
	}

	public override int GetHashCode()
	{
		return (int)Dir;
	}

	/**
	 * Checks if the given other symbol matches the criteria for this symbol.
	 * <p/>
	 * This is for example used by wildcard symbols to match more than one symbol.
	 *
	 * @param other Other symbol
	 * @return True if the other symbol is an equivalent representation of this symbol.
	 */ 
	public abstract bool Matches(ISymbol symbol);

	public abstract ISymbol ToTape(ISymbol input);
	
	public abstract IEnumerable<ISymbol> GetVariations();

	private class MutationNone : Mutation
	{
		public MutationNone() : base(Direction.None)
		{
		}

		public override bool Matches(ISymbol symbol)
		{
			return false;
		}

		public override ISymbol ToTape(ISymbol input)
		{
			throw new NotSupportedException(
				"Tried to apply none mutation to the tape, this is not allowed an must be an error.");
		}

		public override IEnumerable<ISymbol> GetVariations()
		{
			yield break;
		}
	}
}