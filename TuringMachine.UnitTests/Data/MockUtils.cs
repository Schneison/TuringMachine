using TuringMachine.Data;

namespace TuringMachine.UnitTests.Data;

internal static class MockUtils
{
	public static readonly ISymbol SymbolA = SymbolManager.FromChar('a');
	public static readonly ISymbol SymbolB = SymbolManager.FromChar('b');
	public static readonly ISymbol SymbolC = SymbolManager.FromChar('c');
	public static readonly ISymbol SymbolBlank = SymbolManager.Blank;
	public static readonly ISymbol SymbolNone = SymbolManager.None;
	public static readonly string Blank = SymbolBlank.ToPrint();

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

	public static Transition TrxComplex() {
			return Transition.CreateBuilder()
			.WithMutations(
				MockMove(SymbolA, SymbolB, Direction.Left),
				MockMove(SymbolB, SymbolC, Direction.Right),
				MockMove(SymbolC, SymbolA, Direction.Left),
				MockMove(SymbolBlank, SymbolBlank, Direction.Right)
			)
			.Create();
	}

	public static Transition TrxNone()
	{
		return Transition.None;
	}
	
	public static IEnumerable<ISymbol> GetAllPossibleSymbols()
	{
		for (var i = 0; i < 26; i++)
		{
			yield return SymbolManager.FromChar((char)('a' + i));
			yield return SymbolManager.FromChar((char)('A' + i));
		}
		for (var i = 0; i < 10; i++)
		{
			yield return SymbolManager.FromChar((char)('0' + i));
		}

		yield return SymbolBlank;
		yield return SymbolNone;
	}
}