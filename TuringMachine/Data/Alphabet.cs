using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace TuringMachine.Data;

public record Alphabet(ImmutableHashSet<ISymbol> Characters) : IEnumerable<ISymbol>
{
	private const int Prime = 433494437;
	public static readonly Alphabet Empty = CreateBuilder().Create();

	public static Builder CreateBuilder() {
		return new Builder();
	}


	public Builder ToBuilder() {
		return new Builder(this);
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	public IEnumerator<ISymbol> GetEnumerator()
	{
		return Characters.GetEnumerator();
	}

	public virtual bool Equals(Alphabet? other)
	{
		if (ReferenceEquals(null, other)) return false;
		if (ReferenceEquals(this, other)) return true;
		return Characters.SetEquals(other.Characters);
	}

	public override int GetHashCode()
	{
		return Characters.Aggregate(Prime, HashCode.Combine);
	}

	public sealed class Builder {
		private readonly ImmutableHashSet<ISymbol>.Builder _characters = ImmutableHashSet.CreateBuilder<ISymbol>();

		internal Builder() {
		}

		public Builder(Alphabet alphabet) {
			this._characters.UnionWith(alphabet.Characters);
		}

		public Builder WithSymbol(ISymbol character) {
			_characters.Add(character);
			return this;
		}

		public Builder WithoutSymbol(ISymbol character){
			_characters.Remove(character);
			return this;
		}

		public Alphabet Create() {
			return new Alphabet(_characters.ToImmutable());
		}
	}
}
