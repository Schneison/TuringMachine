using System.Collections.Immutable;

namespace TuringMachine.Data;

/**
 * Implementation of a criteria which matchers a set of symbols
 */
public class SetMutation : Mutation, IEquatable<SetMutation> {
	private readonly ISymbol _replacement;

	private readonly ImmutableHashSet<ISymbol> _values;

	private SetMutation(Direction dir, ImmutableHashSet<ISymbol> values, ISymbol replacement) : base(dir) {
		_values = values;
		_replacement = replacement;
	}

	public bool Equals(SetMutation? other) {
		if (ReferenceEquals(null, other)) {
			return false;
		}

		if (ReferenceEquals(this, other)) {
			return true;
		}

		return base.Equals(other) && _values.SetEquals(other._values) && _replacement.Equals(other._replacement);
	}

	public static Builder CreateBuilder() {
		return new Builder();
	}

	public override bool Matches(ISymbol other) {
		return _values.Contains(other);
	}

	public override IEnumerable<ISymbol> GetVariations() {
		return _values;
	}

	public override ISymbol ToTape(ISymbol input) {
		if (!Matches(input)) {
			throw new InvalidOperationException();
		}

		// Retrieve replacement symbol if one is given, if not leave symbol
		return !_replacement.IsNone() ? _replacement : input;
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

		return Equals((SetMutation)obj);
	}

	public override int GetHashCode() {
		var hc = HashCode.Combine(base.GetHashCode(), _replacement.GetHashCode());
		return _values.Aggregate(hc, HashCode.Combine);
	}

	public Builder ToBuilder() {
		return new Builder(this);
	}

	public sealed class Builder {
		private readonly ImmutableHashSet<ISymbol>.Builder _values = ImmutableHashSet.CreateBuilder<ISymbol>();
		private Direction _dir = Direction.None;
		private ISymbol _replacement = SymbolManager.None;

		internal Builder() {
		}

		internal Builder(SetMutation mutation) {
			_values.UnionWith(mutation._values);
			_dir = mutation.Dir;
			_replacement = mutation._replacement;
		}

		public Builder WithInputs(IEnumerable<ISymbol> symbol) {
			_values.UnionWith(symbol);
			return this;
		}

		public Builder WithInputs(params ISymbol[] symbol) {
			_values.UnionWith(symbol);
			return this;
		}

		public Builder WithDir(Direction dir) {
			_dir = dir;
			return this;
		}

		public Builder WithRep(ISymbol rep) {
			_replacement = rep;
			return this;
		}

		public SetMutation Create() {
			return new SetMutation(_dir, _values.ToImmutable(), _replacement);
		}
	}
}