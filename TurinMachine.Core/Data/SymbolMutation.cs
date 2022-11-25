using System.Collections.Generic;

namespace TuringMachine.Data;

/// <summary>
///     Mutation based on one input and one output symbol
/// </summary>
public class SymbolMutation : Mutation {
	/// <summary>
	///     Valid symbol on the tape
	/// </summary>
	private readonly ISymbol _input;

	/// <summary>
	///     Written symbol to the tape
	/// </summary>
	private readonly ISymbol _output;

	public SymbolMutation(ISymbol input, ISymbol output, Direction dir) : base(dir) {
		_input = input;
		_output = output;
	}

	/// <summary>
	///     Retrieves the symbol that should be placed onto the tape if this mutation input matches the symbol on the
	///     corresponding tape.
	/// </summary>
	/// <param name="input">Symbol on the tape that matched <see cref="Matches" /> of this mutation</param>
	/// <returns>Symbol for the tape</returns>
	public override ISymbol ToTape(ISymbol input) {
		return _output;
	}

	public override bool Matches(ISymbol symbol) {
		return Equals(_input, symbol);
	}

	public override IEnumerable<ISymbol> GetVariations() {
		yield return _input;
	}
}