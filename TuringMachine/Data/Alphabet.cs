using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace TuringMachine.Data;

/**
 * Defines set of symbols that can be read and written by the turing machine.
 * Every TM has two alphabets, one defines the words that can be loaded onto the first
 * tape at the start.
 * The other one defines the symbols that can be written by the turing machine.
 */
public record Alphabet(ImmutableHashSet<ISymbol> Symbols) : IEnumerable<ISymbol>
{
	private const int Prime = 433494437;
	public static readonly Alphabet Empty = CreateBuilder().Create();

	public static Builder CreateBuilder()
	{
		return new Builder();
	}

	public int Length => Symbols.Count;

	public bool Contains(ISymbol symbol)
	{
		return Symbols.Contains(symbol);
	}

	public bool Accepts(string text)
	{
		return (from character in text
			where Symbols.Contains(SymbolManager.FromChar(character))
			select character).Any();
	}

	public bool Accepts(ISymbol symbol)
	{
		return Symbols.Contains(symbol);
	}

	public Builder ToBuilder()
	{
		return new Builder(this);
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	public IEnumerator<ISymbol> GetEnumerator()
	{
		return Symbols.GetEnumerator();
	}

	public virtual bool Equals(Alphabet? other)
	{
		if (ReferenceEquals(null, other)) return false;
		if (ReferenceEquals(this, other)) return true;
		return Symbols.SetEquals(other.Symbols);
	}

	public override int GetHashCode()
	{
		return Symbols.Aggregate(Prime, HashCode.Combine);
	}

	public sealed class Builder
	{
		private readonly ImmutableHashSet<ISymbol>.Builder _characters = ImmutableHashSet.CreateBuilder<ISymbol>();

		internal Builder()
		{
		}

		public Builder(Alphabet alphabet)
		{
			this._characters.UnionWith(alphabet.Symbols);
		}

		public Builder WithSymbol(ISymbol character)
		{
			_characters.Add(character);
			return this;
		}

		public Builder WithoutSymbol(ISymbol character)
		{
			_characters.Remove(character);
			return this;
		}

		public Alphabet Create()
		{
			return new Alphabet(_characters.ToImmutable());
		}
	}
}