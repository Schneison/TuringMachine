using TuringMachine.Data;

namespace TuringMachine.UnitTests.Data;

internal static class MockUtils
{
	public static readonly ISymbol SymbolA = SymbolManager.FromChar('a');
	public static readonly ISymbol SymbolB = SymbolManager.FromChar('b');
	public static readonly ISymbol SymbolC = SymbolManager.FromChar('c');
	public static readonly ISymbol SymbolBlank = SymbolManager.Blank;
	public static readonly ISymbol SymbolNone = SymbolManager.None;

	public static Mutation MockMove(Direction dir)
	{
		return MockMove(SymbolManager.Blank, dir);
	}

	public static Mutation MockMove(ISymbol output, Direction dir)
	{
		return MockMove(SymbolManager.Blank, output, dir);
	}

	public static Mutation MockMove(ISymbol input, ISymbol output, Direction dir)
	{
		return new SymbolMutation(input, output, dir);
	}

	public static Transition TrxBlank()
	{
		return Transition.FromSymbol(
			SymbolManager.Blank,
			State.Empty,
			State.Empty,
			SymbolManager.Blank,
			Direction.None);
	}

	public static Transition TrxFromPairs(params (ISymbol input, ISymbol output)[] mutationPairs)
	{
		return Transition.CreateBuilder()
			.WithMutations(
				from pair in mutationPairs
				select new SymbolMutation(pair.input, pair.output, Direction.None)
			)
			.Create();
	}

	public static Transition TrxFrom(ISymbol input, ISymbol output)
	{
		return Transition.FromSymbol(
			input,
			State.Empty,
			State.Empty,
			output,
			Direction.None);
	}

	public static Transition TrxNone()
	{
		return Transition.None;
	}
}