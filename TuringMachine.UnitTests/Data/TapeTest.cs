using TuringMachine.Data;

namespace TuringMachine.UnitTests.Data;

[TestFixture]
public class TapeTest {
	[Test]
	public void Empty() {
		var tape = Tape.CreateEmpty();
		Assert.That(tape, Has.Length.EqualTo(0));
		Assert.Multiple(() => {
			Assert.That(tape.Head, Is.EqualTo(SymbolNone));
			Assert.That(tape.ToPrint(), Is.Empty);
		});
		Assert.Multiple(() => {
			Assert.That(tape.Head, Is.EqualTo(SymbolNone));
			Assert.That(tape.ToPrint(), Is.Empty);
		});
	}

	[Test]
	public void ApplyMutation() {
		var trxLeft = MockMove(SymbolA, Direction.Left);
		var trxNone = MockMove(Direction.None);
		var trxRight = MockMove(Direction.Right);

		var tape = Tape.CreateBlank();
		Assert.That(tape, Has.Length.EqualTo(1));
		Assert.That(tape.Head, Is.EqualTo(SymbolBlank));

		// <- _
		tape.ApplyMutation(trxLeft);
		Assert.That(tape, Has.Length.EqualTo(2));

		// _ -> a
		tape.ApplyMutation(trxRight);
		Assert.That(tape, Has.Length.EqualTo(2));
		Assert.That(tape.ToPrint(), Is.EqualTo($"{Blank}a"));

		// _a ->
		Assert.Throws<NotSupportedException>(() => {
			// Should fail because a is no blank
			tape.ApplyMutation(trxRight);
		});
		// tape.ApplyTransaction(trxRight);
		// Assert.That(tape, Has.Length.EqualTo(3));
	}

	[Test]
	public void AlterValid() {
		var tape = Tape.CreateBlank();
		Assert.That(tape, Has.Length.EqualTo(1));
		Assert.That(tape.Head, Is.EqualTo(SymbolBlank));

		// _ -> a
		tape.Alter(SymbolA, Direction.None);
		Assert.That(tape, Has.Length.EqualTo(1));
		Assert.That(tape.Head, Is.EqualTo(SymbolA));
		Assert.That(tape.ToPrint(), Is.EqualTo("a"));

		// a -> b
		tape.Alter(SymbolB, Direction.None);
		Assert.That(tape, Has.Length.EqualTo(1));
		Assert.That(tape.Head, Is.EqualTo(SymbolB));
		Assert.That(tape.ToPrint(), Is.EqualTo("b"));

		// b -> c
		tape.Alter(SymbolC, Direction.None);
		Assert.That(tape, Has.Length.EqualTo(1));
		Assert.That(tape.Head, Is.EqualTo(SymbolC));
		Assert.That(tape.ToPrint(), Is.EqualTo("c"));
	}
	
	[Test]
	public void AlterInvalid() {
		var tape = Tape.CreateBlank();
		Assert.That(tape, Has.Length.EqualTo(1));
		Assert.That(tape.Head, Is.EqualTo(SymbolBlank));

		Assert.Catch<InvalidOperationException>(() => {
			tape.Alter(SymbolNone, Direction.None);
		});
		
		Assert.Catch<InvalidOperationException>(() => {
			tape.Alter(SymbolA, 3);
		});
		
		Assert.Catch<InvalidOperationException>(() => {
			tape.Alter(SymbolA, -6);
		});
	}

	[Test]
	public void Representation() {
		var tapeBlank = Tape.CreateBlank();
		var trxRight = MockMove(SymbolA, Direction.Right);
		Assert.That(tapeBlank.ToString(), Is.EqualTo($"Tape({Blank})"));
		tapeBlank.ApplyMutation(trxRight);
		// Sets a to the old head position, moves the head to the right and finds new blank
		Assert.That(tapeBlank.ToString(), Is.EqualTo($"Tape(a{Blank})"));
	}
}