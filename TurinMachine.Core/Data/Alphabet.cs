using System.Collections;
using System.Collections.Immutable;

namespace TuringMachine.Data;

/// <summary>
///     Defines set of symbols that can be read and written by the turing machine.
///     Every TM has two alphabets, one defines the words that can be loaded onto the first
///     ape at the start.
///     The other one defines the symbols that can be written by the turing machine.
/// </summary>
/// <param name="Symbols">All symbols defined and accepted by this alphabet</param>
public record Alphabet(ImmutableHashSet<ISymbol> Symbols) : IEnumerable<ISymbol> {
    private const int Prime = 433494437;

    /// <summary>
    ///     Empty alphabet implementation
    ///     <p />
    ///     Should be always used instead of a new empty instance
    /// </summary>
    public static readonly Alphabet Empty = CreateBuilder().Create();

    /// <summary>
    ///     Amount of contained symbols
    /// </summary>
    public int Length => Symbols.Count;

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }

    public IEnumerator<ISymbol> GetEnumerator() {
        return Symbols.GetEnumerator();
    }

    public virtual bool Equals(Alphabet? other) {
        if (ReferenceEquals(null, other)) {
            return false;
        }

        if (ReferenceEquals(this, other)) {
            return true;
        }

        return Symbols.SetEquals(other.Symbols);
    }

    /// <summary>
    ///     Create a new empty builder instance
    /// </summary>
    public static Builder CreateBuilder() {
        return new Builder();
    }

    /// <summary>
    ///     Checks if the given symbol is contained
    /// </summary>
    /// <param name="symbol">Symbol to be checked</param>
    /// <returns>True if the symbol is contained</returns>
    public bool Contains(ISymbol symbol) {
        return Symbols.Contains(symbol);
    }

    /// <summary>
    ///     Checks if all characters of the given text are accepted by this alphabet.
    /// </summary>
    /// <param name="text">Sequence to be checked</param>
    /// <returns>True if all characters have a valid symbol counterpart.</returns>
    public bool Accepts(string text) {
        return (from character in text
            where Symbols.Contains(SymbolManager.FromChar(character))
            select character).Any();
    }

    /// <summary>
    ///     Creates a builder instance that contains all symbols of this instance.
    /// </summary>
    /// <returns>Builder instance copy of this instance</returns>
    public Builder ToBuilder() {
        return new Builder(this);
    }

    public override int GetHashCode() {
        return Symbols.Aggregate(Prime, HashCode.Combine);
    }

    /// <summary>
    ///     Builder that can be used to create an alphabet.
    /// </summary>
    public sealed class Builder {
        private readonly ImmutableHashSet<ISymbol>.Builder _characters = ImmutableHashSet.CreateBuilder<ISymbol>();

        internal Builder() {
        }

        public Builder(Alphabet alphabet) {
            _characters.UnionWith(alphabet.Symbols);
        }

        /// <summary>
        ///     Adds the specified symbol.
        /// </summary>
        /// <param name="character">The symbol.</param>
        /// <returns>The builder instance for builder chaining.</returns>
        public Builder WithSymbol(ISymbol character) {
            _characters.Add(character);
            return this;
        }

        /// <summary>
        ///     Removes the specified symbol.
        /// </summary>
        /// <param name="character">The symbol.</param>
        /// <returns>The builder instance for builder chaining.</returns>
        public Builder WithoutSymbol(ISymbol character) {
            _characters.Remove(character);
            return this;
        }

        /// <summary>
        ///     Creates a immutable alphabet from the content of this builder.
        /// </summary>
        /// <returns></returns>
        public Alphabet Create() {
            return new Alphabet(_characters.ToImmutable());
        }
    }
}