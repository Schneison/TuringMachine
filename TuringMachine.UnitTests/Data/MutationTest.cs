using System.Diagnostics.CodeAnalysis;
using TuringMachine.Data;

namespace TuringMachine.UnitTests.Data;

[TestFixture]
public class MutationTest {
	[Test]
	public void None() {
		var mutation = Mutation.None;
		Assert.That(mutation.GetVariations(), Is.Empty);
		Assert.Catch<NotSupportedException>(() => mutation.ToTape(SymbolNone));
		foreach (var symbol in GetAllPossibleSymbols()) {
			Assert.That(mutation.Matches(symbol), Is.False);
		}
	}

	[Test]
	[SuppressMessage("Assertion", "NUnit2010:Use EqualConstraint for better assertion messages in case of failure")]
	public void Equality() {
		var trxLeft = MockMove(SymbolA, Direction.Left);
		var trxNone = MockMove(Direction.None);
		Assert.Multiple(() => {
			Assert.That(trxLeft, Is.Not.EqualTo(trxNone));
			Assert.That(trxLeft.GetHashCode(), Is.Not.EqualTo(trxNone.GetHashCode()));
		});
		// Test equality for the Mutation class
		var mutation = MockMove(SymbolA, Direction.Left);
		Assert.Multiple(() => {
			// Check equality of Equals(object?)
			Assert.That(mutation.Equals(null), Is.False);
			Assert.That(mutation!.Equals(new object()), Is.False);
			Assert.That(mutation!.Equals(mutation), Is.True);
			Assert.That(mutation, Is.EqualTo(trxLeft));
		});
	}

	[Test]
	[SuppressMessage("Assertion", "NUnit2010:Use EqualConstraint for better assertion messages in case of failure")]
	public void Set() {
		var withA = SetMutation.CreateBuilder()
			.WithInputs(SymbolA).WithDir(Direction.Left).Create();
		var sameA = withA.ToBuilder().Create();
		var withAndB = withA.ToBuilder()
			.WithInputs(SymbolB)
			.WithRep(SymbolManager.Blank)
			.Create();
		Assert.Multiple(() => {
			Assert.That(withA, Is.EqualTo(sameA));
			Assert.That(withA.GetHashCode(), Is.EqualTo(sameA.GetHashCode()));
			Assert.That(withA, Is.Not.EqualTo(withAndB));
		});
		Assert.Multiple(() => {
			// Check equality of Equals(object?)
			Assert.That(withA.Equals(null), Is.False);
			Assert.That(withA!.Equals(new object()), Is.False);
			Assert.That(withA.Equals(sameA), Is.True);
			Assert.That(withA.Equals(withA), Is.True);
		});
		Assert.That(withA.Dir, Is.EqualTo(Direction.Left));
		Assert.Multiple(() => {
			Assert.That(withA.GetVariations(), Contains.Item(SymbolA));
			Assert.That(withA.GetVariations(), Is.Not.Contains(SymbolB));
			Assert.That(withA.Matches(SymbolA), Is.True);
			Assert.That(withA.Matches(SymbolB), Is.False);
			Assert.That(withA.Matches(SymbolManager.Blank), Is.False);
			Assert.That(withA.ToTape(SymbolA), Is.EqualTo(SymbolA));
			Assert.Catch<InvalidOperationException>(() => withA.ToTape(SymbolB));
		});
		Assert.Multiple(() => {
			Assert.That(withAndB.Matches(SymbolA), Is.True);
			Assert.That(withAndB.Matches(SymbolB), Is.True);
			Assert.That(withAndB.GetVariations(), Contains.Item(SymbolA));
			Assert.That(withAndB.GetVariations(), Contains.Item(SymbolB));
			Assert.That(withAndB.Matches(SymbolBlank), Is.False);
			Assert.That(withAndB.ToTape(SymbolA), Is.EqualTo(SymbolBlank));
			Assert.That(withAndB.ToTape(SymbolB), Is.EqualTo(SymbolBlank));
			Assert.Catch<InvalidOperationException>(() => withAndB.ToTape(SymbolBlank));
		});
	}
}