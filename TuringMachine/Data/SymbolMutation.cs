using System.Collections.Generic;

namespace TuringMachine.Data;

public class SymbolMutation : Mutation
{
	private readonly ISymbol _input;
	private readonly ISymbol _output;

	public SymbolMutation(ISymbol input, ISymbol output, Direction dir) : base(dir)
	{
		_input = input;
		_output = output;
	}

	public override ISymbol ToTape(ISymbol input)
	{
		return _output;
	}
	
	public override bool Matches(ISymbol symbol)
	{
		return Equals(_input, symbol);
	}

	public override IEnumerable<ISymbol> GetVariations()
	{
		yield return _input;
	}
}