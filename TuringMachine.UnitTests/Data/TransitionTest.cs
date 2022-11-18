using TuringMachine.Data;

namespace TuringMachine.UnitTests.Data;

[TestFixture]
public class TransitionTest
{
	[Test]
	public void SymbolInputComb()
	{
		var trx = TrxBlank();
		var combinations = trx.GetInputCombinations().ToArray();
		CollectionAssert.Contains(combinations, Tuple.Create(SymbolManager.Blank));
		Assert.That(combinations, Has.Length.EqualTo(1));
		
	}
	
	[Test]
	public void SetInputComb()
	{
		var trx = Transition.CreateBuilder()
			.WithMutation((builder) => builder.WithInputs(SymbolA, SymbolB).WithRep(SymbolC))
			.WithMutation((builder) => builder.WithInputs(SymbolA, SymbolB).WithRep(SymbolC)).Create();
		var combinations = trx.GetInputCombinations().ToArray();
		Assert.That(combinations, Has.Length.EqualTo(4));
		foreach (var symbolA in new []{SymbolA, SymbolB})
		{
			foreach (var symbolB in new [] { SymbolA, SymbolB })
			{
				CollectionAssert.Contains(combinations, Tuple.Create(symbolA, symbolB));
			}
		}
	}

	[Test]
	public void Permutations()
	{
		var first = new[] { 0, 2 };
		var second = new[] { 4, 7, 8 };
		var permutations = Transition.GetPermutations(new[] { first, second }, 2);
		var perTuplesAsText = (from per in permutations
			select ("(" + string.Join(",", per) + ")")).ToArray();
		CollectionAssert.AllItemsAreUnique(perTuplesAsText);
		CollectionAssert.IsNotEmpty(perTuplesAsText);
		CollectionAssert.AllItemsAreNotNull(perTuplesAsText);
		Assert.Multiple(() =>
		{
			foreach (var f in first)
			{
				foreach (var s in second)
				{
					CollectionAssert.Contains(perTuplesAsText, $"({f},{s})");
				}
			}
		});
		Console.Out.Write(string.Join(",", perTuplesAsText));
	}
}