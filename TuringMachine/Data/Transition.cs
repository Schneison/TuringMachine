using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TuringMachine.Utils;

namespace TuringMachine.Data;

/// <summary>
/// </summary>
/// <param name="From">Start state of the transition</param>
/// <param name="To">End state of the transition</param>
/// <param name="Mutations">Mutation to be applied to the tapes</param>
public record Transition(State From, State To, params Mutation[] Mutations) {
	public static readonly Transition None = new(State.Empty, State.Empty, Mutation.None);

	public static Transition FromSymbol(ISymbol input, State from, State to, ISymbol output, Direction dir) {
		return new Transition(from, to, new SymbolMutation(input, output, dir));
	}

	public static Builder CreateBuilder() {
		return new Builder();
	}

	/// <summary>
	///     Retrieves all possible combinations of valid input values of this transition.
	///     The first value of the tuple is always the input for the first mutation.
	/// </summary>
	/// <returns>A tuple for every possible combination of inputs.</returns>
	public IEnumerable<ITuple> GetInputCombinations() {
		var variationArray = (from mutation in Mutations
			select mutation.GetVariations()).ToArray();
		return GetPermutations(variationArray, variationArray.Length)
			.Select(comb => comb.ToTuple());
	}

	public virtual bool Equals(Transition? other) {
		if (ReferenceEquals(null, other)) {
			return false;
		}

		if (ReferenceEquals(this, other)) {
			return true;
		}

		return From.Equals(other.From) && To.Equals(other.To) && Mutations.SequenceEqual(other.Mutations);
	}

	public override int GetHashCode() {
		var hc = Mutations.Aggregate(Mutations.Length, (current, val) => unchecked(current * 314159 + val.GetHashCode()));
		return HashCode.Combine(From, To, hc);
	}

	// https://stackoverflow.com/questions/1952153/what-is-the-best-way-to-find-all-combinations-of-items-in-an-array
	public static IEnumerable<IEnumerable<T>> GetPermutations<T>(IReadOnlyList<IEnumerable<T>> list, int length) {
		// Default case,
		if (length == 1) {
			return list[length - 1].Select(t => new[] { t });
		}

		return GetPermutations(list, length - 1)
			.SelectMany(t => list[length - 1],
				(t1, t2) => t1.Concat(new[] { t2 }));
	}

	public Builder ToBuilder() {
		return new Builder(this);
	}

	public class Builder {
		private State _from;
		private readonly List<Mutation> _mutations = new();
		private readonly State _to;

		public Builder() {
			_from = State.Empty;
			_to = State.Empty;
		}

		public Builder(Transition trx) {
			_from = trx.From;
			_to = trx.To;
			_mutations.AddRange(trx.Mutations);
		}

		public Builder WithFrom(State from) {
			_from = from;
			return this;
		}

		public Builder WithTo(State from) {
			_from = from;
			return this;
		}

		public Builder WithMutation(Action<SetMutation.Builder> consumer) {
			var builder = SetMutation.CreateBuilder();
			consumer(builder);
			return WithMutation(builder.Create());
		}

		public Builder WithMutations(IEnumerable<Mutation> mutations) {
			_mutations.AddRange(mutations);
			return this;
		}

		public Builder WithMutations(params Mutation[] mutations) {
			_mutations.AddRange(mutations);
			return this;
		}

		public Builder WithMutation(Mutation mutation) {
			_mutations.Add(mutation);
			return this;
		}

		public Transition Create() {
			return new Transition(_from, _to, _mutations.ToArray());
		}
	}
}